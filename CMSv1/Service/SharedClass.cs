using CMSv1.Model;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSv1.Service
{
    public class SharedClass
    {

        public MySqlConnection createConn() 
        {
            return new MySqlConnection(AppSettings.Current.ConnectionString);
        }

        public static ResponseData Response(string Status, JObject Data)
        {
            return new ResponseData() { Status = Status, Data = Data };
        }

        public static ResponseData Response(string Status, JArray Data)
        {
            return new ResponseData() { Status = Status, Data = Data };
        }

        public static ResponseData Response(string Status, String Data)
        {
            return new ResponseData() { Status = Status, Data = Data };
        }

        public static LoginResponse LoginResponse(string Status, String Data)
        {
            return new LoginResponse() { Status = Status, Data = Data };
        }
    }
}
