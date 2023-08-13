﻿using System;
using Ardalis.ListStartupServices;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using FastEndpoints;
using FastEndpoints.ApiExplorer;
using FastEndpoints.Swagger.Swashbuckle;
using InteractiveGallery.Core;
using InteractiveGallery.Infrastructure;
using InteractiveGallery.Infrastructure.Data;
using InteractiveGallery.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Serilog;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.StaticFiles;

var builder = WebApplication.CreateBuilder(args);


builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.UseSerilog((_, config) => config.ReadFrom.Configuration(builder.Configuration));

builder.Services.Configure<CookiePolicyOptions>(options =>
{
  options.CheckConsentNeeded = context => true;
  options.MinimumSameSitePolicy = SameSiteMode.None;
});

string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");



builder.Services.AddDbContext(connectionString!);


//builder.Services.AddIdentity<IdentityUser, IdentityRole>(options=>options.SignIn.RequireConfirmedAccount=false).AddRoles<IdentityRole>().AddEntityFrameworkStores<InteractiveGalleryDbContext>().AddDefaultUI();
//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false).AddRoles<IdentityRole>().AddEntityFrameworkStores<InteractiveGalleryDbContext>().AddDefaultUI();
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
           .AddDefaultUI().AddSignInManager<SignInManager<ApplicationUser>>()
           .AddEntityFrameworkStores<InteractiveGalleryDbContext>()
                           .AddDefaultTokenProviders();

builder.Services.AddMvc(options => options.EnableEndpointRouting = false);


builder.Services.AddControllersWithViews().AddNewtonsoftJson();
builder.Services.AddRazorPages();
builder.Services.AddFastEndpoints();
builder.Services.AddFastEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
  c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
  c.EnableAnnotations();
  c.OperationFilter<FastEndpointsOperationFilter>();
});

// add list services for diagnostic purposes - see https://github.com/ardalis/AspNetCoreStartupServices
builder.Services.Configure<ServiceConfig>(config =>
{
  config.Services = new List<ServiceDescriptor>(builder.Services);

  // optional - default path to view services is /listallservices - recommended to choose your own path
  config.Path = "/listservices";
});


builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
  containerBuilder.RegisterModule(new DefaultCoreModule());
  containerBuilder.RegisterModule(new DefaultInfrastructureModule(builder.Environment.EnvironmentName == "Development"));
});

//builder.Logging.AddAzureWebAppDiagnostics(); add this if deploying to Azure

var app = builder.Build();
app.UseAuthentication();
if (app.Environment.IsDevelopment())
{
  app.UseDeveloperExceptionPage();
  app.UseShowAllServicesMiddleware();
}
else
{
  app.UseExceptionHandler("/Home/Error");
  app.UseHsts();
}

StaticFileOptions options = new StaticFileOptions { ContentTypeProvider = new FileExtensionContentTypeProvider() };
options.ServeUnknownFileTypes = true;
app.UseStaticFiles(options);

builder.Services.AddDirectoryBrowser();




app.UseRouting();
app.UseAuthorization();
app.UseAuthentication();
app.UseFastEndpoints();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCookiePolicy();

// Enable middleware to serve generated Swagger as a JSON endpoint.
app.UseSwagger();

// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));



    // Custom route for AdminArea
app.MapAreaControllerRoute(
        name: "admin_area",
        areaName: "AdminArea",
        pattern: "AdminArea/{controller=Home}/{action=Index}/{id?}"
    );


// Custom route for UserArea
app.MapAreaControllerRoute(
        name: "user_area",
        areaName: "UserArea",
        pattern: "UserArea/{controller=Home}/{action=Index}/{id?}"
 );

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}"
    );




//app.MapDefaultControllerRoute();
app.MapRazorPages();

// Seed Database
using (var scope = app.Services.CreateScope())
{
  var services = scope.ServiceProvider;

  try
  {
    var context = services.GetRequiredService<InteractiveGalleryDbContext>();
    //                    context.Database.Migrate();
    context.Database.EnsureCreated();
      await SeedData.Initialize(services);
  }
  catch (Exception ex)
  {
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred seeding the DB. {exceptionMessage}", ex.Message);
  }
}

app.Run();

// Make the implicit Program.cs class public, so integration tests can reference the correct assembly for host building
public partial class Program
{
}
