using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Models
{
    /// <summary>
    /// Class For User registration Request
    /// </summary>
    public class UserReg
    {
        [Required(ErrorMessage = "{0} should not be empty")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Full name should have minimum 3 characters")]
        [RegularExpression(@"(?=^.{0,40}$)^[a-zA-Z]{3,}\s?[a-zA-Z]*$", ErrorMessage = "Full name is not valid")]
        [DefaultValue("FullName")]
        public string FullName { get; set; }


        [Required(ErrorMessage = "{0} should not be empty")]
        [RegularExpression(@"^[a-zA-Z0-9]{3,}([._+-][a-zA-Z0-9]{1,})?@[a-zA-Z0-9]{1,10}[.][a-zA-Z]{2,3}([.][a-zA-Z]{2,3})?$", ErrorMessage = "Email Id is not valid")]
        [DataType(DataType.EmailAddress)]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "{0} should not be empty")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[&%$#@?^*!~]).{8,}$", ErrorMessage = "Passsword is not valid")]
        [DataType(DataType.Password)]
        [DefaultValue("@Password1")]
        public string Password { get; set; }

        [Required(ErrorMessage = "{0} should not be empty")]
        [RegularExpression(@"^[1-9][0-9]{9}$", ErrorMessage = "Mobile Number is not valid")]
        [DataType(DataType.PhoneNumber)]
        public string MobileNumber { get; set; }
    }
}
