using HospitalityModules.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalityModules.Data.Configuration
{
    public class HospitalityStaffConfiguration : IEntityTypeConfiguration<HospitalityStaff>
    {
        public void Configure(EntityTypeBuilder<HospitalityStaff> builder)
        {
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Role).IsRequired();
        }
    }
}
