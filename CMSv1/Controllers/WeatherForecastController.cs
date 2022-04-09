using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CMSv1.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using NETCore.Encrypt;
using NETCore.Encrypt.Internal;
using Newtonsoft.Json.Linq;

namespace CMSv1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            //connDB.getData();

            string test = EncryptProvider.Base64Decrypt("YWRtaW5AMTIzNA==", Encoding.UTF8);
            //string test2 = EncryptProvider.AESDecrypt("U2FsdGVkX1/Q1wYdO8f3ZC9Xoo0Cff/cnJcs3SmtCAQ=", "jA5ng497ST10CKLZKFxfu7NyYGXvnCNt");
            //string test3 = EncryptProvider.AESDecrypt("U2FsdGVkX1/Q1wYdO8f3ZC9Xoo0Cff/cnJcs3SmtCAQ=", "jA5ng497ST10CKLZKFxfu7NyYGXvnCNt");
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }

    public class connDB
    {

        public static void getData()
        {

            try
            {
                SharedClass shared = new SharedClass();
                using (MySqlConnection sqlConn = shared.createConn())
                {
                    using (MySqlCommand command = new MySqlCommand("SP_GetUser", sqlConn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("InputUsername", "");

                        MySqlDataAdapter sqlDA = new MySqlDataAdapter(command);
                        DataSet sqlDS = new DataSet();

                        sqlDA.Fill(sqlDS, "ReturnList");

                        foreach (DataRow objDR in sqlDS.Tables[0].Rows)
                        {
                            JObject cObject = new JObject();

                            cObject.Add("Username", objDR["Username"].ToString());
                            cObject.Add("Password", objDR["Password"].ToString());
                            cObject.Add("Email", objDR["Email"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
    }
}
