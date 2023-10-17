using System;

namespace Sunex.ViewModels
{
    public class OpOrders
    {
        public int OrderId { get; set; }
        public string FullName { get; set; }
        public string ProductName { get; set; }
        public string Date { get; set; }
        public int count { get; set; }
        public int? levelId { get; set; }
    }
}
