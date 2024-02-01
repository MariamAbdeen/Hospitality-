using AutoMapper;
using HospitalityModules.Data;
using HospitalityModules.DTOs;
using HospitalityModules.Entities;

namespace HospitalityModules.Services
{
    public class HospitalitystaffServices
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public HospitalitystaffServices(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public HospitalitystaffServices(AppDbContext context) { _context = context; }

        public List<HospitalityStaff> GetAllHospitalityStaff()
        {
            return _context.HospitalityStaff.ToList();
        }

        public HospitalityStaff GetStaffById(int personId)
        {
            return _context.HospitalityStaff.FirstOrDefault(p => p.Id == personId);
        }

        public void AddPerson(HospitalityStaffDto person)
        {
            var mappedPerson = _mapper.Map<HospitalityStaff>(person);
            _context.HospitalityStaff.Add(mappedPerson);
            _context.SaveChanges();
        }

        public void RemovePerson(int personId)
        {
            var Person = _context.HospitalityStaff.Find(personId);
            if (Person != null)
            {
                _context.HospitalityStaff.Remove(Person);
                _context.SaveChanges();
            }

        }
    }
}
