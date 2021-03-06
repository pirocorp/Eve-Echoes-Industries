# Add SSL to Azure Web App using LetsEncrypt

If you’re interested in adding SSL / HTTPS to your Azure Web App you can buy a certificate within Azure, but if you use [LetsEncrypt](https://letsencrypt.org/) you can add SSL for free (downside: renew your certificate every 3 months)

I’ve seen scripts & websites automating these steps. I could not get them to work for this specific scenario (.NET Core MVC Webapplication + Azure Web App) and that’s why I wrote down the steps to get it to work. If you do not have this setup, there might be an [easier way](https://letsencrypt.org/docs/client-options/).

###### Prerequisites
- Ubuntu (I used the [Ubuntu app](https://www.microsoft.com/en-us/p/ubuntu/9nblggh4msv6) on my Windows Machine)
- An Azure Web App running on a App Service (platform: Windows, minimal plan supporting SSL: Basic)

## Step 1: Install Certbot & OpenSSL

The tools you need to create the certificate with LetsEncrypt and convert it to a format Azure accepts are

- Certbot: Sets up the challenge with LetsEncrypt to verify your domain
- OpenSSL: Converts the certificates created by Certbot to a format that Azure accepts (PFX)

I’ve installed these tools in the [Ubuntu app](https://www.microsoft.com/en-us/p/ubuntu/9nblggh4msv6) on my Windows Machine using the following commands:

```bash
sudo apt-get install software-properties-common
sudo add-apt-repository ppa:certbot/certbot
sudo apt-get install certbot
sudo apt-get install openssl
```

## Step 2: Setup a challenge with LetsEncrypt

Now we’ve installed Certbot we can tell it to setup a challenge with the LetsEncrypt servers to verify you’re the owner of the domain. In this example I will use the HTTP challenge.

```bash
sudo certbot certonly --preferred-challenges http -d www.example.com --manual
```

We use the flag manual to indicate we’re doing this on behalf of a different server, since we’re not running this command from web server itself (Azure doesn’t allow this).

Note: you should replace **www.example.com** with your own domain.

**Please note that these steps will generate the certificate for the exact domain you enter. It matters if you enter `www.yourdomain.com` or `yourdomain.com`! If you want both, complete the steps twice.**

You’ll see instructions on your screen on what file and contents you should create.

Create the file and use the .txt extension . Why? You’ll see in the next step.

## Step 3: Upload the challenge file to your Azure Web App

So now you have a .txt file with the contents that you were supposed to add. You upload this file into a directory on your app service plan. Upload it any way you like (I used FTP (WinSCP client)).

Since I have a .NET core application I had to upload into the `wwwroot` folder in the `wwwroot` of my Azure website. This is the folder for your static files in .NET core.

Check if you can access the file in your browser by going to the full url with the .txt extension.

## Step 4: Modify your web.config to rewrite the challenge file without extension

As you can see in the instructions in Step 1. The servers of LetsEncrypt will visit your challenge file without the .txt extension. By default this isn’t supported by .NET / IIS and that’s why we add an IIS `rewrite` rule to redirect the url LetsEncrypt checks to the .txt file we’ve uploaded.

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <system.webServer>
    <handlers>
      <add name="aspNetCore" path="*" verb="*" modules="AspNetCoreModule" resourceType="Unspecified" />
    </handlers>
    <aspNetCore processPath="dotnet" arguments=".\OMT.Web.dll" stdoutLogEnabled="false" stdoutLogFile=".\logs\stdout" />
	
    <rewrite> 
      <rules> 
        <rule name="wildcard"> 
          <match url=".*well-known/acme-challenge/(?!.*?\.txt$)(.*)$" /> 
          <action type="Redirect" url="/.well-known/acme-challenge/{R:1}.txt" /> 
	</rule> 
      </rules> 
    </rewrite>
	
  </system.webServer>
</configuration>
```

Now you should be able to visit the **exact url**:

```bash
And make it available on your web server at this URL:

http://www.echoesindustries.com/.well-known/acme-challenge/Asdfsgsdfgsdfg34523adf234
```

When successful (you see the contents of the .txt file) you can press enter in your Ubuntu console to complete the challenge and generate the certificate.

## Step 5: Convert the certificate to PFX using OpenSSL

```
IMPORTANT NOTES:
 - Congratulations! Your certificate and chain have been saved at:
   /etc/letsencrypt/live/www.example.com/fullchain.pem
   Your key file has been saved at:
   /etc/letsencrypt/live/www.example.com/privkey.pem
   Your cert will expire on 2021-03-31. To obtain a new or tweaked
   version of this certificate in the future, simply run certbot
   again. To non-interactively renew *all* of your certificates, run
   "certbot renew"
 - If you like Certbot, please consider supporting our work by:

   Donating to ISRG / Let's Encrypt:   https://letsencrypt.org/donate
   Donating to EFF:                    https://eff.org/donate-le
```

When you’ve done this you can convert the certificates using the following command.

```bash
sudo openssl pkcs12 -export -out /etc/letsencrypt/live/www.example.com/www.example.com.pfx -inkey /et c/letsencrypt/live/www.example.com/privkey.pem -in /etc/letsencrypt/live/www.example.com/cert.pem
```

```bash
openssl pkcs12 -export -out exhoesindustries.com.pfx -inkey privkey.pem -in cert.pem -certfile chain.pem
```

It will ask you for a password (remember this) and will generate a .pfx file you can upload into the Azure portal.

If you want to copy the PFX file from your Windows Ubuntu app to your Windows Environment you can use the following folder

`/mnt/c`

There you’ll find the generated PFX file. Copy it to somewhere convenient.

## Step 6: Upload your PFX certificate into the Azure Portal and add the SSL binding

Go to the SSL settings of your web app and upload certificate

Upload your .pfx file and enter the password you remembered from Step 4.

Add the SSL binding to the uploaded certificate

![SSL Bindings](./Resources/SSL_Bindings.png "SSL Bindings")

Wait a few minutes and visit your website using https and you’ll see that it’s served with the LetsEncrypt certificate.

**Remember to do these steps for each subdomain**
