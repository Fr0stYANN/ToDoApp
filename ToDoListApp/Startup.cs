using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BusinessLogic.Models;
using BusinessLogic.Interfaces;
using ToDoListApp.SQL;
using ToDoListApp.XML;
using GraphQL.Server;
using GraphQL;
using GraphQL.Server.Transports.AspNetCore;
using AutoMapper;
using GraphQL.SystemTextJson;
using ToDoListApp.ViewModels;
using GraphQL.Server.Transports.AspNetCore.SystemTextJson;
using Task = BusinessLogic.Models.Task;
using ToDoListApp.GraphQL;
namespace ToDoListApp
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
            //services.AddScoped<ITaskRepository, TaskRepository>();
            //services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddAutoMapper(typeof(AutoMapperProfile));
            services.AddScoped<ICategoryRepository, XmlCategoryRepository>();
            services.AddScoped<ITaskRepository, XmlTaskRepository>();
            services.AddScoped<TasksSchema>();
            services.AddGraphQL().AddSystemTextJson()
                .AddGraphTypes(typeof(TasksSchema),ServiceLifetime.Scoped);
            services.AddDatabaseDeveloperPageExceptionFilter();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseGraphQL<TasksSchema>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Task}/{action=Index}/{id?}");
            });
        }
    }
}
