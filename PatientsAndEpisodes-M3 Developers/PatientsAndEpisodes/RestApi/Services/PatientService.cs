using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using RestApi.Interfaces;
using RestApi.Models;

namespace RestApi.Services
{
    public class PatientService
    {
        private IDatabaseContext _databaseContext;
        public PatientService(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public IList<Patient> getPatientEpisodesById(int patientID)
        {
            try
            {
                //using Eager load to avoid multiple round trip calls to DB , as we need episode details for the requested Patient 
                return _databaseContext.Patients.Include("Episodes").Where(p => p.PatientId == patientID).ToList();
            }
            catch (Exception)
            {
                throw;
            }
          
        }
    }
}