using HospitalityModules.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace HospitalityModules.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() { }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<RoomServicesRequest> RoomServicesRequests { get; set; }
        public virtual DbSet<HospitalityStaff> HospitalityStaff { get; set; }
        public virtual DbSet<Visitor> Visitors { get; set; }




    }





}
