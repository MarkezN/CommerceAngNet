using API.DTOs;
using API.Errors;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductToReturnDto = API.DTOs.ProductToReturnDto;

namespace API.Controllers;

public class ProductsController : BaseApiController
{
    private readonly IGenericRepository<Product> _prodRepo;
    private readonly IGenericRepository<ProductType> _prodTypeRepo;
    private readonly IGenericRepository<ProductBrand> _prodBrepo;
    private readonly IMapper _mapper;
  
    public ProductsController(IGenericRepository<Product> prodRepo, IGenericRepository<ProductType> prodTypeRepo, IGenericRepository<ProductBrand> prodBrepo, IMapper mapper)
    {
        _prodRepo = prodRepo;
        _prodTypeRepo = prodTypeRepo;
        _prodBrepo = prodBrepo;
        _mapper = mapper;
    }
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts()
    { 
        var spec = new ProductsWithTypesAndBrandsSpecification();
        var products = await _prodRepo.ListAsync(spec);

        return Ok(_mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductToReturnDto>>(products));
    }
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
    {
        var spec = new ProductsWithTypesAndBrandsSpecification(id);
        var product = await _prodRepo.GetEntityWithSpec(spec);

        if (product == null) return NotFound(new ApiResponse(404));

        return _mapper.Map<Product, ProductToReturnDto>(product);
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