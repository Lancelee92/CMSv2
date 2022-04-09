using CMSv1.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CMSv1.Service
{
    public class GameService
    {
        public static List<Game> getALLGames()
        {
            List<Game> games = new List<Game>();

            try
            {
                SharedClass shared = new SharedClass();
                using (MySqlConnection sqlConn = shared.createConn())
                {
                    using (MySqlCommand command = new MySqlCommand("SP_GetAllGames", sqlConn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        MySqlDataAdapter sqlDA = new MySqlDataAdapter(command);
                        DataSet sqlDS = new DataSet();

                        sqlDA.Fill(sqlDS, "ReturnList");

                        foreach (DataRow objDR in sqlDS.Tables[0].Rows)
                        {
                            Game game = new Game();
                            game.ID = objDR["ID"].ToString();
                            game.Title = objDR["Title"].ToString();
                            game.WebLink = objDR["WebLink"].ToString();
                            game.ImageUrl = objDR["ImageUrl"].ToString();
                            game.ImageCaption = objDR["ImageCaption"].ToString();
                            game.Description = objDR["Description"].ToString();
                            game.AgentLink = objDR["AgentLink"].ToString();
                            game.AndroidLink = objDR["AndroidLink"].ToString();
                            game.AppleLink = objDR["AppleLink"].ToString();
                            game.DateCreated = Convert.ToDateTime(objDR["DateCreated"]);
                            game.IfComingSoon = Convert.ToBoolean(objDR["IfComingSoon"]);
                            game.IfHot = Convert.ToBoolean(objDR["IfHot"]);
                            game.IfMaintenance = Convert.ToBoolean(objDR["IfMaintenance"]);

                            games.Add(game);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return games;
        }

        public static void addUpdateGame(Game game)
        {
            
            try
            {
                SharedClass shared = new SharedClass();

                if (String.IsNullOrEmpty(game.ID))
                {
                    //DEFAULT
                    game.ID = "0";
                }

                using (MySqlConnection sqlConn = shared.createConn())
                {
                    using (MySqlCommand command = new MySqlCommand("SP_AddUpdateGames", sqlConn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("InputID", game.ID);
                        command.Parameters.AddWithValue("InputTitle", game.Title);
                        command.Parameters.AddWithValue("InputDescription", game.Description);
                        command.Parameters.AddWithValue("InputAndroidLink", game.AndroidLink);
                        command.Parameters.AddWithValue("InputAppleLink", game.AppleLink);
                        command.Parameters.AddWithValue("InputWebLink", game.WebLink);
                        command.Parameters.AddWithValue("InputAgentLink", game.AgentLink);
                        command.Parameters.AddWithValue("InputImageUrl", game.ImageUrl);
                        command.Parameters.AddWithValue("InputImageCaption", game.ImageCaption);
                        command.Parameters.AddWithValue("InputIfComingSoon", game.IfComingSoon);
                        command.Parameters.AddWithValue("InputIfMaintenance", game.IfMaintenance);
                        command.Parameters.AddWithValue("InputIfHot", game.IfHot);

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

        public static void RemoveGame(int ID)
        {
            try
            {
                SharedClass shared = new SharedClass();

                using (MySqlConnection sqlConn = shared.createConn())
                {
                    using (MySqlCommand command = new MySqlCommand("SP_RemoveGame", sqlConn))
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
