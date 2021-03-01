using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment.Data.Models
{
    public class ModelBase
    {
        public DateTime UpdatedOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public string CreatedBy { get; set; }
    }
}
