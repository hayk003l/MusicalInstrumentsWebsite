using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository.Seed
{
    public static class ItemSeed
    {
        public static void SeedItems(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>().HasData(
                new Item
                {
                    Id = new Guid("bec5f8db-f705-4bca-8673-9f6a8c135c1e"),
                    Name = "Acoustic Guitar",
                    Amount = 45000
                },
                new Item
                {
                    Id = new Guid("fc05fb85-62b4-4ffa-8e5c-c001bc7fc876"),
                    Name = "Piano",
                    Amount = 456000
                },
                new Item
                {
                    Id = new Guid("c41c3a08-a43d-464d-a490-c1ae3c4baf20"),
                    Name = "Saxophone",
                    Amount = 800000
                }
            );
        }
    }
}
