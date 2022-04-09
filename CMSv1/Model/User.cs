using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSv1.Model
{
    public class UserRequest : User
    {
        public string type;
    }

    public class User
    {
        public string ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime DateCreated { get; set; }
    }

    public class Login
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
