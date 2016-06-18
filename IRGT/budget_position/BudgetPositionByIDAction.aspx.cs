using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class budget_BudgetPositionByIDAction : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string PageFunction = "budget_position";
        string LANG = cCommon.getLanguage(Request);
        string USER = cCommon.getUserName(Session);

        Session["user_code"] = USER;
        Session["language_" + PageFunction] = LANG;

        string BP_ID = Request.QueryString["BP_ID"];
        Session["BP_ID"] = BP_ID;

        if (LANG == cCommon.Language_Thai)
        {
            //================ POPUP ==================================================
            string PageName = "รายละเอียดงบบุคลากร";

            Session["pop_add_" + PageFunction] = "หน้าต่างจัดการ - เพิ่มข้อมูล" + PageName;
            Session["pop_edit_" + PageFunction] = "หน้าต่างจัดการ - แก้ไขข้อมูล" + PageName;
            Session["pop_sum_" + PageFunction] = "หน้าต่างจัดการ - สรุปข้อมูล" + PageName;
            Session["pop_confirms_" + PageFunction] = "คุณแน่ใจหรือไม่ที่จะยืนยันการตรวจสอบข้อมูล " + PageName + " : " + BP_ID + " นี้ ?";
            Session["pop_approve_" + PageFunction] = "คุณแน่ใจหรือไม่ที่จะอนุมัติข้อมูล " + PageName + " : " + BP_ID + " นี้ ?";
            Session["pop_confirm_" + PageFunction] = "ยืนยันการทำรายการ";

            Session["save_button_text"] = "บันทึก";
            Session["close_button_text"] = "ปิดหน้านี้";
            //================ POPUP ERROR ===========================================
            Session[PageFunction + "_ERROR_01"] = "โปรดระบุ รายการ";
            Session[PageFunction + "_ERROR_02"] = "โปรดเลือก ประเภทค่าใช้จ่าย";
            Session[PageFunction + "_ERROR_03"] = "โปรดระบุ จำนวน";
            Session[PageFunction + "_ERROR_04"] = "โปรดระบุ จำนวนเงิน";
            //================ TABLE ==================================================
            Session["HeaderText"] = PageName + " : " + BP_ID;
            Session["HeaderTable"] = "ข้อมูล - " + PageName;
            Session["add_button"] = "เพิ่มข้อมูล";
            Session["save_button"] = "บันทึกข้อมูล";
            Session["sum_button"] = "สรุปคำขอ";
            Session["confirm_button"] = "ยืนยันคำขอ";
            Session["approve_button"] = "อนุมัติคำขอ";
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
            Session[PageFunction + "_Column01"] = "ตำแหน่ง";
            Session[PageFunction + "_Column02"] = "คุณวุฒิ";
            Session[PageFunction + "_Column03"] = "ระยะเวลาจ้าง";
            Session[PageFunction + "_Column04"] = "จำนวน";
            Session[PageFunction + "_Column041"] = "จำนวนปรับแก้";
            Session[PageFunction + "_Column05"] = "จำนวนเงิน";
            Session[PageFunction + "_Column051"] = "จำนวนเงินปรับแก้";
            Session[PageFunction + "_Column06"] = "เหตุผล";

        }
        else
        {
            //================ POPUP ==================================================
            string PageName = "รายละเอียดงบบุคลากร";

            Session["pop_add_" + PageFunction] = "หน้าต่างจัดการ - เพิ่มข้อมูล" + PageName;
            Session["pop_edit_" + PageFunction] = "หน้าต่างจัดการ - แก้ไขข้อมูล" + PageName;
            Session["pop_sum_" + PageFunction] = "หน้าต่างจัดการ - สรุปข้อมูล" + PageName;
            Session["pop_confirms_" + PageFunction] = "คุณแน่ใจหรือไม่ที่จะยืนยันการตรวจสอบข้อมูล " + PageName + " : " + BP_ID + " นี้ ?";
            Session["pop_approve_" + PageFunction] = "คุณแน่ใจหรือไม่ที่จะอนุมัติข้อมูล " + PageName + " : " + BP_ID + " นี้ ?";
            Session["pop_confirm_" + PageFunction] = "ยืนยันการทำรายการ";

            Session["save_button_text"] = "บันทึก";
            Session["close_button_text"] = "ปิดหน้านี้";
            //================ POPUP ERROR ===========================================
            Session[PageFunction + "_ERROR_01"] = "โปรดระบุ รายการ";
            Session[PageFunction + "_ERROR_02"] = "โปรดเลือก ประเภทค่าใช้จ่าย";
            Session[PageFunction + "_ERROR_03"] = "โปรดระบุ จำนวน";
            Session[PageFunction + "_ERROR_04"] = "โปรดระบุ จำนวนเงิน";
            //================ TABLE ==================================================
            Session["HeaderText"] = PageName + " : " + BP_ID;
            Session["HeaderTable"] = "ข้อมูล - " + PageName;
            Session["add_button"] = "เพิ่มข้อมูล";
            Session["save_button"] = "บันทึกข้อมูล";
            Session["sum_button"] = "สรุปคำขอ";
            Session["confirm_button"] = "ยืนยันคำขอ";
            Session["approve_button"] = "อนุมัติคำขอ";
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
            Session[PageFunction + "_Column01"] = "ตำแหน่ง";
            Session[PageFunction + "_Column02"] = "คุณวุฒิ";
            Session[PageFunction + "_Column03"] = "ระยะเวลาจ้าง";
            Session[PageFunction + "_Column04"] = "จำนวน";
            Session[PageFunction + "_Column041"] = "จำนวนปรับแก้";
            Session[PageFunction + "_Column05"] = "จำนวนเงิน";
            Session[PageFunction + "_Column051"] = "จำนวนเงินปรับแก้";
            Session[PageFunction + "_Column06"] = "เหตุผล";
        }
    }
}