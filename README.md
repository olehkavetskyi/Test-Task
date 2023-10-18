# Test Task: Junior .Net Developer - REST API Development

[![.NET](https://github.com/olehkavetskyi/Test-Task/actions/workflows/dotnet.yml/badge.svg)](https://github.com/olehkavetskyi/Test-Task/actions/workflows/dotnet.yml)

## Task Description

This application is a sample REST API using C# that interacts with an MS SQL database containing a single table. 

## Warning

This application is designed as a test task to showcase my knowledge of various software development patterns. While some of these patterns may be considered overwhelming for this task, I have implemented them.

## Installation

To install this repository to your local machine using Git.

```bash
 git clone https://github.com/olehkavetskyi/Test-Task
```

1. Install the .NET 7 on your machine if it is not already installed.

2. Install the MS SQL Server on your machine if it is not already installed.

2. Navigate to the root of the repository using the command prompt.

3. Run the command dotnet restore to install the dependencies.

### Before running the application you need to put your connection string in ``` appsettings.json``` file. 

```json
  "ConnectionStrings": {
    "DefaultConnection": "your_connection_string"
  },
```
Also you are free to change ``` IpRateLimiting ``` values.

## Libraries and frameworks

1. AspNetCoreRateLimit
1. Microsoft.AspNetCore.OpenApi
1. Microsoft.EntityFrameworkCore.Design
1. Microsoft.EntityFrameworkCore.SqlServer
1. Microsoft.EntityFrameworkCore.Tools
1. Microsoft.Extensions.DependencyInjection.Abstractions
1. Microsoft.NET.Test.Sdk
1. Moq
1. System.Text.Json
1. xunit
1. xunit.runner.visualstudio
