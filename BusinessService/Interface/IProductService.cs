using System;
using System.Collections.Generic;
using System.Text;
using BusinessService.Models;
using GOSDataModel;
using GOSDataModel.Models;

namespace BusinessService.Interface
{
    public  interface IProductService:IRepository<Product>
    {
        IEnumerable<ProductModel> GetHomePage();
        IEnumerable<ProductModel> GetByCategoryId(int categoryId, int skip, int take);
        ProductDetail GetById(int Id);
    }
}
