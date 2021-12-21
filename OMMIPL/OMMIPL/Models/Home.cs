using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OMMIPL.Models
{
    public class Home
    {
        public string PK_UserId { get; set; }
        public string DisplayName { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string ProfilePic { get; set; }

    }
    public class Login
    {
        public string LoginId { get; set; }
        public string Password { get; set; }
    }
}