using System;
using System.Collections.Generic;

#nullable disable

namespace Sunex.Models
{
    public partial class Branch
    {
        public Branch()
        {
            Orders = new HashSet<Order>();
            Users = new HashSet<User>();
        }

        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public string BranchAddress { get; set; }
        public int? BranchPhone { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
