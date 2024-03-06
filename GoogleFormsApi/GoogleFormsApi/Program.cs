using Domain.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;
using Persistence;
using Application.CQRS.Commands.FormActions;
using GoogleFormsApi.MapperProfiles;
using GoogleFormsApi.Middlewares;
using GoogleFormsApi.Helpers;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.RegisterServices();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseMiddleware<AppExceptionHandlerMiddleware>();

        app.UseHttpsRedirection();

        app.UseCors("AllowAll");

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();

        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;

            try
            {
                var context = services.GetRequiredService<GoogleFormsDbContext>();
                context.Database.Migrate();
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError($"An error occurred while migrating or initializing the database. Exception: {ex.Message}");
            }
        }

        app.Run();
    }
}