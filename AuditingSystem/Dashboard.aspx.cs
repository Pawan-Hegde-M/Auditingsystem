using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace AuditingSystem
{
    public partial class Dashboard : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["myconnection"].ConnectionString);
        private object t;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                String username = Session["username"].ToString();
                display(username);
            }                                                                                     //try catch block used to prevent unauthorized access
            catch
            {
                Response.Redirect("~/Login.aspx");
            }
            if (!IsPostBack)
            {
                getloc();
                getun();
                getty();
                getsv();
                getuser();
                getcomp();
            }


        }
        static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array                                                         //used for password hashing
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

        public void display(String username)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT l.role,l.status,e.fname,e.lname FROM login_credentials l ,employee e  WHERE e.ssn=l.ssn and l.ssn=@d1", con);
            cmd.Parameters.AddWithValue("d1", username);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {

                lblusrnm.Text = "Hello!!" + dr[2].ToString()+" "+dr[3].ToString();
                if (dr[0].ToString() == "1")
                {
                    btnaddcomp.Visible = false;
                    btndelloc.Visible = false;
                    btndelun.Visible = false;
                    btndelcp.Visible = false;
                    btndelsv.Visible = false;
                    btnaddus.Visible = false;
                    btneditus.Visible = false;
                    btnaddintc.Visible = false;                                       //used for page display according to privilege
                }
                if (dr[1].ToString() == "4")
                {

                    Panel11.Visible = true;
                    btndelloc.Visible = false;
                    btndelun.Visible = false;
                    btndelcp.Visible = false;
                    btndelsv.Visible = false;
                    btnaddus.Visible = false;
                    btneditus.Visible = false;
                    btnadddat.Visible = false;
                    btnresetp.Visible = false;
                    btnlog.Visible = false;
                    btnaddintc.Visible = false;
                    btnaddcomp.Visible = false;
                }
            }
            con.Close();
        }
        public void getcomp()
        {

            ddaddic1.Items.Clear();
            ddaddic1.Items.Add("SELECT");                                                  //used to fetch company to dropbox
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT company_id FROM client_data where auditor_ssn=@d1 ORDER BY company_id", con);
            cmd.Parameters.AddWithValue("d1", Session["username"].ToString());
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ddaddic1.Items.Add(dr[0].ToString());
            }
            con.Close();
            ddaddic4.Items.Clear();
            ddaddic4.Items.Add("SELECT");
            ddaddic4.Items.Add("Monthly");
            ddaddic4.Items.Add("Quarterly");
            ddaddic4.Items.Add("Halfyearly");
            ddaddic4.Items.Add("Yearly");
            ddaddic5.Items.Clear();
            ddaddic5.Items.Add("SELECT");
            ddaddic5.Items.Add("Very high");
            ddaddic5.Items.Add("High");
            ddaddic5.Items.Add("Medium");
            ddaddic5.Items.Add("Low");
            ddaddic5.Items.Add("Very low");

        }
        public void getloc()
        {
            ddloc.Items.Clear();
            ddloc.Items.Add("SELECT");                                                  //used to fetch location to dropbox
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT distinct i.location FROM client_data c,internal_point i where c.company_id=i.company_id and c.auditor_ssn=@d1", con);
            cmd.Parameters.AddWithValue("d1", Session["username"].ToString());
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ddloc.Items.Add(dr[0].ToString());
            }
            con.Close();
        }
      
        public void getun()
        {                                                                                 //used to fetch unit to dropbox
            ddun.Items.Clear();
            ddun.Items.Add("SELECT");
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT distinct i.unit FROM client_data c,internal_point i where c.company_id=i.company_id and c.auditor_ssn=@d1", con);
            cmd.Parameters.AddWithValue("d1", Session["username"].ToString());
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ddun.Items.Add(dr[0].ToString());


            }
            con.Close();
        }
        public void getty()
        {
            ddcp.Items.Clear();
            ddcp.Items.Add("SELECT");                                                       //used to fetch type to dropbox
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT distinct i.audit_type FROM client_data c,internal_point i where c.company_id=i.company_id and c.auditor_ssn=@d1", con);
            cmd.Parameters.AddWithValue("d1", Session["username"].ToString());
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ddcp.Items.Add(dr[0].ToString());
            }
            con.Close();
        }
        public void getsv()                                                                 //used to fetch severity to dropbox
        {
            ddsv.Items.Clear();
            ddsv.Items.Add("SELECT");
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT distinct i.severity FROM client_data c,internal_point i where c.company_id=i.company_id and c.auditor_ssn=@d1", con);
            cmd.Parameters.AddWithValue("d1", Session["username"].ToString());
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ddsv.Items.Add(dr[0].ToString());
            }
            con.Close();
        }
        public void getuser()
        {
            userdet1.Items.Clear();
            userdet1.Items.Add("SELECT");
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT ssn FROM employee where superssn=@d1", con);
            cmd.Parameters.AddWithValue("d1", Session["username"].ToString());
            SqlDataReader dr = cmd.ExecuteReader();                                                                         //used to get employee ssn to dropbox
            while (dr.Read())
            {
                if (dr[0].ToString() != Session["username"].ToString())
                {
                    userdet1.Items.Add(dr[0].ToString());
                }
            }
            con.Close();
        }
        public void hidepanel()
        {
            Panel1.Visible = false;
            Panel2.Visible = false;
            Panel5.Visible = false;                                             //used to hide panel
            Panel6.Visible = false;
            Panel7.Visible = false;
            Panel8.Visible = false;
            Panel9.Visible = false;
            Panel10.Visible = false;
            Panel11.Visible = false;
        }
