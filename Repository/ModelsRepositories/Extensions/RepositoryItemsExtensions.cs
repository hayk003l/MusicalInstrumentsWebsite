using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace Repository.ModelsRepositories.Extensions
{
    public static class RepositoryItemsExtensions
    {
        public static IQueryable<Item> Search(this IQueryable<Item> items, string searchByQueryString)
        {
            if (string.IsNullOrEmpty(searchByQueryString))
            {
                return items;
            }

            var lowerCaseTerm = searchByQueryString.Trim().ToLower();

            return items.Where(c => c.Name.ToLower().Contains(lowerCaseTerm));
        }

        public static IQueryable<Item> Filter(this IQueryable<Item> items, uint minPrice, uint maxPrice) =>
            items.Where(i => (i.Amount >= minPrice && i.Amount <= maxPrice));

    }
}
