using EventBus.MQTT;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Staff_Application.IntegrationEvents.IncomingEvents;
using Staff_Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Staff_API
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
                    var staffContext = services.GetRequiredService<StaffContext>();
                    bool isCreated = staffContext.Database.EnsureCreated();

                    DbInitializer.Initialize(staffContext);
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
                    mQClient.Connect();
                    Task.Delay(1000).Wait();
                    if (mQClient.IsConnected == true)
                    {
                        mQClient.Subscribe<AppointmentBookedIntegrationEvent, AppointmentBookedIntegrationEventHandler>();
                        mQClient.Subscribe<ZoomLinkCreatedIntegrationEvent, ZoomLinkCreatedIntegrationEventHandler>();
                    }
                    else
                    {
                        var logger = services.GetRequiredService<ILogger<Program>>();
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
