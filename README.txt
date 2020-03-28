This is a simple buggy C# program that reads an input data file and outputs a csv report.  The csv report is a sorted list of four columns 
{Opportunity Name, Opportunity Dollar Amount, Opportunity Probability Percentage, Related Account Name} where each row is sorted by the Opportunity Dollar Amount.  See the desired output file: congaInterviewDesiredOutput.csv.  This was generated using the input data file ExampleData\exampleData.txt.

The program is run like 
	program.exe inputFilePath outputFilePath
	
The existing code compiles on .NET Core 2.1 and is in a compilable state but with runtime bugs.  It is easiest to compile this within Visual Studio 17.  The Community Edition is free and comes with .NET Core 2.1 as an optional package.

The goal of this assignment is to fix the bugs in the code.  There are also maintainability and performance considerations.  Treat this as an app to be maintained by the team.  Feel free to submit any tests you write along with the code.

====================================================
Input DATA
The data format is expected to be 

OBJECT_TYPE1
{
	FIELD1:value1
	FIELD2:value2
}

OBJECT_TYPE2
{
	FIELD1:value1
	FIELD2:value2
}
etc.  See ExampleData\exampleData.txt


Assume no data corruption: no repeats, all fields are present, and no typos in field names.  Assume that if a contact ID is referenced then it is also present in the data.  The order of the fields in the data is fixed.  Accounts, Opportunities, and Contacts can be listed in any order.  Field names in the data are not case sensitive.  E.g., Account and ACCOUNT are both acceptable, as are BillingCity and billingcity.  Also, assume that the input file could be very large, even though this example is not.