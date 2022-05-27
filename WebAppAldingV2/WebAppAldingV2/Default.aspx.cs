using System;
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
        UserAlding ua = new UserAlding();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ua.UserName = txtUserName.Text;
                ua.UserPassword = txtPassword.Text;
                ua.UserActive = chbActive.Checked;
                ua.UserGender = rblGender.SelectedValue;
                ua.UserProvince = ddlProvince.SelectedValue;
                ua.RegistrationDate = Convert.ToDateTime(txtRegistration.Text);
                db.UserAlding.Add(ua);
                db.SaveChanges();

                sdsUsers.DataBind();
                gvUsers.DataBind();
            }
            catch (Exception ex)
            {
                string a = ex.ToString();
            }
            
        }

        protected void CleanFields()
        {
            txtUserName.Text = "";
            txtPassword.Text = "";
            chbActive.Checked = false;
            ddlProvince.SelectedValue = "0";
        }
    }
}