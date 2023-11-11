using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRPortal.Models;
using System.IO;
using System.Configuration;

namespace HRPortal
{
    public partial class Imprest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string allDepartments = new Config().ObjNav1().fnGetDepartments(0, false, 1);
                List<DepartmentsModel> dpts = new List<DepartmentsModel>();
                String[] info51 = allDepartments.Split(new string[] { "::::" }, StringSplitOptions.RemoveEmptyEntries);
                if (info51 != null)
                {
                    foreach (var allInfo in info51)
                    {
                        String[] arr = allInfo.Split('*');

                        DepartmentsModel mdl = new DepartmentsModel();
                        mdl.Name = arr[1];
                        mdl.Code = arr[0];
                        dpts.Add(mdl);

                    }
                }

                dpt.DataSource = dpts;
                dpt.DataTextField = "Name";
                dpt.DataValueField = "Code";
                dpt.DataBind();
                dpt.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--select--", ""));

                string allTravelTypes = new Config().ObjNav1().fntransactionCodes(2);
                List<TransactionTypesModel> trvls = new List<TransactionTypesModel>();
                String[] info511 = allTravelTypes.Split(new string[] { "::::" }, StringSplitOptions.RemoveEmptyEntries);
                if (info511 != null)
                {
                    foreach (var allInfo in info511)
                    {
                        String[] arr = allInfo.Split('*');

                        TransactionTypesModel mdl = new TransactionTypesModel();
                        mdl.description = arr[1];
                        mdl.code = arr[0];
                        trvls.Add(mdl);

                    }
                }

                traveltype.DataSource = trvls;
                traveltype.DataTextField = "description";
                traveltype.DataValueField = "code";
                traveltype.DataBind();
                traveltype.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--select--", ""));

                String employeeNo = Convert.ToString(Session["employeeNo"]);
                string requisitionNo = Request.QueryString["docNo"];

