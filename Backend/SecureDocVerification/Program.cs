using Microsoft.EntityFrameworkCore;
using SecureDocVerification.Data;
using SecureDocVerification.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAll",
//        builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
//});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader());

});


builder.Services.AddScoped<IDocumentService, DocumentService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAngularApp");
app.UseAuthorization();

app.MapControllers();

app.Run();
