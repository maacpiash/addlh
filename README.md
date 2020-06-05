# addlh

> A CLI tool that adds a license header on top of each source file of a directory.

[![Nuget](https://img.shields.io/nuget/v/AddLicenseHeader?logo=nuget&style=flat-square)](https://www.nuget.org/packages/AddLicenseHeader)

## How to install

```shell
dotnet tool install --global AddLicenseHeader
```

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
