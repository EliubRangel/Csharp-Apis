using System.Text.Json.Serialization;
using Agencia_Taxis.DbContexts;
using Agencia_Taxis.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles); ;
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var constr = "server=localhost;database=AgenciaTaxis;uid=root;pwd=pwd123;port=3306;";

builder.Services
    .AddDbContext<AgenciaDbContext>(options =>
        options.UseMySql(constr, ServerVersion.AutoDetect(constr)));


//Agregar servicios a contenedor de dependencias
builder.Services.AddScoped<ChoferServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

