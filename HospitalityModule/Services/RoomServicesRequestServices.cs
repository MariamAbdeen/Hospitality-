using HospitalityModules.Data;
using HospitalityModules.Entities;

namespace HospitalityModules.Services
{
    public class RoomServicesRequestServices
    {
        private readonly AppDbContext _context;

        public RoomServicesRequestServices(AppDbContext context)
        {
            _context = context;
        }


        public void DeleteServiceRequest(int requestId)
        {
            var request = _context.RoomServicesRequests.Find(requestId);
            if (request != null)
            {
                _context.RoomServicesRequests.Remove(request);
                _context.SaveChanges();
            }
        }


        public List<RoomServicesRequest> GetServiceRequestHistory(int patientId)
        {
            return _context.RoomServicesRequests.Where(r => r.PatientId == patientId).ToList();
        }
        public void UpdateServiceRequestStatus(int requestId, string newStatus)
        {
            var request = _context.RoomServicesRequests.Find(requestId);
            if (request != null)
            {

                request.Status = newStatus;
                _context.SaveChanges();
            }
        }
    }
}
