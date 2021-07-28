using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Models
{

    public class ResponseBook : BookModel
    {
   
        public long BookID { get; set; }
        public string BookImage { get; set; }

        [DefaultValue(true)]
        public bool InStock { get; set; }
        public bool InCart { get; set; }

    }
}
