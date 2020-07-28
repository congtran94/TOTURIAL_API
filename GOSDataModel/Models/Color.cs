using System;
using System.Collections.Generic;

namespace GOSDataModel.Models
{
    public partial class Color
    {
        public int Id { get; set; }
        public string ColorName { get; set; }
        public IList<ProductColor> ProductColor { get; set; }
    }
}
