using GOSDataModel.Models;
using System;
using System.Collections.Generic;

namespace BusinessService.Models
{
    public partial class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
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
        public string CategoryName { get; set; }
        public bool TopHot { get; set; }
    }
    public class PagingModel
    {
        public int CategoryId { get; set; }
        public int PageIndex { get; set; }
        public int TotalPage { get; set; }
        public int PageSize { get; set; }
    }
    public class ProductHome
    {
        public List<Product> Kitchens { get; set; }
        public List<Product> Accessories { get; set; }
        public List<Product> Other { get; set; }
    }
   
}
