using System;
using System.Collections.Generic;

namespace RestApi.Models
{
    public class Patient
    {
        public int PatientId { get; set; }

        public string NhsNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }
   
        public  ICollection<Episode> Episodes { get; set; }
    }
}