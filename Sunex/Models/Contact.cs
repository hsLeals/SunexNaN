using System;
using System.Collections.Generic;

#nullable disable

namespace Sunex.Models
{
    public partial class Contact
    {
        public int ContactId { get; set; }
        public string ContactName { get; set; }
        public string ContactSurname { get; set; }
        public string ContactTitle { get; set; }
        public string ContactDescription { get; set; }
    }
}
