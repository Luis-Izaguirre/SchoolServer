//We already have this, very important
using Microsoft.EntityFrameworkCore;
using SchoolServer.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


/*MADE CHANGES TOO => var app = builder.Build(); 04/02/24 */
//ADDING DBCONTEXT SERVICE HERE
builder.Services.AddDbContext<SchooldbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();


/*  LEFT ALONE, DEFAULT SETTINGS */
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Maybe add Cors???

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