/* --------------------------------------------------------------------------------------------------------------------------------*/
//panel1 activities
        protected void btnaddcomp_Click(object sender, EventArgs e)
        {
            hidepanel();
            Panel1.Visible = true;


        }
        protected void buttonadd1_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddaddcmp1.Text.Trim() == "" || ddaddcmp2.Text.Trim() == "" || ddaddcmp3.Text.Trim()== "" || ddaddcmp4.Text.Trim() =="")                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('All fields are mandatory');", true);
                }
                else
                {
                  
                        con.Open();
                        SqlCommand cmd = new SqlCommand("client", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@company_id", SqlDbType.VarChar).Value = ddaddcmp1.Text;
                        cmd.Parameters.Add("@type", SqlDbType.VarChar).Value =ddaddcmp3.Text ;
                        cmd.Parameters.Add("@company_name", SqlDbType.VarChar).Value = ddaddcmp2.Text;
                        cmd.Parameters.Add("@headquarters", SqlDbType.VarChar).Value = ddaddcmp4.Text;
                        cmd.Parameters.Add("@auditor", SqlDbType.VarChar).Value = Session["username"].ToString();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Updated successfully');", true);
                        ddaddcmp1.Text = "";
                        ddaddcmp2.Text = "";
                        ddaddcmp3.Text = "";
                        ddaddcmp4.Text = "";
                        getcomp();
                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Something went wrong');", true);
            }

        }
        protected void buttonex1_Click(object sender, EventArgs e)
        {
            ddaddcmp1.Text = "";
            ddaddcmp2.Text = "";
            ddaddcmp3.Text = "";
            ddaddcmp4.Text = "";
            hidepanel();
        }
