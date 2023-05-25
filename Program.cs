using Csharp_Apis.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Configurar el DbContext para que este accesible en nuestros controladores
var constr = "server=localhost;database=CSHARP-APIS;uid=root;pwd=pwd123;port=3306;";
            
builder.Services
    .AddDbContext<FirstDbContext>(options => 
        options.UseMySql(constr, ServerVersion.AutoDetect(constr)));

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
