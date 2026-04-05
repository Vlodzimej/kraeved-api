using System.Text;
using AutoMapper;
using KraevedAPI.ClassObjects;
using KraevedAPI.Core;
using KraevedAPI.DAL;
using KraevedAPI.Helpers;
using KraevedAPI.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

//CultureInfo.CurrentCulture = new CultureInfo("ru-RU", false);

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDev", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
    options.AddPolicy("AllowAngularProd", policy =>
    {
        policy.WithOrigins("http://localhost:80")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

builder.Services.AddControllers(options =>
{
    options.Filters.Add<HttpResponseExceptionFilter>();
});

builder.Services.Configure<Microsoft.AspNetCore.Http.Features.FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 104857600; // 100MB
    options.ValueLengthLimit = 104857600;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IKraevedService, KraevedService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<DbContext, KraevedContext>();

builder.Services.AddDbContext<KraevedContext>(
    options => options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

builder.Services.AddHttpContextAccessor();

var secretKey = builder.Configuration["Kraeved:Secret"] ?? "";
var key = Encoding.ASCII.GetBytes(secretKey);
builder.Services.AddAuthentication(opt =>
{
	opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
 	x.RequireHttpsMetadata = false;
 	x.SaveToken = true;
 	x.Events = new Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerEvents
 	{
 		OnMessageReceived = context =>
 		{
 			context.Token = context.Request.Cookies["auth_token"];
 			return Task.CompletedTask;
 		}
 	};
 	x.TokenValidationParameters = new TokenValidationParameters
 	{
 		ValidateIssuerSigningKey = true,
 		IssuerSigningKey = new SymmetricSecurityKey(key),
 		ValidateIssuer = false,
 		ValidateAudience = false
 	};
});

builder.Services.AddHttpClient();

// Auto Mapper Configurations
var mappingConfig = new MapperConfiguration(cfg =>
{
    cfg.AddProfile<AutoMapperProfile>();
});

IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<KraevedContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseCors(app.Environment.IsDevelopment() ? "AllowAngularDev" : "AllowAngularProd");

app.UseAuthorization();

app.MapControllers();

app.UseWhen(context => !context.Request.Path.StartsWithSegments("/api/Images"), appBuilder =>
{
    appBuilder.UseMiddleware<ResponseWrapperMiddleware>();
});

app.Run();
