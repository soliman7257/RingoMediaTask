using Microsoft.EntityFrameworkCore;
using RingoMediaTask.Data;
using RingoMediaTask.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(cnf => cnf.UseSqlServer(builder.Configuration["ConnectionStrings:DefualtConnection"]));
builder.Services.AddScoped<DepartmentService>();
builder.Services.AddScoped<ReminderService>();
builder.Services.AddScoped<EmailSenderService>();
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
