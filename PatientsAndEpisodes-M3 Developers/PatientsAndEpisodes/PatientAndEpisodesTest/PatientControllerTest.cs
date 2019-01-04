﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using RestApi.Controllers;
using RestApi.Interfaces;
using RestApi.Models;

namespace PatientAndEpisodesTest
{
    [TestClass]
    public class PatientControllerTest
    {

        [TestMethod]
        public void GetPatientControllerWithMoqTest()
        {
            //Setup patient mock      
            var patientMockList = SetupMockPatientData();

            //Constructing mock IDbSet<Patient> ,so that we can return mock Patient DBSet from PatientContext 
            var mockPatient = new Mock<IDbSet<Patient>>();
            var patientMockListQ = patientMockList.AsQueryable();
            mockPatient.As<IQueryable<Patient>>().Setup(m => m.Provider).Returns(patientMockListQ.Provider);
            mockPatient.As<IQueryable<Patient>>().Setup(m => m.Expression).Returns(patientMockListQ.Expression);
            mockPatient.As<IQueryable<Patient>>().Setup(m => m.ElementType).Returns(patientMockListQ.ElementType);
            mockPatient.As<IQueryable<Patient>>().Setup(m => m.GetEnumerator()).Returns(patientMockListQ.GetEnumerator());

            //setting up PatientContext to return Mocked IDbSet<Patient> 
            var patientContext = new Mock<IDatabaseContext>();
            patientContext.Setup(p => p.Patients).Returns(mockPatient.Object);

            //Passing the PatientContext which has the mocked Patient DBset(not from Database) to PatientsController layer ,to test Get method 
            PatientsController patientsController = new PatientsController(patientContext.Object);
            var httpPatientResponse = patientsController.Get(101);
            var expectedPatientList = JsonConvert.DeserializeObject<List<Patient>>(httpPatientResponse.Content.ReadAsStringAsync().Result);
            //check if the list is not empty and has actual number of episodes as exist in the mock data 
            Assert.IsNotNull(expectedPatientList, "Patient mock obbject is not null");
            Assert.IsTrue(expectedPatientList.Any(p => p.Episodes.Count() == 4), "Patient and Episodes visit equal test passed");

        }


        //Set up mock Data 
        private List<Patient> SetupMockPatientData()
        {
            var patientList = new List<Patient>(){

                                        new Patient() { PatientId = 101,
                                            Episodes =  new List<Episode>() { new Episode() { PatientId = 101, Diagnosis = "Athlete's foot" },
                                                        new Episode() { PatientId = 101, Diagnosis = "Irritation of inner ear" },
                                                        new Episode() { PatientId = 101, Diagnosis = "Sprained wrist" },
                                                        new Episode() { PatientId = 101, Diagnosis = "Stomach cramps" }
                                                                            }
                                                     },
                                        new Patient() { PatientId = 102,
                                            Episodes =  new List<Episode>() { new Episode() { PatientId = 102, Diagnosis = "Laryngitis" },
                                                        new Episode() { PatientId = 101, Diagnosis = "Athlete's foot" },
                                                        new Episode() { PatientId = 101, Diagnosis = "Stomach cramps" }
                                                                            }
                                                       },
                                        new Patient() { PatientId = 103,
                                            Episodes =  new List<Episode>() { new Episode() { PatientId = 103, Diagnosis = "Sprained wrist" },
                                                        new Episode() { PatientId = 101, Diagnosis = "Head Ache" }
                                                                             }
                                                     },
                                        new Patient() { PatientId = 104,
                                            Episodes =  new List<Episode>() { new Episode() { PatientId = 101, Diagnosis = "Laryngitis" }
                                                                            }
                                                     }
            };

            return patientList;
        }

        


        [TestMethod]
        public void GetPatientControllerWithInMemoryTest()
        {
            //Setup patient mock      

            InMemoryPatientContext inMemoryPatientContext = new InMemoryPatientContext();
            foreach (var item in SetupMockPatientData())
            {
                inMemoryPatientContext.Patients.Add(item);
            }

            PatientsController patientsController = new PatientsController(inMemoryPatientContext);
            var httpPatientResponse = patientsController.Get(102);
            var expectedPatientList = JsonConvert.DeserializeObject<List<Patient>>(httpPatientResponse.Content.ReadAsStringAsync().Result);
           
            
            //check if the list is not empty and has actual number of episodes as exist in the mock data 
            Assert.IsNotNull(expectedPatientList, "Patient mock obbject is not null");
            Assert.IsTrue(expectedPatientList.Any(p => p.Episodes.Count() == 3), "Patient and Episodes visit equal test passed");
        }
    }
}
