using System;
using System.Collections.Generic;
using GOSDataModel;
using GOSDataModel.Models;
using System.Linq;
using BusinessService.Interface;

namespace BusinessService
{
    public class OrderService : Repository<Order>, IOrderService 
    {
        public GOSContext Context;
        public OrderService(GOSContext _context) : base(_context)
        {
            Context = _context;
        }
        public IEnumerable<Order> GetOrder(int id)
        {
            return Context.Order.ToList();
        }
    }
}
