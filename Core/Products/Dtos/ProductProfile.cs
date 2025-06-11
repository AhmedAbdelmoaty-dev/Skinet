using Application.Products.Commands.CreateProduct;
using Application.Products.Commands.UpdateProduct;
using AutoMapper;
using Domain.Entites;

namespace Application.Products.Dtos
{
    internal class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<CreateProductCommand, Product>();

            CreateMap<UpdateProductCommand, Product>();

            CreateMap<Product, ProductDto>();
           

        }
    }
}
