﻿using DotnetPgSQL.Application.Contracts.DTOs;
using DotnetPgSQL.Application.Contracts.Services;
using DotnetPgSQL.Domain.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DotnetPgSQL.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductService productService) : ControllerBase
    {
        private readonly IProductService _productService = productService;

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var products = await _productService.GetAllProducts();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var product = await _productService.GetProductById(id);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Product>> AddProduct(ProductDto productDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var createdProduct = await _productService.AddProduct(productDto);
                return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.ProductId }, createdProduct);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, ProductDto productDto)
        {
            try
            {
                var updatedProduct = await _productService.UpdateProduct(id, productDto);

                if (updatedProduct == null)
                    return NotFound($"Product with ID {id} not found.");

                return NoContent(); // 204 No Content for successful update without returning data
            }
            catch (Exception ex)
            {
                // Log exception details here
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                await _productService.DeleteProduct(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                // Log exception details here
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
