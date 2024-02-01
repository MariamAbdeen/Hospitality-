using AutoMapper;
using HospitalityModules.Data;
using HospitalityModules.DTOs;
using HospitalityModules.Entities;
using HospitalityModules.Errors;
using Microsoft.AspNetCore.Http.HttpResults;

namespace HospitalityModules.Services
{
    public class PatientServices
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public PatientServices(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public PatientServices(AppDbContext context) { _context = context; }
        public List<Patient> GetAllPatients()
        {
            return _context.Patients.ToList();
        }

        public Patient GetPatientById(int patientId)
        {
            return _context.Patients.FirstOrDefault(p => p.Id == patientId);
        }

        public void AddPatient(PatientDto patient)
        {
            var mappedPatient = _mapper.Map<Patient>(patient);
            _context.Patients.Add(mappedPatient);
            _context.SaveChanges();
        }

        public void RemovePatient(int patientId)
        {
            var patient = _context.Patients.Find(patientId);
            if (patient != null)
            {
                _context.Patients.Remove(patient);
                _context.SaveChanges();
            }

        }
    }
}