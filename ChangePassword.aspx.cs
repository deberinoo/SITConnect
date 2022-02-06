using System;
using System.Data.SqlClient;

namespace SITConnect
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        string MYDBConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MYDBConnection"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        //protected void btn_update_Click(object sender, EventArgs e)
        //{
        //    SqlConnection con = new SqlConnection(MYDBConnectionString);
        //    con.Open();
        //    string sql = "select * FROM Users";
        //    SqlCommand com = new SqlCommand(sql, con);
        //    SqlDataReader reader = com.ExecuteReader();
        //    while (reader.Read())
        //    {

        //    }
        //}
    }
}