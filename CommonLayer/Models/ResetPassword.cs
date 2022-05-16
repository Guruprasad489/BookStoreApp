using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Models
{
    public class ResetPassword
    {
        [Required(ErrorMessage = "{0} should not be empty")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[&%$#@?^*!~]).{8,}$", ErrorMessage = "Passsword is not valid")]
        [DefaultValue("@Password1")]
        public string Password { get; set; }

        [Required(ErrorMessage = "{0} should not be empty")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[&%$#@?^*!~]).{8,}$", ErrorMessage = "Passsword is not valid")]
        [DefaultValue("@Password1")]
        //[Compare("NewPassword", ErrorMessage = "The NewPassword and ConfirmPassword do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
