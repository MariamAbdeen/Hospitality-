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

namespace HospitalityModuleTests.Services
{
    public class PatientServicesTest
    {
        public PatientServices test()
        {
            var PatientData = new List<Patient> {
              new Patient  { Id = 1, Name = "Patient 1" },
              new Patient  { Id = 2, Name  = "Patient 2" }}.AsQueryable(); // Convert the list to a queryable collection
            var mockDbSet = new Mock<DbSet<Patient>>();
            mockDbSet.As<IQueryable<Patient>>().Setup(m => m.Provider).Returns(PatientData.Provider);
            mockDbSet.As<IQueryable<Patient>>().Setup(m => m.Expression).Returns(PatientData.Expression);
            mockDbSet.As<IQueryable<Patient>>().Setup(m => m.ElementType).Returns(PatientData.ElementType);
            mockDbSet.As<IQueryable<Patient>>().Setup(m => m.GetEnumerator()).Returns(PatientData.GetEnumerator());


            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(c => c.Patients).Returns(mockDbSet.Object); // Set up context to return mock DbSet

            var PatientService = new PatientServices(mockContext.Object);

            return PatientService;
        }
        [Fact]
        public void GetAllPatients()
        {
            var patients = test().GetAllPatients();
            Assert.Equal(2, patients.Count);
            Assert.Equal("Patient 1", patients[0].Name);
            Assert.Equal("Patient 2", patients[1].Name);

        }
        [Fact]
        public void GetPatientById_ReturnsPatientWithSpecifiedId()
        {
            var PatientId = 1;
            var Patient = test().GetPatientById(PatientId);
            Assert.Equal(PatientId, Patient.Id);
            Assert.Equal("Patient 1", Patient.Name);
        }
        [Fact]
        public void RemovePatient()
        {
            var PatientIdToRemove = 1;
            var PatientToRemove = new Patient { Id = PatientIdToRemove, Name = "Mariam" };



            var mockDbSet = new Mock<DbSet<Patient>>();
            mockDbSet.Setup(d => d.Find(PatientIdToRemove)).Returns(PatientToRemove);

            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(c => c.Patients).Returns(mockDbSet.Object);

            var PatientService = new PatientServices(mockContext.Object);

            // Act
            PatientService.RemovePatient(PatientIdToRemove);

            // Assert
            mockDbSet.Verify(d => d.Find(PatientIdToRemove), Times.Once);
            mockDbSet.Verify(d => d.Remove(PatientToRemove), Times.Once);
            mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }
        [Fact]
        public void AddPatient()
        {
            // Arrange
            var PatientDtoToAdd = new PatientDto { };

            var mockContext = new Mock<AppDbContext>();
            var mockDbSet = new Mock<DbSet<Patient>>();
            var mockMapper = new Mock<IMapper>();
            mockContext.Setup(c => c.Patients).Returns(mockDbSet.Object);

            var PatientService = new PatientServices(mockContext.Object, mockMapper.Object);

            // Act
            PatientService.AddPatient(PatientDtoToAdd);

            // Assert
            mockDbSet.Verify(d => d.Add(It.IsAny<Patient>()), Times.Once);
            mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }




    }
}
