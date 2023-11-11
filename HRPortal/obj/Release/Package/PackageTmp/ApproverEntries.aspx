<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ApproverEntries.aspx.cs" Inherits="HRPortal.ApproverEntries" %>
<%@ Import Namespace="HRPortal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
   <% var nav = new Config().ObjNav1(); %>
    <div class="row">
        <div class="cil-md-12 col-lg-12 col-sm-12 col-xs-12">
            <div class="panel panel-primary">
                <div class="panel-heading">
                  Record Requisition Approval Entries
                </div>
                <div class="panel-body">
                    <div runat="server" id="feedback"></div>
                    <table class="table table-striped table-bordered">
                        <thead>
                            <tr>
                                <th>Sequence No.</th>
                                <th>Status</th>
                                <th>Sender Id</th>
                                <th>Approver Id</th>
                                <th>Amount</th>
                                <th>Due Date(dd/mm/yyyy)</th>
                                <th>Date Sent for Approval(dd/mm/yyyy)</th>                                
                                <th>Comment(s)</th>

                            </tr>
                        </thead>
                        <tbody>
                            <%
                                String docNo = Request.QueryString["docNo"];
                                var request = nav.fnApprovalEntries(docNo);
                                String[] info = request.Split(new string[] { "::::" }, StringSplitOptions.RemoveEmptyEntries);
                                if (info != null)
                                {
                                    foreach (var allinfo in info)
                                    {
                                        String[] arr = allinfo.Split('*');
                                        %>
                                <tr>
                                    <td><%=arr[0]%></td>
                                <td><%=arr[1]%></td>
                                <td><%=arr[2]%></td>
                                <td><%=arr[3]%></td>
                                <td><%=arr[4]%></td> 
                                <td><%=Convert.ToDateTime(arr[5]).ToString("dd/MM/yyyy")%></td>
                                    <td><%=Convert.ToDateTime(arr[6]).ToString("dd/MM/yyyy")%></td>
                               
                                <td>
                                    <%
                                        if (arr[7].ToUpper() == "YES")
                                        {
                                            Session["tableid"] = arr[8];
                                            Session["approver"] = arr[3];
                                            Session["recordApprove"] = arr[9];
                                        %>
                                    <label class="btn btn-success" onclick="ViewComment('<%=arr[8] %>','<%=arr[3] %>','<%=arr[10] %>','<%=arr[9] %>');"><i class="fa fa-eye"></i> View Comment</label>
                                <%
                                    }
                                    else if (arr[7].ToUpper() == "NO")
                                    {

                                           %>
                                     <label class="btn btn-warning"('<%=arr[8] %>');"><i class="fa fa-times"></i> No Comment</label>
                        
                                    <% 
                                    } %>                                              

                                </td>
                              </tr>
                                <%

                                }
                            }
                                 %>  
                        </tbody>
                    </table>
                </div>
            </div>
            <%--<asp:Button ID="GoBack" runat="server" CssClass="btn btn-primary" Text="Go Back" OnClick="GoBack_Click" />--%>
        </div>

    </div>


<div id="ApprvalModal" class="modal fade" role="dialog">
  <div class="modal-dialog modal-lg">   
    <!-- Modal content--> 
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal">&times;</button>
        <h4 class="modal-title">Approval Comments</h4>
      </div>
      <div class="modal-body">
              <table class="table table-striped table-bordered"  style="width:100%">
                <thead>
                  <tr>
                    <th>Comment</th>
                    <th>Approver</th>
                    <th> Date And Time</th>            
                  </tr>
                </thead>
                <tbody>
                <asp:TextBox runat="server" ID="txttableId" type="hidden"/>
                <asp:TextBox runat="server" ID="txtapproverId" type="hidden"/>
                <asp:TextBox runat="server" ID="txtworkflowId" type="hidden"/>
                <asp:TextBox runat="server" ID="txtrecordToapprove" type="hidden"/>
                <%
                    string mtableId = Convert.ToString(Session["tableid"]);
                    int ytableid = 0;
                    try
                    {
                        ytableid = Convert.ToInt32(mtableId);
                    }
                    catch
                    { }
                    string mapproverId = Convert.ToString(Session["approver"]);
                    //  var ncomment = nav.ApprovalCommentLine.Where(x=> x.Table_ID == ytableid  && x.Record_ID_to_Approve == "Purchase Header: Purchase Requisition," +docNo);
                    var result = nav.fnApprovalCommentLine(mtableId);
                    String[] info1 = result.Split(new string[] { "::::" }, StringSplitOptions.RemoveEmptyEntries);
                    if (info1 != null)
                    {
                        foreach (var allinfo in info1)
                        {
                            String[] arr = allinfo.Split();
                            %>
                    <tr>
                        <td><%=arr[0]%></td>
                        <td><%=arr[0]%></td>
                        <td><%=arr[0]%></td>
                    </tr>  
                    <%  
                            }
                        }

                     %>                            
                
                </tbody>
              </table>
      </div>
      <div class="modal-footer">

        <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
      </div>
    </div>
  </div>
</div>
<script>
    function ViewComment(tableId, approverId, workflowId) {
        document.getElementById("ContentPlaceHolder1_txttableId").value = tableId;
        document.getElementById("ContentPlaceHolder1_txtapproverId").value = approverId;
        document.getElementById("ContentPlaceHolder1_txtworkflowId").value = workflowId;
        document.getElementById("ContentPlaceHolder1_txtrecordToapprove").value = workflowId;
        $("#ApprvalModal").modal();
    }
</script>
</asp:Content>
