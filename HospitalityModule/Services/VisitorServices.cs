using AutoMapper;
using HospitalityModules.Data;
using HospitalityModules.DTOs;
using HospitalityModules.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HospitalityModules.Services
{
    public class VisitorServices
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public VisitorServices(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public VisitorServices(AppDbContext context) { _context = context; }

        public int RegisterVisitor(int patientId, VisitorsDto visitor)
        {
            var patient = _context.Patients.Find(patientId);
            if (patient != null)
            {

                var mappedVisitor = _mapper.Map<Visitor>(visitor);
                mappedVisitor.PatientId = patient.Id;
                _context.Visitors.Add(mappedVisitor);
                return _context.SaveChanges();

            }
            return 0;


        }
        public List<Visitor> GetVisitorHistory(int patientId)
        {
            return _context.Visitors.Where(v => v.PatientId == patientId).ToList();
        }
        public Visitor GetVisitorDetails(int visitorId)
        {
            return _context.Visitors.Find(visitorId);
        }
        public void DeleteVisitor(int visitorId)
        {
            var visitor = _context.Visitors.Find(visitorId);

            if (visitor != null)
            {
                _context.Visitors.Remove(visitor);
                _context.SaveChanges();
            }


        }
    }
}

