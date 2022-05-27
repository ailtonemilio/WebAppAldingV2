﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAppAldingV2.Model;


namespace WebAppAldingV2
{
    public partial class _Default : Page
    {
        private CRUDModel db = new CRUDModel();
        
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            UserAlding ua = new UserAlding();

            try
            {
                ua.UserName = txtUserName.Text;
                ua.UserPassword = txtPassword.Text;
                ua.UserActive = chbActive.Checked;
                ua.UserGender = rblGender.SelectedValue;
                ua.UserProvince = ddlProvince.SelectedValue;
                ua.RegistrationDate = Convert.ToDateTime(txtRegistration.Text);

                if (hdfUserId.Value == "0")
                {
                    db.UserAlding.Add(ua);
                }
                else 
                {
                    int id = Convert.ToInt32(hdfUserId.Value);
                    ua.UserAldingId = id;
                    var User = db.UserAlding.Find(id);

                    db.Entry(User).CurrentValues.SetValues(ua);
                }

                db.SaveChanges();

                sdsUsers.DataBind();
                gvUsers.DataBind();
                CleanFields();

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Successfully! User inserted.');", true);
            }
            catch (Exception ex)
            {
                string a = ex.ToString();
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Error in insert User');", true);
            }
            
        }

        protected void CleanFields()
        {
            txtUserName.Text = "";
            txtPassword.Text = "";
            chbActive.Checked = false;
            ddlProvince.SelectedValue = "";
            txtRegistration.Text = "";
            hdfUserId.Value = "0";
        }

        protected void gvUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idUser = Convert.ToInt32(((Label)gvUsers.Rows[gvUsers.SelectedRow.RowIndex].FindControl("lblIdUser")).Text);

            var User = db.UserAlding.Where(u => u.UserAldingId == idUser).FirstOrDefault();

            hdfUserId.Value = User.UserAldingId.ToString();
            txtUserName.Text = User.UserName;
            txtPassword.Text = User.UserPassword;
            DateTime dtreg = (DateTime)User.RegistrationDate;
            txtRegistration.Text = dtreg.ToString("MM/dd/yyyy");
            ddlProvince.SelectedValue = User.UserProvince;
            rblGender.SelectedValue = User.UserGender;
            chbActive.Checked = (bool)User.UserActive;
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            CleanFields();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (hdfUserId.Value == "0")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Select user!');", true);
            }
            else
            {
                int id = Convert.ToInt32(hdfUserId.Value);
                var User = db.UserAlding.Find(id);

                db.UserAlding.Remove(User);
                db.SaveChanges();

                sdsUsers.DataBind();
                gvUsers.DataBind();
                CleanFields();
            }
        }
    }
}