using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Faellesskabet_Web.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using Microsoft.AspNetCore.DataProtection;
using Faellesskabet_Web.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Rewrite;

namespace Faellesskabet_Web
{
	public class Startup
	{
		private readonly IWebHostEnvironment _environment;

		public Startup(IConfiguration configuration, IWebHostEnvironment environment)
		{
			Configuration = configuration;
			_environment = environment;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			var keysFolder = Path.Combine(_environment.ContentRootPath, "Keys");
			services.AddDataProtection()
				// This helps surviving a server restart
				.PersistKeysToFileSystem(new DirectoryInfo(keysFolder))
				// This helps surviving a site update: each app has its own store, building the site creates a new app
				.SetApplicationName("Faellesskabet")
				.SetDefaultKeyLifetime(TimeSpan.FromDays(90));

			services.AddDatabaseDeveloperPageExceptionFilter();
			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(
					Configuration.GetConnectionString("DefaultConnection")));
			services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
			{
				options.SignIn.RequireConfirmedAccount = true;
			})
				.AddDefaultUI()
				.AddEntityFrameworkStores<ApplicationDbContext>()
				.AddDefaultTokenProviders();
			services.Configure<IdentityOptions>(options =>
			{
				// Default Password settings.
				options.Password.RequireDigit = true;
				options.Password.RequireLowercase = true;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireUppercase = true;
				options.Password.RequiredLength = 6;
				options.Password.RequiredUniqueChars = 0;
			});

			// Make sure roles are applied so users cant access stuff if their role is removed!
			services.Configure<SecurityStampValidatorOptions>(options =>
			{
				options.ValidationInterval = TimeSpan.FromMinutes(1);
			});

			services.ConfigureApplicationCookie(options =>
			{
				options.Cookie.Name = "Faellesskabet2750";
				options.Cookie.MaxAge = TimeSpan.FromDays(14);
				options.ExpireTimeSpan = TimeSpan.FromDays(14);
				options.SlidingExpiration = true;
			});

			services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
			services.AddSingleton<Services.IEmailSender, EmailSender>();

			services.AddRazorPages();
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
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseRewriter(new RewriteOptions().AddRedirectToWwwPermanent());
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapRazorPages();
			});
		}
	}
}
