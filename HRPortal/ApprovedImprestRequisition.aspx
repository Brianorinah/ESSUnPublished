<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ApprovedImprestRequisition.aspx.cs" Inherits="HRPortal.ApprovedImprestRequisition" %>
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
                <li class="breadcrumb-item active">Approved Imprest Requisition</li>
            </ol>
        </div>
    </div>
    <%        
        String employeeNo = Convert.ToString(Session["employeeNo"]);        
    %>
    <div class="panel panel-primary">
        <div class="panel-heading">
            Approved Imprest Requisition
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
                            <th>Destination</th>
                            <th>Amount</th>
                            <%--<th>Send/Cancel Approval</th>--%>
                            <th>View Approvers</th>
                            <th>Edit</th>
                            <%--<th>Print</th>--%>
                        </tr>
                    </thead>
                    <tbody>
                        <%
                            int counter = 0;                            
                            var request = new Config().ObjNav1().fnImprestHeader(employeeNo);
                            String[] info = request.Split(new string[] { "::::" }, StringSplitOptions.RemoveEmptyEntries);
                            if (info != null)
                            {
                                foreach (var allinfo in info)
                                {
                                    String[] arr = allinfo.Split('*');
                                    if(arr[4] == "Approved" ) {
                                        counter++;

                                    %>
                        <tr>
                            <td><%=counter%></td>
                            <td><%=arr[0]%></td>
                            <td><%=arr[5]%></td>
                            <%--<td><%=arr[2]%></td>
                            <td><%=arr[3]%></td>--%>
                            <td><%=arr[6]%></td> 
                            <td><%=arr[4]%></td>
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
