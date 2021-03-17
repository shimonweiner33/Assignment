using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment.Data.Models
{
    public class Member : ModelBase
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneArea { get; set; }
        public string PhoneNumber { get; set; }
    }
    public class ExtendMember : Member
    {
        public string UserConnectinonId { get; set; }
    }
    public class ExtendMembers
    {
        public List<ExtendMember> Members { get; set; }
    }
}
