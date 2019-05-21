using System;
using System.Collections.Generic;

namespace GOSDataModel.Models
{
    public partial class AspNetUserTokens
    {
        public int UserId { get; set; }
        public int LoginProvider { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        public virtual AspNetUsers User { get; set; }
    }
}
