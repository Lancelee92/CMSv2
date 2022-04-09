using CMSv1.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CMSv1.Service
{
    public class UserService
    {
        public static List<User> getUser(string username)
        {
            List<User> users = new List<User>();

            try
            {
                SharedClass shared = new SharedClass();
                using (MySqlConnection sqlConn = shared.createConn())
                {
                    using (MySqlCommand command = new MySqlCommand("SP_GetUser", sqlConn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("InputUsername", username);
                        MySqlDataAdapter sqlDA = new MySqlDataAdapter(command);
                        DataSet sqlDS = new DataSet();

                        sqlDA.Fill(sqlDS, "ReturnList");

                        foreach (DataRow objDR in sqlDS.Tables[0].Rows)
                        {
                            User user = new User();
                            user.Username = objDR["Username"].ToString();
                            user.Password = objDR["Password"].ToString();
                            user.Email = objDR["Email"].ToString();
                            user.DateCreated = Convert.ToDateTime(objDR["DateCreated"]);

                            users.Add(user);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return users;
        }

        public static void addUpdateUser(string username, string password, string email)
        {
            try
            {
                SharedClass shared = new SharedClass();
                using (MySqlConnection sqlConn = shared.createConn())
                {
                    using (MySqlCommand command = new MySqlCommand("SP_AddUpdateUser", sqlConn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("InputUsername", username);
                        command.Parameters.AddWithValue("InputPassword", password);
                        command.Parameters.AddWithValue("InputEmail", email);

                        sqlConn.Open();
                        command.ExecuteNonQuery();
                        sqlConn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void removeUser(string username)
        {
            try
            {
                SharedClass shared = new SharedClass();
                using (MySqlConnection sqlConn = shared.createConn())
                {
                    using (MySqlCommand command = new MySqlCommand("SP_RemoveUser", sqlConn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("InputUsername", username);

                        sqlConn.Open();
                        command.ExecuteNonQuery();
                        sqlConn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static String getNewToken()
        {
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "admin"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            SymmetricSecurityKey authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSettings.Current.JwtSecret));

            JwtSecurityToken token = new JwtSecurityToken(
                    issuer: AppSettings.Current.JwtIssuer,
                    audience: AppSettings.Current.JwtAudience,
                    expires: DateTime.Now.AddHours(8),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
             );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
