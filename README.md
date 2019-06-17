# productivitywebapp
A webapp for increasing productivity when filling out forms

# Building  
Navigate to the directory containing the project solution file and build:  
`dotnet build`  
  
Create the database:  
`dotnet ef database update`  
  
This project requires iText7 for pdf maniplulation. To install from NuGet:  
`dotnet add package itext7 --version 7.x.x `  
  
Ensure all dependencies are gathered and up to date:  
`dotnet restore`  
  
Now you should be good to run:  
`dotnet run`  
  
To view the application open a browser and navigate to:  
`https://localhost:5001/`
