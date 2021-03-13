using DZ_11_03_21.models;
using Microsoft.EntityFrameworkCore;

namespace DZ_11_03_21.context
{
	class dbContext : DbContext
	{
		public dbContext(DbContextOptions options) : base(options)
		{
		}

		public dbContext()
		{
		}

		public DbSet<Customer> Customers { get; set; }
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Data source=10.211.55.3; Initial catalog=AlifAcademy; user id=shaha; password=1234");
		}
	}
}