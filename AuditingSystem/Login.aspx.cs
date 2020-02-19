using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AuditingSystem
{
    public partial class Login : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["myconnection"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        static string ComputeSha256Hash(string rawData)
        {                                                                                              //used for password hashing
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
/*---------------------------------------------------------------------------------------------------------------------------------------------------------------*/
        protected void loginbtn_Click(object sender, EventArgs e)
        {

            int check = 0;
            if (txtusrname.Text.Trim() == "" || txtpsw.Text.Trim() == "")                                                           //login validation and 45 days password expiry part
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('All fields are mandatory');", true);
            }
            else
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT status,password,count,end_date FROM login_credentials WHERE ssn=@d1", con);
                cmd.Parameters.AddWithValue("d1", txtusrname.Text);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                { 



                    if (dr[0].ToString() == "0" || dr[0].ToString() == "4")
                    {
                        if (dr[1].ToString() == ComputeSha256Hash(txtpsw.Text))
                        {

                            Session["username"] = txtusrname.Text;
                            if (DateTime.Parse(dr[3].ToString()) <= System.DateTime.Today)
                            {
                                con.Close();
                                con.Open();
                                SqlCommand cmd2 = new SqlCommand("Update login_credentials SET status=@d1 WHERE ssn=@d2", con);
                                cmd2.Parameters.AddWithValue("d1", 4);
                                cmd2.Parameters.AddWithValue("d2", txtusrname.Text);
                                cmd2.ExecuteNonQuery();
                                con.Close();
                                con.Open();
                                SqlCommand cmd3 = new SqlCommand("Update login_credentials SET count=@d1  WHERE ssn=@d2", con);
                                cmd3.Parameters.AddWithValue("d1", 0);
                                cmd3.Parameters.AddWithValue("d2", txtusrname.Text);
                                cmd3.ExecuteNonQuery();
                                con.Close();
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Password has expired');window.location='Dashboard.aspx';", true);

                            }
                            else if (DateTime.Parse(dr[3].ToString()).AddDays(-7) == System.DateTime.Today)
                            {
                                con.Close();
                                con.Open();
                                SqlCommand cmd2 = new SqlCommand("Update login_credentials SET count=@d1 WHERE ssn=@d2", con);
                                cmd2.Parameters.AddWithValue("d1", 0);
                                cmd2.Parameters.AddWithValue("d2", txtusrname.Text);
                                cmd2.ExecuteNonQuery();
                                con.Close();
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Password will expire in 7 days');window.location='Dashboard.aspx';", true);
                            }
                            else if (DateTime.Parse(dr[3].ToString()).AddDays(-6) == System.DateTime.Today)
                            {
                                con.Close();
                                con.Open();
                                SqlCommand cmd2 = new SqlCommand("Update login_credentials SET count=@d1 WHERE ssn=@d2", con);
                                cmd2.Parameters.AddWithValue("d1", 0);
                                cmd2.Parameters.AddWithValue("d2", txtusrname.Text);
                                cmd2.ExecuteNonQuery();
                                con.Close();
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Password will expire in 6 days');window.location='Dashboard.aspx';", true);
                            }
                            else if (DateTime.Parse(dr[3].ToString()).AddDays(-5) == System.DateTime.Today)
                            {
                                con.Close();
                                con.Open();
                                SqlCommand cmd2 = new SqlCommand("Update login_credentials SET count=@d1 WHERE ssn=@d2", con);
                                cmd2.Parameters.AddWithValue("d1", 0);
                                cmd2.Parameters.AddWithValue("d2", txtusrname.Text);
                                cmd2.ExecuteNonQuery();
                                con.Close();
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Password will expire in 5 days');window.location='Dashboard.aspx';", true);
                            }
                            else if (DateTime.Parse(dr[3].ToString()).AddDays(-4) == System.DateTime.Today)
                            {
                                con.Close();
                                con.Open();
                                SqlCommand cmd2 = new SqlCommand("Update login_credentials SET count=@d1 WHERE ssn=@d2", con);
                                cmd2.Parameters.AddWithValue("d1", 0);
                                cmd2.Parameters.AddWithValue("d2", txtusrname.Text);
                                cmd2.ExecuteNonQuery();
                                con.Close();
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Password will expire in 4 days');window.location='Dashboard.aspx';", true);
                            }
                            else if (DateTime.Parse(dr[3].ToString()).AddDays(-3) == System.DateTime.Today)
                            {
                                con.Close();
                                con.Open();
                                SqlCommand cmd2 = new SqlCommand("Update login_credentials SET count=@d1 WHERE ssn=@d2", con);
                                cmd2.Parameters.AddWithValue("d1", 0);
                                cmd2.Parameters.AddWithValue("d2", txtusrname.Text);
                                cmd2.ExecuteNonQuery();
                                con.Close();
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Password will expire in 3 days');window.location='Dashboard.aspx';", true);
                            }
                            else if (DateTime.Parse(dr[3].ToString()).AddDays(-2) == System.DateTime.Today)
                            {
                                con.Close();
                                con.Open();
                                SqlCommand cmd2 = new SqlCommand("Update login_credentials SET count=@d1 WHERE ssn=@d2", con);
                                cmd2.Parameters.AddWithValue("d1", 0);
                                cmd2.Parameters.AddWithValue("d2", txtusrname.Text);
                                cmd2.ExecuteNonQuery();
                                con.Close();
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Password will expire in 2 days');window.location='Dashboard.aspx';", true);
                            }
                            else if (DateTime.Parse(dr[3].ToString()).AddDays(-1) == System.DateTime.Today)
                            {
                                con.Close();
                                con.Open();
                                SqlCommand cmd2 = new SqlCommand("Update login_credentials SET count=@d1 WHERE ssn=@d2", con);
                                cmd2.Parameters.AddWithValue("d1", 0);
                                cmd2.Parameters.AddWithValue("d2", txtusrname.Text);
                                cmd2.ExecuteNonQuery();
                                con.Close();
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Password will expire in 1 days');window.location='Dashboard.aspx';", true);
                            }

                            else
                            {
                                con.Close();
                                con.Open();
                                SqlCommand cmd2 = new SqlCommand("Update login_credentials SET count=@d1 WHERE ssn=@d2", con);
                                cmd2.Parameters.AddWithValue("d1", 0);
                                cmd2.Parameters.AddWithValue("d2", txtusrname.Text);
                                cmd2.ExecuteNonQuery();
                                con.Close();
                                Server.Transfer("~/Dashboard.aspx");
                            }


                        }
                        else
                        {
                            if (dr[2].ToString() == "2")
                            {
                                check = 1;
                            }
                            else
                            {

                                check = 2;

                            }
                        }
                    }


                    else if ((dr[0].ToString()) == "1")
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Inactive Account');", true);
                    }

                    else if ((dr[0].ToString()) == "2")
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Account was deleted,Please create a new account');", true);
                    }
                    else if ((dr[0].ToString()) == "3")
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Account has been blocked');", true);
                    }
                }


                else
                {
                    txtusrname.Text = "";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Wrong username/password');", true);
                }
                con.Close();
                if (check == 1)
                {
                    con.Open();
                    SqlCommand cmd1 = new SqlCommand("UPDATE login_credentials SET count=0,status=3 WHERE ssn=@d1", con);
                    cmd1.Parameters.AddWithValue("d1", txtusrname.Text);
                    cmd1.ExecuteNonQuery();
                    con.Close();
                    txtusrname.Text = "";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Wrong Username/password');", true);
                }
                if (check == 2)
                {
                    con.Open();
                    SqlCommand cmd1 = new SqlCommand("UPDATE login_credentials SET count=count+1 WHERE ssn=@d1", con);
                    cmd1.Parameters.AddWithValue("d1", txtusrname.Text);
                    cmd1.ExecuteNonQuery();
                    con.Close();
                    txtusrname.Text = "";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Wrong Username/password');", true);
                }

            }
        }

 /*----------------------------------------------------------------------------------------------------------------------------------------------------------------*/      
        protected void frgtpassbtn_Click(object sender, EventArgs e)                                                                            //forgot password button
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Please contact Developer to activate your account');", true);
        }
    }
}