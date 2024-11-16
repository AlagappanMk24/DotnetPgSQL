using AutoMapper;
using DotnetPgSQL.Application.Contracts.DTOs;
using DotnetPgSQL.Application.Contracts.Persistence;
using DotnetPgSQL.Application.Contracts.Services;
using DotnetPgSQL.Domain.Models.Entities;

namespace DotnetPgSQL.Infrastructure.Services
{
    public class ProductService(IUnitOfWork unitOfWork, IMapper mapper) : IProductService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            var products = await _unitOfWork.Products.GetAllProducts();
            return products;
        }
        public async Task<Product> GetProductById(int id)
        {
            var product = await _unitOfWork.Products.GetProductById(id);
            return product;
        }

        public async Task<Product> AddProduct(ProductDto productDto)
        {
            ArgumentNullException.ThrowIfNull(productDto);

            // Map the DTO to the entity using AutoMapper
            var product = _mapper.Map<Product>(productDto);

            // Add the mapped order to the database via the Unit of Work pattern
            var createdProduct = await _unitOfWork.Products.AddProduct(product);

            await _unitOfWork.Save();

            return createdProduct;
        }

        public async Task<Product> UpdateProduct(int id, ProductDto productDto)
        {
            // Fetch the existing product from the repository
            var existingProduct = await _unitOfWork.Products.GetProductById(id);

            if (existingProduct == null)
            {
                return null;
            }

            // Map the ProductDto to the existing product
            _mapper.Map(productDto, existingProduct); // Only updates properties, does not overwrite object

            // Update the product in the repository
            await _unitOfWork.Products.UpdateProduct(existingProduct);

            // Save changes through UnitOfWork
            await _unitOfWork.Save(); ;

            return existingProduct;
        }

        public async Task DeleteProduct(int id)
        {
            // Perform the delete operation
            await _unitOfWork.Products.DeleteProduct(id);

            // Save changes through UnitOfWork
            await _unitOfWork.Save();
        }
    }
}
