using HospitalityModules.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalityModules.Data.Configuration
{
    public class RoomServicesRequestConfig : IEntityTypeConfiguration<RoomServicesRequest>
    {
        public void Configure(EntityTypeBuilder<RoomServicesRequest> builder)
        {
            builder.Property(S => S.ServicesType).IsRequired();
            builder.Property(S => S.Status).IsRequired();
            //builder.HasOne(S => S.Room)
            //       .WithMany()
            //       .HasForeignKey(S => S.RoomId);
            //builder.HasOne(S=>S.Patient)
            //       .WithMany()
            //       .HasForeignKey (S=>S.PatientId);


        }
    }
}