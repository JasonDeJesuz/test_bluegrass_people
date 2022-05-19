using System;
using Bluegrass.Core.Services.ChangeService;
using Bluegrass.Core.Services.PersonService;
using Bluegrass.Core.Services.UserAuthenticationService;
using Bluegrass.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// Add the database services
builder.Services.RegisterDataServices(builder.Configuration);
// Add the database identity services
builder.Services.AddAuthentication();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJWT(builder.Configuration);
// Add the Service for the Data
builder.Services.AddTransient<IPersonService, PersonService>();
builder.Services.AddTransient<IChangeService, ChangeService>();
builder.Services.AddTransient<IUserAuthenticationService, UserAuthenticationService>();
// Add Automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");
app.Run();