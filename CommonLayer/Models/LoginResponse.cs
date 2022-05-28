using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Models
{
    public class LoginResponse
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string EmailId { get; set; }
        public long MobileNumber { get; set; }
        public string Token { get; set; }
    }
}
