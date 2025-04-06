using GameNightScoresRight.Accessors;
using GameNightScoresRight.Managers;
using GameNightScoresRight.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using GameNightScoresRight.MappingProfiles;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(ControllerCommonMappingProfile), typeof(CommonEFMappingProfile));
builder.Services.AddControllers();

builder.Services.AddDbContext<GameNightDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("GameNightDatabase")));

builder.Services.AddScoped<IAccountManager, AccountManager>();
builder.Services.AddScoped<IAccountAccessor, AccountAccessor>();
builder.Services.AddScoped<IUserManager, UserManager>();
builder.Services.AddScoped<IUserAccessor, UserAccessor>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
    mapper.ConfigurationProvider.AssertConfigurationIsValid();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
