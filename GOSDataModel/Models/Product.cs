using System;
using System.Collections.Generic;
using System.Drawing;

namespace GOSDataModel.Models
{
    public partial class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public DateTime? Date { get; set; }
        public int? Status { get; set; }
        public int? Discount { get; set; }
        public string UrlImage { get; set; }
        public int? SizeId { get; set; }
        public int? TopHot { get; set; }
        public IList<ProductColor> ProductColor { get; set; }
        public virtual Category Category { get; set; }
    }
}
