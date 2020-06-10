# addlh

> A CLI tool that adds a license header on top of each source file of a directory.

[![Nuget](https://img.shields.io/nuget/v/AddLicenseHeader?logo=nuget&style=flat-square)](https://www.nuget.org/packages/AddLicenseHeader)
[![Nuget](https://img.shields.io/nuget/dt/AddLicenseHeader?logo=nuget&style=flat-square)](https://www.nuget.org/packages/AddLicenseHeader)
[![GitHub All Releases](https://img.shields.io/github/downloads/maacpiash/addlh/total?logo=github&style=flat-square)](https://github.com/maacpiash/addlh/releases)
[![AppVeyor](https://img.shields.io/appveyor/build/maacpiash/addlh?logo=appveyor&style=flat-square)](https://ci.appveyor.com/project/maacpiash/addlh)
[![Codecov](https://img.shields.io/codecov/c/gh/maacpiash/addlh?logo=codecov&style=flat-square)](https://codecov.io/gh/maacpiash/addlh)

## How to install

If you have .NET Core (version 3 or above) installed, you can install this package as a tool:

```shell
dotnet tool install --global AddLicenseHeader
```

If you do not have .NET Core installed, you can download a binary file from the [releases](https://github.com/maacpiash/addlh/releases) section.

## How to use

```shell
addlh -h|-l /path/to/license/header -d|-s /path/to/source/directory
```

### Example

```shell
addlh -h ../MIT.txt -s ./src/app
```

or,

```shell
addlh -d ./tests -l ./GPL-2.txt
```

## Limitations

This tool currently works for the following languages:

- C# (.cs)
- JavaScript, TypeScript (.js, .ts)
- Python (.py)
- C, C++ (.c, .cc, .cpp. .h)

The license header file has to be present on the disk.

[![Twitter Follow](https://img.shields.io/twitter/follow/maacpiash?style=social)](https://twitter.com/maacpiash)
