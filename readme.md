# Overview
Tests package restore for floating package versions.

 - package/Test.Package: a nuget package that gets published to nuget.org
 - project/Test.Project: a project that references version 1.0.* for the above package

This repository is intended to reproduce the issue described at https://github.com/NuGet/Home/issues/7198.

# Steps to Reproduce

To reproduce this issue, you'll be:
 1. Building a project with the current package version published to NuGet.org
 2. Publishing a new package version to NuGet.org
 3. Waiting for a period of time
 4. Building the project again

Step 4 fails to restore the new package version, even after waiting several hours for the nuget http-cache to expire.


## Restore / Build the Project
In project/Test.Project:

 1. `dotnet restore --no-cache`
 2. `dotnet build`
 3. `dotnet run`

Note the output: "Current Package Version 1.0.X"

## Publish a New Package Version
In package/Test.Package:

 1. Increment patch level for Version and AssemblyVersion in Test.Package.csproj
 2. `dotnet nuget pack -o .`
 3. `dotnet nuget push .\*.nupkg -k oy2nske6pevupvmtvivqxrsk2baleizu3brb3i75vkgfie`

## Wait for the New Package to be Listed
Verify listing at https://www.nuget.org/packages/mpetito.test.nuget/
 - Wait for validation so the package is available for restore
 - Wait for nuget http-cache expiration, at least 30 minutes

## Attempt to Restore New Package Version
In project/Test.Project:

 1. `dotnet restore`
 2. `dotnet build`
 3. `dotnet run`

Note the output, which shows an old version and indicates the newest version was not restored as expected:
 > Current Package Version 1.0.X

Expected output, which should show the newest version:
 > Current Package Version 1.0.(X+1)

## Workaround / Package Version Verification
To verify that restore should see the new package version:

 1. `dotnet restore --no-cache`
 2. `dotnet build`
 3. `dotnet run`

Verify the output now shows:
 > Current Package Version 1.0.(X+1)
