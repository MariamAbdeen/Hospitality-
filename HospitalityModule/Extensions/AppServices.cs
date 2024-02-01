using HospitalityModules.Mapping;
using HospitalityModules.Services;

namespace HospitalityModules.Extensions
{
    public static class AppServices
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<PatientServices>();
            services.AddScoped<RoomServices>();
            services.AddScoped<VisitorServices>();
            services.AddScoped<RoomServicesRequestServices>();
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddScoped<HospitalitystaffServices>();

            return services;
        }

    }
}
