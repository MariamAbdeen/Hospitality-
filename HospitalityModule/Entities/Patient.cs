using HospitalityModule.Entities;

namespace HospitalityModules.Entities
{
    public class Patient : BaseEntity
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public ICollection<RoomServicesRequest> ServiceRequests { get; set; }
        public ICollection<Visitor> Visitors { get; set; } = new HashSet<Visitor>();


    }
}