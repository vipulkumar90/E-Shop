using Microsoft.EntityFrameworkCore;
using Order.Models;

namespace Order.DataContext
{
	public class OrderDbContext : DbContext
	{
		public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options) { }

		public DbSet<OrderEntity> Orders { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<OrderEntity>().HasKey(r => r.Id);
			modelBuilder.Entity<OrderEntity>().HasData(
				new OrderEntity { Id = 1, CustomerId = 1, ProductId = 2},
				new OrderEntity { Id = 2, CustomerId = 2, ProductId = 2 },
				new OrderEntity { Id = 3, CustomerId = 2, ProductId = 5 },
				new OrderEntity { Id = 4, CustomerId = 2, ProductId = 8 },
				new OrderEntity { Id = 5, CustomerId = 2, ProductId = 3 },
				new OrderEntity { Id = 6, CustomerId = 3, ProductId = 1 },
				new OrderEntity { Id = 7, CustomerId = 3, ProductId = 4 },
				new OrderEntity { Id = 8, CustomerId = 3, ProductId = 7 },
				new OrderEntity { Id = 9, CustomerId = 3, ProductId = 8 }
			);
		}
	}

}
