using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecification(ProductSpecParams productParams)
        : base(x =>(!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId)&&
        (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId)
        )
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
            AddOrderBY(x => x.Name);
            ApplyPaging(productParams.PageSize *  (productParams.pageIndex -1),productParams.PageSize);

            if(!string.IsNullOrEmpty(productParams.Sort))
            {
                switch(productParams.Sort)
                {
                    case "priceAsc":
                    AddOrderBY(p => p.Price);
                    break;
                    case"priceDesc":
                    AddOrderBYDescending(p => p.Price);
                    break;
                    default:
                    AddOrderBY(n => n.Name);
                    break;
                }
            }
        }

        public ProductsWithTypesAndBrandsSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }
    }
}