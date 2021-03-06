using AR_Application;
using AR_Application.Queries;
using AR_Domain.AggregateModel.AccountAggregate;
using AR_Domain.AggregateModel.AppointmentAggregate;
using AR_Infrastructure.Data;
using AR_Infrastructure.Repository;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using AR_Application.Behaviours;
using EventBus.MQTT;
using AR_Application.IntegrationEvents;
using System;
using Microsoft.Extensions.Logging;

namespace AR_API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AR_API", Version = "v1" });
            });

            services.AddDbContext<CustomerContext>(options =>
                options.UseNpgsql(Configuration["ConnectionString"]));
            
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddMediatR(typeof(ApplicationLayerMediatREntryPoint).Assembly);
            services.AddScoped<AccountQueries>(x => new AccountQueries(Configuration["ConnectionString"]));
            services.AddScoped<AppointmentQueries>(x => new AppointmentQueries(Configuration["ConnectionString"]));

            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehaviour<,>));

            services.AddSingleton<IMQClient>(x => new MQClient() { 
                MqttBroker = Configuration["MqttIp"], 
                MqttPort =  Convert.ToInt32(Configuration["MqttPort"]), 
                MqttUserId = Configuration["MqttUsername"], 
                MqttPassword = Configuration["MqttPassword"], 
                UsingLocalBroker = true });
            // Eric - ICustomerIntegrationEventService should be transient, using singleton for easier implementation.
            services.AddSingleton<ICustomerIntegrationEventService, CustomerIntegrationEventService>();
            services.AddHostedService<CustomerIntegrationEventConsumerService>();

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
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AR_API v1"));
            }

            loggerFactory.AddFile(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/qm/logs/ar_api-{Date}.log");

            //app.UseHttpsRedirection();

            app.UseCors();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
