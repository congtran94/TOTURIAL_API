using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessService.Interface
{
    public interface IRepositoryWrapper
    {
        IProductService ProductService { get; }
        IOrderService OrderService { get; }
        IAccountService AccountService { get; }
        void Save();
    }
}
