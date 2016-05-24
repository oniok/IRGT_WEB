using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class budget_BudgetProjectListAction : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string PageFunction = "budget_project_list";
        string LANG = cCommon.getLanguage(Request);
        string USER = cCommon.getUserName(Session);

        Session["user_code"] = USER;
        Session["language_" + PageFunction] = LANG;

        if (LANG == cCommon.Language_Thai)
        {
            //================ POPUP ==================================================
            string PageName = "งบโปรเจค";
            
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
            Session["HeaderSubGroup"] = "พิจารณาคำขอประมาณการ";
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
            string PageName = "Asset Depreciate";
            Session["pop_add_asset_type"] = "window management - New " + PageName;
            Session["pop_edit_asset_type"] = "window management - Edit " + PageName;
            Session["pop_delete_asset_type"] = "Are you sure you want to delete this " + PageName + "?";
            Session["pop_confirm_asset_type"] = "Confirm Delete Data";

            Session["save_button_text"] = "Save";
            Session["close_button_text"] = "Close";
            //================ POPUP ERROR ===========================================
            Session[PageFunction + "_ERROR_01"] = "Please select Asset";
            Session[PageFunction + "_ERROR_04"] = "Please specify Start Date";
            Session[PageFunction + "_ERROR_05"] = "Please specify End Date";
            Session[PageFunction + "_ERROR_06"] = "End Date must be greater than or equal to the start date.";
            //================ TABLE ==================================================
            Session["HeaderText"] = PageName;
            Session["HeaderTable"] = PageName + " Data";
            Session["add_button"] = "New Data";
            Session["PageText"] = "Page";
            Session["PageLast"] = "Page";
            Session["PageStart"] = "Total";
            Session["PageRecord"] = "record";
            //================ Navigate ================================================
            Session["HeaderGroup"] = "Administrator";
            Session["HeaderSubGroup"] = "Asset Master";
            Session["HeaderCurrent"] = PageName;
            //================ Column ==================================================     
            Session[PageFunction + "_ColumnSEQ"] = "No";
            Session[PageFunction + "_ColumnEdit"] = "";
            Session[PageFunction + "_Column01"] = "Asset ID";
            Session[PageFunction + "_Column02"] = "Asset Name";
            Session[PageFunction + "_Column03"] = "Depreciate Rate";
            Session[PageFunction + "_Column06"] = "Standard Price";
            Session[PageFunction + "_Column07"] = "Term Use";

            Session[PageFunction + "_Column04"] = "Start Date";
            Session[PageFunction + "_Column05"] = "End Date";
        }
    }
}