/*-------------------------------------------------------------------------------------------------------------------------------*/
//panel2 activities
        protected void btnaddintc_Click(object sender, EventArgs e)
        {
            hidepanel();
            Panel2.Visible = true;
        }
        protected void buttonadd2_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddaddic1.SelectedIndex == 0 || ddaddic2.Text.Trim() == "" || ddaddic3.Text.Trim() == "" || ddaddic4.Text.Trim() == "" || ddaddic5.Text.Trim() == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('All fields are mandatory');", true);
                }
                {

                    con.Open();
                    SqlCommand cmd = new SqlCommand("internal", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@company_id", SqlDbType.VarChar).Value = ddaddic1.SelectedItem.ToString();
                    cmd.Parameters.Add("@location", SqlDbType.VarChar).Value = ddaddic2.Text;
                    cmd.Parameters.Add("@unit", SqlDbType.VarChar).Value = ddaddic3.Text;
                    cmd.Parameters.Add("@type", SqlDbType.VarChar).Value = ddaddic4.Text;
                    cmd.Parameters.Add("@severity", SqlDbType.VarChar).Value = ddaddic5.Text;
                    cmd.ExecuteNonQuery();
                    con.Close();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Updated successfully');", true);
                    ddaddic2.Text = "";
                    ddaddic3.Text = "";
                    ddaddic4.SelectedIndex = 0;
                    ddaddic5.SelectedIndex = 0;
                    getloc();
                    getun();
                    getty();
                    getsv();
                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Something went wrong');", true);
            }
        }

      
        protected void buttonex2_Click(object sender, EventArgs e)
        {
            ddaddic1.SelectedIndex = 0;
            ddaddic2.Text = "";
            ddaddic3.Text = "";
            ddaddic4.SelectedIndex=0;
            ddaddic5.SelectedIndex = 0;
            hidepanel();
        }
 /*---------------------------------------------------------------------------------------------------------------------------------*/
//panel5 activities
        protected void btneditus_Click(object sender, EventArgs e)
        {
            hidepanel();
            Panel5.Visible = true;
        }

        protected void userdet1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (userdet1.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Select username');", true);
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT e.fname,e.lname,e.city,e.salary,l.status from employee e,login_credentials l WHERE e.ssn=l.ssn and e.ssn=@d1", con);
                    cmd.Parameters.AddWithValue("d1", userdet1.SelectedItem.ToString());
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                       
                        userdet3.Text = dr[0].ToString();
                        userdet4.Text = dr[1].ToString();
                        userdet5.Text = dr[2].ToString();
                        userdet6.Text = dr[3].ToString();
                        if (dr[4].ToString() == "3")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Account has been blocked');", true);
                        }
                        else if (dr[4].ToString() == "4")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Password not set or expired');", true);
                        }
                        else
                        {
                            userdet2.SelectedValue = dr[4].ToString();
                        }
                    }
                    con.Close();
                }
                catch
                {
                }
            }
        }
        protected void buttonset2_Click(object sender, EventArgs e)
        {
            if (buttonset2.Text == "Edit")
            {
                userdet3.Enabled = true;
                userdet4.Enabled = true;
                userdet5.Enabled = true;
                userdet6.Enabled = true;
                buttonset2.Text = "Update";
            }
            else
            {
                if (userdet3.Text.Trim() == "" || userdet4.Text.Trim() == "" || userdet5.Text.Trim() == "" || userdet6.Text.Trim() == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('All fields are mandatory');", true);
                }
                else
                {
                    try
                    {
                        con.Open();
                        SqlCommand cmd2 = new SqlCommand("update employee set fname=@d1,lname=@d2,city=@d3,salary=@d4 where ssn=@d5", con);
                        cmd2.Parameters.AddWithValue("d1", userdet3.Text);
                        cmd2.Parameters.AddWithValue("d2", userdet4.Text);
                        cmd2.Parameters.AddWithValue("d3", userdet5.Text);
                        cmd2.Parameters.AddWithValue("d4", userdet6.Text);
                        cmd2.Parameters.AddWithValue("d5", userdet1.SelectedItem.ToString());
                        cmd2.ExecuteNonQuery();
                        con.Close();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Updated successfully');", true);
                        userdet3.Enabled =false;
                        userdet4.Enabled = false;
                        userdet5.Enabled = false;
                        userdet6.Enabled = false;
                        buttonset2.Text = "Edit";
                    }
                    catch
                    {
                        userdet3.Enabled = false;
                        userdet4.Enabled = false;
                        userdet5.Enabled = false;
                        userdet6.Enabled = false;
                        buttonset2.Text = "Edit";
                    }
                }
            }

        }
        protected void buttonset_Click(object sender, EventArgs e)
        {
            if (userdet1.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Please select username');", true);
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT status FROM login_credentials where ssn=@username", con);
                    cmd.Parameters.AddWithValue("username", userdet1.SelectedItem.ToString());
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        if (dr[0].ToString() == "2" || dr[0].ToString() == "4")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Cannot perform the operation');", true);
                            userdet1.SelectedIndex = 0;
                            userdet2.SelectedIndex = 0;
                            con.Close();
                        }
                        else
                        {
                            con.Close();
                            con.Open();
                            SqlCommand cmd2 = new SqlCommand("UPDATE login_credentials SET status=@d1 where ssn=@username", con);
                            cmd2.Parameters.AddWithValue("username", userdet1.SelectedItem.ToString());
                            cmd2.Parameters.AddWithValue("d1", userdet2.SelectedValue.ToString());
                            cmd2.ExecuteNonQuery();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Updated successfully');", true);
                            con.Close();

                        }
                    }
                }
                catch { }

                }
        }
        protected void buttonex5_Click(object sender, EventArgs e)
        {
            userdet1.SelectedIndex = 0;
            userdet2.SelectedIndex=0;
            userdet3.Text = "";
            userdet4.Text = "";
            userdet5.Text = "";
            userdet6.Text = "";
            userdet3.Enabled = false;
            userdet4.Enabled = false;
            userdet5.Enabled = false;
            userdet6.Enabled = false;
            buttonset2.Text = "Edit";
            hidepanel();
        }
 /*-----------------------------------------------------------------------------------------------------------------------------------*/
