using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
    public class LoginUser
    {

        [Required]
        public string Email { get; set; }


        [Required]
        public string Password { get; set; }
    }
}
