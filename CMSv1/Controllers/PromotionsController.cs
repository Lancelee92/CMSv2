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
using Newtonsoft.Json.Linq;

namespace CMSv1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionsController : ControllerBase
    {
        [Authorize(AuthenticationSchemes = AuthenticationSchemeConstants.ValidateHashAuthenticationScheme)]
        [HttpGet]
        public ResponseData Get()
        {
            ResponseData response = new ResponseData();

            try
            {
                List<Promotion> promotions = PromotionService.getAllPromotions();

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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
                        PromotionService.addUpdatePromotion(promotion);
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