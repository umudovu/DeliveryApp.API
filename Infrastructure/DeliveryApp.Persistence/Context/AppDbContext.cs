using DeliveryApp.Domain.Entities;
using DeliveryApp.Domain.Entities.Common;
using DeliveryApp.Domain.Entities.Photo;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Persistence.Context
{
	public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Courier> Couriers { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<DeliveryApp.Domain.Entities.Customer> Customers { get; set; }


		protected override void OnModelCreating(ModelBuilder builder)
        {

            //builder.Entity<CategoryPhoto>()
            //     .HasOne(x => x.Category).WithOne(x => x.Photo).HasForeignKey<CategoryPhoto>(x => x.Id);

            base.OnModelCreating(builder);

        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var datas = ChangeTracker
                 .Entries<BaseEntity>();

            foreach (var data in datas)
            {
                _ = data.State switch
                {
                    EntityState.Added => data.Entity.CreatedDate = DateTime.UtcNow,
                    EntityState.Modified => data.Entity.UpdateDate = DateTime.UtcNow,
                    _ => DateTime.UtcNow
                };
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
