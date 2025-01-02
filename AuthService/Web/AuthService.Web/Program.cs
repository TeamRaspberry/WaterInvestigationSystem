using AuthService.Application;
using AuthService.Persistence;
using AuthService.Persistence.Database;
using AuthService.Web.Components;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddApplication();
            builder.Services.AddPersistence(builder.Configuration);
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(20);
            });

            builder.Services.AddControllers();

            var app = builder.Build();

            using (IServiceScope scope = app.Services.CreateScope())
            {
                DatabaseContext context = scope.ServiceProvider.GetService<DatabaseContext>() 
                    ?? throw new InvalidOperationException("Database not connected");

                context.Database.Migrate();
            }

            app.UseHttpsRedirection();

            app.UseAntiforgery();

            app.MapStaticAssets();

            app.UseSession();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.MapControllers();

            app.Run();
        }
    }
}
