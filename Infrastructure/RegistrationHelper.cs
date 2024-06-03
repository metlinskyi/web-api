using abc.Infrastructure.Tasks;

public static class RegistrationHelper{
    public static void AddInfrastructure(this IServiceCollection services){
        services.AddSingleton<ITaskList, TaskList>();
    }
}