//panel6 activities
        protected void btndelloc_Click(object sender, EventArgs e)
        {
            hidepanel();
            Panel6.Visible = true;
        }
        protected void buttonupdt1_Click(object sender, EventArgs e)
        {
            if (ddloc.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Please select the location');", true);
            }
            else
            {
                if (buttonupdt1.Text == "update")
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT company_id from  client_data WHERE auditor_ssn=@d1", con);
                    cmd.Parameters.AddWithValue("d1", Session["username"].ToString());
                   SqlDataReader dr=cmd.ExecuteReader();
                    var dt = new DataTable();
                    dt.Load(dr);
                    con.Close();
                    foreach(DataRow row in dt.Rows)
                    {
                        try
                        {
                            con.Open();
                            SqlCommand cmd2 = new SqlCommand("UPDATE internal_point SET location=@d1 WHERE location=@d2 and company_id =@d3", con);
                            cmd2.Parameters.AddWithValue("d1", ddloctxt.Text);
                            cmd2.Parameters.AddWithValue("d2", ddloc.SelectedItem.ToString());
                            cmd2.Parameters.AddWithValue("d3", row[0].ToString());
                            cmd2.ExecuteNonQuery();
                            con.Close();
                        }
                        catch
                        {

                        }
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Updated successfully');", true);
                    buttonupdt1.Text = "Edit";
                    ddloctxt.Text = "";
                    ddloctxt.Enabled = false;
                    ddloc.SelectedIndex = 0;
                    getloc();
                }
                else
                {
                    buttonupdt1.Text = "update";
                    ddloctxt.Enabled = true;
                    ddloctxt.Text = ddloc.SelectedItem.ToString();
                }

            }
        }

        protected void buttondel1_Click(object sender, EventArgs e)
        {
            if (ddloc.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('please enter location name');", true);
            }
            else
            {

                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT company_id from  client_data WHERE auditor_ssn=@d1", con);
                cmd.Parameters.AddWithValue("d1", Session["username"].ToString());
                SqlDataReader dr = cmd.ExecuteReader();
                var dt = new DataTable();
                dt.Load(dr);
                con.Close();
                foreach (DataRow row in dt.Rows)
                {
                    try
                    {
                        con.Open();
                        SqlCommand cmd2 = new SqlCommand("delete from internal_point where location=@d2 and company_id =@d3", con);
                        cmd2.Parameters.AddWithValue("d2", ddloc.SelectedItem.ToString());
                        cmd2.Parameters.AddWithValue("d3", row[0].ToString());
                        cmd2.ExecuteNonQuery();
                        con.Close();
                    }
                    catch
                    {

                    }
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Location deleted successfully');", true);
                getloc();
                buttonupdt1.Text = "Edit";
                ddloctxt.Text = "";
                ddloctxt.Enabled = false;
                ddloc.SelectedIndex = 0;
            }
              

            }
            protected void buttonex6_Click(object sender, EventArgs e)
        {
            ddloctxt.Text = "";
            ddloctxt.Enabled = false;
            buttonupdt1.Text = "Edit";
            ddloc.SelectedIndex = 0;
            hidepanel();
        }
/*-----------------------------------------------------------------------------------------------------------------------------------*/
 //panel 7 activities

        protected void btndelun_Click(object sender, EventArgs e)
        {
            hidepanel();
            Panel7.Visible = true;
        }
        protected void buttondel2_Click(object sender, EventArgs e)
        {
            if (ddun.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('please enter unit name');", true);
            }
            else
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT company_id from  client_data WHERE auditor_ssn=@d1", con);
                cmd.Parameters.AddWithValue("d1", Session["username"].ToString());
                SqlDataReader dr = cmd.ExecuteReader();
                var dt = new DataTable();
                dt.Load(dr);
                con.Close();
                foreach (DataRow row in dt.Rows)
                {
                    try
                    {
                        con.Open();
                        SqlCommand cmd2 = new SqlCommand("delete from internal_point where unit=@d2 and company_id =@d3", con);
                        cmd2.Parameters.AddWithValue("d2", ddun.SelectedItem.ToString());
                        cmd2.Parameters.AddWithValue("d3", row[0].ToString());
                        cmd2.ExecuteNonQuery();
                        con.Close();
                    }
                    catch
                    {

                    }
                }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Unit deleted succesfully');", true);
                ddun.SelectedIndex = 0;
                getun();
                dduntxt.Text = "";
                ddun.SelectedIndex = 0;
                dduntxt.Enabled = false;
                buttonupdt2.Text = "Edit";
            }
        }
        protected void buttonupdt2_Click(object sender, EventArgs e)
        {
            if (ddun.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Please select the Unit');", true);
            }
            else
            {
                if (buttonupdt2.Text == "update")
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT company_id from  client_data WHERE auditor_ssn=@d1", con);
                    cmd.Parameters.AddWithValue("d1", Session["username"].ToString());
                    SqlDataReader dr = cmd.ExecuteReader();
                    var dt = new DataTable();
                    dt.Load(dr);
                    con.Close();
                    foreach (DataRow row in dt.Rows)
                    {
                        try
                        {
                            con.Open();
                            SqlCommand cmd2 = new SqlCommand("UPDATE internal_point SET unit=@d1 WHERE unit=@d2 and company_id =@d3", con);
                            cmd2.Parameters.AddWithValue("d1", dduntxt.Text);
                            cmd2.Parameters.AddWithValue("d2", ddun.SelectedItem.ToString());
                            cmd2.Parameters.AddWithValue("d3", row[0].ToString());
                            cmd2.ExecuteNonQuery();
                            con.Close();
                        }
                        catch
                        {

                        }
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Updated successfully');", true);
                    dduntxt.Text = "";
                    ddun.SelectedIndex = 0;
                    dduntxt.Enabled = false;
                    buttonupdt2.Text = "Edit";
                    getun();
                }
                else
                {
                    buttonupdt2.Text = "update";
                    dduntxt.Enabled = true;
                    dduntxt.Text = ddun.SelectedItem.ToString();
                }

            }
        }
        protected void buttonex7_Click(object sender, EventArgs e)
        {
            dduntxt.Text = "";
            dduntxt.Enabled = false;
            buttonupdt2.Text = "Edit";
            ddun.SelectedIndex = 0;
            hidepanel();

        }


