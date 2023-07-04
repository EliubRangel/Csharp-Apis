using Agencia_Taxis.Controllers;
using Microsoft.EntityFrameworkCore;
using Agencia_Taxis;
var builder = WebApplication.CreateBuilder(args);
var constr = "server=localhost;database=AgenciaTaxis;uid=root;pwd=pwd123;port=3306;";
            
builder.Services
    .AddDbContext<Agencia_Taxis.AgenciaDbContext>(options => 
        options.UseMySql(constr, ServerVersion.AutoDetect(constr)));


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
