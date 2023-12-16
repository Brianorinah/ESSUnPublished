using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HRPortal
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["employeeNo"] == null)
            {

                Response.Redirect("Login.aspx");
            }
            else
            {
                txtemailaddress.Text = Session["EmailAddress"].ToString();
                txtoldpassword.Text = Session["Password"].ToString();
                empId.Text = Session["employeeNo"].ToString();
            }

        }

        protected void changePass_Click(object sender, EventArgs e)
        {
            try
            {
                string tempId = empId.Text;
                string tOldPassword = txtoldpassword.Text;
                string tNewPassword = txtnewpassword.Text;
                string tconfirmNewPassword = txtconfirmnewpassword.Text;

                var nav = new Config().ObjNav();
                var status = nav.changePassword(tempId, tOldPassword, tNewPassword, tconfirmNewPassword);
                var info = status.Split('*');
                if (info[0] == "success")
                {
                    passwordFeedback.InnerHtml = "<div class='alert alert-" + info[0] + "'>" + info[1] + "<a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a></div>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "redirectJS", "setTimeout(function() { window.location.replace('Login.aspx') }, 5000);", true);
                    
                }
                else
                {
                    passwordFeedback.InnerHtml = "<div class='alert alert-" + info[0] + "'>" + info[1] + "<a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a></div>";
                   
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}