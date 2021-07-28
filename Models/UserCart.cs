using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class UserCart
    {
        public long CartID { get; set; }
        public string BookName { get; set; }
        public string AuthorName { get; set; }

        [ForeignKey("User")]
        public long ID { get; set; }
        public string BookImage { get; set; }

        [ForeignKey("ResponseBook")]
        public long BookID { get; set; }
        public int BookPrice { get; set; }
        public int TotalCost { get; set; }
        public int Count { get; set; }








    }
}
