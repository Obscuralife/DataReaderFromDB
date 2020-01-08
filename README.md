    Start program:
    -set startUp projects : EntryPoint, HostedService.
    - add-migration and update dataBase.
    
    
    1	OVERVIEW
Required: Create application (service) which will be able to read data from stub data source, parse the data and push it to database. The service should be implemented as a job which pull data from stub data source time to time. Database schema should be designed by developer according to requirements below.
Optional: Create console application to read data from database using command line interface (design of query language is up to developer)
Optional: Place the service, database to separate docker containers and provide docker compose file to run it with single command from CLI. In case if console application was implemented this application should be able to connect to database from container.
    2	REQUIREMENTS
2.1	DATA USAGES
The data it the set of rooms and locations of this rooms from MS Exchange Server. You can see detailed data design in the attached archive Data.zip in APPENDIX A.
The data will be used for following use cases:
a.	Get all locations;
b.	Get location by unique id;
c.	Get location by unique email address;
d.	Search location by City;
e.	Optional: Full text search by all locations;
2.2	DATA FORMAT
The detailed schema of source data and data examples is described in the in the attached archive Data.zip in APPENDIX A. But source contains two main entities: Locations and Rooms:
1.	Location properties:
a.	Id – { string } unique identifier, can be null;
b.	Name – {string} the room name, format: “Rooms [City], [Optional: Office Name]” ex. - "Rooms Budapest, BJ44","
c.	Address – {string} unique email address of location   ex. -   "RoomsBudapest?BJ44@epam.com"
Properties of rooms and locations should be parsed and stored in the database. Rooms should be linked to the locations. Rooms and locations properties should be extracted from text.
Note: DO NOT focused on exact parsing it can fail sometime, focus more on building correct data schema in the database. 
2.3	JOB IMPLEMENTATION DETAILS
1.	The service which extract data from data source and send data to database should be implemented based on HostedService.
2.	Intervals which service will be used to pull data from should be configurable in application settings. Default interval should be 10 minutes.
2.4	STUB DATA SOURCE
Use attached archive Data.zip in APPENDIX A to implement stub data source. Use location.json file to get locations.
2.5	LOGGING
1.	All cases when rooms or location can NOT be parsed should be logged as warning and reflects which string was not parsed;
2.	All exceptions or critical parsing errors should be logged as errors;
3.	Any other logs up to developer;
4.	Recommended framework: nlog. But feel free to choose any other.
2.6	UNIT TESTS
1.	Parsing logic should be covered by unit tests;
2.	Optional: Services covered by unit test as much as possible;
3.	Recommended frameworks: xUnit, Moq, FluentAssertion, but any others can be used.
2.7	DATABASES RECCOMENDATIONS
1.	Relation database (Preferred) can be used;
2.	Elasticsearch can be used;
3.	Any othe SQL or NoSql solutions.
2.8	SOURCE CONTROL
1.	Use repository on git.epam.com and provide read only access to Siarhei_Zhalezka@epam.com.
2.	Use development branch to integrate changes.
3.	Use feature branch to implement the task.
4.	In case if you implement optional tasks use separate features branch for each task.
5.	Use merge request from feature to develop branch with assignee Siarhei_Zhalezka@epamm.com for review when task completed. 
6.	Reference flow:  Git flow.
