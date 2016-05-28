using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class budget_popup_pop_BudgetAssetSummaryByID : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string PageFunction = "budget_asset_summary";

        //================ Column Pop up Summary ==================================================   
        Session[PageFunction + "_ColumnSEQ"] = "ลำดับ";
        Session[PageFunction + "_ColumnEdit"] = "";
        Session[PageFunction + "_Column01"] = "รายการ";
        Session[PageFunction + "_Column02"] = "จำนวนตามกรอบ";
        Session[PageFunction + "_Column03"] = "หน่วยนับ";
        Session[PageFunction + "_Column04"] = "จำนวนหน่วย";
        Session[PageFunction + "_Column05"] = "ได้รับจากงบ";
        Session[PageFunction + "_Column06"] = "พ.ศ.ที่ได้รับ";
        Session[PageFunction + "_Column07"] = "คส.กรมฯ 269/2556(ปี)";
        Session[PageFunction + "_Column08"] = "อายุ/ปี";
        Session[PageFunction + "_Column09"] = "ดี";
        Session[PageFunction + "_Column10"] = "ซ่อม";
        Session[PageFunction + "_Column11"] = "จำหน่าย";
        Session[PageFunction + "_Column12"] = "จำนวน";
        Session[PageFunction + "_Column13"] = "ราคาต่อหน่วย";
        Session[PageFunction + "_Column14"] = "จำนวนเงิน";
        Session[PageFunction + "_Column15"] = "หมายเหตุ";
    }
}