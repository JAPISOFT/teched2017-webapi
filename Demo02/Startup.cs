using System;
using System.Collections.Generic;
using Demo02.AppFilters;
using Demo02.Lib.Entities;
using Demo02.Lib.Facades;
using Demo02.Lib.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;

namespace Demo02
{
    public class Startup
    {
	    public IConfigurationRoot Configuration { get; }

	    public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

	    public void ConfigureServices(IServiceCollection services)
	    {
		    services.AddDbContext<DemoContext>();
			services.AddScoped<ProductFacade>();
			services.AddScoped<ProductRepository>();
			services.AddScoped<TagRepository>();
			services.AddScoped<UnitOfWork>();

		    var mvc = services.AddMvc(setup =>
		    {
				setup.InputFormatters.Add(new XmlSerializerInputFormatter());
				setup.OutputFormatters.Add(new XmlSerializerOutputFormatter());
			    setup.Filters.Add(new AppExceptionFilter());
		    });

		    mvc.AddJsonOptions(options =>
		    {
				if (options.SerializerSettings.ContractResolver != null)
				{
					options.SerializerSettings.ContractResolver = new DefaultContractResolver()
					{
						NamingStrategy = new DefaultNamingStrategy()
					};
				}
			});

			//services.AddCors(o =>
			//{
			//	o.AddPolicy("Default", cfg =>
			//	{
			//		cfg.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
			//	});
			//});

			services.AddSwaggerGen();
			services.AddHttpCacheHeaders();
		}

	    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();

			//app.UseCors("Default");
			app.UseSwagger();
			app.UseSwaggerUi("doc");
			app.UseHttpCacheHeaders();

			app.UseDeveloperExceptionPage();

			app.UseExceptionHandler(exc =>
			{
				exc.Run(async context =>
				{
					context.Response.StatusCode = 500;
					await context.Response.WriteAsync("Unexpected error happened");
				});
			});

			DemoContext dbContext = app.ApplicationServices.GetService<DemoContext>();
			AddTestData(dbContext);
		}

	    private void AddTestData(DemoContext dbContext)
	    {
		    dbContext.Products.Add(
				new Product
				{
					ProductId = Guid.Parse("eeca4000-0dbb-447c-a4b7-d836559f5278"),
					Title = "Glock 19",
					Tags = new List<Tag>
					{
						new Tag {TagId = Guid.Parse("da421131-7997-4396-8133-531fcffd34a7"), Name = "Glock"},
						new Tag {TagId = Guid.Parse("d131808c-89fe-4400-a69b-d633e755d1ca"), Name = "Austria"}
					}
				}
		    );

			dbContext.Products.Add(
				new Product
				{
					ProductId = Guid.NewGuid(),
					Title = "CZ Skorpion 61 S",
					Tags = new List<Tag>
					{
						new Tag {TagId = Guid.Parse("4e40f581-720a-4d9f-8be4-8a175a2f5418"), Name = "Česká zbrojovka"},
						new Tag {TagId = Guid.Parse("1335da88-d07b-49ee-828d-32806e631943"), Name = "Czechia"}
					}
				}
			);

		    dbContext.Products.Add(
			    new Product
			    {
				    ProductId = Guid.Parse("86f19a80-0be9-413d-8b6f-ac13859aba85"),
				    Title = "Hekler Koch P30 SK",
				    Tags = new List<Tag>
				    {
					    new Tag {TagId = Guid.Parse("dcc2839c-227c-4f00-819c-3d9dde40ad5e"), Name = "Hekler Koch"},
					    new Tag {TagId = Guid.Parse("75117aa5-abd9-4e47-8050-9aa5ef65665e"), Name = "Germany"}
				    }
			    }
		    );

		    dbContext.SaveChanges();
	    }
    }
}

