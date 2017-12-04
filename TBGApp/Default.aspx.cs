using System;
using System.Threading;
using System.Web.UI;
using TBGApp.Helpers;

namespace TBGApp
{
    public partial class _Default : Page
    {
        private static string SCRIPTS_PATH = @"\Database\Scripts\";

        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "TBG - Loading Application";
            if(CreateTbgDatabase() && CreateTbgTables() && CreateTbgData())
            {
                Response.Redirect("~/Questions.aspx");
            }
        }

        private bool CreateTbgDatabase()
        {
            try
            {
                DatabaseHelper.ExecuteNonQueryFromFile(Server.MapPath(".") + SCRIPTS_PATH + "TBG_CreateDatabase.sql");
                CreateDbStatusLabel.Text = "YES";
                CreateDbStatusLabel.CssClass = "tbg-info-yes";
                return true;
            }
            catch (Exception ex)
            { }

            return false;
        }

        private bool CreateTbgTables()
        {
            try
            {
                DatabaseHelper.ExecuteNonQueryFromFile(Server.MapPath(".") + SCRIPTS_PATH + "TBG_CreateTables.sql");
                CreateTablesStatusLabel.Text = "YES";
                CreateTablesStatusLabel.CssClass = "tbg-info-yes";
                return true;
            }
            catch (Exception ex)
            { }

            return false;
        }

        private bool CreateTbgData()
        {
            return false;
        }
    }
}