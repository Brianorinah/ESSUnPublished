<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewPurchase.aspx.cs" Inherits="HRPortal.ViewPurchase" %>

<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="HRPortal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="panel panel-primary">
        <div class="panel-heading">
            Purchase Requisition General Details            
        </div>
        <div class="panel-body">
            <div id="generalFeedback" runat="server"></div>

            <div class="row">
                <div class="col-md-6 col-lg-6">
                    <div class="form-group">
                        <strong>Description:</strong>
                        <asp:TextBox runat="server" ID="description" CssClass="form-control" />
                    </div>
                </div>
                <div class="col-md-6 col-lg-6">
                    <div class="form-group">
                        <strong>Department:</strong>
                        <asp:DropDownList ID="dpt" runat="server" CssClass="form-control select2"></asp:DropDownList>

                    </div>
                </div>




            </div>

            <div class="row">
                <div class="col-md-6 col-lg-6" runat="server" id="aircraft">
                    <div class="form-group">
                        <strong>Aircraft:</strong>
                        <asp:DropDownList ID="airCraftCode" runat="server" CssClass="form-control select2"></asp:DropDownList>

                    </div>
                </div>
                <div class="col-md-6 col-lg-6">
                    <div class="form-group">
                        <strong>Business Unit:</strong>
                        <asp:DropDownList ID="bsnCode" runat="server" CssClass="form-control select2"></asp:DropDownList>
                    </div>
                </div>

            </div>

            <div class="row">
                <div class="col-md-6 col-lg-6" runat="server" id="vehicle">
                    <div class="form-group">
                        <strong>Project :</strong>
                        <asp:DropDownList ID="projectCode" runat="server" CssClass="form-control select2"></asp:DropDownList>

                    </div>
                </div>
                <div class="col-md-6 col-lg-6">
                    <div class="form-group">
                        <strong>Currency:</strong>
                        <asp:DropDownList runat="server" ID="currCode1" CssClass="form-control select2"></asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6 col-lg-6">
                    <div class="form-group">
                        <strong>Requested Receipt Date:</strong>
                        <asp:TextBox runat="server" ID="rcptDate" CssClass="form-control" />
                    </div>
                </div>
            </div>

        </div>
    </div>
    <div class="panel panel-primary">
        <div class="panel-heading">
            Purchase Requisition Lines            
        <div class="panel-body">
            <div runat="server" id="linesFeedback"></div>
            <table class="table table-bordered table-striped">
                <thead>
                    <tr>
                        <th>Item Category</th>
                        <th>Item </th>
                        <th>Delivery Location </th>
                        <th>Unit Cost </th>
                        <th>Quantity Requested </th>
                        <th>Amount </th>
                    </tr>
                </thead>
                <tbody>
                    <%
                        String requisitionNo = Request.QueryString["docNo"];
                        var nav = new Config().ObjNav1();
                        var result = nav.fnPurchaseRequisitionLines(requisitionNo);
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
                        <td><% =arr[6] %></td>
                        <td><% =arr[2] %></td>
                        <td><% = arr[5] %></td>
                        <td><% = arr[8] %></td>
                        <td><% = arr[1] %></td>
                        <td><%=String.Format("{0:n}", Convert.ToDouble(arr[7])) %></td>

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
    </div>
</asp:Content>
