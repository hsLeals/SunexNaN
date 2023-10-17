using System;
using System.Collections.Generic;

#nullable disable

namespace Sunex.Models
{
    public partial class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductUrl { get; set; }
        public int? ProductPrice { get; set; }
        public int? ProductCargoAmount { get; set; }
        public int? ProductCount { get; set; }
        public int? ProductOrderId { get; set; }

        public virtual Order ProductOrder { get; set; }
    }
}
