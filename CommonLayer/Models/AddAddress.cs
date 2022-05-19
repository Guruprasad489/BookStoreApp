using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Models
{
    public class AddAddress
    {
        [Required(ErrorMessage = "{0} should not be empty")]
        public string Address { get; set; }

        [Required(ErrorMessage = "{0} should not be empty")]
        public string City { get; set; }

        [Required(ErrorMessage = "{0} should not be empty")]
        public string State { get; set; }

        [Required(ErrorMessage = "{0} should not be empty")]
        public int TypeId { get; set; }
    }
}
