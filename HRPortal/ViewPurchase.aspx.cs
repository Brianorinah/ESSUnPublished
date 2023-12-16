using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HRPortal
{
    public partial class ViewPurchase : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String employeeNo = Convert.ToString(Session["employeeNo"]);
            string requisitionNo = Request.QueryString["docNo"];

            if (!string.IsNullOrEmpty(requisitionNo))
            {
                var request = new Config().ObjNav1().fnPurchaseRequisitionsSingle(employeeNo, requisitionNo);
                String[] info = request.Split(new string[] { "::::" }, StringSplitOptions.RemoveEmptyEntries);
                if (info != null)
                {
                    foreach (var allinfo in info)
                    {
                        String[] arr = allinfo.Split('*');
                        dpt.SelectedValue = arr[8];
                        bsnCode.SelectedValue = arr[9];
                        airCraftCode.SelectedValue = arr[10];
                        projectCode.SelectedValue = arr[11];
                        description.Text = arr[1];
                        currCode1.SelectedValue = arr[7];

                    }
                }
            }
        }
    }
}