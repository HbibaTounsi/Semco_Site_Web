using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteWeb.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Categories { get; }
        IProductRepository Products { get; }
        IContactMessageRepository ContactMessages { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
