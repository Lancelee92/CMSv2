using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Http;
using CMSv2.Handler;
using CMSv2.Model;
using CMSv2.Service;
using Newtonsoft.Json.Linq;

namespace CMSv2.Controllers
{
    public class PromotionsController : ApiController
    {
        [BasicAuthorize]
        [HttpGet]
        public ResponseData Get(int id)
        {
            ResponseData response = new ResponseData();

            try
            {
                List<Promotion> promotions = PromotionService.getAllPromotions(id);

                response = SharedClass.Response(((int)HttpStatusCode.OK).ToString(), JArray.FromObject(promotions));
            }
            catch (Exception ex)
            {
                response = SharedClass.Response(((int)HttpStatusCode.InternalServerError).ToString(), ex.Message);
            }

            return response;
        }

        /// <summary>
        ///     Creates/Update or Remove Promotion
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Promotions
        ///     {
        ///         "ID": "0",
        ///         "Title": "Data.Title",
        ///         "Description": "Data.Description",
        ///         "ImageUrl": "Data.ImageUrl",
        ///         "ImageCaption": "Data.ImageCaption",
        ///         "IfShowTitle": true
        ///     }
        ///
        /// </remarks>
        [BasicAuthorize]
        [HttpPost]
        public ResponseData Post(PromotionRequest Data)
        {
            ResponseData response = new ResponseData();

            try
            {
                switch (Data.type.ToLower())
                {
                    case "update":

                        Promotion promotion = Data.getPromotion(Data);
                        string id = PromotionService.addUpdatePromotion(promotion);

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

                            string gameFolderPath = Path.Combine(folderPath, "Promotion");
                            if (Directory.Exists(gameFolderPath) == false)
                            {
                                Directory.CreateDirectory(gameFolderPath);
                            }

                            string fullPath = Path.Combine(gameFolderPath, Path.ChangeExtension(id, ".png"));
                            File.WriteAllBytes(fullPath, bytes);

                            string domain = ConfigurationManager.AppSettings.Get("assetFolder");
                            string imagePath = Path.Combine(domain, "Asset", "Promotion", Path.ChangeExtension(id, ".png"));
                            promotion.ImageUrl = imagePath;

                            //update image url
                            PromotionService.addUpdatePromotion(promotion);
                        }


                        response = SharedClass.Response(((int)HttpStatusCode.OK).ToString(), HttpStatusCode.OK.ToString());

                        break;
                    case "remove":

                        PromotionService.removePromotion(Convert.ToInt32(Data.ID));
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