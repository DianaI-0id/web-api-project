using data;
using data.Repository;
using domain.Service;
using domain.UseCase;
using Microsoft.Extensions.DependencyInjection;
using Presence.Desktop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presence.Desktop.DI
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCommonService(this IServiceCollection collection)
        {
            collection
                .AddDbContext<RemoteDatabaseContext>()
                .AddSingleton<IGroupRepository, SQLGroupRepository>()
                .AddTransient<IGroupUseCase, GroupService>()
                .AddTransient<GroupViewModel>()
                .AddSingleton<IAttendanceRepository, SQLAttendanceRepository>()
                .AddTransient<IAttendanceUseCase, AttendanceService>()
                .AddSingleton<IStudentRepository, SQLStudentRepository>()
                .AddTransient<IStudentUseCase, StudentService>();
            //добавить в transient viewModels
        }
    }
}
