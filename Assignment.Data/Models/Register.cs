using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment.Data.Models
{
    public class Register
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string VerificationPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneArea { get; set; }
        public string PhoneNumber { get; set; }
    }
}
