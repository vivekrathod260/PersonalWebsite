using Web.Middleware;
using System.Diagnostics;
using System.Net.Sockets;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Add Data & Business services
builder.Services.AddDataServices(builder.Configuration);
builder.Services.AddBusinessServices();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4466", "http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseMiddleware<GlobalExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
//app.UseCors("AllowAngular");
app.UseRouting();
app.MapControllers();

// Configure Angular
app.UseEndpoints(endpoints => {
    endpoints.MapControllerRoute("default", "{controller}/{action=Index}/{id?}");
});

// Use proxy in dev
app.UseSpa(spa =>
{
    if (!app.Environment.IsDevelopment()) return;

    spa.Options.SourcePath = "ClientApp";

    int devServerPort = 4466;
    var envPort = System.Environment.GetEnvironmentVariable("DevServerPort");
    if (!string.IsNullOrEmpty(envPort) && int.TryParse(envPort, out var parsedPort)) devServerPort = parsedPort;

    int maxAttempts = 30;
    var envMax = System.Environment.GetEnvironmentVariable("MaxAttempts");
    if (!string.IsNullOrEmpty(envMax) && int.TryParse(envMax, out var parsedMax))   maxAttempts = parsedMax;

    bool serverAvailable = false;
    int attempt = 0;

    try
    {
        var process = Process.Start(new ProcessStartInfo("npx", $"ng serve --port {devServerPort}")
        {
            UseShellExecute = true,
            WorkingDirectory = Path.Combine(app.Environment.ContentRootPath, spa.Options.SourcePath)
        });

        // wait for the server to appear
        attempt = 0;
        while (attempt < maxAttempts)
        {
            try
            {
                using var tcp = new TcpClient("localhost", devServerPort);
                serverAvailable = true;
                break;
            }
            catch
            {
                Thread.Sleep(1000);
                attempt++;
            }
        }

        if (process != null && process.HasExited)
        {
            Console.WriteLine("Angular CLI process exited prematurely. Start it manually with 'npm run start' in ClientApp.");
        }
    }
    catch (System.ComponentModel.Win32Exception)
    {
        Console.WriteLine("Failed to start 'npm.cmd'. Ensure Node.js/npm are installed and on PATH.");
    }

    if (serverAvailable)
    {
        Console.WriteLine($"Proxying SPA requests to http://localhost:{devServerPort}");
        spa.UseProxyToSpaDevelopmentServer($"http://localhost:{devServerPort}");
    }
    else
    {
        Console.WriteLine($"Dev server not responding on port {devServerPort}. SPA proxy disabled. Start the dev server manually in ClientApp (npm start) or change the port.");
    }
});

app.Run();
