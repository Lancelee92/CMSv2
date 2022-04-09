using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Http;
using CMSv2.Handler;
using CMSv2.Model;
using CMSv2.Service;
using Newtonsoft.Json.Linq;

namespace CMSv2.Controllers
{
    public class GameListController : ApiController
    {
        [BasicAuthorize]
        [HttpGet]
        public ResponseData Get(int id)
        {
            ResponseData response = new ResponseData();

            try
            {
                List<Game> games = GameService.getALLGames(id);

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
        [BasicAuthorize]
        [HttpPost]
        public ResponseData Post(GameRequest Data)
        {
            ResponseData response = new ResponseData();

            try
            {

                switch (Data.type.ToLower())
                {
                    case "update":

                        Game game = new Game()
                        {
                            ID = Data.ID,
                            Title = Data.Title,
                            Description = Data.Description,
                            AndroidLink = Data.AndroidLink,
                            AppleLink = Data.AppleLink,
                            WebLink = Data.WebLink,
                            AgentLink = Data.AgentLink,
                            ImageUrl = Data.ImageUrl,
                            ImageCaption = Data.ImageCaption,
                            IfComingSoon = Data.IfComingSoon,
                            IfMaintenance = Data.IfMaintenance,
                            IfHot = Data.IfHot,
                        };
                        string id = GameService.addUpdateGame(game);

                        //On Insert Image
                        if (Data.ImageBase64 != null && Data.ImageBase64.Length > 0)
                        {
                            string[] str = Data.ImageBase64.Split(',');
                            byte[] bytes = Convert.FromBase64String(str[1]);

                            string folderPath = HostingEnvironment.MapPath(@"~/Asset/");

                            if (Directory.Exists(folderPath) == false)
                            {
                                Directory.CreateDirectory(folderPath);
                            }

                            string gameFolderPath = Path.Combine(folderPath, "Game");
                            if (Directory.Exists(gameFolderPath) == false)
                            {
                                Directory.CreateDirectory(gameFolderPath);
                            }

                            string fullPath = Path.Combine(gameFolderPath, Path.ChangeExtension(id, ".png"));
                            File.WriteAllBytes(fullPath, bytes);

                            string domain = ConfigurationManager.AppSettings.Get("assetFolder");
                            string imagePath = Path.Combine(domain, "Asset", "Game", Path.ChangeExtension(id, ".png"));
                            game.ImageUrl = imagePath;

                            //update image url
                            GameService.addUpdateGame(game);
                        }                       

                        response = SharedClass.Response(((int)HttpStatusCode.OK).ToString(), HttpStatusCode.OK.ToString());

                        break;
                    case "remove":

                        GameService.RemoveGame(Convert.ToInt32(Data.ID));

                        string imagefolderPath = HostingEnvironment.MapPath(@"~/Asset/");
                        if (Directory.Exists(imagefolderPath) == true)
                        {
                            string gameFolderPath = Path.Combine(imagefolderPath, "Game");
                            if (Directory.Exists(gameFolderPath) == true)
                            {
                                string fullPath = Path.Combine(gameFolderPath, Path.ChangeExtension(Data.ID, ".png"));

                                if (File.Exists(fullPath))
                                {
                                    File.Delete(fullPath);
                                }
                            }
                        }

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