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
                return InsertCardData(basePath) && InsertQuestionData(basePath);
            }
            catch (Exception) { }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="basePath"></param>
        /// <returns></returns>
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
        /// <param name="basePath"></param>
        /// <returns></returns>
        private static bool InsertQuestionData(string basePath)
        {
            var questionList = DataFileReaderHelper.ReadQuestionData(basePath);

            if (questionList.Count > 0)
            {
                foreach (Question question in questionList)
                {
                    ExecuteInsertQuestion(question);
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
                    cmd.Parameters.Clear();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="card"></param>
        private static void ExecuteInsertCard(Card card)
        {
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

            var paramList = new List<CommandParameter>
            {
                new CommandParameter("@Id", card.Id),
                new CommandParameter("@Name", card.Name),
                new CommandParameter("@Theme", card.Theme),
                new CommandParameter("@Difficulty", card.Difficulty)
            };
            ExecuteNonQuery(cmdText, paramList);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="question"></param>
        private static void ExecuteInsertQuestion(Question question)
        {
            var cmdText = "use TBG " +
                          "insert into TBTBG_QUESTIONS(" +
                          "    QUESTION_ID" +
                          ",   QUESTION_DESC" +
                          ",   QUESTION_THEME" +
                          ",   QUESTION_DIFFICULTY" +
                          ") values (" +
                          "    @Id" +
                          ",   @Description" +
                          ",   @Theme" +
                          ",   @Difficulty" +
                          ")";

            var paramList = new List<CommandParameter>
            {
                new CommandParameter("@Id", question.Id),
                new CommandParameter("@Description", question.Description),
                new CommandParameter("@Theme", question.Theme),
                new CommandParameter("@Difficulty", question.Difficulty)
            };
            ExecuteNonQuery(cmdText, paramList);
            ExecuteInsertAlternatives(question);
            ExecuteInsertCardRelationship(question);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="question"></param>
        private static void ExecuteInsertAlternatives(Question question)
        {
            var cmdText = "use TBG " +
                          "insert into TBTBG_QUESTION_ALTERNATIVES(" +
                          "    ALTERNATIVE_DESC" +
                          ",   ALTERNATIVE_CORRECT" +
                          ",   ALTERNATIVE_ORDER" +
                          ",   QUESTION_ID" +
                          ") values (" +
                          "    @Description" +
                          ",   @IsCorrect" +
                          ",   @Order" +
                          ",   @QuestionId" +
                          ")";
            foreach (Alternative alternative in question.Alternatives)
            {
                var paramList = new List<CommandParameter>
                {
                    new CommandParameter("@Description", alternative.Description),
                    new CommandParameter("@IsCorrect", alternative.IsCorrect),
                    new CommandParameter("@Order", alternative.Order),
                    new CommandParameter("@QuestionId", question.Id)
                };
                ExecuteNonQuery(cmdText, paramList);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="question"></param>
        private static void ExecuteInsertCardRelationship(Question question)
        {
            var cmdText = "use TBG " +
                          "insert into TBTBG_QUESTION_CARD_REL " +
                          "    select CARD_ID, " + question.Id + 
                          "      from TBTBG_CARDS" +
                          "     where CARD_THEME      = @Theme" +
                          "       and CARD_DIFFICULTY = @Difficulty";

            var paramList = new List<CommandParameter>
            {
                new CommandParameter("@Theme", question.Theme),
                new CommandParameter("@Difficulty", question.Difficulty)
            };
            ExecuteNonQuery(cmdText, paramList);
        }
    }
}