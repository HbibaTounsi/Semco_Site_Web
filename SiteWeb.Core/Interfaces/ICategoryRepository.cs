using SiteWeb.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteWeb.Core.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<Category?> GetBySlugAsync(string slug);
        Task<Category?> GetWithProductsAsync(int id);
        Task<Category?> GetWithProductsBySlugAsync(string slug);
        Task<IReadOnlyList<Category>> GetAllActiveWithProductCountAsync();
    }
}
