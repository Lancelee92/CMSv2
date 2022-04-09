using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CMSv1.Handler;
using CMSv1.Model;
using CMSv1.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NETCore.Encrypt;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Asn1.Cms;

namespace CMSv1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [Authorize(AuthenticationSchemes = AuthenticationSchemeConstants.ValidateHashAuthenticationScheme)]
        [HttpGet]
        public ResponseData Get()
        {
            ResponseData response = new ResponseData();

            try
            {
                List<User> games = UserService.getUser("");

                response = SharedClass.Response(((int)HttpStatusCode.OK).ToString(), JArray.FromObject(games));
            }
            catch (Exception ex)
            {
                response = SharedClass.Response(((int)HttpStatusCode.InternalServerError).ToString(), ex.Message);
            }

            return response;
        }

        /// <summary>
        ///     Creates/Update or Remove User
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Todo
        ///     {
        ///        "ID": 0,
        ///        "type": "update",
        ///        "Username": "Default",
        ///        "Password": "0123456789",
        ///        "Email": "WhatsApp@gmai.com"
        ///     }
        ///
        /// </remarks>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public ResponseData Post(UserRequest Data)
        {
            ResponseData response = new ResponseData();

            try
            {

                switch (Data.type.ToLower())
                {
                    case "update":

                        string password = EncryptProvider.AESDecrypt(Data.Password, AppSettings.Current.EncryptKey);

                        string encrypted = EncryptProvider.Sha256(password);

                        UserService.addUpdateUser(Data.Username, password, Data.Password);
                        //GameService.addUpdateGame(User);
                        response = SharedClass.Response(((int)HttpStatusCode.OK).ToString(), HttpStatusCode.OK.ToString());

                        break;
                    case "remove":

                        UserService.removeUser(Data.Username);
                        response = SharedClass.Response(((int)HttpStatusCode.OK).ToString(), HttpStatusCode.OK.ToString());

                        break;
                }
            }
            catch (Exception ex)
            {
                response = SharedClass.Response(((int)HttpStatusCode.InternalServerError).ToString(), ex.Message);
            }

            return response;
        }
    }
}