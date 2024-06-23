using Microsoft.AspNetCore.Mvc;
using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;
using StockApp.Application.Services;
using StockApp.Domain.Entities;
using StockApp.Domain.Interfaces;
using System;
namespace StockApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

       
        private readonly IProductService _productService;
=======


        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;

        }
       
     



        [HttpPost]
       
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Product product)
        {
            if (product ==null)
            {
                return BadRequest();
            }
            await _productRepository.UpdateAsync(product);
            return NoContent();
        }
    }
}
