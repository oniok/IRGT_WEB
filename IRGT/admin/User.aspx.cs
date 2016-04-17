using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_User : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string PageFunction = "user";
        string LANG = cCommon.getLanguage(Request);
        Session["language_" + PageFunction] = LANG;
        if (LANG == cCommon.Language_Thai)
        {
            //================ POPUP ==================================================
            string PageName = "ผู้ใช้งาน";

            Session["pop_add_" + PageFunction] = "หน้าต่างจัดการ - เพิ่มข้อมูล" + PageName;
            Session["pop_edit_" + PageFunction] = "หน้าต่างจัดการ - แก้ไขข้อมูล" + PageName;
            Session["pop_delete_" + PageFunction] = "คุณแน่ใจหรือไม่ที่จะลบข้อมูล" + PageName + "นี้ ?";
            Session["pop_confirm_" + PageFunction] = "ยืนยันการลบข้อมูล";

            Session["save_button_text"] = "บันทึก";
            Session["close_button_text"] = "ปิดหน้านี้";
            //================ POPUP ERROR ===========================================
            Session[PageFunction + "_ERROR_01"] = "โปรดระบุประเภทผู้ใช้งาน";
            Session[PageFunction + "_ERROR_02"] = "โปรดระบุชื่อผู้ใช้งาน";
            Session[PageFunction + "_ERROR_03"] = "โปรดระบุรหัสผ่าน";
            //================ TABLE ==================================================
            Session["HeaderText"] = PageName;
            Session["HeaderTable"] = "ข้อมูล - " + PageName;
            Session["add_button"] = "เพิ่มข้อมูล";
            Session["PageText"] = "หน้าที่";
            Session["PageLast"] = "หน้า";
            Session["PageStart"] = "จำนวนทั้งหมด";
            Session["PageRecord"] = "รายการ";
            //================ Navigate ================================================
            Session["HeaderGroup"] = "ผู้ดูแลระบบ";
            Session["HeaderSubGroup"] = "อื่น ๆ";
            Session["HeaderCurrent"] = PageName;
            //================ Column ==================================================   
            Session[PageFunction + "_ColumnSEQ"] = "ลำดับ";
            Session[PageFunction + "_ColumnEdit"] = "";
            Session[PageFunction + "_Column01"] = "ชื่อผู้ใช้งาน";
            Session[PageFunction + "_Column02"] = "ประเภทผู้ใช้งาน";
            Session[PageFunction + "_Column03"] = "สถานะ";
            Session[PageFunction + "_Column04"] = "รหัสผ่าน";
        }
        else
        {
            //================ POPUP ==================================================
            string PageName = "User";
            Session["pop_add_" + PageFunction] = "window management - New " + PageName;
            Session["pop_edit_" + PageFunction] = "window management - Edit " + PageName;
            Session["pop_delete_" + PageFunction] = "Are you sure you want to delete this " + PageName + "?";
            Session["pop_confirm_" + PageFunction] = "Confirm Delete Data";

            Session["save_button_text"] = "Save";
            Session["close_button_text"] = "Close";
            //================ POPUP ERROR ===========================================
            Session[PageFunction + "_ERROR_01"] = "Please specify User type";
            Session[PageFunction + "_ERROR_02"] = "Please specify " + PageName + " Code";
            Session[PageFunction + "_ERROR_03"] = "Please specify Password";
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
            Session["HeaderSubGroup"] = "Other";
            Session["HeaderCurrent"] = PageName;
            //================ Column ==================================================     
            Session[PageFunction + "_ColumnSEQ"] = "No";
            Session[PageFunction + "_ColumnEdit"] = "";
            Session[PageFunction + "_Column01"] = "User Code";
            Session[PageFunction + "_Column02"] = "User Type";
            Session[PageFunction + "_Column03"] = "Status";
            Session[PageFunction + "_Column04"] = "Password";
        }
    }
}