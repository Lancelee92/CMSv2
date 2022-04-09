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
    public class ContactController : ApiController
    {
        [BasicAuthorize]
        [HttpGet]
        public ResponseData Get()
        {
            ResponseData response = new ResponseData();

            try
            {
                List<Contact> contacts = ContactService.getALLContact();

                response = SharedClass.Response(((int)HttpStatusCode.OK).ToString(), JArray.FromObject(contacts));
            }
            catch (Exception ex)
            {
                response = SharedClass.Response(((int)HttpStatusCode.InternalServerError).ToString(), ex.Message);
            }

            return response;
        }

        /// <summary>
        ///     Creates or Remove Contacts
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Contact
        ///     {
        ///        "ID": 0,
        ///        "type": "update",
        ///        "ContactTypeID": "1",
        ///        "ContactName": "Default",
        ///        "ContactNumber": "0123456789",
        ///        "ContactTypeName": "WhatsApp"
        ///     }
        ///
        /// </remarks>
        [BasicAuthorize]
        [HttpPost]       
        public ResponseData Post(ContactRequest Data)
        {
            ResponseData response = new ResponseData();

            try
            {
                //string type = Data["type"].ToString();                

                switch (Data.type.ToLower())
                {
                    case "update":

                        Contact contact = Data.getContact(Data);

                        ContactService.addContact(contact);
                        response = SharedClass.Response(((int)HttpStatusCode.OK).ToString(), HttpStatusCode.OK.ToString());

                        break;
                    case "remove":

                        ContactService.RemoveContact(Data.ID);
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