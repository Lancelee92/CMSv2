using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CMSv1.Handler;
using CMSv1.Model;
using CMSv1.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace CMSv1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameListController : ControllerBase
    {
        [Authorize(AuthenticationSchemes = AuthenticationSchemeConstants.ValidateHashAuthenticationScheme)]
        [HttpGet]
        public ResponseData Get()
        {
            ResponseData response = new ResponseData();

            try
            {
                List<Game> games = GameService.getALLGames();

                response = SharedClass.Response(((int)HttpStatusCode.OK).ToString(), JArray.FromObject(games));
            }
            catch (Exception ex)
            {
                response = SharedClass.Response(((int)HttpStatusCode.InternalServerError).ToString(), ex.Message);
            }
            
            return response;
        }

        /// <summary>
        ///     Creates/Update or Remove Game
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /GameList
        ///     {
        ///        "ID": "0",
        ///        "Title": "Data.Title",
        ///        "Description": "Data.Description",
        ///        "AndroidLink": "Data.AndroidLink",
        ///        "AppleLink": "Data.AppleLink",
        ///        "WebLink": "Data.WebLink",
        ///        "AgentLink": "Data.AgentLink",
        ///        "ImageUrl": "Data.ImageUrl",
        ///        "ImageCaption": "Data.ImageCaption",
        ///        "IfComingSoon": true,
        ///        "IfMaintenance": true,
        ///        "IfHot": true,
        ///     }
        ///
        /// </remarks>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public ResponseData Post(GameRequest Data)
        {
            ResponseData response = new ResponseData();

            try
            {

                switch (Data.type.ToLower())
                {
                    case "update":

                        Game game = Data.getGame(Data);
                        GameService.addUpdateGame(game);
                        response = SharedClass.Response(((int)HttpStatusCode.OK).ToString(), HttpStatusCode.OK.ToString());

                        break;
                    case "remove":

                        GameService.RemoveGame(Convert.ToInt32(Data.ID));
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