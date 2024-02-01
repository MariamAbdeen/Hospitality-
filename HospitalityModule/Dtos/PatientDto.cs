using HospitalityModules.Entities;

namespace HospitalityModules.DTOs
{
    public class PatientDto
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }

        //public ICollection <ServicesRequestDto>  ServiceRequests { get; set; }
        //public ICollection< VisitorsDto > Visitors{ get; set; } 
    }
}
