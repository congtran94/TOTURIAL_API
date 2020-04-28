﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TOTURIAL_API.Models;
using GOSDataModel.Models;


namespace TOTURIAL_API
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<GOSDataModel.Models.Order,Models.OrderModel>();
            CreateMap<Models.OrderModel, GOSDataModel.Models.Order>();
            CreateMap<GOSDataModel.Models.Product, Models.ProductModel>();
            CreateMap<Models.ProductModel, GOSDataModel.Models.Product>();
            CreateMap<Models.UserModel, GOSDataModel.Models.AspNetUsers>();
            CreateMap<GOSDataModel.Models.AspNetUsers, Models.UserModel>();
        }
    }
}
