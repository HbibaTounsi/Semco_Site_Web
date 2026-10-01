using SiteWeb.Core.Interfaces;
using SiteWeb.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteWeb.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private ICategoryRepository? _categories;
        private IProductRepository? _products;
        private IContactMessageRepository? _contactMessages;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public ICategoryRepository Categories =>
            _categories ??= new CategoryRepository(_context);

        public IProductRepository Products =>
            _products ??= new ProductRepository(_context);

        public IContactMessageRepository ContactMessages =>
            _contactMessages ??= new ContactMessageRepository(_context);

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }

}
