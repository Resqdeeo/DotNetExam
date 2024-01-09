using DatabaseProj;
using DatabaseProj.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("database")));
builder.Services.AddScoped<IFightLogicService, FightLogicService>();
builder.Services.AddScoped<ITakeRandomMonsterService, TakeRandomMonsterService>();


var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();