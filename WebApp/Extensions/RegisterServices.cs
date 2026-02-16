using Application.Interfaces;
using Infrastructure.Jwt;
using Infrastructure.PasswordHash;
using Infrastructure.Services;

namespace WebApp.Extensions;

public static class RegisterServices
{
    public static void AddRegistrationServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IPasswordHashed, PasswordHashed>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ICourseService, CourseService>();
        services.AddScoped<ILessonService, LessonService>();
        services.AddScoped<IExamService, ExamService>();
    }
}