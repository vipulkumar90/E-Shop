using Microsoft.EntityFrameworkCore;
using Product.DataContext;
using Tracing;

namespace Product
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddDbContext<ProductDbContext>(options =>
				options.UseInMemoryDatabase("ProductDb"));
			var options = new DbContextOptionsBuilder<ProductDbContext>()
			.UseInMemoryDatabase("ProductDb").Options;
			using (var context = new ProductDbContext(options))
			{
				context.Database.EnsureCreated();
			}
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.AddOpenTelemetryTracing(builder.Configuration);

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			app.UseSwagger();
			app.UseSwaggerUI();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