/*--------------------------------------------------------------------------------------------------------------------------------*/
//panel8 activities
        protected void btndelcp_Click(object sender, EventArgs e)
        {
            hidepanel();
            Panel8.Visible = true;
        }
        protected void buttondel3_Click(object sender, EventArgs e)
        {
            if (ddcp.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('please enter Type name');", true);
            }
            else
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT company_id from  client_data WHERE auditor_ssn=@d1", con);
                cmd.Parameters.AddWithValue("d1", Session["username"].ToString());
                SqlDataReader dr = cmd.ExecuteReader();
                var dt = new DataTable();
                dt.Load(dr);
                con.Close();
                foreach (DataRow row in dt.Rows)
                {
                    try
                    {
                        con.Open();
                        SqlCommand cmd2 = new SqlCommand("delete from internal_point  WHERE audit_type=@d2 and company_id =@d3", con);
                        cmd2.Parameters.AddWithValue("d2", ddcp.SelectedItem.ToString());
                        cmd2.Parameters.AddWithValue("d3", row[0].ToString());
                        cmd2.ExecuteNonQuery();
                        con.Close();
                    }
                    catch
                    {

                    }
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Type deleted succesfully');", true);
                ddcp.SelectedIndex = 0;
                getty();
                ddcptxt.Text = "";
                ddcptxt.Enabled = false;
                buttonupdt4.Text = "Edit";
            }

        }
        protected void buttonupdt4_Click(object sender, EventArgs e)
        {
            if (ddcp.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Please select the type');", true);
            }
            else
            {
                if (buttonupdt4.Text == "update")
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT company_id from  client_data WHERE auditor_ssn=@d1", con);
                    cmd.Parameters.AddWithValue("d1", Session["username"].ToString());
                    SqlDataReader dr = cmd.ExecuteReader();
                    var dt = new DataTable();
                    dt.Load(dr);
                    con.Close();
                    foreach (DataRow row in dt.Rows)
                    {
                        try
                        {
                            con.Open();
                            SqlCommand cmd2 = new SqlCommand("UPDATE internal_point SET audit_type=@d1 WHERE audit_type=@d2 and company_id =@d3", con);
                            cmd2.Parameters.AddWithValue("d1", ddcptxt.Text);
                            cmd2.Parameters.AddWithValue("d2", ddcp.SelectedItem.ToString());
                            cmd2.Parameters.AddWithValue("d3", row[0].ToString());
                            cmd2.ExecuteNonQuery();
                            con.Close();
                        }
                        catch
                        {

                        }
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Updated successfully');", true);
                    buttonupdt4.Text = "Edit";
                    ddcptxt.Text = "";
                    ddcptxt.Enabled = false;
                    ddcp.SelectedIndex = 0;
                    getty();
                }
                else
                {
                    buttonupdt4.Text = "update";
                    ddcptxt.Enabled = true;
                    ddcptxt.Text = ddcp.SelectedItem.ToString();
                }

            }
        }
       
        protected void buttonex8_Click(object sender, EventArgs e)
        {
            ddcptxt.Text = "";
            ddcptxt.Enabled = false;
            buttonupdt4.Text = "Edit";
            ddcp.SelectedIndex = 0;
            hidepanel();
        }
