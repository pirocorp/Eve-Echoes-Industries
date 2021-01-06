Azure Web Deployment Troubleshooting

- Delete the App Setting "WEBSITE_RUN_FROM_PACKAGE" if you have problems with deployment and redeployment. And even if web app is already deployed and not working properly.


## How To Produce Yml File For Building Blazor Wasm Application And Host It In Azure

### Pool

Here we define what system (virtual) will be used

```yml
pool:
  name: Azure Pipelines
  vmImage: windows-2019
  demands:  
  - msbuild
  - visualstudio
  - vstest
```

Windows Server 2019
```yml
vmImage: windows-2019
```
