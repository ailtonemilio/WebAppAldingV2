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
        private string password = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                LoadGrid("");
            }
        }

        private void LoadGrid(string qrt)
        {
            if (String.IsNullOrEmpty(qrt))
                qrt = "SELECT [UserAldingId], [UserName], [UserPassword], [UserActive], [UserGender], [UserProvince], [RegistrationDate] FROM [UserAlding]";
            
            sdsUsers.SelectCommand = qrt;
            gvUsers.DataSourceID = "sdsUsers";
            gvUsers.DataBind();
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
                    
                    ua.UserActive = chbActive.Checked;
                    ua.UserGender = rblGender.SelectedValue;
                    ua.UserProvince = ddlProvince.SelectedValue;
                    ua.RegistrationDate = Convert.ToDateTime(txtRegistration.Text);

                    if (hdfUserId.Value == "0")
                    {
                        
                        ua.UserPassword = txtPassword.Text;
                        db.UserAlding.Add(ua);
                    }
                    else
                    {
                        int id = Convert.ToInt32(hdfUserId.Value);
                        ua.UserAldingId = id;
                        if (chbChangePassword.Checked)
                            ua.UserPassword = txtPassword.Text;
                        else
                            ua.UserPassword = password;
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
            password = "";
            chbActive.Checked = false;
            ddlProvince.SelectedValue = "";
            txtRegistration.Text = "";
            hdfUserId.Value = "0";
            PasswordAlert.Visible = false;
        }

        protected void gvUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idUser = Convert.ToInt32(((Label)gvUsers.Rows[gvUsers.SelectedRow.RowIndex].FindControl("lblIdUser")).Text);

            var User = db.UserAlding.Where(u => u.UserAldingId == idUser).FirstOrDefault();

            hdfUserId.Value = User.UserAldingId.ToString();
            txtUserName.Text = User.UserName;
            password = User.UserPassword;
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

            //Check empty field
            if (String.IsNullOrEmpty(pwd))
                return false;
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
            string query = "SELECT UserAldingId, UserName, UserPassword, UserActive, UserGender, UserProvince, RegistrationDate FROM UserAlding ";
            query = query + "WHERE UserActive = '" + chbSearch.Checked.ToString() + "' ";
            
            if(!String.IsNullOrEmpty(txtSearch.Text))
                query = query + " OR UserName LIKE '%" + txtSearch.Text + "%'";

            if (!String.IsNullOrEmpty(rblGenderSearch.SelectedValue))
                query = query + " OR UserGender = '" + rblGenderSearch.SelectedValue + "'";

            if (!String.IsNullOrEmpty(ddlSearchPro.SelectedValue))
                query = query + " OR UserProvince = '" + ddlSearchPro.SelectedValue + "'";

            if (!String.IsNullOrEmpty(txtRegistrationSearch.Text))
            {
                DateTime dt = Convert.ToDateTime(txtRegistrationSearch.Text);
                query = query + " OR RegistrationDate = '" + dt.ToString("yyyy/MM/dd") + "'";
            }

            LoadGrid(query);
        }

        protected void chbChangePassword_CheckedChanged(object sender, EventArgs e)
        {
            if (chbChangePassword.Checked)
                txtPassword.Enabled = true;
            else
                txtPassword.Enabled = false;
        }
    }
}