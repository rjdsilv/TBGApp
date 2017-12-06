using System;
using System.Web.UI;
using TBGApp.Helpers;

namespace TBGApp
{
    public partial class _Default : Page
    {
        private static string SCRIPTS_PATH = @"\Database\Scripts\";
        private static string DATAFILES_PATH = @"\Database\DataFiles\";

        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "Loading Application";

            if (CreateTbgDatabase() && CreateTbgTables() && CreateTbgData())
            {
                // Avoids exception raise by thread aborting.
                Response.Redirect("~/Questions.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool CreateTbgDatabase()
        {
            try
            {
                DatabaseHelper.ExecuteScriptFromFile(Server.MapPath(".") + SCRIPTS_PATH + "TBG_CreateDatabase.sql");
                CreateDbStatusLabel.Text = "YES";
                CreateDbStatusLabel.CssClass = "tbg-info-yes";
                return true;
            }
            catch (Exception)
            { }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool CreateTbgTables()
        {
            try
            {
                DatabaseHelper.ExecuteScriptFromFile(Server.MapPath(".") + SCRIPTS_PATH + "TBG_CreateTables.sql");
                CreateTablesStatusLabel.Text = "YES";
                CreateTablesStatusLabel.CssClass = "tbg-info-yes";
                return true;
            }
            catch (Exception)
            { }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool CreateTbgData()
        {
            return DatabaseHelper.CreateTbgData(Server.MapPath(".") + DATAFILES_PATH);
        }
    }
}