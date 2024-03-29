using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using API.Dtos;
using AutoMapper;
using API.Errors;
namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productsRepo;
        private readonly IGenericRepository<ProductBrand> _productbrandsRepo;
        private readonly IGenericRepository<ProductType> _producttypesRepo;
        private readonly IMapper _mapper;
        public ProductsController(IGenericRepository<Product> productsRepo, IGenericRepository<ProductBrand> productbrandsRepo, IGenericRepository<ProductType> producttypesRepo, IMapper mapper)
        {
            _productsRepo = productsRepo;
            _producttypesRepo = producttypesRepo;
            _productbrandsRepo =productbrandsRepo;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProducttoReturnDto>>> GetProducts(){
            var spec = new ProductsWithTypesAndBrandsSpecifications();
            var products = await _productsRepo.ListAsync(spec);
            return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProducttoReturnDto>>(products));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]

        [ProducesResponseType(typeof(ApiRespone), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProducttoReturnDto>> GetProducts(int id){
            var spec = new ProductsWithTypesAndBrandsSpecifications(id);
            
            var product = await _productsRepo.GetEntityWithSpec(spec);

            if(product==null) return NotFound(new ApiRespone(404));
            return _mapper.Map<Product, ProducttoReturnDto>(product);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands(){
            var productbrands = await _productbrandsRepo.ListAllAsync();
            return Ok(productbrands);
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes(){
            var producttypes = await _producttypesRepo.ListAllAsync();
            return Ok(producttypes);
        }
    }
}