using Domain.Entites;

namespace Application.Specification
{
    public class ProductSpecification: BaseSpecification<Product>
    {
        public ProductSpecification(string? brand,string? type,string? sort):base((p=>
        (string.IsNullOrEmpty(brand) || p.Brand == brand) &&
         (string.IsNullOrEmpty(type) || p.Type == type) ))
        {
            switch (sort)
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
