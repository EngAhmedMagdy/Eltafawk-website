using AspNetCore.Identity.Mongo;
using AspNetCore.Identity.Mongo.Model;
using EltafawkPlatform.Components;
using EltafawkPlatform.Services;
using EltafawkPlatform.Settings;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using EltafawkPlatform.Models;
using Microsoft.AspNetCore.Identity;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();



builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDbSettings"));

var mongoSettings = builder.Configuration.GetSection("MongoDbSettings").Get<MongoDbSettings>();


builder.Services.AddIdentityMongoDbProvider<ApplicationUser, MongoRole>(identityOptions =>
{
    identityOptions.Password.RequiredLength = 6;
    identityOptions.Password.RequireDigit = false;
    identityOptions.Password.RequireUppercase = false;
}, mongoOptions =>
{
    mongoOptions.UsersCollection = "Users";
    mongoOptions.RolesCollection = "Roles";
    mongoOptions.ConnectionString = mongoSettings.ConnectionString;
});

// تسجيل MongoClient ليكون متاح في DI
builder.Services.AddSingleton<IMongoClient>(serviceProvider =>
{
    var settings = serviceProvider.GetRequiredService<
        Microsoft.Extensions.Options.IOptions<MongoDbSettings>>().Value;

    return new MongoClient(settings.ConnectionString);
});

// تسجيل قاعدة البيانات (اختياري)
builder.Services.AddScoped(serviceProvider =>
{
    var settings = serviceProvider.GetRequiredService<
        Microsoft.Extensions.Options.IOptions<MongoDbSettings>>().Value;

    var client = serviceProvider.GetRequiredService<IMongoClient>();

    return client.GetDatabase(settings.DatabaseName);
});

builder.Services.AddHttpClient("ServerAPI", client =>
{
    client.BaseAddress = new Uri("https://localhost:44361/");
});


builder.Services.AddScoped(sp =>
    sp.GetRequiredService<IHttpClientFactory>().CreateClient("ServerAPI"));

builder.Services.AddScoped<StudentService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<CourseService>();
builder.Services.AddScoped<InboxService>();
builder.Services.AddScoped<UserService>();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapControllers();
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
