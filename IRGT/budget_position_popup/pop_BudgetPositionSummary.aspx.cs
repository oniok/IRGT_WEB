using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class budget_popup_pop_BudgetPositionSummary : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string PageFunction = "budget_position_summary";

        //================ Column Pop up Summary ==================================================   
        Session[PageFunction + "_ColumnSEQ"] = "ลำดับ";
        Session[PageFunction + "_ColumnEdit"] = "";
        Session[PageFunction + "_Column01"] = "ตำแหน่ง";
        Session[PageFunction + "_Column02"] = "คุณวุฒิ";
        Session[PageFunction + "_Column03"] = "จำนวน (อัตรา)";
        Session[PageFunction + "_Column04"] = "จำนวนเงิน/เดือน";
        Session[PageFunction + "_Column05"] = "รวมทั้งสิ้น";
        Session[PageFunction + "_Column06"] = "หมายเหตุ";
    }
}