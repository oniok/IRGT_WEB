using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_MovementType : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string PageFunction = "movement_type";
        string LANG = cCommon.getLanguage(Request);
        Session["language_" + PageFunction] = LANG;
        if (LANG == cCommon.Language_Thai)
        {
            //================ POPUP ==================================================
            string PageName = "ประเภทการเคลื่อนย้าย";

            Session["pop_add_" + PageFunction] = "หน้าต่างจัดการ - เพิ่มข้อมูล" + PageName;
            Session["pop_edit_" + PageFunction] = "หน้าต่างจัดการ - แก้ไขข้อมูล" + PageName;
            Session["pop_delete_" + PageFunction] = "คุณแน่ใจหรือไม่ที่จะลบข้อมูล" + PageName + "นี้ ?";
            Session["pop_confirm_" + PageFunction] = "ยืนยันการลบข้อมูล";

            Session["save_button_text"] = "บันทึก";
            Session["close_button_text"] = "ปิดหน้านี้";
            //================ POPUP ERROR ===========================================
            Session[PageFunction + "_ERROR_01"] = "โปรดระบุ รหัส" + PageName + "";
            Session[PageFunction + "_ERROR_02"] = "โปรดระบุ " + PageName + "(ไทย)";
            Session[PageFunction + "_ERROR_03"] = "โปรดระบุ " + PageName + "(อังกฤษ)";
            Session[PageFunction + "_ERROR_04"] = "โปรดระบุ วันที่เริ่มต้น";
            Session[PageFunction + "_ERROR_05"] = "โปรดระบุ วันที่สิ้นสุด";
            Session[PageFunction + "_ERROR_06"] = "วันที่สิ้นสุดต้องมากกว่าหรือเท่ากับวันที่เริ่มต้น";
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
            Session[PageFunction + "_Column01"] = "รหัส" + PageName;
            Session[PageFunction + "_Column02"] = "ชื่อ" + PageName + "(ไทย)";
            Session[PageFunction + "_Column03"] = "ชื่อ" + PageName + "(อังกฤษ)";
            Session[PageFunction + "_Column04"] = "วันที่เริ่มใช้งาน";
            Session[PageFunction + "_Column05"] = "วันที่สิ้นสุด";
            Session[PageFunction + "_Column06"] = "ค่าคำนวน";
        }
        else
        {
            //================ POPUP ==================================================
            string PageName = "Movement Type";
            Session["pop_add_" + PageFunction] = "window management - New " + PageName;
            Session["pop_edit_" + PageFunction] = "window management - Edit " + PageName;
            Session["pop_delete_" + PageFunction] = "Are you sure you want to delete this " + PageName + "?";
            Session["pop_confirm_" + PageFunction] = "Confirm Delete Data";

            Session["save_button_text"] = "Save";
            Session["close_button_text"] = "Close";
            //================ POPUP ERROR ===========================================
            Session[PageFunction + "_ERROR_01"] = "Please specify " + PageName + " ID";
            Session[PageFunction + "_ERROR_02"] = "Please specify " + PageName + "(thai)";
            Session[PageFunction + "_ERROR_03"] = "Please specify " + PageName + "(eng)";
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
            Session["HeaderSubGroup"] = "Other";
            Session["HeaderCurrent"] = PageName;
            //================ Column ==================================================     
            Session[PageFunction + "_ColumnSEQ"] = "No";
            Session[PageFunction + "_ColumnEdit"] = "";
            Session[PageFunction + "_Column01"] = PageName + " ID";
            Session[PageFunction + "_Column02"] = PageName + "Name(thai)";
            Session[PageFunction + "_Column03"] = PageName + "Name(eng)";
            Session[PageFunction + "_Column04"] = "Start Date";
            Session[PageFunction + "_Column05"] = "End Date";
            Session[PageFunction + "_Column06"] = "Value";
        }
    }
}