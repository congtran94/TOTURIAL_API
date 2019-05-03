using System;
using System.Collections.Generic;
using System.Text;
using GOSDataModel;
using GOSDataModel.Models;

namespace BusinessService.Interface
{
    public  interface IProductService:IRepository<Product>
    {
        IEnumerable<Product> GetAndDoSomeThing();
    }
}
