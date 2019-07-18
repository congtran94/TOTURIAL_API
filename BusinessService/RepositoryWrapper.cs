using BusinessService.Interface;
using GOSDataModel.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessService
{
    public class RepositoryWrapper:IRepositoryWrapper
    {
        private GOSContext Context;
        private EmailSettings EmailSetting;
        private IProductService _productService;
        private IOrderService _orderService;
        private IAccountService _accountService;
        private ISendEmail _sendEmail;
        private IConfiguration Configuration;
        IHostingEnvironment HostingEnvironment;
        public RepositoryWrapper(GOSContext context, IConfiguration configuration, IOptions<EmailSettings> emailSettings, IHostingEnvironment hostingEv)
        {
            Context = context;
            Configuration = configuration;
            EmailSetting = emailSettings.Value;
            HostingEnvironment = hostingEv;
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
                    _sendEmail = new SendEmail(EmailSetting, HostingEnvironment);
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
