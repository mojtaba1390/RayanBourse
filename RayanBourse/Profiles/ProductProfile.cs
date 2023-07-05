using AutoMapper;
using RayanBourse.Domain.Entities;
using RayanBourse.Models;

namespace RayanBourse.Profiles
{
    public class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductModel>();

        }
    }
}
