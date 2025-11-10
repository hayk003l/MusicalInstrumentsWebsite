using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryContextFactory : IDesignTimeDbContextFactory<RepositoryContext>
    {
        public RepositoryContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<RepositoryContext>();
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Server=localhost;Port=3306;Database=musicalinstrumentsstore;User Id=root;Password=123456;");
            optionsBuilder.UseMySql(stringBuilder.ToString(), ServerVersion.AutoDetect(stringBuilder.ToString()));

            return new RepositoryContext(optionsBuilder.Options);
        }
    }


}
