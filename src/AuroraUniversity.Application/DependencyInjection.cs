using AuroraUniversity.Application.Interfaces;
using AuroraUniversity.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AuroraUniversity.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddTransient<IStudentService, StudentService>();
        services.AddTransient<IMarkService, MarkService>();
        services.AddTransient<IEnrollmentService, EnrollmentService>();
        services.AddTransient<IAssessmentService, AssessmentService>();
        services.AddTransient<IGradeCalculationService, GradeCalculationService>();
        services.AddTransient<ITermModuleService, TermModuleService>();

        return services;
    }
}