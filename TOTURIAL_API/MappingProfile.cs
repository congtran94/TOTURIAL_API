using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessService.Models;
using GOSDataModel.Models;


namespace TOTURIAL_API
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Order,Models.OrderModel>();
            CreateMap<Models.OrderModel,Order>();
            CreateMap<Product, ProductModel>();
            CreateMap<ProductModel,Product>();
            CreateMap<UserModel, AspNetUsers>();
            CreateMap<AspNetUsers, UserModel>();
        }
    }
}
