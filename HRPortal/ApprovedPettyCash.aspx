<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ApprovedPettyCash.aspx.cs" Inherits="HRPortal.ApprovedPettyCash" %>
<%@ Import Namespace="HRPortal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-primary">
        <div class="panel-heading">
            <%-- <%=myStatus %> Staff Claims--%>
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-sm-6">
                    <ol class="breadcrumb float-sm-right">
                        <li class="breadcrumb-item"><a href="#">Petty Cash Requisitions</a></li>
                        <li class="breadcrumb-item active">Open Petty Cash Requisitions</li>
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
                        <%--<th>Amount</th>
                        <th>View Approvers</th>--%>
                        <%--<th>Edit</th>--%>
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
                                    if(arr[3] == "Approved") {
                                        counter++;

                                    %>
                        <tr>
                            <td><%=counter%></td>
                            <td><%=arr[0]%></td>
                            <td><%=arr[4]%></td>                            
                            <td><%=arr[3]%></td>                            
                            <td><%=arr[2]%></td> 
                        </tr>
                        <%
                                }    }
                                
                            } %>
                </tbody>
            </table>
        </div>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
</asp:Content>
