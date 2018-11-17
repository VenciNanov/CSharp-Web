using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ChushkaWebApp.Models;
using ChushkaWebApp.ViewModels.Orders;
using ChushkaWebApp.ViewModels.Product;

namespace ChushkaWebApp
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, DetailsProductViewModel>().ReverseMap();

            CreateMap<Product, CreateProductViewModel>().ReverseMap();

            CreateMap<Order, DetailsOrderViewModel>()
                .ForMember(
                    dest => dest.Client,
                    opt => opt.MapFrom(src => src.Client.UserName)
                )
                .ForMember(
                    dest => dest.Product,
                    opt => opt.MapFrom(src => src.Product.Name)
                ).ReverseMap();
        }
    }
}
