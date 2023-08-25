using API.DTOs;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IGenericRepository<Product> _prodRepo;
    private readonly IGenericRepository<ProductType> _prodTypeRepo;
    private readonly IGenericRepository<ProductBrand> _prodBrepo;
  
    public ProductsController(IGenericRepository<Product> prodRepo, IGenericRepository<ProductType> prodTypeRepo, IGenericRepository<ProductBrand> prodBrepo)
    {
        _prodRepo = prodRepo;
        _prodTypeRepo = prodTypeRepo;
        _prodBrepo = prodBrepo;
    }
    [HttpGet]
    public async Task<ActionResult<List<ProductToReturnDto>>> GetProducts()
    {
        var spec = new ProductsWithTypesAndBrandsSpecification();
        var products = await _prodRepo.ListAsync(spec);

        return products.Select(product => new ProductToReturnDto()
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            PictureUrl = product.PictureUrl,
            ProductType = product.ProductType.Name,
            Brand = product.Brand.Name
        }).ToList();
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
    {
        var spec = new ProductsWithTypesAndBrandsSpecification(id);
        var product = await _prodRepo.GetEntityWithSpec(spec);

        return new ProductToReturnDto()
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            PictureUrl = product.PictureUrl,
            ProductType = product.ProductType.Name,
            Brand = product.Brand.Name
        };
    }
    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
    {
        var brands = await _prodBrepo.ListAllAsync();
        return Ok(brands);
    }
    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
    {
        return Ok(await _prodTypeRepo.ListAllAsync());
    }
}