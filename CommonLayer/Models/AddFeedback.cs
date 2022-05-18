using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Models
{
    public class AddFeedback
    {
        public int Rating { get; set; } 
        public string Comment { get; set; } 
        public int BookId { get; set; } 
    }
}
