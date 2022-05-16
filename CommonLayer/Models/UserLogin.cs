using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Models
{
    public class UserLogin
    {
        [Required(ErrorMessage = "{0} should not be empty")]
        [RegularExpression(@"^[a-zA-Z0-9]{3,}([._+-][a-zA-Z0-9]{1,})?@[a-zA-Z0-9]{1,10}[.][a-zA-Z]{2,3}([.][a-zA-Z]{2,3})?$", ErrorMessage = "Email Id is not valid")]
        [DataType(DataType.EmailAddress)]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "{0} should not be empty")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[&%$#@?^*!~]).{8,}$", ErrorMessage = "Passsword is not valid")]
        [DefaultValue("@Password1")]
        public string Password { get; set; }
    }
}
