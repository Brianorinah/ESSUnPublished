using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRPortal.Models;
using System.IO;
using System.Configuration;
using System.Web.UI.HtmlControls;

namespace HRPortal
{
    public partial class ImprestSurrenders : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            String employeeNo = Convert.ToString(Session["employeeNo"]);
            string requisitionNo = Request.QueryString["docNo"];

            if (!IsPostBack)
            {
                string unsurrenderedImprest = new Config().ObjNav1().fnUnSurrenderedImprests(employeeNo);
                List<UnSurrenderedImprest> unsurrImprest = new List<UnSurrenderedImprest>();
                String[] info51 = unsurrenderedImprest.Split(new string[] { "::::" }, StringSplitOptions.RemoveEmptyEntries);
                if (info51 != null)
                {
                    foreach (var allInfo in info51)
                    {
                        String[] arr = allInfo.Split('*');

                        UnSurrenderedImprest mdl = new UnSurrenderedImprest();
                        mdl.description = arr[0] + "-" + arr[1];
                        mdl.number = arr[0];
                        unsurrImprest.Add(mdl);

                    }
                }

                unImprest.DataSource = unsurrImprest;
                unImprest.DataTextField = "description";
                unImprest.DataValueField = "number";
                unImprest.DataBind();
                unImprest.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--select--", ""));

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
            }

            //ImprestCode.DataSource = trvls;
            //ImprestCode.DataTextField = "description";
            //ImprestCode.DataValueField = "code";
            //ImprestCode.DataBind();
            //ImprestCode.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--select--", ""));


            try
            {

                var nav = new Config().ObjNav1();
                var result = nav.fnImprestSurrendersLines(requisitionNo);
                String[] info = result.Split(new string[] { "::::" }, StringSplitOptions.RemoveEmptyEntries);
                if (info.Count() > 0)
                {
                    foreach (var allinfo in info)
                    {
                        String[] arr = allinfo.Split('*');

                        HtmlTableRow row = new HtmlTableRow();
                        //Account Type 	Account No 	Account Name 	Amount 	Actual Spent 	Receipt No
                        HtmlTableCell accountType = new HtmlTableCell();
                        accountType.InnerText = arr[1];

                        //HtmlTableCell accountNo = new HtmlTableCell();
                        //accountNo.InnerText = line.Account_No;

                        //HtmlTableCell accountName = new HtmlTableCell();
                        //accountName.InnerText = line.Account_Name;

                        HtmlTableCell amountCell = new HtmlTableCell();
                        amountCell.InnerText = String.Format("{0:n}", Convert.ToDouble(arr[2]));

                        HtmlTableCell actualSpent = new HtmlTableCell();
                        TextBox amountSpent = new TextBox();
                        amountSpent.CssClass = "form-control actualSpent";
                        amountSpent.ID = "amountSpent" + arr[0];
                        actualSpent.Controls.Add(amountSpent);
                        amountSpent.Text = arr[3] + "";

                        //HtmlTableCell receipt = new HtmlTableCell();
                        //DropDownList receiptNo = new DropDownList();

                        //List<String> receiptNos = new List<string>();
                        //receiptNos.Add("");
                        //foreach (var myReceipt in receipts)
                        //{
                        //    receiptNos.Add(myReceipt.No);
                        //}

                        //receiptNo.DataSource = receiptNos;
                        //receiptNo.DataBind();
                        //receiptNo.CssClass = "form-control";
                        //receiptNo.ID = "receipt" + line.Line_No;
                        //receipt.Controls.Add(receiptNo);
                        //try
                        //{
                        //    receiptNo.SelectedValue = line.Receipt_No;
                        //}
                        //catch (Exception)
                        //{

                        //}

                        row.Cells.Add(accountType);
                        //row.Cells.Add(accountNo);
                        //row.Cells.Add(accountName);
                        row.Cells.Add(amountCell);
                        row.Cells.Add(actualSpent);
                        //row.Cells.Add(receipt);


                        linesTable.Rows.Add(row);
                    }
                }
            }
            catch (Exception)
            {

            }

