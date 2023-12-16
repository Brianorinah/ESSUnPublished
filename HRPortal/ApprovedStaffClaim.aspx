<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ApprovedStaffClaim.aspx.cs" Inherits="HRPortal.ApprovedStaffClaim" %>
<%@ Import Namespace="HRPortal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <ol class="breadcrumb float-sm-right">
                <li class="breadcrumb-item"><a href="Dashboard.aspx">Dashboard</a></li>
                <li class="breadcrumb-item active">Open Purchase Requisition</li>
            </ol>
        </div>
    </div>
    <%        
        String employeeNo = Convert.ToString(Session["employeeNo"]);        
    %>
    <div class="panel panel-primary">
        <div class="panel-heading">
            Open Purchase Requisitions
        </div>
        <div class="panel-body">
            <div runat="server" id="feedback"></div>
            <div class="table-responsive">
                <table id="example1" class="table table-bordered table-striped">
                    <thead>
                        <tr>
                            <th>#</th>
                            <th>Requisition No</th>
                            <th>Description</th>
                            <%--<th>Requisition Product Group</th>
                            <th>Priority Level</th>--%>
                            <th>Status</th>
                            <%--<th>View Approvers</th>--%>
                            <%--<th>Send/Cancel Approval</th>--%>
                            <%--<th>Edit</th>--%>
                            <%--<th>Print</th>--%>
                        </tr>
                    </thead>
                    <tbody>
                        <%
                            int counter = 0;                            
                            var request = new Config().ObjNav1().fnStaffClaims(employeeNo,6);
                            String[] info = request.Split(new string[] { "::::" }, StringSplitOptions.RemoveEmptyEntries);
                            if (info != null)
                            {
                                foreach (var allinfo in info)
                                {
                                    String[] arr = allinfo.Split('*');
                                    if(arr[3] == "Approved") {
                                        counter++;

                                    %>
                        <tr>
                            <td><%=counter%></td>
                            <td><%=arr[0]%></td>
                            <td><%=arr[4]%></td>
                            <%--<td><%=arr[2]%></td>
                            <td><%=arr[3]%></td>--%>
                            <td><%=arr[3]%></td> 

                        </tr>
                        <%
                                }    }
                                
                            } %>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
