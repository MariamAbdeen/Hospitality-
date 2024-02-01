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

namespace HospitalityModulesTest.Services
{
    public class HospitalityStaffServicesTest
    {
        public HospitalitystaffServices test()
        {
            var StaffData = new List<HospitalityStaff> {
              new HospitalityStaff  { Id = 1, Name = "Mariam" },
              new HospitalityStaff  { Id = 2, Name  = "Ahmed"        }}.AsQueryable(); // Convert the list to a queryable collection
            var mockDbSet = new Mock<DbSet<HospitalityStaff>>();
            mockDbSet.As<IQueryable<HospitalityStaff>>().Setup(m => m.Provider).Returns(StaffData.Provider);
            mockDbSet.As<IQueryable<HospitalityStaff>>().Setup(m => m.Expression).Returns(StaffData.Expression);
            mockDbSet.As<IQueryable<HospitalityStaff>>().Setup(m => m.ElementType).Returns(StaffData.ElementType);
            mockDbSet.As<IQueryable<HospitalityStaff>>().Setup(m => m.GetEnumerator()).Returns(StaffData.GetEnumerator());


            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(c => c.HospitalityStaff).Returns(mockDbSet.Object); // Set up context to return mock DbSet

            var StaffServices = new HospitalitystaffServices(mockContext.Object);

            return StaffServices;
        }

        [Fact]
        public void GetAllHospitalityStaff()
        {
            var Staff = test().GetAllHospitalityStaff();
            Assert.Equal(2, Staff.Count);
            Assert.Equal("Mariam", Staff[0].Name);
            Assert.Equal("Ahmed", Staff[1].Name);
        }


        [Fact]
        public void GetHospitalityStaffById()
        {
            var personId = 1;
            var person = test().GetStaffById(personId);
            Assert.Equal(personId, person.Id);
            Assert.Equal("Mariam", person.Name);
        }
        [Fact]
        public void RemoveHospitalityStaf()
        {
            var PersonIdToRemove = 1;
            var PersonToRemove = new HospitalityStaff { Id = 1, Name = "Mariam" };



            var mockDbSet = new Mock<DbSet<HospitalityStaff>>();
            mockDbSet.Setup(d => d.Find(PersonIdToRemove)).Returns(PersonToRemove);

            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(c => c.HospitalityStaff).Returns(mockDbSet.Object);

            var PersonServices = new HospitalitystaffServices(mockContext.Object);

            // Act
            PersonServices.RemovePerson(PersonIdToRemove);

            // Assert
            mockDbSet.Verify(d => d.Find(PersonIdToRemove), Times.Once);
            mockDbSet.Verify(d => d.Remove(PersonToRemove), Times.Once);
            mockContext.Verify(c => c.SaveChanges(), Times.Once);

        }

        [Fact]
        public void AddHospitalityStaff()
        {

            // Arrange
            var PersonDtoToAdd = new HospitalityStaffDto { };

            var mockContext = new Mock<AppDbContext>();
            var mockDbSet = new Mock<DbSet<HospitalityStaff>>();
            var mockMapper = new Mock<IMapper>();
            mockContext.Setup(c => c.HospitalityStaff).Returns(mockDbSet.Object);

            var StaffServices = new HospitalitystaffServices(mockContext.Object, mockMapper.Object);

            // Act
            StaffServices.AddPerson(PersonDtoToAdd);

            // Assert
            mockDbSet.Verify(d => d.Add(It.IsAny<HospitalityStaff>()), Times.Once);
            mockContext.Verify(c => c.SaveChanges(), Times.Once);

        }

    }
}