/*-------------------------------------------------------------------------------------------------------------------------------*/
 //panel9 activities
        protected void btndelsv_Click(object sender, EventArgs e)
        {
            hidepanel();
            Panel9.Visible = true;
        }
        protected void buttondel4_Click(object sender, EventArgs e)
        {
            if (ddsv.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('please enter severity name');", true);
            }
            else
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT company_id from  client_data WHERE auditor_ssn=@d1", con);
                cmd.Parameters.AddWithValue("d1", Session["username"].ToString());
                SqlDataReader dr = cmd.ExecuteReader();
                var dt = new DataTable();
                dt.Load(dr);
                con.Close();
                foreach (DataRow row in dt.Rows)
                {
                    try
                    {
                        con.Open();
                        SqlCommand cmd2 = new SqlCommand("delete from internal_point WHERE severity=@d2 and company_id =@d3", con);
                        cmd2.Parameters.AddWithValue("d2", ddsv.SelectedItem.ToString());
                        cmd2.Parameters.AddWithValue("d3", row[0].ToString());
                        cmd2.ExecuteNonQuery();
                        con.Close();
                    }
                    catch
                    {

                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('severity deleted succesfully');", true);
                }
                ddsv.SelectedIndex = 0;
                getsv();
                ddsvtxt.Text = "";
                ddsvtxt.Enabled = false;
                buttonupdt3.Text = "Edit";
            }

        }
        protected void buttonupdt3_Click(object sender, EventArgs e)
        {
            if (ddsv.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Please select the Severity');", true);
            }
            else
            {
                if (buttonupdt3.Text == "update")
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT company_id from  client_data WHERE auditor_ssn=@d1", con);
                    cmd.Parameters.AddWithValue("d1", Session["username"].ToString());
                    SqlDataReader dr = cmd.ExecuteReader();
                    var dt = new DataTable();
                    dt.Load(dr);
                    con.Close();
                    foreach (DataRow row in dt.Rows)
                    {
                        try
                        {
                            con.Open();
                            SqlCommand cmd2 = new SqlCommand("UPDATE internal_point SET severity=@d1 WHERE severity=@d2 and company_id =@d3", con);
                            cmd2.Parameters.AddWithValue("d1", ddsvtxt.Text);
                            cmd2.Parameters.AddWithValue("d2", ddsv.SelectedItem.ToString());
                            cmd2.Parameters.AddWithValue("d3", row[0].ToString());
                            cmd2.ExecuteNonQuery();
                            con.Close();
                        }
                        catch
                        {

                        }
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Updated successfully');", true);
                    buttonupdt3.Text = "Edit";
                    ddsvtxt.Text = "";
                    ddsvtxt.Enabled = false;
                    ddsv.SelectedIndex = 0;
                    getsv();
                }
                
                else
                {
                    buttonupdt3.Text = "update";
                    ddsvtxt.Enabled = true;
                    ddsvtxt.Text = ddsv.SelectedItem.ToString();
                }

            }
        }

        protected void buttonex9_Click(object sender, EventArgs e)
        {
            ddsvtxt.Text = "";
            ddsvtxt.Enabled = false;
            buttonupdt3.Text = "Edit";
            ddsv.SelectedIndex = 0;
            hidepanel();
        }
 /*---------------------------------------------------------------------------------------------------------------------------*/
 //panel10 activities

        protected void btnaddus_Click(object sender, EventArgs e)
        {
            hidepanel();
            Panel10.Visible = true;
        }
        protected void btnregsub_Click(object sender, EventArgs e)
        {

            try
            {
                if (txtreguser.Text.Trim() == " " || txtregfname.Text.Trim() == "" || txtreglname.Text.Trim() == "" || txtreggen.SelectedIndex == 0 || TextBox1.Text.Trim() == "" || TextBox2.Text.Trim() == "" || txtregpass.Text.Trim() == "" || txtregconpass.Text.Trim() == "" || txtregprev.SelectedIndex == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('All fields are mandatory');", true);
                }
                else
                {
                    if (txtregpass.Text.Trim() == txtregconpass.Text.Trim())
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("employee_data", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ssn", SqlDbType.VarChar).Value = txtreguser.Text;
                        cmd.Parameters.Add("@fname", SqlDbType.VarChar).Value = txtregfname.Text;
                        cmd.Parameters.Add("@lname", SqlDbType.VarChar).Value = txtreglname.Text;
                        if (txtreggen.SelectedItem.ToString() == "Male")
                        {
                            cmd.Parameters.Add("@sex", SqlDbType.Char).Value = "m";
                        }
                        else
                        {
                            cmd.Parameters.Add("@sex", SqlDbType.Char).Value = "f";
                        }
                        cmd.Parameters.Add("@city", SqlDbType.VarChar).Value = TextBox1.Text;
                        cmd.Parameters.Add("@salary", SqlDbType.BigInt).Value = Int64.Parse(TextBox2.Text.Trim());
                        cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = ComputeSha256Hash(txtregpass.Text);
                        if (txtregprev.SelectedItem.ToString() == "Auditor")
                        {
                            cmd.Parameters.Add("@role", SqlDbType.Int).Value = 0;
                            cmd.Parameters.Add("@superssn", SqlDbType.VarChar).Value = txtreguser.Text;
                        }
                        else
                        {
                            cmd.Parameters.Add("@role", SqlDbType.Int).Value = 1;
                            cmd.Parameters.Add("@superssn", SqlDbType.VarChar).Value = Session["username"].ToString();
                        }
                        cmd.ExecuteNonQuery();
                        con.Close();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Updated successfully');", true);
                        txtreguser.Text = "";
                        txtreglname.Text = "";
                        txtregfname.Text = "";
                        txtreggen.SelectedIndex = 0;
                        txtregprev.SelectedIndex = 0;
                        TextBox1.Text = "";
                        TextBox2.Text = "";
                        getuser();

                    }
                    else if (txtregpass.Text.Trim() != txtregconpass.Text.Trim())
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Password does not match');", true);


                    }

                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Something went wrong');", true);
            }

        }
        protected void buttonex10_Click(object sender, EventArgs e)
        {
            txtreguser.Text = "";
            txtreglname.Text = "";
            txtregfname.Text = "";
            txtreggen.SelectedIndex = 0;
            txtregprev.SelectedIndex = 0;
            TextBox1.Text = "";
            TextBox2.Text = "";
            hidepanel();
        }
 /*----------------------------------------------------------------------------------------------------------------*/
