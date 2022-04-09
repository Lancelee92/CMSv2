using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CMSv1.Handler;
using CMSv1.Model;
using CMSv1.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NETCore.Encrypt;
using Newtonsoft.Json.Linq;

namespace CMSv1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
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
        [Authorize(AuthenticationSchemes = AuthenticationSchemeConstants.ValidateHashAuthenticationScheme)]
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
                                string password = EncryptProvider.Base64Decrypt(Data.Password, Encoding.UTF8);

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


                        //GameService.addUpdateGame(User);
                        response = SharedClass.LoginResponse(((int)HttpStatusCode.OK).ToString(), HttpStatusCode.OK.ToString());

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