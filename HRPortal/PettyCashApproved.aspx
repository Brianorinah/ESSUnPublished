<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PettyCashApproved.aspx.cs" Inherits="HRPortal.PettyCashApproved" %>

<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="HRPortal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="panel panel-primary">
        <div class="panel-heading">
            <%-- <%=myStatus %> Staff Claims--%>
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-sm-6">
                    <ol class="breadcrumb float-sm-right">
                        <li class="breadcrumb-item"><a href="#">Petty Cash Requisitions</a></li>
                        <li class="breadcrumb-item active">Approved Petty Cash Requisitions</li>
                    </ol>
                </div>
            </div>
            <%
        String employeeNo = Convert.ToString(Session["employeeNo"]);

    %>
            <div runat="server" id="feedback"></div>
            <table id="example1" class="table table-bordered table-striped">
                <thead>
                    <tr>
                        <th>#</th>
                        <th>Petth Cash No</th>
                        <th>Description</th>
                        <th>Status</th>
                        <th>Amount</th>
                        <th>View Approvers</th>
                        <th>Edit</th>
                    </tr>
                </thead>
                <tbody>
                    <%
                            int counter = 0;                            
                            var request = new Config().ObjNav1().fnPettycashHeader(employeeNo,8);
                            String[] info = request.Split(new string[] { "::::" }, StringSplitOptions.RemoveEmptyEntries);
                            if (info != null)
                            {
                                foreach (var allinfo in info)
                                {
                                    String[] arr = allinfo.Split('*');
                                    if(arr[3] == "Open" || arr[3] == "Pending Approval") {
                                        counter++;

                                    %>
                        <tr>
                            <td><%=counter%></td>
                            <td><%=arr[0]%></td>
                            <td><%=arr[4]%></td>                            
                            <td><%=arr[3]%></td>                            
                            <td><%=arr[2]%></td> 
                            <td><a href="ApproverEntries.aspx?docNo=<%=arr[0] %>" class="btn btn-warning"><i class="fa fa-eye"></i>View Approvers</a></td> 
                            <td><a href="PettyCashRequisition.aspx?step=1&&docNo=<%=arr[0] %>" class="btn btn-info"><i class="fa fa-edit"></i>Edit</a></td>                      

                        </tr>
                        <%
                                }    }
                                
                            } %>
                </tbody>
            </table>
        </div>

    </div>

    <script>
        function sendApprovalRequest(documentNumber) {
            document.getElementById("approveImprestMemoNo").innerHTML = documentNumber;
            document.getElementById("ContentPlaceHolder1_imprestMemoToApprove").value = documentNumber;

            $("#sendPettyCashForApproval").modal();
        }
        function cancelApprovalRequest(documentNumber) {

            document.getElementById("cancelImprestMemoText").innerHTML = documentNumber;
            document.getElementById("ContentPlaceHolder1_cancelImprestMemoNo").value = documentNumber;

            $("#cancelPettyCashForApprovalModal").modal();
        }
    </script>

    <div id="sendPettyCashForApproval" class="modal fade" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Send Petty Cash Requisition For Approval</h4>
                </div>
                <div class="modal-body">
                    <asp:TextBox runat="server" ID="imprestMemoToApprove" type="hidden" />
                    Are you sure you want to send Petty Cash Requisition No <strong id="approveImprestMemoNo"></strong>for approval ? 
                </div>
                <div class="modal-footer">
                    <asp:Button runat="server" CssClass="btn btn-success" Text="Send Approval" ID="sendApproval" OnClick="sendApproval_Click" />
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                </div>
            </div>

        </div>
    </div>
    <div id="cancelPettyCashForApprovalModal" class="modal fade" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Cancel Petty Cash Requisition Approval</h4>
                </div>
                <div class="modal-body">
                    <asp:TextBox runat="server" ID="cancelImprestMemoNo" type="hidden" />
                    Are you sure you want to cancel approval of  Petty Cash Requisition No <strong id="cancelImprestMemoText"></strong>? 
                </div>
                <div class="modal-footer">
                    <asp:Button runat="server" CssClass="btn btn-danger" Text="Cancel Approval" ID="cancelApproval" OnClick="cancelApproval_Click" />
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
