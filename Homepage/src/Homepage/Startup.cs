using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Homepage.Client;
using Homepage.Controllers.Web;
using Homepage.Utils;
using Homepage.ViewModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace Homepage
{
    public class Startup
    {

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)// TODO* Bug is not working any more? ->, IHostingEnvironment environment)
        {
            //Registation of external services
            services.AddScoped<IServices, RegistrationManagerController>();
            services.AddScoped<IWebRequests, WebRequests>();
            services.AddMvc(config =>
            {
# if !DEBUG //TODO* Has to change to IHostingEnvironment environment used to work but sudenly not any more, see above.
            // if (environment.IsProduction())
                {
                //Redirects to a https, only used in productions
                //TODO Needs to be included, in order to use https
                //config.Filters.Add(new RequireHttpsAttribute());
                }
# endif               
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
            IHostingEnvironment environment, 
            ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            //Only while debuging
            if (environment.IsDevelopment())
            {
                //To catch exeption in browsers
                app.UseDeveloperExceptionPage();
                app.UseStaticFiles();
            }

            //Default configuration of the MVCs controller and action. If nothing is spezified in the url.
            app.UseMvc(config =>
            {
                config.MapRoute(
                    name: "Default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "App", action = "Index" }
                    );
            });
        }
    }
}
