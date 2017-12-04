using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using TBGApp.Database;
using TBGApp.Database.Models;

namespace TBGApp.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public class DatabaseHelper
    {
        private static string CNN_STR = ConfigurationManager.ConnectionStrings["TBGCnnStr"].ConnectionString;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        public static void ExecuteScriptFromFile(string path)
        {
            if (File.Exists(path))
            {
                ExecuteNonQuery(File.ReadAllText(path));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="basePath"></param>
        /// <returns></returns>
        public static bool CreateTbgData(string basePath)
        {
            try
            {
                return InsertCardData(basePath);
            }
            catch(Exception) { }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="basePath"></param>
        private static bool InsertCardData(string basePath)
        {
            var cardList = DataFileReaderHelper.ReadCardData(basePath);

            if (cardList.Count > 0)
            {
                foreach (Card card in cardList)
                {
                    ExecuteInsertCard(card);
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmdText"></param>
        private static void ExecuteNonQuery(string cmdText)
        {
            using (SqlConnection cnn = new SqlConnection(CNN_STR))
            {
                using (SqlCommand cmd = new SqlCommand(cmdText, cnn))
                {
                    cnn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="paramList"></param>
        private static void ExecuteNonQuery(string cmdText, List<CommandParameter> paramList)
        {
            using (SqlConnection cnn = new SqlConnection(CNN_STR))
            {
                using (SqlCommand cmd = new SqlCommand(cmdText, cnn))
                {
                    cnn.Open();

                    foreach(CommandParameter param in paramList)
                    {
                        cmd.Parameters.AddWithValue(param.Name, param.Value);
                    }

                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="card"></param>
        private static void ExecuteInsertCard(Card card)
        {
            var paramList = new List<CommandParameter>();
            var cmdText = "use TBG " +
                          "insert into TBTBG_CARDS(" +
                          "    CARD_ID" +
                          ",   CARD_NAME" +
                          ",   CARD_THEME" +
                          ",   CARD_DIFFICULTY" +
                          ") values (" +
                          "    @Id" +
                          ",   @Name" +
                          ",   @Theme" +
                          ",   @Difficulty" +
                          ")";

            paramList.Add(new CommandParameter("@Id", card.Id));
            paramList.Add(new CommandParameter("@Name", card.Name));
            paramList.Add(new CommandParameter("@Theme", card.Theme));
            paramList.Add(new CommandParameter("@Difficulty", card.Difficulty));
            ExecuteNonQuery(cmdText, paramList);
        }
    }
}