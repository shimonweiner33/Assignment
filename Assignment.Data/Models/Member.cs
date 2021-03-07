using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment.Data.Models
{
    public class Member : ModelBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneArea { get; set; }
        public string PhoneNumber { get; set; }
    }
}
