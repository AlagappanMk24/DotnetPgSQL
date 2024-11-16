using DotnetPgSQL.Application.Contracts.DTOs;
using DotnetPgSQL.Domain.Models.Entities;

namespace DotnetPgSQL.Application.Contracts.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> GetProductById(int id);
        Task<Product> AddProduct(ProductDto product);
        Task<Product> UpdateProduct(int id, ProductDto productDto);
        Task DeleteProduct(int id);
    }
}
