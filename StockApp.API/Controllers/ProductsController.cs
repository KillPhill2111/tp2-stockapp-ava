﻿using Microsoft.AspNetCore.Mvc;
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

        private readonly IReviewService _reviewService;
        private readonly IReviewRepository _reviewRepository;
        private readonly IProductService _productService;
        

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;

        }
       
     

         [HttpPost("{productId}/review")]
        public async Task<IActionResult> AddReview(int productId, [FromBody] Review review)
        {
            try
            {
                if (review.Rating < 1 || review.Rating > 5)
                {
                    return BadRequest("A nota deve estar entre 1 e 5.");
                }

                review.ProductId = productId;
                review.Date = DateTime.Now;

                await _reviewRepository.AddAsync(review);

                return Ok(review);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao adicionar avaliação: " + ex.Message);
            }

        }




        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpGet("All")]
        [ResponseCache(Duration =50, Location =ResponseCacheLocation.Any, NoStore =false)]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAll(int pageNumber=1, int pageSize = 10)
        {
            var products = await _productRepository.GetAllAsync(pageNumber, pageSize);
            return Ok(products);
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
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product= _productRepository.GetById(id);
            if (product.Id==null)
            {
                return NotFound("product not found =(...");
            }
            await _productRepository.Remove(id);
            return NoContent();
        }
        [HttpPut("bulk-update", Name ="BulkUpdateProducts")]
        public async Task<IActionResult> BulkUpdate([FromBody] List<Product> products)
        {
            if (products==null)
            {
                return NotFound();
            }

            await _productRepository.BulkUpdateAsync(products);
            return NoContent();
        }
    }
}
