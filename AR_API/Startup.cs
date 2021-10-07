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
using MediatR;
using AR_Application;
using AR_Application.Queries;
using AR_Infrastructure.Data;
using AR_Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using AR_Domain.AggregateModel.AccountAggregate;
using AR_Domain.AggregateModel.AppointmentAggregate;

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
                options.UseNpgsql(Configuration.GetConnectionString("CustomerContext")));
            
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddMediatR(typeof(ApplicationLayerMediatREntryPoint).Assembly);
            services.AddScoped<AccountQueries>(x => new AccountQueries(Configuration["ConnectionString"]));
            services.AddScoped<AppointmentQueries>(x => new AppointmentQueries(Configuration["ConnectionString"]));

            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AR_API v1"));
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
