using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class budget_BudgetPositionList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string PageFunction = "budget_position_list";
        string LANG = cCommon.getLanguage(Request);
        string USER = cCommon.getUserName(Session);

        Session["user_code"] = USER;
        Session["language_" + PageFunction] = LANG;

        if (LANG == cCommon.Language_Thai)
        {
            //================ POPUP ==================================================
            string PageName = "งบบุคลากร";
            
            Session["pop_sum_" + PageFunction] = "หน้าต่างจัดการ - สรุปข้อมูล" + PageName;
            
            //================ TABLE ==================================================
            Session["HeaderText"] = PageName;
            Session["HeaderTable"] = "รายการ - " + PageName;
            Session["sum_button"] = "สรุปคำขอ";
            Session["detail_button"] = "ดูรายละเอียด";
            Session["PageText"] = "หน้าที่";
            Session["PageLast"] = "หน้า";
            Session["PageStart"] = "จำนวนทั้งหมด";
            Session["PageRecord"] = "รายการ";
            //================ Navigate ================================================
            Session["HeaderGroup"] = "งบประมาณเงินทุนหมุนเวียน";
            Session["HeaderSubGroup"] = "ประวัติคำขอประมาณการ";
            Session["HeaderCurrent"] = PageName;
            //================ Column ==================================================   
            Session[PageFunction + "_ColumnSEQ"] = "ลำดับ";
            Session[PageFunction + "_ColumnEdit"] = "";
            Session[PageFunction + "_Column01"] = "หน่วยงาน";
            Session[PageFunction + "_Column02"] = "ปีงบประมาณ";
            Session[PageFunction + "_Column03"] = "วันที่ส่งคำขอ";
            Session[PageFunction + "_Column04"] = "วันที่ปรับปรุง";
            Session[PageFunction + "_Column05"] = "สถานะ";

        }
        else
        {
            //================ POPUP ==================================================
            string PageName = "งบบุคลากร";

            Session["pop_sum_" + PageFunction] = "หน้าต่างจัดการ - สรุปข้อมูล" + PageName;

            //================ TABLE ==================================================
            Session["HeaderText"] = PageName;
            Session["HeaderTable"] = "รายการ - " + PageName;
            Session["sum_button"] = "สรุปคำขอ";
            Session["detail_button"] = "ดูรายละเอียด";
            Session["PageText"] = "หน้าที่";
            Session["PageLast"] = "หน้า";
            Session["PageStart"] = "จำนวนทั้งหมด";
            Session["PageRecord"] = "รายการ";
            //================ Navigate ================================================
            Session["HeaderGroup"] = "งบประมาณเงินทุนหมุนเวียน";
            Session["HeaderSubGroup"] = "ประวัติคำขอประมาณการ";
            Session["HeaderCurrent"] = PageName;
            //================ Column ==================================================   
            Session[PageFunction + "_ColumnSEQ"] = "ลำดับ";
            Session[PageFunction + "_ColumnEdit"] = "";
            Session[PageFunction + "_Column01"] = "หน่วยงาน";
            Session[PageFunction + "_Column02"] = "ปีงบประมาณ";
            Session[PageFunction + "_Column03"] = "วันที่ส่งคำขอ";
            Session[PageFunction + "_Column04"] = "วันที่ปรับปรุง";
            Session[PageFunction + "_Column05"] = "สถานะ";
        }
    }
}