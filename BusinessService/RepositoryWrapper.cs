using BusinessService.Interface;
using GOSDataModel.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessService
{
    public class RepositoryWrapper:IRepositoryWrapper
    {
        private GOSContext Context;
        private IProductService _productService;
        private IOrderService _orderService;
        public RepositoryWrapper(GOSContext context)
        {
            Context = context;
        }
        public IProductService ProductService
        {
            get
            {
                if(_productService == null)
                {
                    _productService = new ProductService(Context);
                }
                return _productService;
            }
        }
        public IOrderService OrderService
        {
            get
            {
                if (_orderService == null)
                {
                    _orderService = new OrderService(Context);
                }
                return _orderService;
            }
        }


        public void Save()
        {
            Context.SaveChanges();
        }
    }
}
