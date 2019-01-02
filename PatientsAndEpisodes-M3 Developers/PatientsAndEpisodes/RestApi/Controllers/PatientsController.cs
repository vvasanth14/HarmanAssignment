using System;
using System.Collections.Generic;
using System.Web.Http;
using RestApi.Models;
using System.Net.Http;
using System.Net;
using RestApi.Services;
using RestApi.Interfaces;

namespace RestApi.Controllers
{
    public class PatientsController : ApiController
    {
        private readonly IDatabaseContext _databaseContext;
        //DI -Constructor injection
        public PatientsController( IDatabaseContext databaseContext )
        {
            _databaseContext = databaseContext;
        }
        [HttpGet]
        public IEnumerable<Patient> Get(int patientId)
        {
            try
            {
                //Calling service layer to get the request Patient and episode details 
                PatientService patientService = new PatientService(_databaseContext);
                return  patientService.getPatientEpisodesById(1);
            }
            catch (Exception ex)
            {
                var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(string.Format("Error occured while fetching data for patientID= {0}", patientId)),
                    ReasonPhrase = ex.Message
                };
                throw new HttpResponseException(response);
            }

        }
    }
}