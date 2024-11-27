using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace PropertyApi
{
    public class Startup
    {
        // ConfigureServices is where you register services for dependency injection
        public void ConfigureServices(IServiceCollection services)
        {
            // Registering the controllers and swagger services
            services.AddControllers();
            services.AddSwaggerGen();

            // Registering CORS services
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()  // Allow requests from any origin
                        .AllowAnyMethod()      // Allow any HTTP method (GET, POST, etc.)
                        .AllowAnyHeader();     // Allow any headers
                });
            });
        }

        // Configure is where you define how the app responds to HTTP requests
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Enabling CORS globally for the entire application
            app.UseCors("AllowAll"); // Use the CORS policy that we registered

            app.UseRouting(); // Enable routing to controller actions

            // Map controller endpoints
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); // Define endpoints for the controllers
            });
        }
    }
}
