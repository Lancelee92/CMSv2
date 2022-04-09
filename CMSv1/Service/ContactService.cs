using CMSv1.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CMSv1.Service
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
                            contact.ContactName = objDR["ContactName"].ToString();
                            contact.ContactNumber = objDR["ContactNumber"].ToString();
                            contact.ContactTypeID = objDR["ContactTypeID"].ToString();
                            contact.ContactTypeName = objDR["ContactTypeName"].ToString();

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
                    using (MySqlCommand command = new MySqlCommand("SP_AddContact", sqlConn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("InputContactName", contact.ContactName);
                        command.Parameters.AddWithValue("InputContactNumber", contact.ContactNumber);
                        command.Parameters.AddWithValue("InputContactTypeID", contact.ContactTypeID);                        

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

        public static void RemoveContact(string contactNumber)
        {
            try
            {
                SharedClass shared = new SharedClass();

                using (MySqlConnection sqlConn = shared.createConn())
                {
                    using (MySqlCommand command = new MySqlCommand("SP_RemoveContact", sqlConn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("InputContactNumber", contactNumber);

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
