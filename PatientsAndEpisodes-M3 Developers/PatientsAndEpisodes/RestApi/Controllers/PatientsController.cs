using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using RestApi.Interfaces;
using RestApi.Services;

namespace RestApi.Controllers
{
    public class PatientsController : ApiController
    {
        private readonly IDatabaseContext _databaseContext;
        //DI -Constructor injection
        public PatientsController(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        [HttpGet]
        public HttpResponseMessage Get(int patientId)
        {
            var response = new HttpResponseMessage();

            try
            {
                //Calling service layer to get the request Patient and episode details 
                PatientService patientService = new PatientService(_databaseContext);
                var patientList = patientService.getPatientEpisodesById(patientId);
                response.StatusCode = HttpStatusCode.OK;
                response.Content = new StringContent(JsonConvert.SerializeObject(patientService.getPatientEpisodesById(patientId)),
                                                        System.Text.Encoding.UTF8, "application/json");
            }
            catch (Exception ex)
            {
                response.Content = new StringContent(string.Format("Error occured while fetching data for patientID= {0}", patientId));
                response.ReasonPhrase = ex.Message;

            }
            return response;
        }
    }
}