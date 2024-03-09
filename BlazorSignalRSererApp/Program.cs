using BetModels.Interfaces;
using BlazorSignalRSererApp.Data;
using BlazorSignalRSererApp.Data.Text;
using BlazorSignalRSererApp.SignalR.Hubs;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
// Gerald: Enable SignalR functionality
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

builder.Services.AddSignalR();
builder.Services.AddMudServices();


builder.Services.AddTransient<UpdateFormService>();
builder.Services.AddTransient<ITextService, TextGenerator>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    
}
app.UseCors("AllowAll");
app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();

app.MapFallbackToPage("/_Host");



app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<FormHub>("/formhub");
   
   

});
app.Run();
