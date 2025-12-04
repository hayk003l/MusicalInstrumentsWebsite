using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository.Seed
{
    public static class DescriptionSeed
    {
        public static void SeedDescriptions(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Description>().HasData(
                new Description
                {
                    Id = new Guid("58c1006e-3a28-4ebb-85cf-d872e653773d"),
                    DescriptionText = "Classic acoustic guitar with great sound",
                    Country = "Spain",
                    ItemId = new Guid("bec5f8db-f705-4bca-8673-9f6a8c135c1e")
                },
                new Description
                {
                    Id = new Guid("c347e50b-205b-438c-b2de-ce8d78b6fcad"),
                    DescriptionText = "Piano with great sound",
                    Country = "Italy",
                    ItemId = new Guid("fc05fb85-62b4-4ffa-8e5c-c001bc7fc876")
                },
                new Description
                {
                    Id = new Guid("3d1bd386-df80-4e6c-835d-45986f5ab5a5"),
                    DescriptionText = "Saxophone with great sound",
                    Country = "America",
                    ItemId = new Guid("c41c3a08-a43d-464d-a490-c1ae3c4baf20")
                }
            );
        }
    }
}
