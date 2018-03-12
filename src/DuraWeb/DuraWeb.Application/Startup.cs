using AutoMapper;
using DuraWeb.Application.Mapping;
using DuraWeb.EF;
using DuraWeb.EF.Repositories;
using DuraWeb.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DuraWeb.Application
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
      services.AddMvc();

      services.AddTransient<ICustomerRepository, CustomerRepository>();
      services.AddTransient<IInvoiceRepository, InvoiceRepository>();

      var connection = Configuration.GetConnectionString("DuraWebDatabase");
      services.AddDbContext<DuraContext>(options => options.UseSqlServer(connection));
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
      {
        var context = serviceScope.ServiceProvider.GetRequiredService<DuraContext>();
        context.Database.Migrate();
      }

      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      Mapper.Initialize(cfg =>
      {
        cfg.AddProfile<DuraProfile>();
      });

      app.UseMvc();
    }
  }
}
