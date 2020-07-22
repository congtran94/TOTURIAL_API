using System;
using System.Collections.Generic;
using System.Text;
using GOSDataModel;
using GOSDataModel.Models;

namespace BusinessService.Interface
{
    public  interface IProductService:IRepository<Product>
    {
        IEnumerable<Product> GetHomePage();
        IEnumerable<Product> GetByCategoryId(int categoryId, int skip, int take);
        Product GetById(int Id);
    }
}
