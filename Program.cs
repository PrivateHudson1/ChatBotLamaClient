using ChatBotLlamaClient.Models;
using Microsoft.AspNetCore.ResponseCompression;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();


builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"ApiSettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));

var app = builder.Build();

var apiSettings = app.Configuration.GetSection("ApiSettings").Get<ApiSettings>();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
   
    Console.WriteLine($"Environment: {app.Environment.EnvironmentName}");
    Console.WriteLine($"BaseUrl: {apiSettings?.BaseUrl}");
    Console.WriteLine($"Timeout: {apiSettings?.Timeout}");

    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{

    Console.WriteLine($"Environment: {app.Environment.EnvironmentName}");
    Console.WriteLine($"BaseUrl: {apiSettings?.BaseUrl}");
    Console.WriteLine($"Timeout: {apiSettings?.Timeout}");

    foreach (var config in app.Configuration.AsEnumerable())
    {
        Console.WriteLine($"{config.Key}: {config.Value}");
    }

}




foreach (var config in app.Configuration.AsEnumerable())
{
    Console.WriteLine($"{config.Key}: {config.Value}");
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
