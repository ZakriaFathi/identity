

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Server;
using Server.Data;
using Server.Model;
using Server.Services;
using Server.Validator;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>((serviceProvider, dbContextOptionsBuilder) =>
{
    dbContextOptionsBuilder.UseSqlServer(serviceProvider.GetRequiredService<IConfiguration>().GetConnectionString("IdentityConnection"),
        SqlServerOptionsAction);
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<ApplicationDbContext>();


builder.Services.AddIdentityServer()
    .AddAspNetIdentity<ApplicationUser>()
    .AddInMemoryApiScopes(Config.ApiScopes)
    .AddInMemoryClients(Config.Clients)
    .AddInMemoryApiResources(Config.ApiResources)
    .AddInMemoryIdentityResources(Config.IdentityResources)
    .AddExtensionGrantValidator<UserCredentialsValidator>()
    .AddCustomTokenRequestValidator<RegistrationResponse>()
    //.AddResourceOwnerValidator<ResourceOwnerPasswordValidator>()
    .AddProfileService<ApplicationProfileService>()
    .AddDeveloperSigningCredential();

builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseAuthorization();
app.UseRouting();
app.UseAuthentication();
app.UseIdentityServer();
app.UseHttpsRedirection();

app.MapControllers();

app.UseEndpoints(endpoints =>
{

    endpoints.MapDefaultControllerRoute();
});
app.Run();

void SqlServerOptionsAction(SqlServerDbContextOptionsBuilder options)
{
    options.MigrationsAssembly(typeof(Program).GetTypeInfo().Assembly.GetName().Name);
}
