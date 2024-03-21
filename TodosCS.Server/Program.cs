
using Microsoft.EntityFrameworkCore;
using TodosCS.Server.Data;
using TodosCS.Server.Data.Repositories;

namespace TodosCS.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(opts =>
            {
                opts.AddPolicy("AllowMyAngularApp",
                    builder => builder.WithOrigins("https://localhost:4200")
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            builder.Services.AddDbContext<TodoContext>(opts =>
                opts.UseNpgsql(builder.Configuration.GetConnectionString("TodoDatabase")));

            builder.Services.AddScoped<ITodoRepository, DatabaseTodoRepository>();

            var app = builder.Build();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowMyAngularApp");
            app.UseAuthorization();


            app.MapControllers();

            app.MapFallbackToFile("/index.html");

            app.Run();
        }
    }
}
