using Delivery.sql.Table;
using Microsoft.EntityFrameworkCore;

namespace Delivery.sql
{
	public class OrderContext : DbContext
	{
		public virtual DbSet<Order> Order => Set<Order>();
		public virtual DbSet<Table.Region> Region => Set<Table.Region>();

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite(@$"Data Source=Delivery.db");
		}
	}
}
