using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AuditingSystem
{
    public partial class AddView : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["myconnection"].ConnectionString);
        String super;
        protected void Page_Load(object sender, EventArgs e)
        {
           
            try
            {
                String username = Session["username"].ToString();
                superssn(username);
            }                                                                                     //try catch block used to prevent unauthorized access
            catch
            {
                Response.Redirect("~/Login.aspx");
            }
            if (!IsPostBack)
            {

                getcomp();
              
            }
        }

        public void superssn(String username)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select superssn from employee where ssn=@d1", con);           //used to fetch superssn
            cmd.Parameters.AddWithValue("d1",username);
            SqlDataReader dr= cmd.ExecuteReader();
            if (dr.Read())
            {
                super = dr[0].ToString();
            }
            con.Close();
        }
        public void getcomp()
        {
            ddaddcid.Items.Clear();
            ddaddcid.Items.Add("SELECT"); //used to fetch company to dropbox
            ddunit_search.Items.Clear();
            ddunit_search.Items.Add("SELECT");
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT company_id FROM client_data where auditor_ssn=@d1 ORDER BY company_id", con);
            cmd.Parameters.AddWithValue("d1",super);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ddaddcid.Items.Add(dr[0].ToString());
                ddunit_search.Items.Add(dr[0].ToString());
            }
            con.Close();
        }
        public void DisplayGrid()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from audit_data where date=@d1 and company_id=@d2 and location=@d3 and unit=@d4 and type=@d5 and severity=@d6 and auditor_id=@d7", con);
            cmd.Parameters.AddWithValue("d1", ddadddt.Text);
            cmd.Parameters.AddWithValue("d2", ddaddcid.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("d3", ddaddloc.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("d4", ddaddun.SelectedItem.ToString());                      //used to display gridview1
            cmd.Parameters.AddWithValue("d5", ddaddty.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("d6", ddaddsv.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("d7", super);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            GridView1.DataSource = ds;
            GridView1.DataBind();
            con.Close();
            GridView1.Visible = true;


        }
        public void insert()                                                                            //used for inserting values
        {

            if (ddadddt.Text.Trim() == "" || ddaddloc.SelectedIndex == 0 || ddaddun.SelectedIndex == 0 || ddaddty.SelectedIndex == 0 || ddaddsv.SelectedIndex == 0 || ddaddty.SelectedIndex == 0 || ddaddrm.Text.Trim() == "" || ddaddrec.Text.Trim() == "" || ddaddob.Text.Trim() == "" || ddaddcs.SelectedIndex == 0 || ddaddcd.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('All Fields are mandatory');", true);
            }

            else
            {
                try
                {
                    con.Open();
                    string query = "";
                    if (btnsac.Text == "Submit and Continue")
                    {
                        query = "INSERT INTO audit_data (date,company_id,location,unit,type,severity,remarks,recommendation,observation,closure_status,closure_date,auditor_id) VALUES(@d1,@d12,@d2,@d3,@d4,@d5,@d6,@d7,@d8,@d9,@d10,@d11)";
                    }
                    else
                    {
                        query = "UPDATE audit_data set  date=@d1,company_id=@d12,location=@d2,unit=@d3,type=@d4,severity=@d5,remarks=@d6,recommendation=@d7,observation=@d8,closure_status=@d9,closure_date=@d10,auditor_id=@d11 where date=@d1 and company_id=@d12 and location=@d2 and unit=@d3 and type=@d4 and severity=@d5 and remarks='" + ViewState["ddaddrm"].ToString() + "' and recommendation='" + ViewState["ddaddrec"].ToString() + "' and observation='" + ViewState["ddaddob"].ToString() + "' and closure_status='" + ViewState["ddaddcs"].ToString() + "' and closure_date='" + ViewState["ddaddcd"].ToString() + "' and auditor_id=@d11";
                    }
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("d1", ddadddt.Text.ToString());
                    cmd.Parameters.AddWithValue("d2", ddaddloc.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("d3", ddaddun.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("d4", ddaddty.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("d5", ddaddsv.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("d6", ddaddrm.Text);
                    cmd.Parameters.AddWithValue("d7", ddaddrec.Text);
                    cmd.Parameters.AddWithValue("d8", ddaddob.Text);
                    cmd.Parameters.AddWithValue("d9", ddaddcs.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("d10", ddaddcd.Text);
                    cmd.Parameters.AddWithValue("d11", super);
                    cmd.Parameters.AddWithValue("d12", ddaddcid.SelectedItem.ToString());
                    cmd.ExecuteNonQuery();
                    con.Close();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('data entered succesfully');", true);
                    DisplayGrid();
                }
                catch
                {

                }
            }
        }

        public void TabViewData()                                                               //used to display gridview2
        {

            foreach (ListItem item in ddChkBoxPending.Items)
            {
                if (item.Selected)
                {
                    GridView2.Columns[int.Parse(item.Value)].Visible = false;
                }
                else
                {
                    GridView2.Columns[int.Parse(item.Value)].Visible = true;
                }
            }

            string loc = "";
            foreach (ListItem item in ddloc_search.Items)
            {
                if (item.Selected)
                {
                    if (loc == "")
                    {
                        loc = "('" + item.Value + "'";
                    }
                    else
                    {
                        loc = loc + ",'" + item.Value + "'";
                    }
                }

            }

            string query = "";
            if (txtfrom.Text != "" && txtto.Text != "")
            {
                if (loc == "")
                {
                    query = "Select * from audit_data where company_id LIKE @unit and cast(closure_date as date) >= '" + txtfrom.Text + "' and cast(closure_date as date) <= '" + txtto.Text + "' order by closure_date";
                }
                else
                {
                    loc = loc + ")";
                    query = "Select * from audit_data where location in " + loc + " and company_id LIKE @unit and cast(closure_date as date) >= '" + txtfrom.Text + "' and cast(closure_date as date) <= '" + txtto.Text + "' order by closure_date";
                }
            }
            else
            {
                if (loc == "")
                {
                    query = "Select * from audit_data" +
                        " where company_id LIKE @unit";
                }
                else
                {
                    loc = loc + ")";
                    query = "Select * from audit_data where location in " + loc + " and company_id LIKE @unit";
                }

            }

            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
             cmd.Parameters.AddWithValue("unit", ddunit_search.SelectedItem.ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            GridView2.DataSource = ds;
            GridView2.DataBind();
            GridView2.Visible = true;
            con.Close();

        }

 //====================================================================================================================================================================================================================
 //add data activity    
        protected void ddaddcid_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddaddloc.Items.Clear();
            ddaddloc.Items.Add("SELECT");
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT distinct location FROM internal_point where company_id=@d1", con);
            cmd.Parameters.AddWithValue("d1", ddaddcid.SelectedItem.ToString());
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ddaddloc.Items.Add(dr[0].ToString());
            }
            con.Close(); 
        }
        protected void ddaddloc_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddaddun.Items.Clear();
            ddaddun.Items.Add("SELECT");
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT distinct unit FROM internal_point where location=@d1 and company_id=@d2", con);
            cmd.Parameters.AddWithValue("d2", ddaddcid.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("d1", ddaddloc.SelectedItem.ToString());
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ddaddun.Items.Add(dr[0].ToString());
            }
            con.Close();
        }

        protected void ddaddun_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddaddty.Items.Clear();
            ddaddty.Items.Add("SELECT");
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT distinct audit_type FROM internal_point where location=@d1 and unit=@d2 and company_id=@d3", con);
            cmd.Parameters.AddWithValue("d3", ddaddcid.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("d2", ddaddun.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("d1", ddaddloc.SelectedItem.ToString());
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ddaddty.Items.Add(dr[0].ToString());
            }
            con.Close();
        }

        protected void ddaddty_SelectedIndexChanged1(object sender, EventArgs e)
        {
            ddaddsv.Items.Clear();
            ddaddsv.Items.Add("SELECT");
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT distinct severity FROM internal_point where audit_type=@d4 and location=@d1 and unit=@d2 and company_id=@d3", con);
            cmd.Parameters.AddWithValue("d3", ddaddcid.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("d2", ddaddun.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("d1", ddaddloc.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("d4", ddaddty.SelectedItem.ToString());
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ddaddsv.Items.Add(dr[0].ToString());
            }
            con.Close();
        }

        protected void btnsac_Click(object sender, EventArgs e)
        {
            insert();
            ddaddob.Text = "";
            ddaddrm.Text = "";
            ddaddrec.Text = "";
            ddaddcs.SelectedIndex = 0;
            ddaddcd.Text = "";
            ddadddt.Enabled = false;
            ddaddcid.Enabled = false;
            ddaddloc.Enabled = false;
            ddaddun.Enabled = false;
            ddaddsv.Enabled = false;
            ddaddty.Enabled = false;
            DisplayGrid();

            if(btnsac.Text == "Update and Continue")
             {
                 btnsac.Text = "Submit and Continue";
                 btnsar.Text = "Submit and Reset";
                 BtnCancel.Visible = false;
             }
        }
        protected void btnsar_Click(object sender, EventArgs e)
        {
            insert();
            ddadddt.Text = "";
            ddaddloc.Items.Clear();
            ddaddcid.SelectedIndex = 0;
            ddaddty.Items.Clear();
            ddaddun.Items.Clear();
            ddaddsv.Items.Clear();
            ddaddrm.Text = "";
            ddaddob.Text = "";
            ddaddrec.Text = "";
        
            ddaddcs.SelectedIndex = 0;
            ddaddcd.Text = "";
            GridView1.Visible = false;
            ddadddt.Enabled = true;
            ddaddloc.Enabled = true;
            ddaddun.Enabled = true;
            ddaddty.Enabled = true;
            ddaddsv.Enabled = true;
            ddaddcid.Enabled = true;
            if (btnsar.Text == "Update and Reset")
            {
                btnsac.Text = "Submit and Continue";
                btnsar.Text = "Submit and Reset";
                BtnCancel.Visible = false;
            }
          
        }
        protected void btnclear_Click(object sender, EventArgs e)
        {
             ddadddt.Text = "";
             ddaddcid.SelectedIndex=0;
             ddaddun.Items.Clear();
             ddaddloc.Items.Clear();
             ddaddty.Items.Clear();
             ddaddsv.Items.Clear();
             ddaddrm.Text="";
             ddaddob.Text = "";
             ddaddrec.Text = "";

             ddaddcs.SelectedIndex= 0;
             ddaddcd.Text = "";
            GridView1.Visible = false;
             ddadddt.Enabled = true;
             ddaddloc.Enabled = true;
             ddaddun.Enabled = true;
             ddaddcid.Enabled = true;
             ddaddsv.Enabled = true;
             ddaddty.Enabled = true;
        }

        protected void BtnUpdate_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton btn = (ImageButton)sender;
             GridViewRow grv = (GridViewRow)btn.NamingContainer;
             con.Open();
             SqlCommand cmd = new SqlCommand("Select * from audit_data where ID=@d1", con);
             cmd.Parameters.AddWithValue("d1", grv.Cells[0].Text);
             SqlDataReader dr = cmd.ExecuteReader();
             while (dr.Read())
             {
                 ddadddt.Text = dr[1].ToString();
                ddaddcid.Text = dr[2].ToString();
                 ddaddun.Text = dr[4].ToString();
                 ddaddloc.Text = dr[3].ToString();
                 ddaddty.Text = dr[5].ToString();
                 ddaddsv.Text = dr[6].ToString();
                 ddaddrm.Text = dr[7].ToString();
                 ddaddrec.Text = dr[8].ToString();
                 ddaddob.Text = dr[9].ToString();
       
                 ddaddcs.Text = dr[10].ToString();
                 ddaddcd.Text = dr[11].ToString();
                ViewState["ddaddrm"] = dr[7].ToString();
                ViewState["ddaddrec"] = dr[8].ToString();
                ViewState["ddaddob"] = dr[9].ToString();
                ViewState["ddaddcs"] = dr[10].ToString();
                ViewState["ddaddcd"] = dr[11].ToString();
            }
            con.Close();
            btnsac.Text = "Update and Continue";
             btnsar.Text = "Update and Reset";
             BtnCancel.Visible = true;
        }

        protected void BtnDelete_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton btn = (ImageButton)sender;
            GridViewRow grv = (GridViewRow)btn.NamingContainer;
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Delete FROM audit_data WHERE ID=@d1", con);
                cmd.Parameters.AddWithValue("d1", grv.Cells[0].Text);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch
            {

            }
            DisplayGrid();
        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            ddaddrm.Text = "";
            ddaddob.Text = "";
            ddaddrec.Text = "";

            ddaddcs.SelectedIndex = 0;
            ddaddcd.Text = "";
            btnsac.Text = "Submit and Continue";
            btnsar.Text = "Submit and Reset";
            BtnCancel.Visible = false;
        }

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Dashboard.aspx");
        }
//====================================================================================================================================================================
        protected void ddunit_search_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddloc_search.Items.Clear();
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT distinct location FROM internal_point where company_id=@d1", con);
            cmd.Parameters.AddWithValue("d1", ddunit_search.SelectedItem.ToString());
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ddloc_search.Items.Add(dr[0].ToString());
            }
            con.Close();
        }
        protected void BtnSearch_Click(object sender, EventArgs e)
        {

            if (ddunit_search.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Please select company');", true);
            }
            else
            {
                TabViewData();
            }
        }

        protected void BtnClearSearch_Click(object sender, EventArgs e)
        {
            txtfrom.Text = "";
            txtto.Text = "";
            ddunit_search.SelectedIndex = 0;
            ddloc_search.Items.Clear();
            GridView2.Visible = false;
            ddChkBoxPending.SelectedIndex = 0;
        }

        protected void Btncsvexprt_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
             //   GridView1.AllowPaging = false;
             //   GridView2.BindGrid();

                GridView2.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in GridView2.HeaderRow.Cells)
                {
                    cell.BackColor = GridView2.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in GridView2.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = GridView2.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = GridView2.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                GridView2.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

    
    }
}