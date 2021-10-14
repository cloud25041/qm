using Staff_Application;
using Staff_Application.Behaviours;
using Staff_Application.Queries;
using Staff_Infrastructure.Data;
using Staff_Infrastructure.Repository;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Staff_Domain.AggregateModel.AppointmentAggregate;
using Staff_Domain.AggregateModel.AccountAggregate;

namespace Staff_API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Staff_API", Version = "v1" });
            });
          
           services.AddDbContext<StaffContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("StaffContext")));

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddMediatR(typeof(ApplicationLayerMediatREntryPoint).Assembly);
            services.AddScoped<AccountQueries>(x => new AccountQueries(Configuration["ConnectionString"]));
            services.AddScoped<AppointmentQueries>(x => new AppointmentQueries(Configuration["ConnectionString"]));

            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehaviour<,>));

            services.AddCors(options =>
                options.AddDefaultPolicy(builder =>
                    builder.WithOrigins("https://localhost:44361").WithOrigins("https://localhost:44362").AllowAnyMethod().AllowAnyHeader()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Staff_API v1"));
                app.UseCors();
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
