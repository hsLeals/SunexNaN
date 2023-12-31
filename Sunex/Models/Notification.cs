﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Sunex.Models
{
    public partial class Notification
    {
        public int NotificationId { get; set; }
        public int? NotificationOrderId { get; set; }
        public int? NotificationClientId { get; set; }
        public string NotificationTitle { get; set; }
        public string NotificationDescription { get; set; }
        public DateTime NotificationDate { get; set; }
        public bool NotificationIsRead { get; set; }

        public virtual User NotificationClient { get; set; }
        public virtual Order NotificationOrder { get; set; }
    }
}
