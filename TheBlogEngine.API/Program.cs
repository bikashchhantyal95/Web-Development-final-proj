
using Microsoft.EntityFrameworkCore;
using TheBlogEngine.API.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<BlogDbContext>(opt =>
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnetion")));

// builder.Services.AddDbContext<BlogDbContext>(options => options.UseSqlite(builder.Configuration.GetSection("ConnectionStrings:DbConn").Value));

builder.Services.AddScoped<IBlogRepository, BlogRepository>();

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