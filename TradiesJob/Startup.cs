
#region Modification Log
/*------------------------------------------------------------------------------------------------------------------------------------------------- 
    System      -   TradiesJob
    Client      -   Fergus Software Ltd New Zealand         
    Module      -   Core
    Sub_Module  -   Api

    Copyright   -   Anuruddha Rajapaksha 
 
 Modification History:
 ==================================================================================================================================================
 Date              Version      Modify by              Description
 --------------------------------------------------------------------------------------------------------------------------------------------------
 03/06/2022         1.0      Anuruddha                  Initial Version
--------------------------------------------------------------------------------------------------------------------------------------------------*/
#endregion

#region Namespace
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradiesJob.Core.Cqrs;
using TradiesJob.Core.DataAccess.Connection;
using TradiesJob.Core.DataAccess.Database;
using TradiesJob.Core.DataAccess.Encrypter;
using TradiesJob.Core.DataAccess.RequestContext;
using TradiesJob.Core.DataAccess.UserHelper;
using TradiesJob.Domain.CommandHandlers;
using TradiesJob.Domain.QueryHandlers;
using TradiesJob.Public.Commands;
using TradiesJob.Public.Queries;
using TradiesJob.Public.Results;
#endregion

namespace TradiesJob {
    public class Startup {

        public IConfiguration Configuration { get; }
        public readonly string AllowAllOrigins = "AllowAllOrigins ";

        public Startup(IConfiguration configuration) {
            Configuration = configuration;
            // Database.ConfigCS = Configuration.GetConnectionString("Connection");
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services) {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddCors(options => {
                options.AddPolicy(AllowAllOrigins,
                    builder => {
                        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                    });
            });

            services.AddMvc(options => options.EnableEndpointRouting = false);

            services.AddTransient<Messages>();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IRequestContextService, RequestContextService>();
            services.AddTransient<IEncrypter, Encrypter>();
            services.AddTransient<IUserHelper, UserHelper>();
            services.AddTransient<ConnectionStrings>();
            services.AddTransient<IDatabase, Database>();
            services.AddTransient<IQueryHandler<JobSearchQuery, List<JobSearchResult>>, JobSearchQueryHandler>();
            services.AddTransient<IQueryHandler<JobQuery, JobResult>, JobQueryHandler>();
            services.AddTransient<ICommandHandler<JobCreateCommand, AppResult>, JobCreateCommandHandler>();
            services.AddTransient<ICommandHandler<JobUpdateCommand, AppResult>, JobUpdaetCommandHandler>();
            services.AddTransient<ICommandHandler<NoteCreateCommand, AppResult>, NoteCreateCommandHandler>();
            services.AddTransient<ICommandHandler<NoteUpdateCommand, AppResult>, NoteUpdateCommandHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            } else {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors(AllowAllOrigins);
            app.UseHttpsRedirection();



            // https://stackoverflow.com/questions/53906866/neterr-invalid-http-response-error-after-post-request-with-angular-7
            app.Use(async (ctx, next) => {
                await next();
                if (ctx.Response.StatusCode == 204) {
                    ctx.Response.ContentLength = 0;
                }
            });

            /*
            app.UseRouting();
            
            app.UseEndpoints(endpoints => {
                endpoints.MapGet("/", async context => {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
            */

            app.UseMvc(cfg => {
                cfg.MapRoute("Default",
                  "{controller}/{action}/{id?}",
                  new { controller = "App", Action = "Index" });
            });

        }
    }
}
