using System;
using System.Collections.Generic;

#nullable disable

namespace Sunex.Models
{
    public partial class User
    {
        public User()
        {
            Notifications = new HashSet<Notification>();
            Orders = new HashSet<Order>();
        }

        public int Userid { get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public int? UserPhone { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public int? UserStatusId { get; set; }
        public int? UserBranchId { get; set; }

        public virtual Branch UserBranch { get; set; }
        public virtual Status UserStatus { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
