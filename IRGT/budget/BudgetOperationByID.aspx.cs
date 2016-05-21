using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class budget_BudgetOperationByID : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string PageFunction = "budget_operation";
        string LANG = cCommon.getLanguage(Request);
        string USER = cCommon.getUserName(Session);

        Session["user_code"] = USER;
        Session["language_" + PageFunction] = LANG;

        if (LANG == cCommon.Language_Thai)
        {
            //================ POPUP ==================================================
            string PageName = "งบดำเนินการ";

            Session["pop_add_" + PageFunction] = "หน้าต่างจัดการ - เพิ่มข้อมูล" + PageName;
            Session["pop_edit_" + PageFunction] = "หน้าต่างจัดการ - แก้ไขข้อมูล" + PageName;
            Session["pop_sum_" + PageFunction] = "หน้าต่างจัดการ - สรุปข้อมูล" + PageName;
            Session["pop_delete_" + PageFunction] = "คุณแน่ใจหรือไม่ที่จะลบข้อมูล" + PageName + "นี้ ?";
            Session["pop_send_" + PageFunction] = "คุณแน่ใจหรือไม่ที่จะส่งข้อมูล" + PageName + "นี้ ?";
            Session["pop_confirm_" + PageFunction] = "ยืนยันการทำรายการ";

            Session["save_button_text"] = "บันทึก";
            Session["close_button_text"] = "ปิดหน้านี้";
            //================ POPUP ERROR ===========================================
            Session[PageFunction + "_ERROR_01"] = "โปรดระบุ รายการ";
            Session[PageFunction + "_ERROR_02"] = "โปรดเลือก ประเภทค่าใช้จ่าย";
            Session[PageFunction + "_ERROR_03"] = "โปรดระบุ จำนวน";
            Session[PageFunction + "_ERROR_04"] = "โปรดระบุ จำนวนเงิน";
            //================ TABLE ==================================================
            Session["HeaderText"] = PageName;
            Session["HeaderTable"] = "ข้อมูล - " + PageName;
            Session["add_button"] = "เพิ่มข้อมูล";
            Session["save_button"] = "บันทึกข้อมูล";
            Session["sum_button"] = "สรุปคำขอ";
            Session["confirm_button"] = "ส่งคำขอ";
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
            Session[PageFunction + "_Column01"] = "รายการ";
            Session[PageFunction + "_Column02"] = "ประเภทค่าใช้จ่าย";
            Session[PageFunction + "_Column03"] = "จำนวน";
            Session[PageFunction + "_Column04"] = "จำนวนเงิน";
            Session[PageFunction + "_Column05"] = "เหตุผล";

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