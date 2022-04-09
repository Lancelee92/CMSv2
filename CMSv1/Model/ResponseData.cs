using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSv1.Model
{
    public class ResponseData
    {
        public virtual string Status { get; set; }
        public virtual Object Data { get; set; }
    }

    public class LoginResponse : ResponseData
    {
        public string Token;
    }
}
