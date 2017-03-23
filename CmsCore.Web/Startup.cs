﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using CmsCore.Data;
using CmsCore.Model.Entities;
using CmsCore.Service;
using CmsCore.Data.Infrastructure;
using CmsCore.Data.Repositories;
using CmsCore.Web.Models;

namespace CmsCore.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                //builder.AddUserSecrets();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            //services.Configure<AppSettings>(mySetting =>
            //{
            //    mySetting.AssetsUrl = "http://assets.bilgikoleji.com/";
            //    mySetting.UploadPath = "C:\\Users\\Admin\\Source\\Repos\\CmsCore\\CmsCore.Assets\\wwwroot\\uploads";
            //});
            services.AddMvc();

            // Add application services.
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // repositories
            services.AddTransient<IPageRepository, PageRepository>();
            services.AddTransient<IMenuRepository, MenuRepository>();
            services.AddTransient<ITemplateRepository, TemplateRepository>();
            services.AddTransient<IMenuLocationRepository, MenuLocationRepository>();
            services.AddTransient<IPostCategoryRepository, PostCategoryRepository>();
            services.AddTransient<IPostRepository, PostRepository>();
            services.AddTransient<IMenuItemRepository, MenuItemRepository>();
            services.AddTransient<IWidgetRepository, WidgetRepository>();
            services.AddTransient<ISectionRepository, SectionRepository>();
            services.AddTransient<ISettingRepository, SettingRepository>();
            services.AddTransient<IRedirectRepository, RedirectRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();
            services.AddTransient<ILinkRepository, LinkRepository>();
            services.AddTransient<ILinkCategoryRepository, LinkCategoryRepository>();
            services.AddTransient<IMediaRepository, MediaRepository>();
            services.AddTransient<ILanguageRepository, LanguageRepository>();
            services.AddTransient<IFormRepository, FormRepository>();
            services.AddTransient<IFormFieldRepository, FormFieldRepository>();
            services.AddTransient<ISliderRepository, SliderRepository>();
            services.AddTransient<ISlideRepository, SlideRepository>();


            // services
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
            services.AddTransient<IPageService, PageService>();
            services.AddTransient<IMenuService, MenuService>();
            services.AddTransient<IMenuLocationService, MenuLocationService>();
            services.AddTransient<ITemplateService, TemplateService>();
            services.AddTransient<IPostService, PostService>();
            services.AddTransient<IPostCategoryService, PostCategoryService>();
            services.AddTransient<IMenuItemService, MenuItemService>();
            services.AddTransient<IWidgetService, WidgetService>();
            services.AddTransient<ISectionService, SectionService>();
            services.AddTransient<ISettingService, SettingService>();
            services.AddTransient<IRedirectService, RedirectService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IProductCategoryService, ProductCategoryService>();
            services.AddTransient<ILinkService, LinkService>();
            services.AddTransient<ILinkCategoryService, LinkCategoryService>();
            services.AddTransient<IMediaService, MediaService>();
            services.AddTransient<ILanguageService, LanguageService>();
            services.AddTransient<IFormService, FormService>();
            services.AddTransient<IFormFieldService, FormFieldService>();
            services.AddTransient<ISliderService, SliderService>();
            services.AddTransient<ISlideService, SlideService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.ApplicationServices.GetRequiredService<ApplicationDbContext>().Seed();
            app.UseIdentity();

            // Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
