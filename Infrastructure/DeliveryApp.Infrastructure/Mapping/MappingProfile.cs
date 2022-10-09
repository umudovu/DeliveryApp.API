using AutoMapper;
using DeliveryApp.Application.DTOs;
using DeliveryApp.Application.DTOs.Category;
using DeliveryApp.Application.DTOs.User;
using DeliveryApp.Application.ViewModels;
using DeliveryApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Infrastructure.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryUpdateVM>().ReverseMap();
            CreateMap<Product, ProductCreateVM>().ReverseMap();
            CreateMap<Product, ProductUpdateVM>().ReverseMap();

            CreateMap<AppUser, CreateUser>().ReverseMap();
            CreateMap<Category, CategoryResponse>().ReverseMap();
            CreateMap<Product, ProductResponse>().ReverseMap();
            CreateMap<Company, CompanyResponse>().ReverseMap();


        }
    }
}
