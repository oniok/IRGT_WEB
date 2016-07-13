using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class budget_BudgetProjectByID : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string PageFunction = "budget_project";
        string LANG = cCommon.getLanguage(Request);
        string USER = cCommon.getUserName(Session);

        Session["user_code"] = USER;
        Session["language_" + PageFunction] = LANG;

        string BJ_ID = Request.QueryString["BJ_ID"];
        Session["BJ_ID"] = BJ_ID;

        if (LANG == cCommon.Language_Thai)
        {
            //================ POPUP ==================================================
            string PageName = "รายละเอียดงบโครงการ";

            Session["pop_add_" + PageFunction] = "หน้าต่างจัดการ - เพิ่มข้อมูล" + PageName;
            Session["pop_edit_" + PageFunction] = "หน้าต่างจัดการ - แก้ไขข้อมูล" + PageName;
            Session["pop_sum_" + PageFunction] = "หน้าต่างจัดการ - สรุปข้อมูล" + PageName;
            Session["pop_delete_" + PageFunction] = "คุณแน่ใจหรือไม่ที่จะลบข้อมูล" + PageName + "นี้ ?";
            Session["pop_send_" + PageFunction] = "คุณแน่ใจหรือไม่ที่จะส่งข้อมูล" + PageName + "นี้ ?";
            Session["pop_save_" + PageFunction] = "คุณแน่ใจหรือไม่ที่จะบันทึกข้อมูล" + PageName + "นี้ ?";

            Session["pop_confirm_" + PageFunction] = "ยืนยันการทำรายการ";

            Session["save_button_text"] = "บันทึก";
            Session["print_button_text"] = "พิมพ์หน้านี้";
            Session["close_button_text"] = "ปิดหน้านี้";
            //================ POPUP ERROR ===========================================
            Session[PageFunction + "_ERROR_01"] = "โปรดระบุ ประเด็นยุทธศาสตร์";
            Session[PageFunction + "_ERROR_02"] = "โปรดเลือก เป้าประสงค์";
            Session[PageFunction + "_ERROR_03"] = "โปรดระบุ กลยุทธ์";
            Session[PageFunction + "_ERROR_04"] = "โปรดระบุ ชื่อโครงการ";
            Session[PageFunction + "_ERROR_05"] = "โปรดระบุ เหตุผลความจำเป็น";
            Session[PageFunction + "_ERROR_06"] = "โปรดระบุ วัตถุประสงค์";
            Session[PageFunction + "_ERROR_07"] = "โปรดระบุ สถานที่ดำเนินงาน";
            Session[PageFunction + "_ERROR_08"] = "โปรดระบุ ระยะเวลาการดำเนินงาน";
            Session[PageFunction + "_ERROR_09"] = "โปรดระบุ วงเงินทั้งสิ้นของโครงการ";
            Session[PageFunction + "_ERROR_10"] = "โปรดระบุ ขั้นตอนการดำเนินงาน หรือกิจกรรมที่สำคัญ";
            Session[PageFunction + "_ERROR_11"] = "โปรดระบุ ตัวชี้วัดความสำเร็จของโครงการ";
            Session[PageFunction + "_ERROR_12"] = "โปรดระบุ ผลประโยชน์ที่คาดว่าจะได้รับ";
            Session[PageFunction + "_ERROR_13"] = "โปรดระบุ ผู้รับผิดชอบ";
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
            Session[PageFunction + "_Column01"] = "ประเด็นยุทธศาสตร์";
            Session[PageFunction + "_Column02"] = "เป้าประสงค์";
            Session[PageFunction + "_Column03"] = "กลยุทธ์";
            Session[PageFunction + "_Column04"] = "ชื่อโครงการ";
            Session[PageFunction + "_Column05"] = "เหตุผลความจำเป็น";
            Session[PageFunction + "_Column06"] = "วัตถุประสงค์";
            Session[PageFunction + "_Column07"] = "สถานที่ดำเนินงาน";
            Session[PageFunction + "_Column08"] = "ระยะเวลาการดำเนินงาน";
            Session[PageFunction + "_Column09"] = "วงเงินทั้งสิ้นของโครงการ";
            Session[PageFunction + "_Column10"] = "ขั้นตอนการดำเนินงาน หรือกิจกรรมที่สำคัญ";
            Session[PageFunction + "_Column11"] = "ตัวชี้วัดความสำเร็จของโครงการ";
            Session[PageFunction + "_Column12"] = "ผลประโยชน์ที่คาดว่าจะได้รับ";
            Session[PageFunction + "_Column13"] = "ผู้รับผิดชอบ";
        }
        else
        {
            //================ POPUP ==================================================
            string PageName = "รายละเอียดงบโครงการ";

            Session["pop_add_" + PageFunction] = "หน้าต่างจัดการ - เพิ่มข้อมูล" + PageName;
            Session["pop_edit_" + PageFunction] = "หน้าต่างจัดการ - แก้ไขข้อมูล" + PageName;
            Session["pop_sum_" + PageFunction] = "หน้าต่างจัดการ - สรุปข้อมูล" + PageName;
            Session["pop_delete_" + PageFunction] = "คุณแน่ใจหรือไม่ที่จะลบข้อมูล" + PageName + "นี้ ?";
            Session["pop_send_" + PageFunction] = "คุณแน่ใจหรือไม่ที่จะส่งข้อมูล" + PageName + "นี้ ?";
            Session["pop_save_" + PageFunction] = "คุณแน่ใจหรือไม่ที่จะบันทึกข้อมูล" + PageName + "นี้ ?";

            Session["pop_confirm_" + PageFunction] = "ยืนยันการทำรายการ";

            Session["save_button_text"] = "บันทึก";
            Session["close_button_text"] = "ปิดหน้านี้";
            //================ POPUP ERROR ===========================================
            Session[PageFunction + "_ERROR_01"] = "โปรดระบุ ประเด็นยุทธศาสตร์";
            Session[PageFunction + "_ERROR_02"] = "โปรดเลือก เป้าประสงค์";
            Session[PageFunction + "_ERROR_03"] = "โปรดระบุ กลยุทธ์";
            Session[PageFunction + "_ERROR_04"] = "โปรดระบุ ชื่อโครงการ";
            Session[PageFunction + "_ERROR_05"] = "โปรดระบุ เหตุผลความจำเป็น";
            Session[PageFunction + "_ERROR_06"] = "โปรดระบุ วัตถุประสงค์";
            Session[PageFunction + "_ERROR_07"] = "โปรดระบุ สถานที่ดำเนินงาน";
            Session[PageFunction + "_ERROR_08"] = "โปรดระบุ ระยะเวลาการดำเนินงาน";
            Session[PageFunction + "_ERROR_09"] = "โปรดระบุ วงเงินทั้งสิ้นของโครงการ";
            Session[PageFunction + "_ERROR_10"] = "โปรดระบุ ขั้นตอนการดำเนินงาน หรือกิจกรรมที่สำคัญ";
            Session[PageFunction + "_ERROR_11"] = "โปรดระบุ ตัวชี้วัดความสำเร็จของโครงการ";
            Session[PageFunction + "_ERROR_12"] = "โปรดระบุ ผลประโยชน์ที่คาดว่าจะได้รับ";
            Session[PageFunction + "_ERROR_13"] = "โปรดระบุ ผู้รับผิดชอบ";
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
            Session[PageFunction + "_Column01"] = "ประเด็นยุทธศาสตร์";
            Session[PageFunction + "_Column02"] = "เป้าประสงค์";
            Session[PageFunction + "_Column03"] = "กลยุทธ์";
            Session[PageFunction + "_Column04"] = "ชื่อโครงการ";
            Session[PageFunction + "_Column05"] = "เหตุผลความจำเป็น";
            Session[PageFunction + "_Column06"] = "วัตถุประสงค์";
            Session[PageFunction + "_Column07"] = "สถานที่ดำเนินงาน";
            Session[PageFunction + "_Column08"] = "ระยะเวลาการดำเนินงาน";
            Session[PageFunction + "_Column09"] = "วงเงินทั้งสิ้นของโครงการ";
            Session[PageFunction + "_Column10"] = "ขั้นตอนการดำเนินงาน หรือกิจกรรมที่สำคัญ";
            Session[PageFunction + "_Column11"] = "ตัวชี้วัดความสำเร็จของโครงการ";
            Session[PageFunction + "_Column12"] = "ผลประโยชน์ที่คาดว่าจะได้รับ";
            Session[PageFunction + "_Column13"] = "ผู้รับผิดชอบ";
        }
    }
}