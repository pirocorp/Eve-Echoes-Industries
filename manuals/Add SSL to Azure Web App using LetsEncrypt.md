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

```sudo apt-get install software-properties-common
sudo add-apt-repository ppa:certbot/certbot
sudo apt-get install certbot
sudo apt-get install openssl```
