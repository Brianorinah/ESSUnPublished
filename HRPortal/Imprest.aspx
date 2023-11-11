<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Imprest.aspx.cs" Inherits="HRPortal.Imprest" %>

<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="HRPortal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%
        int step = 1;
        try
        {
            step = Convert.ToInt32(Request.QueryString["step"]);
            if (step > 6 || step < 1)
            {
                step = 1;
            }
        }
        catch (Exception)
        {
            step = 1;
        }
        if (step == 1)
        {
    %>
    <div class="panel panel-primary">
        <div class="panel-heading">
            Imprest Memo General Details
            <span class="pull-right"><i class="fa fa-chevron-left"></i>Step 1 of 6 <i class="fa fa-chevron-right"></i></span><span class="clearfix"></span>
        </div>
        <div class="panel-body">
            <div id="generalFeedback" runat="server"></div>
            <div class="row">

                <div class="col-md-6 col-lg-6">
                    <div class="form-group">
                        <strong>Description:</strong>
                        <asp:TextBox runat="server" ID="description" CssClass="form-control" placeholder="Description" />
                    </div>
                </div>
                <div class="col-md-6 col-lg-6">
                    <div class="form-group">
                        <strong>Destination:</strong>
                        <asp:TextBox runat="server" ID="destinationNarration" CssClass="form-control" placeholder="Destination Narration" TextMode="MultiLine" MaxLength="100" />
                    </div>
                </div>
                <div class="col-md-6 col-lg-6">
                    <div class="form-group">
                        <strong>Date From:</strong>
                        <asp:TextBox runat="server" ID="dateFrom" CssClass="form-control" placeholder="Date From" TextMode="Date" />
                    </div>
                </div>
                <div class="col-md-6 col-lg-6">
                    <div class="form-group">
                        <strong>Date To:</strong>
                        <asp:TextBox runat="server" ID="dateTo" CssClass="form-control" placeholder="Date To" TextMode="Date" />
                    </div>
                </div>
                <div class="col-md-6 col-lg-6">
                    <div class="form-group">
                        <strong>Currency:</strong>
                        <asp:DropDownList runat="server" ID="currCode1" CssClass="form-control select2">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="KES">KES</asp:ListItem>
                            <asp:ListItem Value="USD">USD</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-6 col-lg-6">
                    <div class="form-group">
                        <strong>Department:</strong>
                        <asp:DropDownList ID="dpt" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="dpt_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>

                    </div>
                </div>
                <div class="col-md-6 col-lg-6">
                    <div class="form-group">
                        <strong>Business Unit:</strong>
                        <asp:DropDownList ID="bsnCode" runat="server" CssClass="form-control select2"></asp:DropDownList>

                    </div>
                </div>
                <div class="col-md-6 col-lg-6">
                    <div class="form-group">
                        <strong>Vehicle:</strong>
                        <asp:DropDownList ID="vhcleCode" runat="server" CssClass="form-control select2"></asp:DropDownList>

                    </div>
                </div>
                <div class="col-md-6 col-lg-6">
                    <div class="form-group">
                        <strong>Aircraft:</strong>
                        <asp:DropDownList ID="airCraftCode" runat="server" CssClass="form-control select2"></asp:DropDownList>

                    </div>
                </div>

            </div>

        </div>
    </div>
    <div class="panel-footer">
        <asp:Button runat="server" CssClass="btn btn-success pull-right" Text="Next" ID="addGeneralDetails" OnClick="addGeneralDetails_Click" />
        <span class="clearfix"></span>
    </div>

    <% 
        }
        else if (step == 2)
        {
    %>
    <div class="panel panel-primary">
        <div class="panel-heading">
            Team
              <span class="pull-right"><i class="fa fa-chevron-left"></i>Step 2 of 6 <i class="fa fa-chevron-right"></i></span><span class="clearfix"></span>
        </div>
        <div class="panel-body">
            <div runat="server" id="linesFeedback"></div>
            <!--team(destination town, vote item, no, number of days) -->
            <div class="col-md-6 col-lg-6">
                <div class="form-group">
                    <strong>Travel Type:</strong>
                    <asp:DropDownList runat="server" CssClass="form-control select2" ID="traveltype">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-md-6 col-lg-6">
                <div class="form-group">
                    <strong>Amount:</strong>
                    <asp:TextBox runat="server" CssClass="form-control " ID="txtamount" TextMode="Number"/>
                </div>
            </div>
            <div class="col-md-6 col-lg-6">
                <div class="form-group">
                    <strong>Quantity:</strong>
                    <asp:TextBox runat="server" CssClass="form-control " ID="quantity"  TextMode="Number"/>
                </div>
            </div>
            <div class="form-group">
                <asp:Button runat="server" CssClass="btn btn-success pull-right" Text="Add Imprest Line" ID="addImprestLine" OnClick="addImprestLine_Click" />
                <div class="clearfix"></div>
            </div>
            <hr />
            <table id="example1" class="table table-bordered table-striped">
                <thead>
                    <tr>
                        <th>Travel Type</th>
                        <th>Description</th>
                        <th>Quantity</th>
                        <th>Amount</th>
                        <th>Remove</th>
                    </tr>
                </thead>
                <tbody>
                    <%
                        String requisitionNo = Request.QueryString["docNo"];
                        var nav = new Config().ObjNav1();
                        var result = nav.fnImprestLines(requisitionNo);
                        String[] info = result.Split(new string[] { "::::" }, StringSplitOptions.RemoveEmptyEntries);
                        if (info.Count() > 0)
                        {
                            if (info != null)
                            {
                                foreach (var allinfo in info)
                                {
                                    String[] arr = allinfo.Split('*');

                    %>
                    <tr>
                        <%--                        <td><% =line.Procurement_Plan_ID %></td>
                        <td><% =line.Procurement_Plan_Entry_No %></td>--%>
                        <%-- <td><% =line.ProcurementPlanDescription %></td>--%>
                        <td><% =arr[4] %></td>
                        <td><% =arr[2] %></td>
                        <td><% = arr[1] %></td>                        
                        <td><%=String.Format("{0:n}", Convert.ToDouble(arr[3])) %></td>

                        <td>
                            <label class="btn btn-danger" onclick="removeLine('<% =arr[4] %>','<%=arr[0] %>');"><i class="fa fa-trash"></i>Delete</label></td>
                    </tr>
                    <% 
                            }
                        }
                    }
                    %>
                </tbody>
            </table>

        </div>
        <div class="panel-footer">
            <asp:Button runat="server" CssClass="btn btn-warning pull-left" Text="Previous" OnClick="previous_Click" />
            <asp:Button runat="server" CssClass="btn btn-success pull-right" Text="Next" OnClick="Unnamed1_Click" />
            <div class="clearfix"></div>
        </div>
    </div>
    <%
        }
        else if (step == 3)
        {
    %>

    <div class="panel panel-primary">
        <div class="panel-heading">
            Supporting Documents
              <span class="pull-right"><i class="fa fa-chevron-left"></i>Step 3 of 3 <i class="fa fa-chevron-right"></i></span><span class="clearfix"></span>
        </div>
        <div class="panel-body">
            <div runat="server" id="documentsfeedback"></div>
            <div class="row">
                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                    <div class="form-group">
                        <strong>Select file to upload:</strong>
                        <asp:FileUpload runat="server" ID="document" CssClass="form-control" Style="padding-top: 0px;" />
                    </div>
                </div>
                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                    <div class="form-group">
                        <br />
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Upload Document" ID="uploadDocument" OnClick="uploadDocument_Click" />
                    </div>
                </div>
            </div>
            <table class="table table-bordered table-striped">
                <thead>
                    <tr>
                        <th>Document Title</th>
                        <%--<th>Download</th>--%>
                        <th>Delete</th>
                    </tr>
                </thead>
                <tbody>
                    <%
                        try
                        {
                            String fileFolderApplication = ConfigurationManager.AppSettings["FileFolderApplication"];
                            String filesFolder = ConfigurationManager.AppSettings["FilesLocation"] + "ImprestRequisition/";
                            String imprestNo = Request.QueryString["docNo"];
                            //imprestNo = imprestNo.Replace('/', '_');
                            //imprestNo = imprestNo.Replace(':', '_');
                            String documentDirectory = filesFolder + imprestNo + "/";
                            if (Directory.Exists(documentDirectory))
                            {
                                foreach (String file in Directory.GetFiles(documentDirectory, "*.*", SearchOption.AllDirectories))
                                {
                                    String url = documentDirectory;
                    %>
                    <tr>
                        <td><% =file.Replace(documentDirectory, "") %></td>
                        <%-- <td><a href="<%=filesFolder %><% =imprestNo+"\\"+file.Replace(documentDirectory, "") %>" class="btn btn-success">Download</a></td>--%>


                        <%-- <td><a href="<%=fileFolderApplication %>\PurchaseRequisition Header\<% =imprestNo+"\\"+file.Replace(documentDirectory, "") %>" class="btn btn-success" >Download</a></td>--%>
                        <td>
                            <label class="btn btn-danger" onclick="deleteFile('<%=file.Replace(documentDirectory, "")%>');"><i class="fa fa-trash-o"></i>Delete</label></td>
                    </tr>
                    <%
                                }
                            }
                        }
                        catch (Exception)
                        {

                        }%>
                </tbody>
            </table>
        </div>
        <div class="panel-footer">
            <asp:Button runat="server" CssClass="btn btn-warning pull-left" Text="Previous" OnClick="previous_Click" />
            <asp:Button runat="server" CssClass="btn btn-success pull-right" Text="Send Approval Request" ID="sendApproval" OnClick="sendApproval_Click" /><div class="clearfix"></div>
        </div>
    </div>



    <%
        }
    %>
    <script>

        function deleteFile(fileName) {
            document.getElementById("filetoDeleteName").innerText = fileName;
            document.getElementById("ContentPlaceHolder1_fileName").value = fileName;
            $("#deleteFileModal").modal();
        }
    </script>

    <div id="deleteFileModal" class="modal fade" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Confirm Deleting File</h4>
                </div>
                <div class="modal-body">
                    <p>Are you sure you want to delete the file <strong id="filetoDeleteName"></strong>?</p>
                    <asp:TextBox runat="server" ID="fileName" type="hidden" />
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                    <asp:Button runat="server" CssClass="btn btn-danger" Text="Delete File" OnClick="deleteFile_Click" />
                </div>
            </div>

        </div>
    </div>
    <script>
        function removeLine(itemName, lineNo) {
            document.getElementById("itemName").innerText = itemName;
            document.getElementById("ContentPlaceHolder1_lineNo").value = lineNo;
            $("#removeLineModal").modal();
        }
    </script>
    <div id="removeLineModal" class="modal fade" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Confirm Remove Line</h4>
                </div>
                <div class="modal-body">
                    <p>Are you sure you want to remove the item <strong id="itemName"></strong>from the Imprest Requisition?</p>
                    <asp:TextBox runat="server" ID="lineNo" type="hidden" />
                </div>

                <div class="modal-footer">
                     <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                    <asp:Button runat="server" CssClass="btn btn-danger" Text="Delete Line" OnClick="deleteLine_Click" />
                </div>
            </div>

        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
</asp:Content>
