using StockApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Domain.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        Product GetById(int id);
        Product Create(Product product);

        Task<IEnumerable<Product>> GetProducts();

        Task<Product> GetById(int? id);
        Task<Product> Create(Product product);
        Task<Product> Update(Product product);
        Task<Product> Remove(Product product);

        Task<IEnumerable<Product>> GetAllAsync(int pageNumber, int pageSize);
        Task<IEnumerable<Product>> GetLowStockAsync(int threshold);
        Task<IEnumerable<Product>> GetFilteredAsync(string name, decimal? minPrice, decimal? maxPrice);
        Task<IEnumerable<Product>> SearchAsync(string query, string sortBy, bool descending);
        Task<IEnumerable<Product>> GetByIdsAsync(IEnumerable<int> ids);


        Task GetAllAsync(int pageNumber);
        Task GetByIdAsync(int id);
        Task AddAsync(Product product);
        IEnumerable<Product> GetAll();
        Task UpdateAsync(Product product);

        Task Remove(int id);
        public async Task BulkUpdateAsync(List<Product> products)
        {
            if (products == null || !products.Any())
                throw new ArgumentException("Product list cannot be null or empty", nameof(products));

            foreach (var product in products)
            {
                var existingProduct = await _context.Products.FindAsync(product.Id);
                if (existingProduct != null)
                {
                    existingProduct.Name = product.Name;
                    existingProduct.Description = product.Description;
                    existingProduct.Price = product.Price;
                    existingProduct.Stock = product.Stock;
                    existingProduct.Image = product.Image;
                }
            }
        }
    }
}