//panel11 activities
        protected void btnresetp_Click(object sender, EventArgs e)
        {
            hidepanel();
            Panel11.Visible = true;
        }
        protected void resetbtn_Click(object sender, EventArgs e)
        {
            if (rstusername.Text.Trim() == "" || rstoldpass.Text.Trim() == "" || rstnewpass.Text.Trim() == "" || rstconpass.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('All fields are mandatory');", true);
            }
            else if (rstnewpass.Text != rstconpass.Text)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Passwords does not match');", true);

            }
            else if (rstnewpass.Text == rstoldpass.Text)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Passwords cannot be same');", true);

            }
            else
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT password FROM login_credentials WHERE ssn=@d1", con);
                cmd.Parameters.AddWithValue("d1", rstusername.Text);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    if (dr[0].ToString() == ComputeSha256Hash(rstoldpass.Text))
                    {
                        con.Close();
                        try
                        {
                            con.Open();
                            SqlCommand cmd2 = new SqlCommand("UPDATE login_credentials SET password=@pass,status=@d1,start_date=@d2,end_date=@d3 where ssn=@username", con);
                            cmd2.Parameters.AddWithValue("username", rstusername.Text);
                            cmd2.Parameters.AddWithValue("pass", ComputeSha256Hash(rstnewpass.Text));
                            cmd2.Parameters.AddWithValue("d1", 0);
                            cmd2.Parameters.AddWithValue("d2", System.DateTime.Today);
                            cmd2.Parameters.AddWithValue("d3", System.DateTime.Today.AddDays(45));
                            cmd2.ExecuteNonQuery();
                            con.Close();
                            Session["username"] = "";
                            Session.Clear();
                            Session.RemoveAll();
                            Cache.Remove("username");
                            Session.Abandon();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Password reset successfully');window.location='login.aspx';", true);

                        }
                        catch
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Something went wrong');", true);
                        }
                    }
                    else
                    {
                        con.Close();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Wrong Password');", true);
                    }
                }
                else
                {
                    con.Close();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Invalid username');", true);
                }
            }

        }

        protected void buttonex11_Click(object sender, EventArgs e)
        {
            hidepanel();
        }


/*------------------------------------------------------------------------------------------------------------------------------*/
        protected void btnadddat_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AddView.aspx");
        }

        protected void btnlog_Click(object sender, EventArgs e)
        {

            Session["username"] = "";
            Session.Clear();
            Session.RemoveAll();
            Cache.Remove("username");
            Session.Abandon();
            Server.Transfer("~/Login.aspx");
        } 
    }
}
