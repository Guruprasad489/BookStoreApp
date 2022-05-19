using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Models
{
    public class AdminLogin
    {
        [Required(ErrorMessage = "{0} should not be empty")]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "{0} should not be empty")]
        public string Password { get; set; }
    }
}
