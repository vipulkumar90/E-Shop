using Microsoft.EntityFrameworkCore;
using Product.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Product.DataContext
{
	public class ProductDbContext : DbContext
	{
		public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options) { }

		public DbSet<ProductEntity> Products { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<ProductEntity>().HasKey(r => r.Id);
			modelBuilder.Entity<ProductEntity>().HasData(
				new ProductEntity { Id = 1, Name = "Laptop", Price = 999.99m },
				new ProductEntity { Id = 2, Name = "Smartphone", Price = 799.99m },
				new ProductEntity { Id = 3, Name = "Headphones", Price = 199.99m },
				new ProductEntity { Id = 4, Name = "Smartwatch", Price = 299.99m },
				new ProductEntity { Id = 5, Name = "Tablet", Price = 499.99m },
				new ProductEntity { Id = 6, Name = "Camera", Price = 599.99m },
				new ProductEntity { Id = 7, Name = "Bluetooth Speaker", Price = 149.99m },
				new ProductEntity { Id = 8, Name = "Gaming Console", Price = 399.99m },
				new ProductEntity { Id = 9, Name = "Monitor", Price = 299.99m },
				new ProductEntity { Id = 10, Name = "Keyboard", Price = 99.99m }
			);
		}
	}
}
