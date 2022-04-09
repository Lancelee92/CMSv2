using CMSv2.Model;
using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace CMSv2.Service
{
    public class SharedClass
    {

        public MySqlConnection createConn()
        {
            string dbConn = ConfigurationManager.AppSettings.Get("DBConnectionString");
            return new MySqlConnection(dbConn);
        }

        public static string getApiKey()
        {
            return ConfigurationManager.AppSettings.Get("JWT_Key");
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
