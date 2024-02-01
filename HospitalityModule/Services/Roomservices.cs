using AutoMapper;
using HospitalityModules.Data;
using HospitalityModules.DTOs;
using HospitalityModules.Entities;
using HospitalityModules.Errors;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalityModules.Services
{
    public class RoomServices
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;


        public RoomServices(AppDbContext context, IMapper mapper)
        {

            _context = context;
            _mapper = mapper;
        }

        public RoomServices(AppDbContext context) { _context = context; }
        public RoomServices() { }

        public int AllocateRoom(int RoomId, int PatientId)
        {
            var room = _context.Rooms.Where(R => R.Id == RoomId).FirstOrDefault();

            if (room is null)
            {
                return 0;
            }

            room.PatientId = PatientId;
            room.Availability = false;
            return _context.SaveChanges();
        }

        public List<Room> GetAllRooms()
        {
            return _context.Rooms.ToList();
        }

        public Room GetRoomById(int RoomId)
        {
            return _context.Rooms.FirstOrDefault(R => R.Id == RoomId);
        }

        public void AddRoom(RoomDto room)
        {
            var mappedRoom = _mapper.Map<Room>(room);
            _context.Rooms.Add(mappedRoom);
            _context.SaveChanges();
        }

        public void RemoveRoom(int RoomId)
        {
            var room = _context.Rooms.Find(RoomId);
            if (room != null)
            {
                _context.Rooms.Remove(room);
                _context.SaveChanges();
            }

        }
    }


}