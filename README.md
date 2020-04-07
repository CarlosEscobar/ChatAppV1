# ChatAppV1
A simple browser-based chat application in .NET Core.

## Getting Started

### Prerequisites
* Visual Studio IDE.
* Microsoft .NET Core.
* SQL Server.

### Dependencies
| Nugget Package | Version 
| ------------- | ------------- |
| CsvHelper | 12.2.1 |
| Microsoft.AspNetCore.App | 2.1.1 |
| Microsoft.AspNetCore.Identity.EntityFrameworkCore | 2.1.1 |
| Microsoft.AspNetCore.Mvc.Core | 2.1.1 |
| Microsoft.AspNetCore.Razor.Design | 2.1.2 |
| Microsoft.EntityFrameworkCore.Design | 2.1.11 |
| Microsoft.EntityFrameworkCore.SqlServer | 2.1.11 |
| Microsoft.EntityFrameworkCore.Tools | 2.1.11 |
| Microsoft.NET.Test.Sdk | 15.9.0 |
| Microsoft.NETCore.App | 2.1.0 |
| Microsoft.VisualStudio.Web.CodeGeneration.Design | 2.1.9 |
| Moq | 4.13.1 | 
| WebSocketManager | 1.0.1 |
| xunit | 2.4.0 |
| xunit.runner.visualstudio | 2.4.0 |

### Run Locally
- Get Code   
	- Download git repository
	- Open Solution
- Create Local Database
	- Open the Package Manager Console
	- Set the .DataAccess project as the Default project 
	- Run the command "Add-Migration Initial"
	- Run the command "Update-Database"
- Create Debug Profile 
	- Set the Web Application as the default project for the solution
	- Right click the Web Application project
	- Select Properties -> Debug
	- Select the Profile called ChatAppV1
	- Select the Project option for Launch
	- Launch a browser to Views/login.html
	- Set the App URL to http://localhost:5000
- Run your application

## Authors
Carlos Escobar

## License
This project is licensed under the MIT License.
