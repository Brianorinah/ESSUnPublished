﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="HRPortal.Dashboard" %>

<%@ Import Namespace="System.Runtime.InteropServices" %>
<%@ Import Namespace="HRPortal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <ol class="breadcrumb float-sm-right">
                <li class="breadcrumb-item"><a href="Dashboard.aspx">Boskovic Air Charters Self Service Portal</a></li>
                <li class="breadcrumb-item active">Dashboard</li>
            </ol>
        </div>
    </div>
    <% //var nav = new Config().ReturnNav();
        String employeeNo = Convert.ToString(Session["employeeNo"]);
        //string tstatus =
        decimal unSurrCount = 0;
        decimal purchCount = 0;
        decimal storeCount = 0;
        decimal imprestCount = 0;
        decimal surrenderCount = 0;
        decimal staffCount = 0;

        var allImprestsUnsurrendered = new Config().ObjNav1().fnUnSurrenderedImprests(employeeNo);

        string[] info2 = allImprestsUnsurrendered.Split(new string[] { "::::" }, StringSplitOptions.RemoveEmptyEntries);
        if (info2.Count() > 0)
        {
            foreach (var allInfo in info2)
            {
                String[] arr = allInfo.Split('*');
                unSurrCount = unSurrCount + Convert.ToDecimal(arr[3]);
            }
        }


    %>
    <div class="main">
        <div class="main-inner">
            <div class="container" style="max-width: 1050px;">
                <div class="row" style="width: 98%;">
                    <div class="col-md-6 col-lg-6">

                        <div class="widget">
                            <div class="widget-header">
                                <i class="icon-file"></i>
                                <h3>Welcome <%=Session["name"]%></h3>
                            </div>
                            <div runat="server" id="photosize"></div>
                            <!-- /widget-header -->
                            <div class="widget-content">
                                <center>
                                <div style="width: 100%; display: block; margin: auto;">
                                    <img id="passportimage" runat="server" CssClass="img-circle"/>
                                </div>
                                </center>
                                <div runat="server" id="documentsfeedback"></div>
                                <button type="button" class="btn btn-primary pull-right" data-toggle="modal" data-target="#myModal">Upload Image</button>
                                <br />
                                <table class="table table-striped table-bordered">

                                    <tbody>
                                        <tr>
                                            <td style="font-weight: bold">Employee Number:</td>
                                            <td><%= Session["employeeNo"]%></td>
                                        </tr>
                                        <tr>
                                            <td style="font-weight: bold">Name:</td>
                                            <td><%= Session["name"] %></td>
                                        </tr>
                                        <!--<tr><td> ID Number:</td><td> <%--= employee.ID_Number --%> </td></tr> -->
                                        <tr>
                                            <td style="font-weight: bold">Email:</td>
                                            <td><%= Session["EmailAddress"] %> </td>
                                        </tr>
                                        <%--<tr>
                                            <td style="font-weight: bold">Phone Number:</td>
                                            <td><%= Session["PhoneNo"] %> </td>
                                        </tr>--%>
                                        <tr>
                                            <td style="font-weight: bold">Unsurrendered Imprest Amount:</td>
                                            <td>Kshs. <%= unSurrCount %> </td>
                                        </tr>


                                    </tbody>
                                </table>
                            </div>
                            <!-- /widget-content -->
                        </div>
                        <!-- /widget -->
                    </div>
                    <div class="col-md-6 col-lg-6">

                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                            <!-- small box -->
                            <div class="small-box bg-aqua">
                                <div class="inner">
                                    <h3>
                                        <% 
                                            var requestPurchase = new Config().ObjNav1().fnPurchaseRequisitions(employeeNo);
                                            String[] infoPurchase = requestPurchase.Split(new string[] { "::::" }, StringSplitOptions.RemoveEmptyEntries);
                                            if (infoPurchase != null)
                                            {
                                                foreach (var allinfo in infoPurchase)
                                                {
                                                    String[] arr = allinfo.Split('*');
                                                    if (arr[2] == "Open" || arr[2] == "Pending Approval")
                                                    {
                                                        purchCount++;
                                                    }
                                                }
                                            }


                                        %>
                                        <% = purchCount %>
                                    </h3>

                                    <p>Purchase Requisitions</p>
                                </div>
                                <%--            <div class="icon">
              <i class="fa fa-sign-out"></i>
            </div>--%>
                                <a href="PurchaseRequisitions.aspx?status=open" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                            </div>
                        </div>
                        <!-- ./col -->
                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                            <!-- small box -->
                            <div class="small-box bg-green">
                                <div class="inner">
                                    <h3>
                                        <% 
                                            var requestStaff = new Config().ObjNav1().fnStaffClaims(employeeNo, 6);
                                            String[] infoStaff = requestStaff.Split(new string[] { "::::" }, StringSplitOptions.RemoveEmptyEntries);
                                            if (infoStaff != null)
                                            {
                                                foreach (var allinfo in infoStaff)
                                                {
                                                    String[] arr = allinfo.Split('*');
                                                    if (arr[3] == "Open" || arr[3] == "Pending Approval")
                                                    {
                                                        staffCount++;
                                                    }
                                                }
                                            }
                                        %>
                                        <% = staffCount %>
                                    </h3>

                                    <p>Store Requisitions</p>
                                </div>
                                <%--            <div class="icon">
              <i class="fa fa-money"></i>
            </div>--%>
                                <a href="StaffClaims.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                            </div>
                        </div>
                        <!-- ./col -->
                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                            <!-- small box -->
                            <div class="small-box bg-yellow">
                                <div class="inner">
                                    <h3>
                                        <% 
                                            var requestStore = new Config().ObjNav1().fnStoreRequisitions(employeeNo);
                                            String[] infoStore = requestStore.Split(new string[] { "::::" }, StringSplitOptions.RemoveEmptyEntries);
                                            if (infoStore != null)
                                            {
                                                foreach (var allinfo in infoStore)
                                                {
                                                    String[] arr = allinfo.Split('*');
                                                    if (arr[3] == "Open" || arr[3] == "Pending Approval")
                                                    {
                                                        storeCount++;
                                                    }
                                                }
                                            }
                                        %>
                                        <% = storeCount %>
                                    </h3>

                                    <p>Staff Claims</p>
                                </div>
                                <%--            <div class="icon">
              <i class="fa fa-money"></i>
            </div>--%>
                                <a href="StoreRequisitions.aspx?status=open" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                            </div>
                        </div>
                        <!-- ./col -->
                        <%--<div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                            <!-- small box -->
                            <div class="small-box bg-red">
                                <div class="inner">
                                    <h3>
                                        <% 
                                            var request3 = new Config().ObjNav1().fnPettycashHeader(employeeNo, 1);
                                            int pettyCash = request3.Split(new string[] { "::::" }, StringSplitOptions.RemoveEmptyEntries).Count();
                                        %>
                                        <% = pettyCash %>
                                    </h3>

                                    <p>Petty cash</p>
                                </div>
                              
                                <a href="PettyCashApproved.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                            </div>
                        </div>--%>
                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                            <!-- small box -->
                            <div class="small-box bg-aqua">
                                <div class="inner">
                                    <h3>
                                        <% 
                                            var requestImprest = new Config().ObjNav1().fnImprestHeader(employeeNo);
                                            String[] infoImprest = requestImprest.Split(new string[] { "::::" }, StringSplitOptions.RemoveEmptyEntries);
                                            if (infoImprest != null)
                                            {
                                                foreach (var allinfo in infoImprest)
                                                {
                                                    String[] arr = allinfo.Split('*');
                                                    if (arr[4] == "Open" || arr[4] == "Pending Approval")
                                                    {
                                                        imprestCount++;
                                                    }
                                                }
                                            }
                                        %>
                                        <% = imprestCount %>
                                    </h3>

                                    <p>Imprest Requisitions</p>
                                </div>
                                <%--            <div class="icon">
              <i class="fa fa-shopping-cart"></i>
            </div>--%>
                                <a href="ImprestMemo.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                            </div>
                        </div>
                        <!-- ./col -->
                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                            <!-- small box -->
                            <div class="small-box bg-green">
                                <div class="inner">
                                    <h3>
                                        <% 
                                            var requestSurrender = new Config().ObjNav1().fnImprestSurrenders(employeeNo);
                                            String[] infoSurrender = requestSurrender.Split(new string[] { "::::" }, StringSplitOptions.RemoveEmptyEntries);
                                            if (infoSurrender != null)
                                            {
                                                foreach (var allinfo in infoSurrender)
                                                {
                                                    String[] arr = allinfo.Split('*');
                                                    if (arr[4] == "Open" || arr[4] == "Pending Approval")
                                                    {
                                                        surrenderCount++;
                                                    }
                                                }
                                            }
                                        %>
                                        <% = surrenderCount %>
                                    </h3>

                                    <p>Imprest Surrenders</p>
                                </div>
                                <%--            <div class="icon">
              <i class="fa fa-building"></i>
            </div>--%>
                                <a href="ImprestSurrenders.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                            </div>
                        </div>
                        <!-- ./col -->
                    </div>
                </div>
                <!-- /row -->
            </div>
            <!-- /container -->
        </div>
        <!-- /main-inner -->
    </div>
    <div id="myModal" class="modal" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Upload your passport Size Photo</h4>
                </div>
                <div class="modal-body">
                    <asp:FileUpload runat="server" ID="document" CssClass="form-control" Style="padding-top: 0px;" />
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                    <asp:Button runat="server" CssClass="btn btn-success" Text="Upload Image" OnClick="UPloadImage_Click" />
                </div>
            </div>

        </div>
    </div>
</asp:Content>
