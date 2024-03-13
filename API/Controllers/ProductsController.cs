using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Core.Entities;
using Core.Interfaces;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IproductRepository _repo;
        public ProductsController(IproductRepository repo)
        {
            _repo = repo;   
        }
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts(){
            var products = await _repo.GetProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProducts(int id){
            var product = await _repo.GetProductbyIdAsync(id);
            return product;
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands(){
            var productbrands = await _repo.GetProductBrandsAsync();
            return Ok(productbrands);
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes(){
            var producttypes = await _repo.GetProductTypesAsync();
            return Ok(producttypes);
        }
    }
}