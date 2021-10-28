using EventBus.MQTT;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Staff_Application;
using Staff_Application.Behaviours;
using Staff_Application.IntegrationEvents;
using Staff_Application.Queries;
using Staff_Domain.AggregateModel.AccountAggregate;
using Staff_Domain.AggregateModel.AgencyAggregate;
using Staff_Domain.AggregateModel.AppointmentAggregate;
using Staff_Infrastructure.Data;
using Staff_Infrastructure.Repository;
using System;

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
                options.UseNpgsql(Configuration["ConnectionString"]));

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddMediatR(typeof(ApplicationLayerMediatREntryPoint).Assembly);
            services.AddScoped<AccountQueries>(x => new AccountQueries(Configuration["ConnectionString"]));
            services.AddScoped<AppointmentQueries>(x => new AppointmentQueries(Configuration["ConnectionString"]));
            services.AddScoped<AgencyQueries>(x => new AgencyQueries(Configuration["ConnectionString"]));

            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IAgencyRepository, AgencyRepository>();

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehaviour<,>));

            services.AddSingleton<IMQClient>(x => new MQClient()
            {
                MqttBroker = Configuration["MqttIp"],
                MqttPort = Convert.ToInt32(Configuration["MqttPort"]),
                MqttUserId = Configuration["MqttUsername"],
                MqttPassword = Configuration["MqttPassword"],
                UsingLocalBroker = true
            });
            services.AddSingleton<IStaffIntegrationEventService, StaffIntegrationEventService>();
            services.AddHostedService<StaffIntegrationEventConsumerService>();

            services.AddCors(options =>
                options.AddDefaultPolicy(builder =>
                    builder.WithOrigins("https://localhost:44361").WithOrigins("https://localhost:44362")
                    .WithOrigins("http://localhost:40897").WithOrigins("http://localhost:48563")
                    .AllowAnyOrigin()
                    .AllowAnyMethod().AllowAnyHeader()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Staff_API v1"));
                app.UseCors();
            }

            loggerFactory.AddFile(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/qm/logs/staff_api-{Date}.log");
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
