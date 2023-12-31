﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Sunex.Models
{
    public partial class Status
    {
        public Status()
        {
            Users = new HashSet<User>();
        }

        public int Statusid { get; set; }
        public string StatusName { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
