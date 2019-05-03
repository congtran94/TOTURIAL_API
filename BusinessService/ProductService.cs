using System;
using System.Collections.Generic;
using GOSDataModel;
using GOSDataModel.Models;
using System.Linq;
using BusinessService.Interface;

namespace BusinessService
{
    public class ProductService : Repository<Product>, IProductService
    {
        public GOSContext Context;
        public ProductService(GOSContext _context) : base(_context)
        {
            Context = _context;
        }

        public IEnumerable<Product> GetAndDoSomeThing()
        {
            IEnumerable<Product> products = Find(s=>s.Id >=5).ToList();
            return products;
           
        }

        public IEnumerable<Product> GetProduct(int id)
        {
            return Context.Product.ToList();
        }
    }
}
