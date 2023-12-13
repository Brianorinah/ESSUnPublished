using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HRPortal
{
    public partial class ViewImprest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                String employeeNo = Convert.ToString(Session["employeeNo"]);
                string requisitionNo = Request.QueryString["docNo"];

                if (!string.IsNullOrEmpty(requisitionNo))
                {
                    var request = new Config().ObjNav1().fnImprestHeaderSingle(employeeNo, requisitionNo);
                    String[] info = request.Split(new string[] { "::::" }, StringSplitOptions.RemoveEmptyEntries);
                    if (info != null)
                    {
                        foreach (var allinfo in info)
                        {
                            String[] arr = allinfo.Split('*');
                            dpt.Text = arr[8];
                            //dptMach(arr[9]);
                            bsnCode.Text = arr[9];
                            airCraftCode.Text = arr[10];
                            projectCode.Text = arr[11];
                            description.Text = arr[5];
                            currCode1.Text = arr[7];
                            dateFrom.Text = arr[1];
                            dateTo.Text = arr[2];



                        }
                    }
                }
            }
        }
    }
}