using System;
using System.Collections.Generic;

namespace GOSDataModel.Models
{
    public partial class Order
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public int? Cash { get; set; }
        public DateTime? Date { get; set; }
        public int? Status { get; set; }
    }
}