                if (!string.IsNullOrEmpty(requisitionNo))
                {
                    var request = new Config().ObjNav1().fnImprestHeaderSingle(employeeNo, requisitionNo);
                    String[] info = request.Split(new string[] { "::::" }, StringSplitOptions.RemoveEmptyEntries);
                    if (info != null)
                    {
                        foreach (var allinfo in info)
                        {
                            String[] arr = allinfo.Split('*');
                            dpt.SelectedValue = arr[9];
                            dptMach(arr[9]);
                            bsnCode.SelectedValue = arr[8];
                            airCraftCode.SelectedValue = arr[10];
                            vhcleCode.SelectedValue = arr[11];
                            description.Text = arr[5];
                            currCode1.SelectedValue = arr[7];
                            destinationNarration.Text = arr[6];
                            dateFrom.Text = arr[1];
                            dateTo.Text = arr[2];



                        }
                    }
                }
            }
        }
        public void dptMach(string tdpt)
        {
            string allBusinessCodes = new Config().ObjNav1().fnGetDepartments2(0, false, 2, tdpt);
            List<DepartmentsModel> dpts2 = new List<DepartmentsModel>();
            String[] info52 = allBusinessCodes.Split(new string[] { "::::" }, StringSplitOptions.RemoveEmptyEntries);
            if (info52 != null)
            {
                foreach (var allInfo in info52)
                {
                    String[] arr = allInfo.Split('*');

                    DepartmentsModel mdl = new DepartmentsModel();
                    mdl.Name = arr[1];
                    mdl.Code = arr[0];
                    dpts2.Add(mdl);

                }
            }

            bsnCode.DataSource = dpts2;
            bsnCode.DataTextField = "Name";
            bsnCode.DataValueField = "Code";
            bsnCode.DataBind();
            bsnCode.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--select--", ""));

            string allAirCraftCodes = new Config().ObjNav1().fnGetDepartments2(0, false, 3, tdpt);
            List<DepartmentsModel> dpts3 = new List<DepartmentsModel>();
            String[] info53 = allAirCraftCodes.Split(new string[] { "::::" }, StringSplitOptions.RemoveEmptyEntries);
            if (info53 != null)
            {
                foreach (var allInfo in info53)
                {
                    String[] arr = allInfo.Split('*');

                    DepartmentsModel mdl = new DepartmentsModel();
                    mdl.Name = arr[1];
                    mdl.Code = arr[0];
                    dpts3.Add(mdl);

                }
            }

            airCraftCode.DataSource = dpts3;
            airCraftCode.DataTextField = "Name";
            airCraftCode.DataValueField = "Code";
            airCraftCode.DataBind();
            airCraftCode.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--select--", tdpt));

            string allVehicles = new Config().ObjNav1().fnGetDepartments2(0, false, 5, "");
            List<DepartmentsModel> dpts4 = new List<DepartmentsModel>();
            String[] info54 = allVehicles.Split(new string[] { "::::" }, StringSplitOptions.RemoveEmptyEntries);
            if (info54 != null)
            {
                foreach (var allInfo in info54)
                {
                    String[] arr = allInfo.Split('*');

                    DepartmentsModel mdl = new DepartmentsModel();
                    mdl.Name = arr[1];
                    mdl.Code = arr[0];
                    dpts4.Add(mdl);

                }
            }

            vhcleCode.DataSource = dpts4;
            vhcleCode.DataTextField = "Name";
            vhcleCode.DataValueField = "Code";
            vhcleCode.DataBind();
            vhcleCode.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--select--", ""));
        }

        protected void addGeneralDetails_Click(object sender, EventArgs e)
        {
            String requisitionNo = "";
            Boolean newRequisition = false;
            String employeeNo = Convert.ToString(Session["employeeNo"]);
            string tdescription = description.Text.Trim();
            string tdestinationNarration = destinationNarration.Text.Trim();
            DateTime tdateFrom = Convert.ToDateTime(dateFrom.Text.Trim());
            DateTime tdateTo = Convert.ToDateTime(dateTo.Text.Trim());
            string tcurrCode1 = currCode1.SelectedValue.Trim();
            string tdpt = dpt.SelectedValue.Trim();
            string tbsnCode = bsnCode.SelectedValue.Trim();
            string tvhcleCode = vhcleCode.SelectedValue.Trim();
            string tairCraftCode = airCraftCode.SelectedValue.Trim();

            try
            {
                requisitionNo = Request.QueryString["docNo"];
                if (String.IsNullOrEmpty(requisitionNo))
                {
                    requisitionNo = "";
                    newRequisition = true;
                }
            }
            catch (Exception)
            {
                newRequisition = true;
                requisitionNo = "";
            }


            var nav = new Config().ObjNav();

            String status = nav.createImprestHeader(employeeNo, requisitionNo, tdescription, tdateFrom, tdateTo, tdescription, tbsnCode, tdpt, tairCraftCode, tvhcleCode, tcurrCode1);
            String[] info = status.Split('*');
            if (info[0] == "success")
            {
                generalFeedback.InnerHtml = "<div class='alert alert-" + info[0] + "'>" + info[1] + "<a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a></div>";
                Response.Redirect("Imprest.aspx?step=2&&docNo=" + info[2]);

            }
            else
            {
                generalFeedback.InnerHtml = "<div class='alert alert-danger'>" + info[1] + " <a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a></div>";
            }



        }
        protected void addImprestLine_Click(object sender, EventArgs e)
        {
            string ttraveltype = traveltype.SelectedValue.Trim();
            decimal ttxtamount = Convert.ToDecimal(txtamount.Text.Trim());
            decimal tquantity = Convert.ToDecimal(quantity.Text.Trim());
            string requisitionNo = Request.QueryString["docNo"];

            var nav = new Config().ObjNav();

            String status = nav.createImprestLine(requisitionNo, ttraveltype, ttxtamount, tquantity);
            String[] info = status.Split('*');
            if (info[0] == "success")
            {
                linesFeedback.InnerHtml = "<div class='alert alert-" + info[0] + "'>" + info[1] + "<a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a></div>";


            }
            else
            {
                linesFeedback.InnerHtml = "<div class='alert alert-danger'>" + info[1] + " <a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a></div>";
            }
        }

        protected void deleteFile_Click(object sender, EventArgs e)
        {
            try
            {
                String tFileName = fileName.Text.Trim();
                String filesFolder = ConfigurationManager.AppSettings["FilesLocation"] + "ImprestRequisition/";
                String imprestNo = Request.QueryString["docNo"];
                imprestNo = imprestNo.Replace('/', '_');
                imprestNo = imprestNo.Replace(':', '_');
                String documentDirectory = filesFolder + imprestNo + "/";
                String myFile = documentDirectory + tFileName;
                if (File.Exists(myFile))
                {
                    File.Delete(myFile);
                    if (File.Exists(myFile))
                    {
                        documentsfeedback.InnerHtml = "<div class='alert alert-danger'>The file could not be deleted <a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a></div>";
                    }
                    else
                    {
                        documentsfeedback.InnerHtml = "<div class='alert alert-success'>The file was successfully deleted <a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a></div>";
                    }
                }
                else
                {
                    documentsfeedback.InnerHtml = "<div class='alert alert-danger'>A file with the given name does not exist in the server <a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a></div>";
                }



            }
            catch (Exception m)
            {
                documentsfeedback.InnerHtml = "<div class='alert alert-danger'>" + m.Message + " <a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a></div>";

            }
        }

        protected void uploadDocument_Click(object sender, EventArgs e)
        {
            String filesFolder = ConfigurationManager.AppSettings["FilesLocation"] + "ImprestRequisition/";


            if (document.HasFile)
            {
                try
                {
                    if (Directory.Exists(filesFolder))
                    {
                        String extension = System.IO.Path.GetExtension(document.FileName);
                        if (new Config().IsAllowedExtension(extension))
                        {
                            String imprestNo = Request.QueryString["docNo"];
                            string imprest = imprestNo;
                            imprestNo = imprestNo.Replace('/', '_');
                            imprestNo = imprestNo.Replace(':', '_');
                            String documentDirectory = filesFolder + imprestNo + "/";
                            Boolean createDirectory = true;
                            try
                            {
                                if (!Directory.Exists(documentDirectory))
                                {
                                    Directory.CreateDirectory(documentDirectory);
                                }
                            }
                            catch (Exception)
                            {
                                createDirectory = false;
                                documentsfeedback.InnerHtml = "<div class='alert alert-danger'>We could not create a directory for your documents. Please try again" +
                                                                "<a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a></div>";

                            }
                            if (createDirectory)
                            {
                                string filename = documentDirectory + document.FileName;
                                if (System.IO.File.Exists(filename))
                                {
                                    documentsfeedback.InnerHtml =
                                                                       "<div class='alert alert-danger'>A document with the given name already exists. Please delete it before uploading the new document or rename the new document<a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a></div>";

                                }
                                else
                                {
                                    document.SaveAs(filename);
                                    if (System.IO.File.Exists(filename))
                                    {
                                        var status = new Config().ObjNav().createImprestDocumentLink(imprestNo, document.FileName, filename);
                                        string[] info = status.Split('*');
                                        if (info[0] == "success")
                                        {
                                            documentsfeedback.InnerHtml = "<div class='alert alert-success'>" + info[1] + " <a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a></div>";
                                        }
                                        else
                                        {
                                            documentsfeedback.InnerHtml = "<div class='alert alert-danger'>" + info[1] + " <a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a></div>";
                                        }

                                        //Config.navExtender.AddLinkToRecord("Imprest Surrender", imprest, filename, "");
                                        //documentsfeedback.InnerHtml =
                                        //    "<div class='alert alert-success'>The document was successfully uploaded. <a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a></div>";
                                    }
                                    else
                                    {
                                        documentsfeedback.InnerHtml =
                                            "<div class='alert alert-danger'>The document could not be uploaded. Please try again <a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a></div>";
                                    }
                                }
                            }
                        }
                        else
                        {
                            documentsfeedback.InnerHtml = "<div class='alert alert-danger'>The document's file extension is not allowed. <a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a></div>";
                        }

                    }
                    else
                    {
                        documentsfeedback.InnerHtml = "<div class='alert alert-danger'>The document's root folder defined does not exist in the server. Please contact support. <a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a></div>";
                    }

                }
                catch (Exception ex)
                {
                    documentsfeedback.InnerHtml = "<div class='alert alert-danger'>'" + ex.Message + "'. Please try again <a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a></div>";
                }
            }
            else
            {
                documentsfeedback.InnerHtml = "<div class='alert alert-danger'>Please select the document to upload. (or the document is empty) <a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a></div>";

            }
        }
        
        protected void dpt_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tdpt = dpt.Text.Trim();

            string allBusinessCodes = new Config().ObjNav1().fnGetDepartments2(0, false, 2, tdpt);
            List<DepartmentsModel> dpts2 = new List<DepartmentsModel>();
            String[] info52 = allBusinessCodes.Split(new string[] { "::::" }, StringSplitOptions.RemoveEmptyEntries);
            if (info52 != null)
            {
                foreach (var allInfo in info52)
                {
                    String[] arr = allInfo.Split('*');

                    DepartmentsModel mdl = new DepartmentsModel();
                    mdl.Name = arr[1];
                    mdl.Code = arr[0];
                    dpts2.Add(mdl);

                }
            }

            bsnCode.DataSource = dpts2;
            bsnCode.DataTextField = "Name";
            bsnCode.DataValueField = "Code";
            bsnCode.DataBind();
            bsnCode.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--select--", ""));

            string allAirCraftCodes = new Config().ObjNav1().fnGetDepartments2(0, false, 3, tdpt);
            List<DepartmentsModel> dpts3 = new List<DepartmentsModel>();
            String[] info53 = allAirCraftCodes.Split(new string[] { "::::" }, StringSplitOptions.RemoveEmptyEntries);
            if (info53 != null)
            {
                foreach (var allInfo in info53)
                {
                    String[] arr = allInfo.Split('*');

                    DepartmentsModel mdl = new DepartmentsModel();
                    mdl.Name = arr[1];
                    mdl.Code = arr[0];
                    dpts3.Add(mdl);

                }
            }

            airCraftCode.DataSource = dpts3;
            airCraftCode.DataTextField = "Name";
            airCraftCode.DataValueField = "Code";
            airCraftCode.DataBind();
            airCraftCode.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--select--", tdpt));

            string allVehicles = new Config().ObjNav1().fnGetDepartments2(0, false, 5, "");
            List<DepartmentsModel> dpts4 = new List<DepartmentsModel>();
            String[] info54 = allVehicles.Split(new string[] { "::::" }, StringSplitOptions.RemoveEmptyEntries);
            if (info54 != null)
            {
                foreach (var allInfo in info54)
                {
                    String[] arr = allInfo.Split('*');

                    DepartmentsModel mdl = new DepartmentsModel();
                    mdl.Name = arr[1];
                    mdl.Code = arr[0];
                    dpts4.Add(mdl);

                }
            }

            vhcleCode.DataSource = dpts4;
            vhcleCode.DataTextField = "Name";
            vhcleCode.DataValueField = "Code";
            vhcleCode.DataBind();
            vhcleCode.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--select--", ""));
        }
        protected void previous_Click(object sender, EventArgs e)
        {
            int step = Convert.ToInt16(Request.QueryString["step"]);
            try
            {
                step = step - 1;
                if (step < 1)
                {
                    step = 1;
                }
            }
            catch
            {
                step = 1;
            }
            String requisitionNo = Request.QueryString["docNo"];
            Response.Redirect("Imprest.aspx?step=" + step + "&&docNo=" + requisitionNo);
        }
        protected void Unnamed1_Click(object sender, EventArgs e)
        {
            String requisitionNo = Request.QueryString["docNo"];           
            Response.Redirect("Imprest.aspx?step=3&&docNo=" + requisitionNo );
        }
        protected void sendApproval_Click(object sender, EventArgs e)
        {
            try
            {
                String requisitionNo = Request.QueryString["docNo"];
                // Convert.ToString(Session["employeeNo"]),
                //String status = Config.ObjNav.SendPurchaseRequisitionApproval(Convert.ToString(Session["employeeNo"]),
                //    requisitionNo);
                //String[] info = status.Split('*');
                //if (info[0] == "success")
                //{
                //    documentsfeedback.InnerHtml = "<div class='alert alert-" + info[0] + "'>" + info[1] + "<a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a></div>";
                //    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "redirectJS",
                //    "setTimeout(function() { window.location.replace('Dashboard.aspx') }, 5000);", true);
                //}
                //else
                //{
                //    documentsfeedback.InnerHtml = "<div class='alert alert-" + info[0] + "'>" + info[1] + "<a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a></div>";

                //}
                documentsfeedback.InnerHtml = "<div class='alert alert-success'>Document has been succesfully sent for for approval.<a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a></div>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "redirectJS", "setTimeout(function() { window.location.replace('Dashboard.aspx') }, 500);", true);

            }
            catch (Exception t)
            {
                documentsfeedback.InnerHtml = "<div class='alert alert-danger'>" + t.Message + "<a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a></div>";
            }
        }
        protected void deleteLine_Click(object sender, EventArgs e)
        {
            try
            {
                int tLineNo = 0;
                Boolean hasError = false;
                try
                {
                    tLineNo = Convert.ToInt32(lineNo.Text.Trim());
                }
                catch (Exception)
                {
                    hasError = true;
                }
                if (hasError)
                {
                    linesFeedback.InnerHtml = "<div class='alert alert-danger'>We encountered an error while processing your request. Please try again later <a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a></div>";
                }
                else
                {
                    String employeeName = Convert.ToString(Session["employeeNo"]);
                    String requisitionNo = Request.QueryString["docNo"];
                    String status = new Config().ObjNav().deleteImprestLine(requisitionNo, tLineNo);
                    String[] info = status.Split('*');
                    linesFeedback.InnerHtml = "<div class='alert alert-" + info[0] + "'>" + info[1] + " <a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a></div>";
                }
            }
            catch (Exception t)
            {
                linesFeedback.InnerHtml = "<div class='alert alert-danger'>" + t.Message + " <a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a></div>";
            }

        }
    }
}