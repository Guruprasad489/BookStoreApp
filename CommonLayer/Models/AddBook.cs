using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Models
{
    public class AddBook
    {
        [Required(ErrorMessage = "{0} should not be empty")]
        public string BookName { get; set; }

        [Required(ErrorMessage = "{0} should not be empty")]
        public string Author { get; set; }

        [Required(ErrorMessage = "{0} should not be empty")]
        public string BookImage { get; set; }

        [Required(ErrorMessage = "{0} should not be empty")]
        public string BookDetail { get; set; }

        [Required(ErrorMessage = "{0} should not be empty")]
        public double DiscountPrice { get; set; }

        [Required(ErrorMessage = "{0} should not be empty")]
        public double ActualPrice { get; set; }

        [Required(ErrorMessage = "{0} should not be empty")]
        public int Quantity { get; set; }
        public double Rating { get; set; }
        public int RatingCount { get; set; }
    }
}
