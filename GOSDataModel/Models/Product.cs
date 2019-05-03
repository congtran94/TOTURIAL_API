using System;
using System.Collections.Generic;

namespace GOSDataModel.Models
{
    public partial class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public DateTime? Date { get; set; }
        public int? Status { get; set; }
        public int? Discount { get; set; }
        public int? SupplyId { get; set; }
        public string ImageUrl { get; set; }
        public string SizeId { get; set; }
        public string ColorId { get; set; }
    }
   
}
