using Azure.Security.KeyVault.Secrets;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using TravelHubAgency.Data;
using TravelHubAgency.Helpers;
using TravelHubAgency.Repositories;
using TravelHubAgency.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<HelperUploadFiles>();
builder.Services.AddTransient<HelperPathProvider>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddAuthentication(options =>
{

    options.DefaultAuthenticateScheme =
CookieAuthenticationDefaults.AuthenticationScheme;

    options.DefaultSignInScheme =
    CookieAuthenticationDefaults.AuthenticationScheme;

    options.DefaultChallengeScheme =
    CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
    config =>
    {
        config.AccessDeniedPath = "/Managed/ErrorAcceso";
    });


builder.Services.AddHttpContextAccessor();

// incluimos las politica para el acceso a determinados roles
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Administrador",
        policy => policy.RequireClaim("Administrador"));

});

builder.Services.AddAzureClients(factory =>
{
    factory.AddSecretClient
    (builder.Configuration.GetSection("KeyVault"));
});

SecretClient secretClient = builder.Services.BuildServiceProvider().GetService<SecretClient>();
KeyVaultSecret secret = await secretClient.GetSecretAsync("StorageAccount");
string azureKeys = secret.Value;

BlobServiceClient blobServiceClient = new BlobServiceClient(azureKeys);
builder.Services.AddTransient<BlobServiceClient>(x => blobServiceClient);
builder.Services.AddTransient<ServiceStorageBlobs>();


// Add services to the container.
builder.Services.AddControllersWithViews(options =>
    options.EnableEndpointRouting = false)
    .AddSessionStateTempDataProvider();

builder.Services.AddTransient<TravelhubServices>();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});

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
app.UseSession();

app.UseMvc(routes =>
{
    routes.MapRoute(
        name: "default",
        template: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
