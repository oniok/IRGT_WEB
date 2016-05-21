using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class budget_popup_pop_BudgetOperationDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string PageFunction = "budget_operation_detail";

        string PageFunctionSum = "budget_operation_summary";
        string PageFunctionDetail = "budget_operation";

        //================ Pop up Deatail ==================================================   
        Session[PageFunctionDetail + "_ColumnSEQ"] = "ลำดับ";
        Session[PageFunctionDetail + "_ColumnEdit"] = "";
        Session[PageFunctionDetail + "_Column01"] = "รายการ";
        Session[PageFunctionDetail + "_Column02"] = "ประเภทค่าใช้จ่าย";
        Session[PageFunctionDetail + "_Column03"] = "จำนวน";
        Session[PageFunctionDetail + "_Column04"] = "จำนวนเงิน";
        Session[PageFunctionDetail + "_Column05"] = "เหตุผล";

        //================ Pup up Summary ==================================================   
        Session[PageFunctionSum + "_ColumnSEQ"] = "ลำดับ";
        Session[PageFunctionSum + "_ColumnEdit"] = "";
        Session[PageFunctionSum + "_Column01"] = "รายการ";
        Session[PageFunctionSum + "_Column02"] = "ประเภทค่าใช้จ่าย";
        Session[PageFunctionSum + "_Column03"] = "จำนวนเงิน/เดือน";
        Session[PageFunctionSum + "_Column04"] = "รวมทั้งสิ้น";
        Session[PageFunctionSum + "_Column05"] = "หมายเหตุ";

        //================ TABLE ==================================================
        Session["HeaderText_Summary"] = "สรุปข้อมูลงบดำเนินการ";
        Session["HeaderText_Detail"] = "รายละเอียดข้อมูลงบดำเนินการ";

    }
}