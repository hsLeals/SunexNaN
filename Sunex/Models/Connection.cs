using System;
using System.Collections.Generic;

#nullable disable

namespace Sunex.Models
{
    public partial class Connection
    {
        public int ConnectionId { get; set; }
        public string ConnectionUser { get; set; }
        public string ConnectionEmail { get; set; }
        public string ConnectionTitle { get; set; }
        public string ConnectionDescription { get; set; }
        public int? ConnectionLvl { get; set; }
    }
}
