using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Text;

namespace Repository
{
    public class RepositoryContextFactory : IDesignTimeDbContextFactory<RepositoryContext>
    {
        public RepositoryContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<RepositoryContext>();
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Server=musical-instruments-db.cfao2gm6ax16.eu-north-1.rds.amazonaws.com;Port=3306;Database=MusicalInstrumentsDB;User=admin;Password=Kjkszpj11$;SslMode=Required;");
            optionsBuilder.UseMySql(stringBuilder.ToString(), ServerVersion.AutoDetect(stringBuilder.ToString()));

            return new RepositoryContext(optionsBuilder.Options);
        }
    }


}
