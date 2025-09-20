using ChatBotLlamaClient.Models;
using Microsoft.AspNetCore.ResponseCompression;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddRazorPages();


builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"ApiSettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiConnect"));



var app = builder.Build();


var apiSettings = app.Configuration.GetSection("ApiSettings").Get<ApiSettings>();


if (!app.Environment.IsDevelopment())
{
   
    Console.WriteLine($"Environment: {app.Environment.EnvironmentName}");
    Console.WriteLine($"BaseUrl: {apiSettings?.BaseUrl}");
    Console.WriteLine($"Timeout: {apiSettings?.Timeout}");

    app.UseExceptionHandler("/Error");
    app.UseResponseCompression();
    app.UseHsts();

}
else
{
    app.Use(async (context, next) =>
    {
        context.Response.Headers.Remove("Content-Security-Policy");
        await next();
    });

    Console.WriteLine($"Environment: {app.Environment.EnvironmentName}");
    Console.WriteLine($"BaseUrl: {apiSettings?.BaseUrl}");
    Console.WriteLine($"Timeout: {apiSettings?.Timeout}");


    app.UseDeveloperExceptionPage(); 

    //foreach (var config in app.Configuration.AsEnumerable())
    //{
    //    Console.WriteLine($"{config.Key}: {config.Value}");
    //}

}




foreach (var config in app.Configuration.AsEnumerable())
{
    Console.WriteLine($"{config.Key}: {config.Value}");
}

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
