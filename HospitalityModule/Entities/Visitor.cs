using HospitalityModule.Entities;

namespace HospitalityModules.Entities
{
    public class Visitor : BaseEntity
    {
        public string Name { get; set; }
        public int PatientId { get; set; }
        /*public Patient Patient { get; set; }*/ //Navigation Property(ONE)

    }
}
