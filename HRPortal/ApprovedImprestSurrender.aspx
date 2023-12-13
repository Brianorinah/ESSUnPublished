<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ApprovedImprestSurrender.aspx.cs" Inherits="HRPortal.ApprovedImprestSurrender" %>
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
                <li class="breadcrumb-item active">Approved Imprest Surrenders</li>
            </ol>
        </div>
    </div>
    <%
        String employeeNo = Convert.ToString(Session["employeeNo"]);

    %>
    <div class="panel panel-primary">
        <div class="panel-heading">
            Approved Imprest Surrenders
        </div>
        <div class="panel-body">
            <div runat="server" id="feedback"></div>
            <!-- No,date , imprest Amount, payee, status , view /edit -->
            <table id="example1" class="table table-bordered table-striped">
                <thead>
                    <tr>
                        <th>#</th>
                        <th>SUrrender No</th>
                        <th>Imprest Number</th>                        
                        <th>Status</th>                        
                        <th>Amount</th>
                       
                        
                    </tr>
                </thead>
                <tbody>
                    <%
                            int counter = 0;                            
                            var request = new Config().ObjNav1().fnImprestSurrenders(employeeNo);
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
                            <td><%=arr[1]%></td>                            
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
</asp:Content>
