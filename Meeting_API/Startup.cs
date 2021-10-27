using EventBus.MQTT;
using MediatR;
using Meeting_Application;
using Meeting_Application.Behaviours;
using Meeting_Application.IntegrationEvents;
using Meeting_Domain.DomainService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meeting_API
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

            services.AddMediatR(typeof(ApplicationLayerMediatREntryPoint).Assembly);
            services.AddSingleton<ZoomLinkCreatorService>(x => new ZoomLinkCreatorService(Configuration["ZoomKey"], Configuration["ZoomSecret"]));
            services.AddSingleton<IMQClient>(x => new MQClient()
            {
                MqttBroker = Configuration["MqttIp"],
                MqttPort = Convert.ToInt32(Configuration["MqttPort"]),
                MqttUserId = Configuration["MqttUsername"],
                MqttPassword = Configuration["MqttPassword"],
                UsingLocalBroker = true
            });
            // Eric - ICustomerIntegrationEventService should be transient, using singleton for easier implementation.
            services.AddSingleton<IMeetingIntegrationEventService, MeetingIntegrationEventService>();
            services.AddHostedService<MeetingIntegrationEventConsumerService>();

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehaviour<,>));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Meeting_API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Meeting_API v1"));
            }

            loggerFactory.AddFile(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/qm/logs/meeting_api-{Date}.log");

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
