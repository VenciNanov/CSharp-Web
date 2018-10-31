using SIS.Framework.Api;
using SIS.Framework.Services;
using Torshia.Services;
using Torshia.Services.Contracts;

namespace Torshia
{
    public class StartUp : MvcApplication
    {
        public override void ConfigureServices(IDependencyContainer dependencyContainer)
        {
            dependencyContainer.RegisterDependency<IUsersSevice, UsersService>();
            dependencyContainer.RegisterDependency<ITasksService, TasksService>();
            dependencyContainer.RegisterDependency<IReportsService,ReportsService>();

        }
    }
}