using System;
using System.Collections.Generic;

namespace TOTURIAL_API.Models
{
    public partial class OrderModel
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public int? Cash { get; set; }
        public DateTime? Date { get; set; }
        public int? Status { get; set; }
    }
}
