using Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.AspNetCore.SpaServices;
using Microsoft.AspNetCore.SpaServices.Extensions;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using System.Net.Sockets;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddBusinessServices(); // Add Business Services

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Database
var connStr = builder.Configuration.GetConnectionString("AppDb");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connStr));

var app = builder.Build();


// Apply migration
using (var serviceScope = app.Services.CreateScope())
{
    var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
    context?.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();    // 1. Redirect HTTP ? HTTPS
app.UseStaticFiles();         // 2. Serve files from wwwroot (css/js/images…)

app.UseRouting();             // 3. Match the URL to an endpoint

app.UseAuthorization();       // 5. Enforce any [Authorize] rules

app.MapControllers();         // 6. Hook up your attribute-routed controllers


// Configure Angular
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute("default", "{controller}/{action=Index}/{id?}");
});


// Use proxy in dev
app.UseSpa(spa =>
{
    spa.Options.SourcePath = "ClientApp";

    if (!app.Environment.IsDevelopment()) return;

    // spa.UseAngularCliServer(npmScript: "start");
    
    try
    {
        new TcpClient("localhost", 4466).Close();
    }
    catch
    {
        Process? process = Process.Start(new ProcessStartInfo("npm.cmd", "start")
        {
            UseShellExecute = true,
            WorkingDirectory = Path.Combine(app.Environment.ContentRootPath, spa.Options.SourcePath)
        });
        Thread.Sleep(1000);
        if (process != null && process.HasExited)
        {
            Console.Write("Failed to start Angluar Cli server. Run using command 'npm run start'");
        }
    }
    spa.UseProxyToSpaDevelopmentServer($"http://localhost:4466");
});


app.Run(); // Start application