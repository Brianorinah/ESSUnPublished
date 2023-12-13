<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewImprest.aspx.cs" Inherits="HRPortal.ViewImprest" %>
<%@ Import Namespace="HRPortal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="panel panel-primary">
        <div class="panel-heading">
            Imprest Memo General Details            
        </div>
        <div class="panel-body">
            <div id="generalFeedback" runat="server"></div>
            <div class="row">

                <div class="col-md-6 col-lg-6">
                    <div class="form-group">
                        <strong>Description:</strong>
                        <asp:TextBox runat="server" ID="description" CssClass="form-control"  Readonly/>
                    </div>
                </div>
                
                <div class="col-md-6 col-lg-6">
                    <div class="form-group">
                        <strong>Date From:</strong>
                        <asp:TextBox runat="server" ID="dateFrom" CssClass="form-control"   Readonly/>
                    </div>
                </div>
                <div class="col-md-6 col-lg-6">
                    <div class="form-group">
                        <strong>Date To:</strong>
                        <asp:TextBox runat="server" ID="dateTo" CssClass="form-control"  Readonly />
                    </div>
                </div>
                <div class="col-md-6 col-lg-6">
                    <div class="form-group">
                        <strong>Currency:</strong>
                        <asp:TextBox runat="server" ID="currCode1" CssClass="form-control"   Readonly/>
                        
                    </div>
                </div>
                <div class="col-md-6 col-lg-6">
                    <div class="form-group">
                        <strong>Department:</strong>
                        <asp:TextBox runat="server" ID="dpt" CssClass="form-control"  Readonly />                        

                    </div>
                </div>
                <div class="col-md-6 col-lg-6">
                    <div class="form-group">
                        <strong>Business Unit:</strong>
                        <asp:TextBox runat="server" ID="bsnCode" CssClass="form-control" Readonly />
                        

                    </div>
                </div>
                <div class="col-md-6 col-lg-6">
                    <div class="form-group">
                        <strong>Project :</strong>
                        <asp:TextBox runat="server" ID="projectCode" CssClass="form-control"   Readonly/>                        

                    </div>
                </div>
                <div class="col-md-6 col-lg-6">
                    <div class="form-group">
                        <strong>Aircraft:</strong>
                        <asp:TextBox runat="server" ID="airCraftCode" CssClass="form-control"  Readonly/>                        

                    </div>
                </div>

            </div>

        </div>
    </div>
    <div class="panel panel-primary">
        <div class="panel-heading">
            Team             
        </div>
        <div class="panel-body">
            <div runat="server" id="linesFeedback"></div>            
            <table id="example1" class="table table-bordered table-striped">
                <thead>
                    <tr>
                        <th>Travel Type</th>
                        <th>Description</th>
                        <th>Quantity</th>
                        <th>Amount</th>                        
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
                        <td><% =arr[4] %></td>
                        <td><% =arr[2] %></td>
                        <td><% = arr[1] %></td>                        
                        <td><%=String.Format("{0:n}", Convert.ToDouble(arr[3])) %></td>
                        
                    </tr>
                    <% 
                            }
                        }
                    }
                    %>
                </tbody>
            </table>
        </div>        
    </div>
</asp:Content>
