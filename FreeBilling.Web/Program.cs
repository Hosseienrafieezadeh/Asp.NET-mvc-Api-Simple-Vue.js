
using FreeBilling.Web.Data;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using FreeBilling.Web.Apis;
using FreeBilling.Web.Data.Entitis;
using FreeBilling.Web.Validators;
using FluentValidation;
using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Configuration.AddJsonFile("appsettings.json");
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<BillingContext>(
    options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<TimeBillUsers>(
    options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.Password.RequiredLength = 8;
    })
    .AddEntityFrameworkStores<BillingContext>();
builder.Services.AddAuthentication()
    .AddJwtBearer(cfg =>
    {
        cfg.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidIssuer = builder.Configuration["Token:Issuer"],
            ValidAudience = builder.Configuration["Token:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:Key"]))
        };
    });
builder.Services.AddAuthorization(cfg =>
{
    cfg.AddPolicy("ApiPolicy", bldr =>
    {
        bldr.RequireAuthenticatedUser();
        bldr.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
    });
});
builder.Services.AddDbContext<BillingContext>();
builder.Services.AddScoped<IBillingRepozitory, BillingRepozitory>();

//IConfigurationBuilder configBuilder = builder.Configuration;
//configBuilder.Sources.Clear();
//configBuilder.AddJsonFile("appsettings.json")
//    .AddJsonFile("appsettings.Development.json", true)
//    .AddUserSecrets(Assembly.GetExecutingAssembly())
//    .AddEnvironmentVariables()
//    .AddCommandLine(args);

builder.Services.AddControllers();
builder.Services.AddValidatorsFromAssemblyContaining<TimeBillModelValidator>();
TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetEntryAssembly()!);
var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseDefaultFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();


app.MapRazorPages();

TimeBillsApi.Register(app);
AuthApi.Register(app);
EmployeesApi.Register(app);
app.MapFallbackToPage("/customersBilling");
app.MapControllers();

app.Run();
