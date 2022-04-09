using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CMSv2.Handler;
using CMSv2.Model;
using CMSv2.Service;
using Newtonsoft.Json.Linq;
using System.Web.Http;

namespace CMSv2.Controllers
{
    public class LoginController : ApiController
    {
        /// <summary>
        ///     Creates or Remove Contacts
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Todo
        ///     {
        ///        "Username": "Default",
        ///        "Password": "0123456789",
        ///     }
        ///
        /// </remarks>
        [BasicAuthorize]
        [HttpPost]
        public LoginResponse Post(UserRequest Data)
        {
            LoginResponse response = new LoginResponse();

            try
            {

                switch (Data.type.ToLower())
                {
                    case "login":

                        if(String.IsNullOrEmpty(Data.Username) == false)
                        {
                            User existingUser = UserService.getUser(Data.Username).FirstOrDefault();

                            if (existingUser != null)
                            {
                                string password = EncryptProvider.Base64Decrypt(Data.Password);

                                string encryptedPassword = EncryptProvider.Sha256(password);

                                if(existingUser.Password == encryptedPassword)
                                {
                                    string token = UserService.getNewToken();

                                    response = SharedClass.LoginResponse(((int)HttpStatusCode.OK).ToString(), "Login Successfully!");
                                    response.Token = token;
                                }
                                else
                                {
                                    response = SharedClass.LoginResponse(((int)HttpStatusCode.Unauthorized).ToString(), "Wrong Password!");
                                }
                            }
                            else
                            {
                                response = SharedClass.LoginResponse(((int)HttpStatusCode.Unauthorized).ToString(), "User Not Found!");
                            }
                        }
                        else
                        {
                            response = SharedClass.LoginResponse(((int)HttpStatusCode.BadRequest).ToString(), "Missing Fields!");
                        }

                        break;                    
                }
            }
            catch (Exception ex)
            {
                response = SharedClass.LoginResponse(((int)HttpStatusCode.InternalServerError).ToString(), ex.Message);
            }

            return response;
        }
    }
}