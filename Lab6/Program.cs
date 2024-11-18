using Lab6.Data;
using Microsoft.EntityFrameworkCore;

namespace Lab6
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<HospitalManagementDbContext>(options =>
            {
                var dbType = builder.Configuration.GetValue<string>("DatabaseType");
                switch (dbType)
                {
                    case "MSSQL":
                        options.UseSqlServer(builder.Configuration.GetConnectionString("MSSQLConnection"));
                        break;
                    case "Postgres":
                        options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection"));
                        break;
                    case "SqlLite":
                        options.UseSqlite(builder.Configuration.GetConnectionString("SqlLiteConnection"));
                        break;
                    case "InMemory":
                        options.UseInMemoryDatabase("InMemoryDb");
                        break;
                }
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                try
                {
                    var services = scope.ServiceProvider;
                    var context = services.GetRequiredService<HospitalManagementDbContext>();
                    context.Database.Migrate();
                    HospitalManagementDbContext.Seed(context);
                }
                catch (Exception ex)
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }

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
        }
    }
}