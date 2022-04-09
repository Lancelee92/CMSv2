using CMSv2.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CMSv2.Service
{
    public class PromotionService
    {
        public static List<Promotion> getAllPromotions(int ID)
        {
            List<Promotion> promotions = new List<Promotion>();

            try
            {
                SharedClass shared = new SharedClass();
                using (MySqlConnection sqlConn = shared.createConn())
                {
                    using (MySqlCommand command = new MySqlCommand("SP_GetAllPromotions", sqlConn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("InputID", ID);
                        MySqlDataAdapter sqlDA = new MySqlDataAdapter(command);
                        DataSet sqlDS = new DataSet();

                        sqlDA.Fill(sqlDS, "ReturnList");

                        foreach (DataRow objDR in sqlDS.Tables[0].Rows)
                        {
                            Promotion promotion = new Promotion();
                            promotion.ID = objDR["ID"].ToString();
                            promotion.Title = objDR["Title"].ToString();
                            promotion.Description = objDR["Description"].ToString();
                            promotion.ImageCaption = objDR["ImageCaption"].ToString();
                            promotion.ImageUrl = objDR["ImageUrl"].ToString();
                            promotion.IfShowTitle = Convert.ToBoolean(objDR["IfShowTitle"]);
                            promotion.DateCreated = Convert.ToDateTime(objDR["DateCreated"]);

                            promotions.Add(promotion);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return promotions;
        }

        public static string addUpdatePromotion(Promotion promotion)
        {

            try
            {
                if (String.IsNullOrEmpty(promotion.ID))
                {
                    //DEFAULT
                    promotion.ID = "0";
                }

                SharedClass shared = new SharedClass();
                using (MySqlConnection sqlConn = shared.createConn())
                {
                    using (MySqlCommand command = new MySqlCommand("SP_AddUpdatePromotions", sqlConn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("InputID", promotion.ID);
                        command.Parameters.AddWithValue("InputTitle", promotion.Title);
                        command.Parameters.AddWithValue("InputDescription", promotion.Description);
                        command.Parameters.AddWithValue("InputIfShowTitle", promotion.IfShowTitle);
                        command.Parameters.AddWithValue("InputImageUrl", promotion.ImageUrl);
                        command.Parameters.AddWithValue("InputImageCaption", promotion.ImageCaption);

                        using (MySqlDataAdapter sqlDA = new MySqlDataAdapter(command))
                        {
                            using (DataSet sqlDS = new DataSet())
                            {
                                sqlDA.Fill(sqlDS, "ReturnList");

                                promotion.ID = sqlDS.Tables[0].Rows[0]["ID"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return promotion.ID;
        }

        public static void removePromotion(int ID)
        {
            try
            {
                SharedClass shared = new SharedClass();

                using (MySqlConnection sqlConn = shared.createConn())
                {
                    using (MySqlCommand command = new MySqlCommand("SP_RemovePromotions", sqlConn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("InputID", ID);

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
