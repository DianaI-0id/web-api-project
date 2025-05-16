using data.Repository;
using data;
using domain.Service;
using domain.UseCase;

namespace Presence.API.Extensions
{
    public static class ServiceCollectionsExtension
    {
        public static void AddCommonServices(this IServiceCollection services)
        {
            services
                .AddDbContext<RemoteDatabaseContext>()
                .AddScoped<IGroupRepository, SQLGroupRepository>()
                .AddScoped<IGroupUseCase, GroupService>()
                .AddScoped<IStudentRepository, SQLStudentRepository>()
                .AddScoped<IStudentUseCase, StudentService>()
                .AddScoped<IAttendanceRepository, SQLAttendanceRepository>()
                .AddScoped<IAttendanceUseCase, AttendanceService>();
        }
    }
}
