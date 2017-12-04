using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;

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
        public static void ExecuteNonQueryFromFile(string path)
        {
            if (File.Exists(path))
            {
                ExecuteNonQuery(File.ReadAllText(path));
            }
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
    }
}