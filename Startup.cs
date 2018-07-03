using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using ProductCatalog.Data;
using ProductCatalog.Repositories;
using Swashbuckle.AspNetCore.Swagger;

namespace ProductCatalog
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
           services.AddMvc();

           services.AddResponseCompression();
           services.AddScoped<StoreDataContext, StoreDataContext>(); // Verifica se já existe uma aberta
           services.AddTransient<ProductRepository, ProductRepository>();
           services.AddTransient<CategoryRepository, CategoryRepository>();

           services.AddSwaggerGen( x => {
               x.SwaggerDoc("v1", new Info {Title = "Balta Aula", Version = "v1"});
           });

           //services.AddTransient<StoreDataContext, StoreDataContext>(); Abre sempre uma nova
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
               app.UseDeveloperExceptionPage();

               
    
            app.UseMvc();

            app.UseResponseCompression();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
               // c.SwaggerEndpoint("/swagger/v1/swagger.json", "Balta Aula - V1");
               c.SwaggerEndpoint("/swagger/v1/swagger.json", "Balta Aula - V1");
            });

            


            // app.Run(async (context) =>
            // {
            //     await context.Response.WriteAsync("Hello World!");
            // });
        }
    }
}
