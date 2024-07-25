
using Customer.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Tracing;

namespace Customer
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddDbContext<CustomerDbContext>(options =>
				options.UseInMemoryDatabase("CustomerDb"));
			var options = new DbContextOptionsBuilder<CustomerDbContext>()
			.UseInMemoryDatabase("CustomerDb").Options;
			using (var context = new CustomerDbContext(options))
			{
				context.Database.EnsureCreated();
			}
			builder.Services.AddHttpClient();
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
