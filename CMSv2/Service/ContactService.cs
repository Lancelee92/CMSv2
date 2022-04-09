using CMSv2.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CMSv2.Service
{
    public class ContactService
    {
        /// <summary>
        /// Get All contacts
        /// </summary>
        /// <returns></returns>
        public static List<Contact> getALLContact()
        {
            List<Contact> contacts = new List<Contact>();

            try
            {
                SharedClass shared = new SharedClass();
                using (MySqlConnection sqlConn = shared.createConn())
                {
                    using (MySqlCommand command = new MySqlCommand("SP_GetAllContact", sqlConn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        MySqlDataAdapter sqlDA = new MySqlDataAdapter(command);
                        DataSet sqlDS = new DataSet();

                        sqlDA.Fill(sqlDS, "ReturnList");

                        foreach (DataRow objDR in sqlDS.Tables[0].Rows)
                        {
                            Contact contact = new Contact();
                            contact.ID = objDR["ID"].ToString();
                            contact.ContactName = objDR["ContactName"].ToString();
                            contact.ContactNumber = objDR["ContactNumber"].ToString();
                            contact.ContactTypeID = objDR["ContactTypeID"].ToString();
                            contact.ContactTypeName = objDR["ContactTypeName"].ToString();
                            contact.PrimaryContactID = objDR["PrimaryContactID"].ToString();

                            if(contact.PrimaryContactID == contact.ID)
                            {
                                contact.bIsPrimaryContact = true;
                            }
                            else
                            {
                                contact.bIsPrimaryContact = false;
                            }
                            contacts.Add(contact);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return contacts;
        }

        public static void addContact(Contact contact)
        {

            try
            {
                SharedClass shared = new SharedClass();

                using (MySqlConnection sqlConn = shared.createConn())
                {
                    using (MySqlCommand command = new MySqlCommand("SP_AddUpdateContact", sqlConn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("InputContactID", contact.ID);
                        command.Parameters.AddWithValue("InputContactName", contact.ContactName);
                        command.Parameters.AddWithValue("InputContactNumber", contact.ContactNumber);
                        command.Parameters.AddWithValue("InputContactTypeID", contact.ContactTypeID);
                        command.Parameters.AddWithValue("InputIsPrimary", contact.bIsPrimaryContact);

                        sqlConn.Open();
                        command.ExecuteNonQuery();
                        sqlConn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void RemoveContact(string ID)
        {
            try
            {
                SharedClass shared = new SharedClass();

                using (MySqlConnection sqlConn = shared.createConn())
                {
                    using (MySqlCommand command = new MySqlCommand("SP_RemoveContact", sqlConn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("InputContactID", ID);

                        sqlConn.Open();
                        command.ExecuteNonQuery();
                        sqlConn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
