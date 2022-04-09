using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using CMSv2.Handler;
using CMSv2.Model;
using CMSv2.Service;
using Newtonsoft.Json.Linq;

namespace CMSv2.Controllers
{
    public class UserController : ApiController
    {
        [BasicAuthorize]
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
        [BasicAuthorize]
        [HttpPost]
        public ResponseData Post(UserRequest Data)
        {
            ResponseData response = new ResponseData();

            try
            {

                switch (Data.type.ToLower())
                {
                    case "verify":
                        List<User> users = UserService.getUser(Data.Username);
                        response = SharedClass.Response(((int)HttpStatusCode.OK).ToString(), JArray.FromObject(users));

                        break;
                    case "update":

                        string password = EncryptProvider.Base64Decrypt(Data.Password);

                        string encrypted = EncryptProvider.Sha256(password);

                        UserService.addUpdateUser(Data.Username, encrypted, "");
                        //GameService.addUpdateGame(User);
                        response = SharedClass.Response(((int)HttpStatusCode.OK).ToString(), HttpStatusCode.OK.ToString());

                        break;
                    case "remove":

                        UserService.removeUser(Data.ID);
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