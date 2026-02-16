# MtpFun

Sample projects demonstrating how to configure [Microsoft.Testing.Platform (MTP)](https://learn.microsoft.com/en-us/dotnet/core/testing/microsoft-testing-platform-intro) with common .NET test frameworks.


## Global Configuration


### global.json

The [test runner configuration](https://learn.microsoft.com/en-us/dotnet/core/testing/microsoft-testing-platform-integration-dotnet-test#globaltest-runner) sets MTP as the default test runner.

<!-- snippet: global.json -->
<a id='snippet-global.json'></a>
```json
{
  "sdk": {
    "version": "10.0.103",
    "allowPrerelease": true,
    "rollForward": "latestFeature"
  },
  "test": {
    "runner": "Microsoft.Testing.Platform"
  }
}
```
<sup><a href='/global.json#L1-L10' title='Snippet source file'>snippet source</a> | <a href='#snippet-global.json' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

This is [required in the repository root](https://learn.microsoft.com/en-us/dotnet/core/testing/microsoft-testing-platform-intro?tabs=continuous-integration#run-and-debug-tests) for CI environments using the Azure DevOps `DotNetCoreCLI` task with the `test` command.


### Directory.Packages.props

[Central Package Management](https://learn.microsoft.com/en-us/nuget/consume-packages/Central-Package-Management) is used to manage package versions:

<!-- snippet: Directory.Packages.props -->
<a id='snippet-Directory.Packages.props'></a>
```props
<Project>
  <PropertyGroup>
    <ManagePackageVersionsCentrally>true</ManagePackageVersionsCentrally>
    <CentralPackageTransitivePinningEnabled>true</CentralPackageTransitivePinningEnabled>
  </PropertyGroup>
  <ItemGroup>
    <PackageVersion Include="Microsoft.NET.Test.Sdk" Version="18.0.1" />
    <PackageVersion Include="NUnit" Version="4.4.0" />
    <PackageVersion Include="NUnit3TestAdapter" Version="5.2.0" Pinned="true" />
    <PackageVersion Include="xunit.v3" Version="3.2.2" />
    <PackageVersion Include="xunit.runner.visualstudio" Version="3.1.5" />
    <PackageVersion Include="MSTest" Version="4.1.0" />
    <PackageVersion Include="TUnit" Version="1.15.0" />
  </ItemGroup>
</Project>
```
<sup><a href='/Directory.Packages.props#L1-L15' title='Snippet source file'>snippet source</a> | <a href='#snippet-Directory.Packages.props' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


## Common Properties

The following properties are common across all test frameworks when using MTP:

 * [`OutputType`](https://learn.microsoft.com/en-us/dotnet/core/project-sdk/msbuild-props#outputtype) set to `Exe`. MTP requires test projects to be executable.


## xUnit v3

[xUnit v3 has built-in support for MTP](https://xunit.net/docs/getting-started/v3/cmdline).

 * [`UseMicrosoftTestingPlatformRunner`](https://xunit.net/docs/configuration-files#useMicrosoftTestingPlatformRunner) set to `true`. Enables the MTP runner for xUnit v3.

<!-- snippet: XunitV3Tests/XunitV3Tests.csproj -->
<a id='snippet-XunitV3Tests/XunitV3Tests.csproj'></a>
```csproj
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net10.0</TargetFramework>
    <OutputType>Exe</OutputType>
    <UseMicrosoftTestingPlatformRunner>true</UseMicrosoftTestingPlatformRunner>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include="xunit.v3" />
  </ItemGroup>
</Project>
```
<sup><a href='/XunitV3Tests/XunitV3Tests.csproj#L1-L10' title='Snippet source file'>snippet source</a> | <a href='#snippet-XunitV3Tests/XunitV3Tests.csproj' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


## NUnit

[NUnit supports MTP via the NUnit runner](https://docs.nunit.org/articles/vs-test-adapter/NUnit-And-Microsoft-Test-Platform.html).

 * [`EnableNUnitRunner`](https://docs.nunit.org/articles/vs-test-adapter/NUnit-And-Microsoft-Test-Platform.html) set to `true`. Enables the MTP runner for NUnit.
 * [NUnit3TestAdapter](https://www.nuget.org/packages/NUnit3TestAdapter) version 5.0 or greater is required. It contains the MTP integration layer for NUnit.
 * [Microsoft.NET.Test.Sdk](https://www.nuget.org/packages/Microsoft.NET.Test.Sdk) is required to provide the MTP infrastructure and auto-generate the entry point.

<!-- snippet: NUnitTests/NUnitTests.csproj -->
<a id='snippet-NUnitTests/NUnitTests.csproj'></a>
```csproj
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net10.0</TargetFramework>
    <OutputType>Exe</OutputType>
    <EnableNUnitRunner>true</EnableNUnitRunner>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include="Microsoft.NET.Test.Sdk" />
    <PackageReference Include="NUnit" />
    <PackageReference Include="NUnit3TestAdapter" />
  </ItemGroup>
</Project>
```
<sup><a href='/NUnitTests/NUnitTests.csproj#L1-L12' title='Snippet source file'>snippet source</a> | <a href='#snippet-NUnitTests/NUnitTests.csproj' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### Problems

Omitting `<EnableNUnitRunner>true</EnableNUnitRunner>` gives

```
global.json defines test runner to be Microsoft.Testing.Platform. All projects must use that test runner.
The following test projects are using VSTest test runner:
NUnitTests.csproj

See https://aka.ms/dotnet-test/mtp for more information.
Get projects properties with MSBuild didn't execute properly with exit code: 1.
```

But https://aka.ms/dotnet-test/mtp does not tell you how to fix the problem.


## MSTest

[MSTest has native MTP support via the MSTest runner](https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-mstest-runner-intro).

 * [`EnableMSTestRunner`](https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-mstest-runner-intro#enable-mstest-runner) set to `true`. Enables the MTP runner for MSTest.

<!-- snippet: MSTestTests/MSTestTests.csproj -->
<a id='snippet-MSTestTests/MSTestTests.csproj'></a>
```csproj
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net10.0</TargetFramework>
    <OutputType>Exe</OutputType>
    <EnableMSTestRunner>true</EnableMSTestRunner>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include="MSTest" />
  </ItemGroup>
</Project>
```
<sup><a href='/MSTestTests/MSTestTests.csproj#L1-L10' title='Snippet source file'>snippet source</a> | <a href='#snippet-MSTestTests/MSTestTests.csproj' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### Problems

Omitting `<EnableMSTestRunner>true</EnableMSTestRunner>` gives

```
global.json defines test runner to be Microsoft.Testing.Platform. All projects must use that test runner.
The following test projects are using VSTest test runner:
MSTestTests.csproj

See https://aka.ms/dotnet-test/mtp for more information.
Get projects properties with MSBuild didn't execute properly with exit code: 1.
```

But https://aka.ms/dotnet-test/mtp does not tell you how to fix the problem.


## TUnit

[TUnit is built natively on MTP](https://thomhurst.github.io/TUnit/).

<!-- snippet: TUnitTests/TUnitTests.csproj -->
<a id='snippet-TUnitTests/TUnitTests.csproj'></a>
```csproj
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net10.0</TargetFramework>
    <OutputType>Exe</OutputType>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include="TUnit" />
  </ItemGroup>
</Project>
```
<sup><a href='/TUnitTests/TUnitTests.csproj#L1-L9' title='Snippet source file'>snippet source</a> | <a href='#snippet-TUnitTests/TUnitTests.csproj' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


## dotnet test

`dotnet test` in the solution directory works as expected:


```
C:\Code\MtpFun>dotnet test
Running tests from XunitV3Tests\bin\Debug\net10.0\XunitV3Tests.dll (net10.0|x64)
Running tests from NUnitTests\bin\Debug\net10.0\NUnitTests.dll (net10.0|x64)
Running tests from MSTestTests\bin\Debug\net10.0\MSTestTests.dll (net10.0|x64)
Running tests from TUnitTests\bin\Debug\net10.0\TUnitTests.dll (net10.0|x64)
MSTestTests\bin\Debug\net10.0\MSTestTests.dll (net10.0|x64) passed (448ms)
NUnitTests\bin\Debug\net10.0\NUnitTests.dll (net10.0|x64) passed (647ms)
TUnitTests\bin\Debug\net10.0\TUnitTests.dll (net10.0|x64) passed (633ms)
XunitV3Tests\bin\Debug\net10.0\XunitV3Tests.dll (net10.0|x64) passed (756ms)

Test run summary: Passed!
  TUnitTests\bin\Debug\net10.0\TUnitTests.dll (net10.0|x64) passed (633ms)
  MSTestTests\bin\Debug\net10.0\MSTestTests.dll (net10.0|x64) passed (448ms)
  NUnitTests\bin\Debug\net10.0\NUnitTests.dll (net10.0|x64) passed (647ms)
  XunitV3Tests\bin\Debug\net10.0\XunitV3Tests.dll (net10.0|x64) passed (756ms)

  total: 4
  failed: 0
  succeeded: 4
  skipped: 0
  duration: 1s 220ms
```


`dotnet test directory` does not:


```
C:\Code>dotnet test C:\Code\MtpFun
Restore complete (2.4s)
  TUnitTests net10.0 failed with 1 error(s) (0.0s)
    C:\Users\simon\.nuget\packages\microsoft.testing.platform.msbuild\2.1.0\buildMultiTargeting\Microsoft.Testing.Platform.MSBuild.targets(263,5): error Testing with VSTest target is no longer supported by Microsoft.Testing.Platform on .NET 10 SDK and later. If you use dotnet test, you should opt-in to the new dotnet test experience. For more information, see https://aka.ms/dotnet-test-mtp-error
  MSTestTests net10.0 failed with 1 error(s) (0.0s)
    C:\Users\simon\.nuget\packages\microsoft.testing.platform.msbuild\2.1.0\buildMultiTargeting\Microsoft.Testing.Platform.MSBuild.targets(263,5): error Testing with VSTest target is no longer supported by Microsoft.Testing.Platform on .NET 10 SDK and later. If you use dotnet test, you should opt-in to the new dotnet test experience. For more information, see https://aka.ms/dotnet-test-mtp-error
  NUnitTests net10.0 succeeded (0.6s) → MtpFun\NUnitTests\bin\Debug\net10.0\NUnitTests.dll
NUnit Adapter 5.2.0.0: Test execution started
Running all tests in C:\Code\MtpFun\NUnitTests\bin\Debug\net10.0\NUnitTests.dll
   NUnit3TestExecutor discovered 1 of 1 NUnit test cases using Current Discovery mode, Non-Explicit run
NUnit Adapter 5.2.0.0: Test execution complete
  NUnitTests test net10.0 succeeded (2.1s)

Test summary: total: 1, failed: 0, succeeded: 1, skipped: 0, duration: 2.0s
Build failed with 2 error(s) in 5.5s
```


## TestingPlatformDotnetTestSupport

regarding the `<TestingPlatformDotnetTestSupport>true</TestingPlatformDotnetTestSupport>` setting 

This [unit-testing-with-dotnet-test](https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-dotnet-test) says

> It is highly recommended to set the TestingPlatformDotnetTestSupport property in Directory.Build.props.

But [dotnet-test-with-mtp](https://devblogs.microsoft.com/dotnet/dotnet-test-with-mtp/) says

> With native MTP integration in .NET 10, several MSBuild properties from the previous approach are no longer needed and can be safely removed from your test projects: TestingPlatformDotnetTestSupport: No longer required for MTP integration. TestingPlatformShowTestsFailure: Obsolete, as test failures are now displayed by default in the new experience

I have found it not to be necessary.