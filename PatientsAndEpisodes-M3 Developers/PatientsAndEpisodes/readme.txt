#RestApi project

-Used local Sql database /AppData/developerassessment.mdf file & updated the connection string to use it .

-populated Seed method of DbMigrations Configuration file with default data for the Patient & Episodes table 

-In addition to the solution which was provided , added a service layer PatientService.cs file as a best practice to wrap all logics interacting with PatientContext ,instead of using the context directly in controller 

-when you call http://localhost:65534/patients/1/episodes , it hits the database (developerassessment.mdf) and fetches the data for requested patient (1)

-Dependency injection -Implemented constructor injection in PatientsController & PatientService, registered PatientContext in Autofac

#unit test projects 
RestApi.UnitTests/PatientAndEpisodesTest

-Used MOQ for mocking the data & constructed IDbSet<Patient> ,to return mock Patient DBSet<Patient> from PatientContext & test getPatientEpisodesById() method independently without hitting the database 