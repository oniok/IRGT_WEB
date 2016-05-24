using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class budget_popup_pop_BudgetAssetSummary : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string PageFunction = "budget_asset_summary";

        //================ Column Pop up Summary ==================================================   
        Session[PageFunction + "_ColumnSEQ"] = "ลำดับ";
        Session[PageFunction + "_ColumnEdit"] = "";
        Session[PageFunction + "_Column01"] = "รายการ";
        Session[PageFunction + "_Column02"] = "จำนวน";
        Session[PageFunction + "_Column03"] = "ราคาต่อหน่วย";
        Session[PageFunction + "_Column04"] = "จำนวนเงิน";
        Session[PageFunction + "_Column05"] = "หมายเหตุ";
    }
}