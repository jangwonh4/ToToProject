﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace ToToProject
{
    public partial class Landing : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Comp229TeamProjectConnectionString"].ToString());
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlCommand comm = new SqlCommand("SELECT * FROM Cars WHERE CarStatus = 'In Stock'", conn);

            conn.Open();
            SqlDataReader reader = comm.ExecuteReader();
            GridView1.DataSource = reader;
            GridView1.DataBind();
            reader.Close();
            conn.Close();
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

            GridViewRow row = GridView1.SelectedRow;
            string CAR = row.Cells[1].Text;
            Response.Redirect("CarList.aspx?Name=" + CAR);
        }

        protected void btnlogin_Click(object sender, EventArgs e)
        {
            string query = "select count(*) from Customer where Username='" + txtUser.Text + "' and pass='" + txtPass.Text + "' ";
            SqlCommand cmd = new SqlCommand(query, conn);
            string output = cmd.ExecuteScalar().ToString();

            if(output=="1")
            {
                //create a sesstion
                Session["user"] = txtUser.Text;
                Response.Write("Log in");
            }
            else
            {
                Response.Write("Login Faild");
            }
        }
    }
}