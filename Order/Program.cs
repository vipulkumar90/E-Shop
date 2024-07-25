using Microsoft.EntityFrameworkCore;
using Order.Controllers;
using Order.DataContext;
using Tracing;

namespace Order
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddDbContext<OrderDbContext>(options =>
				options.UseInMemoryDatabase("OrderDb"));
			var options = new DbContextOptionsBuilder<OrderDbContext>()
			.UseInMemoryDatabase("OrderDb").Options;
			using (var context = new OrderDbContext(options))
			{
				context.Database.EnsureCreated();
			}
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.AddOpenTelemetryTracing(builder.Configuration);

			var app = builder.Build();

			app.UseSwagger();
			app.UseSwaggerUI();

			app.UseRouting();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
			app.UseHttpsRedirection();


			app.Run();
		}
	}
}
