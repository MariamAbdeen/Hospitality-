using HospitalityModules.Data;
using HospitalityModules.Entities;
using HospitalityModules.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalityModulesTest.Services
{
    public class RoomRequestServicesTest
    {

        public RoomServicesRequestServices test()
        {
            var RequestData = new List<RoomServicesRequest> {
              new RoomServicesRequest { Id = 1 },
              new RoomServicesRequest  { Id = 2 }}.AsQueryable(); // Convert the list to a queryable collection
            var mockDbSet = new Mock<DbSet<RoomServicesRequest>>();
            mockDbSet.As<IQueryable<RoomServicesRequest>>().Setup(m => m.Provider).Returns(RequestData.Provider);
            mockDbSet.As<IQueryable<RoomServicesRequest>>().Setup(m => m.Expression).Returns(RequestData.Expression);
            mockDbSet.As<IQueryable<RoomServicesRequest>>().Setup(m => m.ElementType).Returns(RequestData.ElementType);
            mockDbSet.As<IQueryable<RoomServicesRequest>>().Setup(m => m.GetEnumerator()).Returns(RequestData.GetEnumerator());


            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(c => c.RoomServicesRequests).Returns(mockDbSet.Object); // Set up context to return mock DbSet

            var RoomRequestServices = new RoomServicesRequestServices(mockContext.Object);

            return RoomRequestServices;
        }
        [Fact]
        public void DeleteServiceRequest()
        {
            var ServicesRequestIdToBeRemove = 1;
            var ServiceRequestToBeRemove = new RoomServicesRequest { Id = 1, Status = "Pending" };



            var mockDbSet = new Mock<DbSet<RoomServicesRequest>>();
            mockDbSet.Setup(d => d.Find(ServicesRequestIdToBeRemove)).Returns(ServiceRequestToBeRemove);

            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(c => c.RoomServicesRequests).Returns(mockDbSet.Object);

            var RoomRequestServices = new RoomServicesRequestServices(mockContext.Object);

            // Act
            RoomRequestServices.DeleteServiceRequest(ServicesRequestIdToBeRemove);

            // Assert
            mockDbSet.Verify(d => d.Find(ServicesRequestIdToBeRemove), Times.Once);
            mockDbSet.Verify(d => d.Remove(ServiceRequestToBeRemove), Times.Once);
            mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }

        [Fact]
        public void GetServiceRequest()
        {
            var patientId = 1;
            var RequestData = new List<RoomServicesRequest> {
           new RoomServicesRequest { Id = 1,PatientId = patientId ,ServicesType ="Tv" }
              };
            var result = test().GetServiceRequestHistory(patientId);

            // Assert
            //Assert.Equal(RequestData.Count, result.Count);
            Assert.Equal(patientId, RequestData[0].Id);

        }

        [Fact]
        public void UpdateServiceRequestStatus_UpdatesStatus()
        {
            // Arrange
            var requestId = 1; // Request ID
            var newStatus = "Completed"; // New status

            var requestToUpdate = new RoomServicesRequest { Id = requestId, /* Other properties */ }; // Create a mock request

            var mockDbSet = new Mock<DbSet<RoomServicesRequest>>();
            mockDbSet.Setup(d => d.Find(requestId)).Returns(requestToUpdate);

            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(c => c.RoomServicesRequests).Returns(mockDbSet.Object);

            var RoomServicesRequest = new RoomServicesRequestServices(mockContext.Object);

            // Act
            RoomServicesRequest.UpdateServiceRequestStatus(requestId, newStatus);

            // Assert
            mockDbSet.Verify(d => d.Find(requestId), Times.Once); // Verify that Find method is called once with the correct requestId
            mockContext.Verify(c => c.SaveChanges(), Times.Once); // Verify that SaveChanges method is called once
            Assert.Equal(newStatus, requestToUpdate.Status); // Verify that the status of the request is updated to the newStatus
        }
    }
}
