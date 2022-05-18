using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CommonLayer.Models
{
    public class OrdersResponse
    {
        public int OrderId { get; set; }
        public int AddressId { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int BooksQty { get; set; }
        [JsonIgnore]
        public DateTime OrderDateTime { get; set; }
        public object OrderDate { get; set; }
        public double OrderPrice { get; set; }
        public double ActualPrice { get; set; }
        public string BookName { get; set; }
        public string BookImage { get; set; }
        public string Author { get; set; }
    }
}
