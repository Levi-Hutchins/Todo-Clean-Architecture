using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Todo.Application.IServices;
using Todo.Infrastructure.EntityFramework;
using Todo.Infrastructure.Services;

namespace Todo.Infrastructure;

public static class DependencyInjection
{
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TodoDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            
            
            services.AddScoped<ITodoService, TodoService>();

            return services;
        }
}