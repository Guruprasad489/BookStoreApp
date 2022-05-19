using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Models
{
    public class AddToCart
    {
        public int BookId { get; set; }

        [Required(ErrorMessage = "{0} should not be empty")]
        public int BooksQty { get; set; }
    }
}
