## How To Configure CI For Blazor WASM Hosted Application

### Step 0: [Trigger](https://docs.microsoft.com/en-us/azure/devops/pipelines/repos/github?view=azure-devops&tabs=yaml#ci-triggers)

You can control which branches get CI triggers with a simple syntax:

```
trigger:
- main
```

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

Clean set to True if you want to rebuild all the code in the code projects.

```yml
clean: true
```

You can pass additional arguments to MSBuild. For syntax, see [MSBuild Command-Line Reference](https://docs.microsoft.com/en-us/visualstudio/msbuild/msbuild-command-line-reference).

```yml
msbuildArgs: '/p:DeployOnBuild=true /p:WebPublishMethod=WebDeploy /p:PackageAsSingleFile=false /p:SkipInvalidConfigurations=true /p:Configuration=Release /p:Platform="Any CPU"'
```

### Step 5: Tests (not implemented yet)

### Step 6: Publish

[DotNetCoreCLI@2](https://docs.microsoft.com/en-us/azure/devops/pipelines/tasks/build/dotnet-core-cli?view=azure-devops) use this task to build, test, package, or publish a dotnet application, or to run a custom dotnet command.

```yml
# .NET Core
# Build, test, package, or publish a dotnet application, or run a custom dotnet command
- task: DotNetCoreCLI@2
  inputs:
    #command: 'build' # Options: build, push, pack, publish, restore, run, test, custom
    #publishWebProjects: true # Required when command == Publish
    #projects: # Optional
    #custom: # Required when command == Custom
    #arguments: # Optional
    #publishTestResults: true # Optional
    #testRunTitle: # Optional
    #zipAfterPublish: true # Optional
    #modifyOutputPath: true # Optional
    #feedsToUse: 'select' # Options: select, config
    #vstsFeed: # Required when feedsToUse == Select
    #feedRestore: # Required when command == restore. projectName/feedName for project-scoped feed. FeedName only for organization-scoped feed.
    #includeNuGetOrg: true # Required when feedsToUse == Select
    #nugetConfigPath: # Required when feedsToUse == Config
    #externalFeedCredentials: # Optional
    #noCache: false
    restoreDirectory:
    #verbosityRestore: 'Detailed' # Options: -, quiet, minimal, normal, detailed, diagnostic
    #packagesToPush: '$(Build.ArtifactStagingDirectory)/*.nupkg' # Required when command == Push
    #nuGetFeedType: 'internal' # Required when command == Push# Options: internal, external
    #publishVstsFeed: # Required when command == Push && NuGetFeedType == Internal
    #publishPackageMetadata: true # Optional
    #publishFeedCredentials: # Required when command == Push && NuGetFeedType == External
    #packagesToPack: '**/*.csproj' # Required when command == Pack
    #packDirectory: '$(Build.ArtifactStagingDirectory)' # Optional
    #nobuild: false # Optional
    #includesymbols: false # Optional
    #includesource: false # Optional
    #versioningScheme: 'off' # Options: off, byPrereleaseNumber, byEnvVar, byBuildNumber
    #versionEnvVar: # Required when versioningScheme == ByEnvVar
    #majorVersion: '1' # Required when versioningScheme == ByPrereleaseNumber
    #minorVersion: '0' # Required when versioningScheme == ByPrereleaseNumber
    #patchVersion: '0' # Required when versioningScheme == ByPrereleaseNumber
    #buildProperties: # Optional
    #verbosityPack: 'Detailed' # Options: -, quiet, minimal, normal, detailed, diagnostic
    workingDirectory:
```

**Publish web projects is very important to be false. With Blazor Web Assembly Hosted we have two separate applications. Web application which serves blazor application. Otherwise this will produce two zip files and deployment will fail.**

```yml
publishWebProjects: false
```

**Projects must points to Server project(Web App) not Blazor!**

```yml
projects: '**/EveEchoesPlanetaryProductionApi.Api.csproj'
```


Build.ArtifactStagingDirectory is predefined variables. These variables are automatically set by the system and read-only. The local path on the agent where any artifacts are copied to before being pushed to their destination. For example: c:\agent_work\1\a

```yml
arguments: '--configuration Release --output $(build.artifactstagingdirectory)'
```

### Step 7: [Publish Build Artifacts](https://docs.microsoft.com/en-us/azure/devops/pipelines/tasks/utility/publish-build-artifacts?view=azure-devops)

Use this task in a build pipeline to publish build artifacts to Azure Pipelines, TFS, or a file share.

```yml
# Publish build artifacts
# Publish build artifacts to Azure Pipelines or a Windows file share
- task: PublishBuildArtifacts@1
  inputs:
    #pathToPublish: '$(Build.ArtifactStagingDirectory)' 
    #artifactName: 'drop' 
    #publishLocation: 'Container' # Options: container, filePath
    #targetPath: # Required when publishLocation == FilePath
    #parallel: false # Optional
    #parallelCount: # Optional
    #fileCopyOptions: #Optional
```
