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

                string allVehicles = new Config().ObjNav1().fnGetDepartments(0, false, 4);
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

                projectCode.DataSource = dpts4;
                projectCode.DataTextField = "Name";
                projectCode.DataValueField = "Code";
                projectCode.DataBind();
                projectCode.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--select--", ""));

                traveltype.DataSource = trvls;
                traveltype.DataTextField = "description";
                traveltype.DataValueField = "code";
                traveltype.DataBind();
                traveltype.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--select--", ""));

                string allBusinessCodes = new Config().ObjNav1().fnGetDepartments2(0, false, 2);
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

                string allAirCraftCodes = new Config().ObjNav1().fnGetDepartments2(0, false, 3);
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
                airCraftCode.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--select--", ""));

                string allCurrencies = new Config().ObjNav1().fnCurrencies();
                List<Currency> curr = new List<Currency>();
                String[] info57 = allCurrencies.Split(new string[] { "::::" }, StringSplitOptions.RemoveEmptyEntries);
                if (info57 != null)
                {
                    foreach (var allInfo in info57)
                    {
                        String[] arr = allInfo.Split('*');

                        Currency mdl = new Currency();
                        mdl.description = arr[0] + "-" + arr[1];
                        mdl.code = arr[0];
                        curr.Add(mdl);

                    }
                }

                currCode1.DataSource = curr;
                currCode1.DataTextField = "description";
                currCode1.DataValueField = "code";
                currCode1.DataBind();
                currCode1.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--select--", ""));

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
                            dpt.SelectedValue = arr[8];
                            //dptMach(arr[9]);
                            bsnCode.SelectedValue = arr[9];
                            airCraftCode.SelectedValue = arr[10];
                            projectCode.SelectedValue = arr[11];
                            description.Text = arr[5];
                            currCode1.SelectedValue = arr[7];
                            dateFrom.Text = arr[1];
                            dateTo.Text = arr[2];                            

                        }
                    }
                }
            }
        }
        public void dptMach(string tdpt)
        {
            string allBusinessCodes = new Config().ObjNav1().fnGetDepartments2(0, false, 2);
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

            string allAirCraftCodes = new Config().ObjNav1().fnGetDepartments2(0, false, 3);
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

        }

        protected void addGeneralDetails_Click(object sender, EventArgs e)
        {
            int counter = 0;
            String employeeNo = Convert.ToString(Session["employeeNo"]);
            String requisitionNo = "";
            requisitionNo = Request.QueryString["docNo"];
            var request = new Config().ObjNav1().fnImprestHeader(employeeNo);
            String[] info2 = request.Split(new string[] { "::::" }, StringSplitOptions.RemoveEmptyEntries);
            if (info2 != null)
            {
                foreach (var allinfo in info2)
                {
                    String[] arr = allinfo.Split('*');
                    if (arr[4] == "Open")
                    {
                        counter++;
                    }
                }
            }

            if (counter > 0 && string.IsNullOrEmpty(requisitionNo))
            {
                generalFeedback.InnerHtml = "<div class='alert alert-danger'>You have an open Imprest Requisition. Kindly edit the existing document. <a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a></div>";
            }
            else
            {

                Boolean newRequisition = false;
                Boolean error = false;
                string message = "";

                string tcurrCode1 = currCode1.SelectedValue.Trim();
                string tdpt = dpt.SelectedValue.Trim();
                string tbsnCode = bsnCode.SelectedValue.Trim();
                string tprojectCode = projectCode.SelectedValue.Trim();
                string tairCraftCode = airCraftCode.SelectedValue.Trim();

                if (string.IsNullOrEmpty(dateFrom.Text.Trim()))
                {
                    error = true;
                    message = "Date From is Required.";
                }
                if (string.IsNullOrEmpty(dateTo.Text.Trim()))
                {
                    error = true;
                    message = "Date To is Required.";
                }
                if (string.IsNullOrEmpty(description.Text.Trim()))
                {
                    error = true;
                    message = "Description is Required.";
                }

                if (error)
                {
                    generalFeedback.InnerHtml = "<div class='alert alert-danger'>" + message + "<a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a></div>";
                }
                else
                {
                    try
                    {

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

                    string tdescription = description.Text.Trim();
                    DateTime tdateFrom = Convert.ToDateTime(dateFrom.Text.Trim());
                    DateTime tdateTo = Convert.ToDateTime(dateTo.Text.Trim());


                    var nav = new Config().ObjNav();

                    String status = nav.createImprestHeader(employeeNo, requisitionNo, tdescription, tdateFrom, tdateTo, tdpt, tbsnCode, tairCraftCode, tprojectCode, tcurrCode1);
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


            }



        }
        protected void addImprestLine_Click(object sender, EventArgs e)
        {
            Boolean error = false;
            string message = "";
            
            if (string.IsNullOrEmpty(quantity.Text.Trim()))
            {
                error = true;
                message = "Description is Required.";
            }
            if (string.IsNullOrEmpty(unitCost.Text.Trim()))
            {
                error = true;
                message = "Description is Required.";
            }
            if (error)
            {
                linesFeedback.InnerHtml = "<div class='alert alert-danger'>" + message + "<a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a></div>";
            }
            else
            {

                string ttraveltype = traveltype.SelectedValue.Trim();
                decimal tquantity = Convert.ToDecimal(quantity.Text.Trim());
                decimal tunitCost = Convert.ToDecimal(unitCost.Text.Trim());
                string requisitionNo = Request.QueryString["docNo"];

                var nav = new Config().ObjNav();

                String status = nav.createImprestLine(requisitionNo, ttraveltype, tunitCost, tquantity);
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

            string allBusinessCodes = new Config().ObjNav1().fnGetDepartments2(0, false, 2);
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

            string allAirCraftCodes = new Config().ObjNav1().fnGetDepartments2(0, false, 3);
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
            Response.Redirect("Imprest.aspx?step=3&&docNo=" + requisitionNo);
        }
        protected void sendApproval_Click(object sender, EventArgs e)
        {
            try
            {
                String requisitionNo = Request.QueryString["docNo"];
                string empNo = Convert.ToString(Session["employeeNo"]);
                String status = new Config().ObjNav().sendImprestRequisitionApproval(requisitionNo, empNo);
                String[] info = status.Split('*');
                if (info[0] == "success")
                {
                    documentsfeedback.InnerHtml = "<div class='alert alert-" + info[0] + "'>" + info[1] + "<a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a></div>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "redirectJS",
                    "setTimeout(function() { window.location.replace('Dashboard.aspx') }, 5000);", true);
                }
                else
                {
                    documentsfeedback.InnerHtml = "<div class='alert alert-" + info[0] + "'>" + info[1] + "<a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a></div>";

                }
                

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