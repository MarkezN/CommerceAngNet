using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications;

public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
{
    public ProductsWithTypesAndBrandsSpecification(ProductSpecParams productSpecParams)
      : base(x => (string.IsNullOrEmpty(productSpecParams.Search) || x.Name.ToLower().Contains(productSpecParams.Search))
        && (!productSpecParams.BrandId.HasValue || x.ProductBrandId == productSpecParams.BrandId)
        && (!productSpecParams.TypeId.HasValue || x.ProductTypeId == productSpecParams.TypeId))
    {
        AddInclude(x => x.ProductType);
        AddInclude(x => x.Brand);
        AddOrderBy(x => x.Name);
        ApplyPaging(productSpecParams.PageSize * (productSpecParams.PageIndex - 1), productSpecParams.PageSize);
        
        if (!string.IsNullOrEmpty(productSpecParams.Sort) && productSpecParams.Sort.Any())
        {
            switch (productSpecParams.Sort)
            {
                case "priceAsc":
                    AddOrderBy(x => x.Price);
                    break;
                case "priceDesc":
                    AddOrderByDescending(x => x.Price);
                    break;
                default:
                    AddOrderBy(n=> n.Name);
                    break;
            }
        }
    }

    public ProductsWithTypesAndBrandsSpecification(int id) : base(x => x.Id == id)
    {
        AddInclude(x => x.ProductType);
        AddInclude(x => x.Brand);
    }
    
}