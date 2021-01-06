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
  - task:
  
  - task:
```

### Step 3: [NuGet Tool Installer task](https://docs.microsoft.com/en-us/azure/devops/pipelines/tasks/tool/nuget?view=azure-devops)

Use this task to find, download, and cache a specified version of NuGet and add it to the PATH.

```yml
  # NuGet tool installer
  # Acquires a specific version of NuGet from the internet or the tools cache and adds it to the PATH. 
  # Use this task to change the version of NuGet used in the NuGet tasks.
  - task: NuGetToolInstaller@1
    inputs:
      #versionSpec: # Optional
      #checkLatest: false # Optional
```

### Step 4: [Visual Studio Build task](https://docs.microsoft.com/en-us/azure/devops/pipelines/tasks/build/visual-studio-build?view=azure-devops)

Use this task to build with MSBuild and set the Visual Studio version property.

#### Demands
msbuild, visualstudio

```yml
# Visual Studio build
# Build with MSBuild and set the Visual Studio version property
- task: VSBuild@1
  inputs:
    #solution: '**\*.sln' 
    #vsVersion: 'latest' # Optional. Options: latest, 16.0, 15.0, 14.0, 12.0, 11.0
    #msbuildArgs: # Optional
    #platform: # Optional
    #configuration: # Optional
    #clean: false # Optional
    #maximumCpuCount: false # Optional
    #restoreNugetPackages: false # Optional
    #msbuildArchitecture: 'x86' # Optional. Options: x86, x64
    #logProjectEvents: true # Optional
    #createLogFile: false # Optional
    #logFileVerbosity: 'normal' # Optional. Options: quiet, minimal, normal, detailed, diagnostic
```

`**\` or `**/` - recursive search pattern

Set to True if you want to rebuild all the code in the code projects.

```yml
clean: true
```
