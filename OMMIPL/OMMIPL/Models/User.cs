using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using OMMIPL.Models;

namespace OMMIPL.Models
{
    public class User
    {
        public string PK_UserId { get; set; }
        public string DisplayName { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string ProfilePic { get; set; }
        public string LoginId { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }

    }
}