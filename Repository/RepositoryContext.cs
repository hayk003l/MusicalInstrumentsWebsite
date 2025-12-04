using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Seed;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Repository
{
    public partial class RepositoryContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options) { }
        public RepositoryContext() { }

        public DbSet<Item> Items { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Description> Descriptions { get; set; }
        public DbSet<ShippingDetails> ShippingDetails { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>(entity =>
            {
                entity.Property(n => n.Name)
                .HasColumnType("nvarchar(200)")
                .HasMaxLength(200)
                .IsRequired(true);

                entity.Property(n => n.Amount)
                .HasColumnType("decimal(7,1)")
                .IsRequired(true);

            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasOne(d => d.ItemNavigation)
                .WithMany(p => p.Orders)
                .HasForeignKey(d => d.ItemId)
                .HasConstraintName("FK_Orders_Items");

                entity.HasOne(u => u.User)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);


            });

            modelBuilder.Entity<Description>(entity =>
            {
                entity.HasOne(d => d.ItemNavigation)
                .WithOne(p => p.Description)
                .HasForeignKey<Description>(d => d.ItemId)
                .HasConstraintName("FK_Descriptions_Items");

                entity.Property(p => p.Country)
                .HasColumnType("nvarchar(60)")
                .HasMaxLength(60)
                .IsRequired(true);

                entity.Property(p => p.DescriptionText)
                .HasColumnType("nvarchar(max)")
                .HasMaxLength(1000);
            });

            modelBuilder.Entity<ShippingDetails>(entity =>
            {
                entity.HasOne(n => n.OrderNavigation)
                .WithOne(m => m.ShippingDetails)
                .HasForeignKey<ShippingDetails>(d => d.OrderId)
                .HasConstraintName("FK_ShippingDetails_Orders");

                entity.Property(p => p.Status)
                .HasConversion<string>();
            });
            base.OnModelCreating(modelBuilder);

            ItemSeed.SeedItems(modelBuilder);
            DescriptionSeed.SeedDescriptions(modelBuilder);
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
        }


    }
}
