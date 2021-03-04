using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment.Data.Models
{
    public class LoginResultModel
    {
        public bool IsUserAuth { get; set; }
        public string Error { get; set; }

        public LoginResultModel()
        {
            IsUserAuth = false;
        }
    }
}
