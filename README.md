# AccountLedgerAPI
<b>Technologies Used</b> <br/>

C#<br/>
Visual Studio 2022<br/>
.Net 6.0<br/>
SQL Server Express 2019<br/>
Entity FrameworkCore<br/>

<b>Instruction to setup the app.</b>

1. Create a database in SQL Server Express 2019 by the name AccountLedger.<br/>
2. Run the attached script in the email (AccountLedger_Script.sql) on AccountLedger database created above.<br/>
3. Fork or Clone the app in Visual Studio 2022<br/>
4. Update the app ConnectionStrings in the appsettings.json and appsettings.Development.json files, to make sure the app is pointing to the right instance of SQL Server.<br/>
5. Run the app.<br/>


<b>Notes</b> <br/>
Register, Authenticate endpoints responses are in the headers.<br/>
To hit the Authenticate endpoint add the ApplicationUserCode in the headers from Register endpoint.<br/>
Keep the AccessToken after hitting the Authenticate endpoint, because it's going to be required in the headers for ApplicationUserAccount and Transaction endpoinds<br/>
