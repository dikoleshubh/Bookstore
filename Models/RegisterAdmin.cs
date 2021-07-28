using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
    public class RegisterAdmin
    {


        [Required]
        public string AdminEmail { get; set; }


        [Required]
        public string AdminPassword { get; set; }

    }
}
