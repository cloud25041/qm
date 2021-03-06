using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AR_Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using EventBus.MQTT;
using AR_Application.IntegrationEvents.IncomingEvents;

namespace AR_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            CreateDbIfNotExists(host);
            ConnectToRabbitMQ(host);
            host.Run();
        }

        private static void CreateDbIfNotExists(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var customerContext = services.GetRequiredService<CustomerContext>();
                    bool isCreated = customerContext.Database.EnsureCreated();

                    DbInitializer.Initialize(customerContext);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB.");
                }
            }
        }

        private static void ConnectToRabbitMQ(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    IMQClient mQClient = services.GetRequiredService<IMQClient>();
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogInformation("connecting to mqtt, ip:" + mQClient.MqttBroker + " port:" + mQClient.MqttPort.ToString() + " username:" + mQClient.MqttUserId + " password:" + mQClient.MqttPassword);
                    mQClient.Connect();
                    while (!mQClient.IsConnected)
                    {
                        Task.Delay(1000).Wait();
                    }
                    if(mQClient.IsConnected == true)
                    {
                        mQClient.Subscribe<AppointmentConfirmedByStaffIntegrationEvent, AppointmentConfirmedByStaffIntegrationEventHandler>();
                        mQClient.Subscribe<AppointmentCompletedIntegrationEvent, AppointmentCompletedIntegrationEventHandler>();
                        mQClient.Subscribe<NoShowAppointmentIntegrationEvent, NoShowAppointmentIntegrationEventHandler>();
                        mQClient.Subscribe<ZoomLinkCreatedIntegrationEvent, ZoomLinkCreatedIntegrationEventHandler>();
                    }
                    else
                    {
                        logger.LogError("MQ client not connected!");
                    }
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred when connecting to RabbitMQ.");
                }
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
