using System;
using System.Collections.Generic;
using System.Text;

namespace GOSDataModel.Models
{
	public partial class ProductColor
	{
		public int ProductId { get; set; }
		public int ColorId { get; set; }
		public virtual Product  Product { get; set; }
		public virtual Color Color { get; set; }
 	}
}
