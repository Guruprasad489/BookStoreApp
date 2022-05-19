﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Models
{
    public class CartResponse
    {
        public int CartId { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public int BooksQty { get; set; }
        public string BookName { get; set; }
        public string BookImage { get; set; }
        public string Author { get; set; }
        public double DiscountPrice { get; set; }
        public double ActualPrice { get; set; }
        public int Stock { get; set; }
    }
}
