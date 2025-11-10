using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IDescriptionRepository
    {
        Task<Description> GetDescriptionForItem(Guid itemId, bool trackingChanges);
        void CreateDescriptionForItem(Guid itemId, Description description);
    }
}
