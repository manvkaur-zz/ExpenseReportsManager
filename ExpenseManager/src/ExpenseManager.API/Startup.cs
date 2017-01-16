using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ExpenseManager.API.Entities;
using Microsoft.EntityFrameworkCore;
using ExpenseManager.API.Services;

namespace ExpenseManager.API
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddCors(o => o.AddPolicy("ExpenseManagerCORS", builder =>
            {
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            }));
            var connectionString = @"Server=(localdb)\mssqllocaldb;Database=ExpenseDB;Trusted_Connection=True";
            services.AddDbContext<ExpenseDbContext>(o=>o.UseSqlServer(connectionString));

            services.AddScoped<IExpenseRepository, ExpenseRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory,
            ExpenseDbContext expenseDbContext)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }

            expenseDbContext.EnsureSeedDataForContext();
            app.UseStatusCodePages();

            AutoMapper.Mapper.Initialize(cfg => 
            {
                cfg.CreateMap<Entities.ExpenseReport, Models.ExpenseReportWithoutTransactionDto>();
                cfg.CreateMap<Entities.ExpenseReport, Models.ExpenseReportDto>();
                cfg.CreateMap<Entities.Transaction, Models.TransactionDto>();
                cfg.CreateMap<Models.TransactionForCreationDto, Entities.Transaction>();
                cfg.CreateMap<Models.ExpenseReportForCreationDto, Entities.ExpenseReport>();

            });

            app.UseCors("ExpenseManagerCORS");
            app.UseMvc();
            

        }
    }
}
