using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _context;
        public ProductRepository(ICatalogContext context)
        {
            this._context = context;
        }

        public async Task<bool> Delete(string id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(x => x.Id, id);

            DeleteResult deleteResult = await _context.Products.DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _context.Products.Find(x => true).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetByCategory(string category)
        {
            return await _context.Products.Find(x => x.Category.Equals(category)).ToListAsync();
        }

        public async Task<Product> GetById(string id)
        {
            return await _context.Products.Find(x => x.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetByName(string name)
        {
            return await _context.Products.Find(x => x.Name.Equals(name)).ToListAsync();
        }

        public async Task Save(Product product)
        {
            await _context.Products.InsertOneAsync(product);
        }

        public async Task<bool> Update(Product product)
        {
            var updateResult = await _context.Products.ReplaceOneAsync(filter: x => x.Id.Equals(product.Id), replacement: product);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }
    }
}