            //    var imprestLines = nav.ImprestLines.Where(r => r.No == surrenderNo).ToList();
            //    foreach (var line in imprestLines)
            //    {
            //        HtmlTableRow row = new HtmlTableRow();
            //        //Account Type 	Account No 	Account Name 	Amount 	Actual Spent 	Receipt No
            //        HtmlTableCell accountType = new HtmlTableCell();
            //        accountType.InnerText = line.Account_Type;

            //        HtmlTableCell accountNo = new HtmlTableCell();
            //        accountNo.InnerText = line.Account_No;

            //        HtmlTableCell accountName = new HtmlTableCell();
            //        accountName.InnerText = line.Account_Name;

            //        HtmlTableCell amountCell = new HtmlTableCell();
            //        amountCell.InnerText = String.Format("{0:n}", Convert.ToDouble(line.Amount));

            //        HtmlTableCell actualSpent = new HtmlTableCell();
            //        TextBox amountSpent = new TextBox();
            //        amountSpent.CssClass = "form-control actualSpent";
            //        amountSpent.ID = "amountSpent" + line.Line_No;
            //        actualSpent.Controls.Add(amountSpent);
            //        amountSpent.Text = line.Actual_Spent + "";

            //        HtmlTableCell receipt = new HtmlTableCell();
            //        DropDownList receiptNo = new DropDownList();

            //        List<String> receiptNos = new List<string>();
            //        receiptNos.Add("");
            //        foreach (var myReceipt in receipts)
            //        {
            //            receiptNos.Add(myReceipt.No);
            //        }

            //        receiptNo.DataSource = receiptNos;
            //        receiptNo.DataBind();
            //        receiptNo.CssClass = "form-control";
            //        receiptNo.ID = "receipt" + line.Line_No;
            //        receipt.Controls.Add(receiptNo);
            //        try
            //        {
            //            receiptNo.SelectedValue = line.Receipt_No;
            //        }
            //        catch (Exception)
            //        {

            //        }

            //        row.Cells.Add(accountType);
            //        row.Cells.Add(accountNo);
            //        row.Cells.Add(accountName);
            //        row.Cells.Add(amountCell);
            //        row.Cells.Add(actualSpent);
            //        row.Cells.Add(receipt);


            //        linesTable.Rows.Add(row);
            //    }
            //}
            //catch (Exception)
            //{

            //}


