using Customer.Models;
using Microsoft.EntityFrameworkCore;

namespace Customer.DataContext
{
	public class CustomerDbContext : DbContext
	{
		public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options) { }

		public DbSet<CustomerEntity> Customers { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<CustomerEntity>().HasKey(r => r.Id);
			modelBuilder.Entity<CustomerEntity>().HasData(
				new CustomerEntity { Id = 1, Name = "Alice" },
				new CustomerEntity { Id = 2, Name = "Bob" },
				new CustomerEntity { Id = 3, Name = "Charlie" },
				new CustomerEntity { Id = 4, Name = "Diana" },
				new CustomerEntity { Id = 5, Name = "Ethan" }

			);
		}
	}
}
