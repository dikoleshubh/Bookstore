using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
    class ResetPassword
    {
    }
    public class ResetPasswordModel
    {
        public string Email { get; set; }
        [Required]
        public string NewPassword { get; set; }
    }
}
