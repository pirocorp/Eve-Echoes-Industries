Azure Web Deployment Troubleshooting

- Delete the App Setting "WEBSITE_RUN_FROM_PACKAGE" if you have problems with deployment and redeployment. And even if web app is already deployed and not working properly.


## How To Produce Yml File For Building Blazor Wasm Application And Host It In Azure

### Step 1: Pool

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

vmImage define agent specification (Windows Server 2019 with Visual Studio 2019). List of available [agent specifications](https://docs.microsoft.com/en-us/azure/devops/pipelines/agents/hosted?view=azure-devops&tabs=yaml#software).

```yml
vmImage: windows-2019
```

Use demands to make sure that the capabilities your pipeline needs are present on the agents that run it. Demands are asserted automatically by tasks or manually by you.

```yml
  demands:  
  - msbuild
  - visualstudio
  - vstest
```

### Step 2: Steps

A step is a linear sequence of operations that make up a job. Each step runs in its own process on an agent and has access to the pipeline workspace on a local hard drive. This behavior means environment variables aren't preserved between steps but file system changes are.

[Tasks](https://docs.microsoft.com/en-us/azure/devops/pipelines/process/tasks?view=azure-devops) are the building blocks of a pipeline. There's a [catalog of tasks](https://docs.microsoft.com/en-us/azure/devops/pipelines/tasks/?view=azure-devops) available to choose from.

```yml
steps:
```