            if (!string.IsNullOrEmpty(requisitionNo))
            {
                var request = new Config().ObjNav1().fnImprestSurrendersSingle(employeeNo, requisitionNo);
                String[] info1 = request.Split(new string[] { "::::" }, StringSplitOptions.RemoveEmptyEntries);
                if (info1 != null)
                {
                    foreach (var allinfo in info1)
                    {
                        String[] arr = allinfo.Split('*');
                        unImprest.SelectedValue = arr[1];
                    }
                }

            }

        }

        protected void next_Click(object sender, EventArgs e)
        {
            String requisitionNo = "";
            Boolean newRequisition = false;
            String employeeNo = Convert.ToString(Session["employeeNo"]);
            int counter = 0;
            var request = new Config().ObjNav1().fnImprestSurrenders(employeeNo);
            String[] info11 = request.Split(new string[] { "::::" }, StringSplitOptions.RemoveEmptyEntries);
            if (info11 != null)
            {
                foreach (var allinfo in info11)
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
                generalFeedback.InnerHtml = "<div class='alert alert-danger'>You have an open Imprest Surrender. Kindly Proceed and edit the existing one.<a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a></div>";
            }
            else
            {

                Boolean error = false;
                string message = "";

                if (string.IsNullOrEmpty(unImprest.SelectedValue.Trim()))
                {
                    error = true;
                    message = "Imprest No. is Required.";
                }

                if (error)
                {
                    generalFeedback.InnerHtml = "<div class='alert alert-danger'>" + message + "<a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a></div>";
                }
                else
                {
                    string tunImprest = unImprest.SelectedValue.Trim();


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

                    String status = nav.createSurrender(employeeNo, requisitionNo, tunImprest);
                    String[] info = status.Split('*');
                    if (info[0] == "success")
                    {
                        generalFeedback.InnerHtml = "<div class='alert alert-" + info[0] + "'>" + info[1] + "<a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a></div>";
                        Response.Redirect("ImprestSurrenders.aspx?step=2&&docNo=" + info[2]);

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
            //string tImprestCode = ImprestCode.SelectedValue.Trim();
            //decimal tamntAdvanced = Convert.ToDecimal(amntAdvanced.Text.Trim());
            //decimal tamntSpent = Convert.ToDecimal(amntSpent.Text.Trim());
            //string treceiptNumber = receiptNumber.Text.Trim();
            //string requisitionNo = Request.QueryString["docNo"];

            var nav = new Config().ObjNav();

            //String status = nav.createSurrenderLine(requisitionNo, tImprestCode, tamntAdvanced, tamntSpent, treceiptNumber);
            //String[] info = status.Split('*');
            //if (info[0] == "success")
            //{
            //    linesFeedback.InnerHtml = "<div class='alert alert-" + info[0] + "'>" + info[1] + "<a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a></div>";


            //}
            //else
            //{
            //    linesFeedback.InnerHtml = "<div class='alert alert-danger'>" + info[1] + " <a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a></div>";
            //}
        }
        protected void deleteFile_Click(object sender, EventArgs e)
        {
            try
            {
                String tFileName = fileName.Text.Trim();
                String filesFolder = ConfigurationManager.AppSettings["FilesLocation"] + "ImprestSurrender/";
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
            String filesFolder = ConfigurationManager.AppSettings["FilesLocation"] + "ImprestSurrender/";


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
            Response.Redirect("ImprestSurrenders.aspx?step=" + step + "&&docNo=" + requisitionNo);
        }
        protected void Unnamed1_Click(object sender, EventArgs e)
        {
            String requisitionNo = Request.QueryString["docNo"];
            Response.Redirect("ImprestSurrenders.aspx?step=3&&docNo=" + requisitionNo);
        }
        protected void sendApproval_Click(object sender, EventArgs e)
        {
            try
            {
                String requisitionNo = Request.QueryString["docNo"];
                string empNo = Convert.ToString(Session["employeeNo"]);
                String status = new Config().ObjNav().sendSurrenderRequisitionApproval(requisitionNo, empNo);
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
            //try
            //{
            //    int tLineNo = 0;
            //    Boolean hasError = false;
            //    try
            //    {
            //        tLineNo = Convert.ToInt32(lineNo.Text.Trim());
            //    }
            //    catch (Exception)
            //    {
            //        hasError = true;
            //    }
            //    if (hasError)
            //    {
            //        linesFeedback.InnerHtml = "<div class='alert alert-danger'>We encountered an error while processing your request. Please try again later <a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a></div>";
            //    }
            //    else
            //    {
            //        String employeeName = Convert.ToString(Session["employeeNo"]);
            //        String requisitionNo = Request.QueryString["docNo"];
            //        String status = new Config().ObjNav().deleteSurrenderLine(requisitionNo, tLineNo);
            //        String[] info = status.Split('*');
            //        linesFeedback.InnerHtml = "<div class='alert alert-" + info[0] + "'>" + info[1] + " <a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a></div>";
            //    }
            //}
            //catch (Exception t)
            //{
            //    linesFeedback.InnerHtml = "<div class='alert alert-danger'>" + t.Message + " <a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a></div>";
            //}

        }
        protected void UnnamedSave_Click(object sender, EventArgs e)
        {
            if (SaveLines())
            {
                String surrenderNo = Request.QueryString["docNo"];
                Response.Redirect("ImprestSurrenders.aspx?step=3&&docNo=" + surrenderNo);
            }
        }
        protected void save_Click(object sender, EventArgs e)
        {
            if (SaveLines())
            {
                linesFeedback.InnerHtml = "<div class='alert alert-success'>Your Imprest Surrender was successfully saved <a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a></div>";
            }
        }
        public Boolean SaveLines()
        {
            Boolean error = false;
            String message = "";
            List<SurrenderLine> allValues = new List<SurrenderLine>();
            HtmlTableRowCollection allRows = linesTable.Rows;
            // int size = allRows.Count
            foreach (HtmlTableRow row in allRows)
            {
                SurrenderLine myLine = new SurrenderLine();
                //DropDownList
                Decimal mAmountSpent = 0;
                String receiptNo = "";
                int tLineNo = 0;
                HtmlTableCellCollection allCells = row.Cells;
                foreach (HtmlTableCell myCell in allCells)
                {
                    ControlCollection myControls = myCell.Controls;
                    foreach (Control control in myControls)
                    {
                        String controlType = control.GetType().ToString().Trim();

                        if (controlType == "System.Web.UI.WebControls.DropDownList")
                        {
                            DropDownList tReceipt = (DropDownList)control;
                            receiptNo = tReceipt.SelectedValue;
                            myLine.receiptNo = receiptNo;
                        }
                        else if (controlType == "System.Web.UI.WebControls.TextBox")
                        {

                            TextBox tAmountSpent = (TextBox)control;
                            String tSpentAmount = tAmountSpent.Text;
                            String textBoxId = tAmountSpent.ID;
                            String lineNo = textBoxId.Replace("amountSpent", "");

                            try
                            {
                                int mLineNo = Convert.ToInt32(lineNo);
                                tLineNo = mLineNo;
                                myLine.lineNo = tLineNo;
                                try
                                {
                                    Decimal sAmount = Convert.ToDecimal(tSpentAmount);
                                    myLine.amount = sAmount;
                                }
                                catch (Exception)
                                {
                                    error = true;
                                    message =
                                        "Invalid Amount*Some values you have entered for spent amount are wrong. Please try again*error";
                                    break;
                                }
                            }
                            catch (Exception)
                            {
                                error = true;
                                message = "Wrong Line No*The line number you have entered is wrong*error";
                                break;
                            }




                        }

                    }

                }
                allValues.Add(myLine);

            }
            if (error)
            {
                /* error = true;
                 String values = "";
                 foreach (SurrenderLine value in allValues)
                 {
                     values +=  value.lineNo +" Amount"+ value.amount + value.receiptNo+"<br/>";
                 }*/
                linesFeedback.InnerHtml = "<div class='alert alert-danger'>" + message + " <a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a></div>";
            }
            else
            {
                foreach (SurrenderLine value in allValues)
                {
                    if (value.lineNo > 0)
                    {


                        try
                        {

                            //Convert.ToString(Session["employeeNo"])                            
                            int myLineNo = value.lineNo;
                            var nav = new Config().ObjNav();
                            String requisitionNo = Request.QueryString["docNo"];
                            String status = nav.createSurrenderLine(requisitionNo, value.lineNo, value.amount);
                            //String status =
                            //    Config.ObjNav.UpdateSurrenderLine(Convert.ToString(Session["employeeNo"]),
                            //        surrenderNo, value.lineNo, value.amount, value.receiptNo, branchCode, divisionCode);
                            String[] info = status.Split('*');
                            if (info[0] == "danger")
                            {
                                error = true;
                                linesFeedback.InnerHtml = "<div class='alert alert-danger'>" + info[1] +
                                                          " <a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a></div>";

                                break;

                            }
                        }
                        catch (Exception r)
                        {
                            error = true;
                            linesFeedback.InnerHtml = "<div class='alert alert-danger'>" + r.Message +
                                                      " <a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a></div>";
                        }
                    }
                }
            }
            return !error;
        }
    }
}