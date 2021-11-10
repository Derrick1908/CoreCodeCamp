using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CoreCodeCamp.Controllers;
using CoreCodeCamp.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc.Versioning.Conventions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CoreCodeCamp
{
  public class Startup
  {
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddDbContext<CampContext>();
      services.AddScoped<ICampRepository, CampRepository>();

      services.AddAutoMapper(Assembly.GetExecutingAssembly());
      services.AddApiVersioning( opt =>
      {
          opt.AssumeDefaultVersionWhenUnspecified = true;
          opt.DefaultApiVersion = new ApiVersion(1, 1);
          opt.ReportApiVersions = true;
          opt.ApiVersionReader = new UrlSegmentApiVersionReader();

          /*
          opt.ApiVersionReader = new QueryStringApiVersionReader("ver");  //Used to Change the Query String from ?api-version to ?ver
          opt.ApiVersionReader = new HeaderApiVersionReader("X-Version");   //Using a Header "X-Version" we can specify which Version we want rather than specifying Query String
          opt.ApiVersionReader = ApiVersionReader.Combine(              //Either Header or Query String can be used
              new HeaderApiVersionReader("X-Version"),  
              new QueryStringApiVersionReader("ver","version")          //Either Ver or Version can be used
              );
          */
          /*
          opt.Conventions.Controller<TalksController>()                 //Rather than specifiying Version Information on the Controller Itself, we can place only that Info Centrally in the Start Up Class.
             .HasApiVersion(new ApiVersion(1, 0))
             .HasApiVersion(new ApiVersion(1, 1))
             .Action(c => c.Delete(default(string), default(int)))      //This Means that Delete Operation is only applicable to Version 1.1
             .MapToApiVersion(1, 1);
         */
      });
      services.AddControllers();
      
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseRouting();

      app.UseAuthentication();
      app.UseAuthorization();

      app.UseEndpoints(cfg =>
      {
        cfg.MapControllers();
      });
    }
  }
}
