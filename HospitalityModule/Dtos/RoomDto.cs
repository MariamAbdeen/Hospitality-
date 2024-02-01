namespace HospitalityModules.DTOs
{
    public class RoomDto
    {

        public int Number { get; set; }
        public string Type { get; set; }
        public bool Availability { get; set; } = true;
        public string Status { get; set; }
        // public ICollection<ServicesRequestDto> ServiceRequests { get; set; }


    }
}
