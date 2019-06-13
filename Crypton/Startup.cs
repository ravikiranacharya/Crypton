using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Crypton.Data;
using Crypton.Models;
using Crypton.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Swagger;
using Crypton.DataHelpers.CoinMarketCapHelpers;
using Hangfire;

namespace Crypton
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
            // Requires using Microsoft.AspNetCore.Mvc;
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new RequireHttpsAttribute());
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            
            services.AddIdentity<ApplicationUser, IdentityRole>(config =>
            {
                config.SignIn.RequireConfirmedEmail = true;
                //config.SignIn.RequireConfirmedPhoneNumber = true;
            }).AddEntityFrameworkStores<ApplicationDbContext>()
              .AddDefaultTokenProviders();

            /* This is for signing in with Facebook, Microsoft and Google. To add more services, add App Id and secret keys here */
            services.AddAuthentication().AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"]; // This is read from secrets.json
                facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"]; // This is read from secrets.json
            }).AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = Configuration["Authentication:Google:ClientID"];
                googleOptions.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
            }).AddMicrosoftAccount(microsoftOptions =>
            {
                microsoftOptions.ClientId = Configuration["Authentication:Microsoft:ClientID"];
                microsoftOptions.ClientSecret = Configuration["Authentication:Microsoft:ClientSecret"];
            });


            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<ISmsSender, SmsSender>();

            // This is for sending verification emails. We are using SendGrid's services to send Emails.
            services.Configure<AuthMessageSenderOptions>(sendGridOptions =>
            {
                sendGridOptions.SendGridUser = Configuration["Communication:SendGrid:Username"];
                sendGridOptions.SendGridKey = Configuration["Communication:SendGrid:Key"];
            });

            //This is for sending SMS. We are using Twilio's services to send SMS.
            services.Configure<TwilioSMSSenderOptions>(twilioOptions =>
            {
                twilioOptions.SMSAccountIdentification = Configuration["Communication:Twilio:AccountSID"];
                twilioOptions.SMSAccountPassword = Configuration["Communication:Twilio:AuthToken"];
                twilioOptions.SMSAccountFrom = Configuration["Communication:Twilio:SenderID"];
            });

            //Setting authorization for razor pages. Add all restricted pages here.
            services.AddMvc().AddRazorPagesOptions(options =>
            {
                options.Conventions.AuthorizeFolder("/Account/Manage");
                options.Conventions.AuthorizePage("/Account/Logout");
            });
        
            //This is to prevent bruteforcing of accounts. Account gets locked after 10 failed attempts.
            services.Configure<IdentityOptions>(options =>
            {
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
            });

            //This section is for API documentation.
            //Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Crypton API", Version = "v1" });
            });

            // Add Hangfire services.  
            services.AddHangfire(x => x.UseSqlServerStorage(Configuration.GetConnectionString("DefaultConnection")));

            // Add Elmah
            //services.Configure<ElmahIoOptions>(Configuration.GetSection("ElmahIo"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            /*  HangFire 
                Reference: http://docs.hangfire.io/en/latest/quick-start.html
             */
            app.UseHangfireDashboard();
            app.UseHangfireServer();
            /* Schedule all the jobs here */
            RecurringJob.AddOrUpdate(() => new Schedule(app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope().ServiceProvider.GetService<ApplicationDbContext>()).RefreshDataAsync(), Cron.Minutely);

            ///* Elmah */
            //app.UseElmahIo();


            app.UseDeveloperExceptionPage();
            app.UseBrowserLink();


            app.UseStaticFiles();

            app.UseAuthentication();

            //// Enable middleware to serve generated Swagger as a JSON endpoint.            
            app.UseSwagger(c =>
            {
                c.RouteTemplate = "api-docs/{documentName}/swagger.json";
            });

            if (env.IsProduction())
            {
                app.UseReDoc(c =>
                {
                    c.RoutePrefix = "api-docs";
                    c.SpecUrl = "v1/swagger.json";
                });
            }
            else
            {
                // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
                // specifying the Swagger JSON endpoint.
                app.UseSwaggerUI(c =>
                {
                    c.RoutePrefix = "api-docs";
                    c.DocumentTitle = "Crypton API Documentation";
                    c.SwaggerEndpoint("v1/swagger.json", "Crypton API V1");
                    c.InjectStylesheet("/css/swagger-ui.css");
                    c.InjectJavascript("/js/swagger-ui.js");
                });

            }



            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });


        }
    }
}
