
using HospitalityModule.Entities;
using System.ComponentModel.DataAnnotations;

namespace HospitalityModules.Entities
{
    public class Room : BaseEntity
    {
        public int Number { get; set; }
        public string Type { get; set; }
        public bool Availability { get; set; } = true;
        public int? PatientId { get; set; }
        //public Patient Patient { get; set; }

        //Navigation property (Many)
        public ICollection<RoomServicesRequest> ServiceRequests { get; set; } = new HashSet<RoomServicesRequest>();

    }
}