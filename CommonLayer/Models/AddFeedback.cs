using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Models
{
    public class AddFeedback
    {
        [Required(ErrorMessage = "{0} should not be empty")]
        public string Rating { get; set; } 
        public string Comment { get; set; } 
        public int BookId { get; set; } 
    }
}
