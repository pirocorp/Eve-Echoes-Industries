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

vmImage define agent specification in this case Windows Server 2019 with Visual Studio 2019. List of available [agent specifications](https://docs.microsoft.com/en-us/azure/devops/pipelines/agents/hosted?view=azure-devops&tabs=yaml#software)

```yml
vmImage: windows-2019
```
