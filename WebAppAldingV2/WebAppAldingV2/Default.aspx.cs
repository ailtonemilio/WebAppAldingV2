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
        
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidatePassword(txtPassword.Text))
            {
                PasswordAlert.Visible = true;
            }
            else
            {
                try
                {
                    UserAlding ua = new UserAlding();
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

        public bool ValidatePassword(string pwd, int minLength = 8, int numUpper = 2, int numNumbers = 1, int numSpecial = 2)
        {

            // Replace [A-Z] with \p{Lu}, to allow for Unicode uppercase letters.
            var upper = new System.Text.RegularExpressions.Regex("[A-Z]");
            var number = new System.Text.RegularExpressions.Regex("[0-9]");
            // Special is "none of the above".
            var special = new System.Text.RegularExpressions.Regex("[^a-zA-Z0-9]");

            // Check the length.
            if (pwd.Length < minLength)
                return false;
            // Check for minimum number of occurrences.
            if (upper.Matches(pwd).Count < numUpper)
                return false;
            if (number.Matches(pwd).Count < numNumbers)
                return false;
            if (special.Matches(pwd).Count < numSpecial)
                return false;

            // Passed all checks.
            return true;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        { 
        
        }

    }
}