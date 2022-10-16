# AccountLedgerAPI
Technologies Used

C#
Visual Studio 2022
.Net 6.0
SQL Server Express 2019
Entity FrameworkCore

Instruction to setup the app.

Create a database in SQL Server Express 2019 by the name AccountLedger.
Run the attached script in the email (AccountLedger_Script.sql) on AccountLedger database created above.
Fork or Clone the app in Visual Studio 2022
Update the app ConnectionStrings in the appsettings.json and appsettings.Development.json files, to make sure the app is pointing to the right instance of SQL Server.
Run the app.


Note
Register, Authenticate responses are in headers.
Keep the AccessToken after hitting the Authenticate endpoint, because it's going to be required in the headers for ApplicationUserAccount and Transaction endpoinds
