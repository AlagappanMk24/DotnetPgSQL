using DotnetPgSQL.Domain.Models.Entities;

namespace DotnetPgSQL.Application.Contracts.Persistence
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> GetProductById(int id);
        Task<Product> AddProduct(Product product);
        Task UpdateProduct(Product product);
        Task DeleteProduct(int id);
    }
}
