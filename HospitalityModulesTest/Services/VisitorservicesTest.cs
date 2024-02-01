using AutoMapper;
using HospitalityModules.Data;
using HospitalityModules.DTOs;
using HospitalityModules.Entities;
using HospitalityModules.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalityModulesTests.Services
{
    public class VisitorservicesTest
    {
        public VisitorServices test()
        {
            var VisitorData = new List<Visitor> {
              new Visitor  { Id = 1, Name = "Visitor 1" ,PatientId = 1},
              new Visitor  { Id = 2, Name  = "Visitor 2" , PatientId = 1}}.AsQueryable(); // Convert the list to a queryable collection
            var mockDbSet = new Mock<DbSet<Visitor>>();
            mockDbSet.As<IQueryable<Visitor>>().Setup(m => m.Provider).Returns(VisitorData.Provider);
            mockDbSet.As<IQueryable<Visitor>>().Setup(m => m.Expression).Returns(VisitorData.Expression);
            mockDbSet.As<IQueryable<Visitor>>().Setup(m => m.ElementType).Returns(VisitorData.ElementType);
            mockDbSet.As<IQueryable<Visitor>>().Setup(m => m.GetEnumerator()).Returns(VisitorData.GetEnumerator());


            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(c => c.Visitors).Returns(mockDbSet.Object); // Set up context to return mock DbSet

            var visitorService = new VisitorServices(mockContext.Object);

            return visitorService;
        }
        [Fact]
        public void DeleteVisitor()
        {
            var VisitorIdToRemove = 1;
            var VisitorToRemove = new Visitor { Id = VisitorIdToRemove, Name = "Mariam" };



            var mockDbSet = new Mock<DbSet<Visitor>>();
            mockDbSet.Setup(d => d.Find(VisitorIdToRemove)).Returns(VisitorToRemove);

            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(c => c.Visitors).Returns(mockDbSet.Object);

            var visitorService = new VisitorServices(mockContext.Object);

            // Act
            visitorService.DeleteVisitor(VisitorIdToRemove);

            // Assert
            mockDbSet.Verify(d => d.Find(VisitorIdToRemove), Times.Once);
            mockDbSet.Verify(d => d.Remove(VisitorToRemove), Times.Once);
            mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }

        [Fact]
        public void GetVisitorDetails()
        {
            var visitorId = 1;

            var visitorToReturn = new Visitor { Id = visitorId, }; 

            var mockDbSet = new Mock<DbSet<Visitor>>();
            mockDbSet.Setup(d => d.Find(visitorId)).Returns(visitorToReturn);

            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(c => c.Visitors).Returns(mockDbSet.Object);

            var visitorService = new VisitorServices(mockContext.Object);

            // Act
            var result = visitorService.GetVisitorDetails(visitorId);

            // Assert
            Assert.NotNull(result); // Ensure that the result is not null
            Assert.Equal(visitorId, result.Id);


        }

        [Fact]
        public void GetVisitorHistory()
        {
            var PatientId = 1;
            var expectedVisitorHistory = new List<Visitor>
        {
            new Visitor { Id=2,Name="Asmaa",PatientId =1},
            new Visitor { Id=1,Name ="Sara",PatientId =1},

        };
            

         
            var Result = test().GetVisitorHistory(PatientId);
            Assert.Equal(expectedVisitorHistory.Count, Result.Count);




        }

  
        //[Fact]
        //public void RegisterVisitor_ReturnVisitor  ()
        //{
        //    // Arrange
        //    int patientId = 1;
        //    var VisitorDtoToAdd = new VisitorsDto  { Name="Ahmed" };

        //    var mockContext = new Mock<AppDbContext>();
        //    var mockDbSet = new Mock<DbSet<Visitor >>();
        //    var mockMapper = new Mock<IMapper>();
        //    mockContext.Setup(c => c.Visitors ).Returns(mockDbSet.Object);

        //    var visitorServices = new VisitorServices (mockContext.Object, mockMapper.Object);

        //    // Act
        //    visitorServices.RegisterVisitor (patientId,VisitorDtoToAdd);

        //    // Assert
        //   mockDbSet.Verify(d => d.Add(It.IsAny<Visitor >()), Times.Once);
        //    mockContext.Verify(c => c.SaveChanges(), Times.Once);
        //}


    }
}
