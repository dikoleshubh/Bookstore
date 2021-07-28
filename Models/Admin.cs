using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Admin
    {
       public long  AdminID { get; set; }
        public string AdminEmail { get; set; }
     
        public string AdminPassword { get; set; }
        public string ServiceType { get; } = "Admin";
        public string token { get; set; }
    }
}
