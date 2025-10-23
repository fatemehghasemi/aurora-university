using AuroraUniversity.Domain.Interfaces;
using AuroraUniversity.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace AuroraUniversity.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddTransient<IStudentRepository, StudentRepository>();
        services.AddTransient<ITermRepository, TermRepository>();
        services.AddTransient<ITermModuleRepository, TermModuleRepository>();
        services.AddTransient<IAssessmentRepository, AssessmentRepository>();
        services.AddTransient<IMarkRepository, MarkRepository>();
        return services;
    }
}
