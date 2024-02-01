using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HospitalityModules.Controllers;
using HospitalityModules.Data;
using HospitalityModules.DTOs;
using HospitalityModules.Entities;
using HospitalityModules.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;


namespace HospitalityModulesTest.Services

{
    public class RoomservicesTest

    {
        public RoomServices test()
        {
            var roomsData = new List<Room> {
              new Room { Id = 1, Type = "Room 1" },
              new Room { Id = 2, Type = "Room 2" }}.AsQueryable(); // Convert the list to a queryable collection
            var mockDbSet = new Mock<DbSet<Room>>();
            mockDbSet.As<IQueryable<Room>>().Setup(m => m.Provider).Returns(roomsData.Provider);
            mockDbSet.As<IQueryable<Room>>().Setup(m => m.Expression).Returns(roomsData.Expression);
            mockDbSet.As<IQueryable<Room>>().Setup(m => m.ElementType).Returns(roomsData.ElementType);
            mockDbSet.As<IQueryable<Room>>().Setup(m => m.GetEnumerator()).Returns(roomsData.GetEnumerator());


            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(c => c.Rooms).Returns(mockDbSet.Object); // Set up context to return mock DbSet

            var roomService = new RoomServices(mockContext.Object);

            return roomService;
        }






        [Fact]

        public void GetAllRooms_ReturnsListOfRooms()
        {


            // Act
            var rooms = test().GetAllRooms();

            // Assert

            Assert.Equal(2, rooms.Count); // Check if correct number of rooms is returned
            Assert.Equal("Room 1", rooms[0].Type); // Check if first room's name is correct
            Assert.Equal("Room 2", rooms[1].Type); // Check if second room's name is correct
        }

        [Fact]
        public void GetRoomById_ReturnsRommWithSpecifiedId()
        {
            var roomIdToFind = 1;
            var room = test().GetRoomById(roomIdToFind);
            Assert.NotNull(room);
            Assert.Equal(roomIdToFind, room.Id); // Check if correct room is returned
            Assert.Equal("Room 1", room.Type);

        }

        [Fact]
        public void DeleteRoomById()
        {
            var roomIdToRemove = 1;
            var roomToRemove = new Room { Id = roomIdToRemove, Type = "Room to Remove" };

            var mockDbSet = new Mock<DbSet<Room>>();
            mockDbSet.Setup(d => d.Find(roomIdToRemove)).Returns(roomToRemove);

            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(c => c.Rooms).Returns(mockDbSet.Object);

            var roomService = new RoomServices(mockContext.Object);

            // Act
            roomService.RemoveRoom(roomIdToRemove);

            // Assert
            mockDbSet.Verify(d => d.Find(roomIdToRemove), Times.Once);
            mockDbSet.Verify(d => d.Remove(roomToRemove), Times.Once);
            mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }

        [Fact]
        public void AddRoom()
        {
            // Arrange
            var roomDtoToAdd = new RoomDto { Availability = true };

            var mockContext = new Mock<AppDbContext>();
            var mockDbSet = new Mock<DbSet<Room>>();
            var mockMapper = new Mock<IMapper>();
            mockContext.Setup(c => c.Rooms).Returns(mockDbSet.Object);

            var roomService = new RoomServices(mockContext.Object, mockMapper.Object);

            // Act
            roomService.AddRoom(roomDtoToAdd);

            // Assert
            mockDbSet.Verify(d => d.Add(It.IsAny<Room>()), Times.Once);
            mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }

        [Fact]
        public void AllocateRoomToPatient_ReturnsZeroIfRoomNotFound()
        {
            var roomId = 1; // Room ID to allocate
            var patientId = 123; // Patient ID
            var result = test().AllocateRoom(roomId, patientId);

            // Assert
            Assert.Equal(0, result);

        }

        // [Fact]
        //public void AllocateRoomToPatient()
        //{

        //    var roomId = 1; // Room ID to allocate
        //    var patientId = 123; // Patient ID

        //    var roomsData = new List<Room>
        //{
        //    new Room { Id = roomId, Availability = true }
        //    // Add more rooms if needed for your test scenario
        //}.AsQueryable();

        //    var mockDbSet = new Mock<DbSet<Room>>();
        //    mockDbSet.As<IQueryable<Room>>().Setup(m => m.Provider).Returns(roomsData.Provider);
        //    mockDbSet.As<IQueryable<Room>>().Setup(m => m.Expression).Returns(roomsData.Expression);
        //    mockDbSet.As<IQueryable<Room>>().Setup(m => m.ElementType).Returns(roomsData.ElementType);
        //    mockDbSet.As<IQueryable<Room>>().Setup(m => m.GetEnumerator()).Returns(roomsData.GetEnumerator());

        //    var mockContext = new Mock<AppDbContext>();
        //    mockContext.Setup(c => c.Rooms).Returns(mockDbSet.Object);

        //    var roomService = new RoomServices(mockContext.Object);

        //    // Act
        //    var result = roomService.AllocateRoom(roomId, patientId);

        //    // Assert
        //    //mockDbSet.Verify(R => R.Where(It.IsAny<Expression<Func<Room, bool>>>()), Times.Once);
        //    //mockContext.Verify(c => c.SaveChanges(), Times.Once);
        //    Assert.Equal(1, result); // Ensure that SaveChanges returns 1
        //}
    }


}
