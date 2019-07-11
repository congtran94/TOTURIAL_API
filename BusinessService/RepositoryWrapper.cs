using BusinessService.Interface;
using GOSDataModel.Models;
using Microsoft.Extensions.Configuration;
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
        private IAccountService _accountService;
        private ISendEmail _sendEmail;
        private IConfiguration _configuration;
        public RepositoryWrapper(GOSContext context, IConfiguration configuration)
        {
            Context = context;
            _configuration = configuration;
        }
        public IAccountService AccountService
        {
            get
            {
                if(_accountService == null)
                {
                    _accountService = new AccountService(Context);
                }
                return _accountService;
                
            }
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
        public ISendEmail SendEmail
        {
            get
            {
                if (_sendEmail == null)
                {
                    _sendEmail = new SendEmail();
                }
                return _sendEmail;
            }
        }
        public void Save()
        {
            Context.SaveChanges();
        }
    }
}
