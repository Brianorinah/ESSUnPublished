using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HRPortal
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void signin_Click(object sender, EventArgs e)
        {
            try
            {
                string tUsername = email.Text.Trim();
                string mUsername = tUsername.ToLower();
                //string mUsername = tUsername;
                string mPassword = password.Text.Trim();
                string tpassword = "";
                string temail = "";
                var changePass = "";

                string allData = new Config().ObjNav1().loginUser(mUsername, mPassword);
                String[] allInfo = allData.Split(new string[] { "::::" }, StringSplitOptions.RemoveEmptyEntries);
                if (allInfo != null)
                {
                    foreach (var item in allInfo)
                    {

                        String[] oneItem = item.Split(new string[] { "****" }, StringSplitOptions.None);
                        if (oneItem[0] == "danger")
                        {
                            feedback.InnerHtml = "<div class='alert alert-" + oneItem[0] + "'>" + oneItem[1] + " <a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a></div>";
                        }
                        else
                        {
                            Session["name"] = oneItem[1];
                            Session["idNo"] = oneItem[2];
                            Session["gender"] = oneItem[3];
                            Session["employeeNo"] = oneItem[4];
                            //Session["Directorate"] = oneItem[10];
                            Session["Department"] = oneItem[9];
                            Session["username"] = oneItem[1];
                            Session["EmailAddress"] = oneItem[7];
                            Session["Password"] = oneItem[5];
                            Session["PhoneNo"] = "";
                            changePass = oneItem[6];

                            if (changePass == "Yes")
                            {
                                Response.Redirect("Dashboard.aspx");
                            }
                            else if (changePass == "No" || string.IsNullOrEmpty(changePass))
                            {
                                Response.Redirect("ChangePassword.aspx");
                            }
                        }
                    }
                }
                else
                {
                    feedback.InnerHtml = "<div class='alert alert-danger'>Kindly enter correct Employee Number and Password <a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a></div>";
                }
            }
            catch(Exception ex)
            {
                feedback.InnerHtml = "<div class='alert alert-danger'>"+ ex.Message +"<a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a></div>";
            }


        }

        [System.Web.Services.WebMethod(EnableSession = false)]
        public static string ChangePassword(string temailAddres, string tOldPassword, string tNewPassword, string tconfirmNewPassword)
        {
            var results = (dynamic)null;
            try
            {
                var nav = new Config().ObjNav();
                var status = nav.changePassword(temailAddres, tOldPassword, tNewPassword, tconfirmNewPassword);
                var info = status.Split('*');
                if (info[0] == "success")
                {
                    results = info[0];
                }
                else
                {
                    results = info[1];
                }

            }
            catch (Exception ex)
            {
                results = ex.Message;
            }
            return results;

        }
        [System.Web.Services.WebMethod(EnableSession = false)]
        public static string ChangePassword1()
        {
            return "";
        }
    }
}