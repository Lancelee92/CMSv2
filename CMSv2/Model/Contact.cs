using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSv2.Model
{
    public class ContactRequest : Contact
    {
        public string type { get; set; }

        public Contact getContact(Contact Data)
        {
            return new Contact() 
            { 
                ID = Data.ID,
                ContactName = Data.ContactName, 
                ContactNumber = Data.ContactNumber, 
                ContactTypeID = Data.ContactTypeID, 
                ContactTypeName = Data.ContactTypeName,
                PrimaryContactID = Data.PrimaryContactID,
                bIsPrimaryContact = Data.bIsPrimaryContact
            };
        }
        //public 
    }

    public class Contact
    {
        public virtual string ID { get; set; }
        public virtual string ContactTypeID { get; set; }
        public virtual string ContactName { get; set; }
        public virtual string ContactNumber { get; set; }
        public virtual string ContactTypeName { get; set; }
        public virtual string PrimaryContactID { get; set; }
        public virtual bool bIsPrimaryContact { get; set; }
    }

    public static class ContactType
    {
        public static string WhatsApp = "1";
        public static string Telegram = "2";
        public static string Hotline = "3";
        public static string TelegramLink = "4";
    }
}
