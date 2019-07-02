# productivitywebapp
A webapp for increasing productivity when filling out forms

## Building  
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
  
## Using  
### The interface  
![image](https://user-images.githubusercontent.com/7513894/60538515-c7c5aa80-9cd8-11e9-9218-d2e2104948b5.png)  
  
  
### Creating a template  
Click the *Create Template* button  
![image](https://user-images.githubusercontent.com/7513894/60538768-386cc700-9cd9-11e9-82ca-35af12ae9687.png)  
   
Give the template the following information:  
+ Name  
+ Description  [optional]  
+ One or more fillable PDFs  
+ Thumbnail image [optional]  
    
![image](https://user-images.githubusercontent.com/7513894/60538626-f80d4900-9cd8-11e9-81fb-6c8d6925c4ea.png)  
  
    
Click *Submit* to continue  
####  Adding fields and criteria
![image](https://user-images.githubusercontent.com/7513894/60539152-060f9980-9cda-11e9-8869-6d9a45d5dc13.png)  
To add a field the following information must be provided:  
+ Tag : A short name, similar to a variable identifier, used to reference user input  
+ Prompt : A longer message, describing what is to be input by the user  
+ Kind : The type of data that the user will input (String, number, date)  
  
  
To add criteria, the following must be provided:  
+ Category :  A short name, similar to a **Tag** , used to reference what field is being filtered
+ Prompt :  A longer message, describing what is to be input by the user  
+ Answers : A list of comma separated values that show up as radio button options  
![image](https://user-images.githubusercontent.com/7513894/60539923-e5484380-9cdb-11e9-9758-ddbee49e5e2c.png)  
When all desired fields and criteria are entered, click *Save*  

#### Assigning output to PDFs  
PDF document will show one at a time with their fillable boxed outlined in red  
![image](https://user-images.githubusercontent.com/7513894/60540088-512aac00-9cdc-11e9-933a-f6a18d2f194e.png)  
  
When a fillable box is clicked the assignmnet window will appear  
  
![image](https://user-images.githubusercontent.com/7513894/60540246-bbdbe780-9cdc-11e9-9d57-d986b13a5fc0.png)  
  
From the assignemnt window, select the prompt that receives the data that will be printed to the current selected fillable box  
Click *Fill*  
Repeat the assignment process for any desired outputs and fillable boxes  
![image](https://user-images.githubusercontent.com/7513894/60540570-7d92f800-9cdd-11e9-8013-9052521acef8.png)  
Repeat for each form that has been uploaded. __*Any prompts that map to multiple output boxes, even on different forms, will write the data across all applicable forms.*__  
  
### Filling out a workflow from a template
To begin a flow, click on whichever template is desired, and enter the information into the fields and criteria prompts  
![image](https://user-images.githubusercontent.com/7513894/60540968-6a345c80-9cde-11e9-85b5-a83896ac7fc0.png)  
When all information is entered, click *Generate*  
### Downloading/Sending filled documents
From the homepage, find the flow that is to be downloaded/sent from the list of **Existing Flows**  
![image](https://user-images.githubusercontent.com/7513894/60541091-ca2b0300-9cde-11e9-90ca-5484a3174869.png)  
Click *Send*  
A download will trigger automatically, with a zipped file containing all forms from the completed flow  
There is also a copy/paste ready link that cant be sent given to anyone so they may download the zip file as well.  
![image](https://user-images.githubusercontent.com/7513894/60541406-94d2e500-9cdf-11e9-8ce0-794bf1d613c1.png)  
All of the information should map exactly to the output boxes they were assigned, accross all PDF documents in the workflow  
![image](https://user-images.githubusercontent.com/7513894/60541507-bf24a280-9cdf-11e9-9ca3-47c5675b2730.png)
