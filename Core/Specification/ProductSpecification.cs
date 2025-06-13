using Domain.Entites;

namespace Application.Specification
{
    public class ProductSpecification: BaseSpecification<Product>
    {
        public ProductSpecification(ProductSpecParams specParams):base(p=>
            (string.IsNullOrEmpty(specParams.Search)|| p.Name.ToLower().Contains(specParams.Search))&&
            (specParams.Types.Count==0||specParams.Types.Contains(p.Type))&&
            (specParams.Brands.Count==0||specParams.Brands.Contains(p.Brand)))
        
        {
            ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);
            switch (specParams.sort)
            {
                case "priceAsc":
                    AddOrderByAsc(p => p.Price);
                    break;
                case "priceDesc":
                    AddOrderByDesc(p => p.Price);
                    break;
                default:
                    AddOrderByAsc(p => p.Price);
                    break;

                    
            }
        }
    }
    
}
