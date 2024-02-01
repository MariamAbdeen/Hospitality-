using AutoMapper;
using HospitalityModules.DTOs;
using HospitalityModules.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HospitalityModules.Mapping

{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RoomDto, Room>().ReverseMap();
            CreateMap<PatientDto, Patient>().ReverseMap();
            CreateMap<ServicesRequestDto, RoomServicesRequest>().ReverseMap();
            CreateMap<VisitorsDto, Visitor>().ReverseMap();
            CreateMap<HospitalityStaffDto, HospitalityStaff>().ReverseMap();
        }
    }
}
