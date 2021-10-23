using EventBus.MQTT;
using Meeting_Application.IntegrationEvents.IncomingEvents;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meeting_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();
            ConnectToRabbitMQ(host);
            host.Run();
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
                        mQClient.Subscribe<AppointmentConfirmedByStaffIntegrationEvent, AppointmentConfirmedByStaffIntegrationEventHandler>();
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
