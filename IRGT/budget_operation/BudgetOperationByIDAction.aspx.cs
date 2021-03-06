﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class budget_BudgetOperationByIDAction : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string PageFunction = "budget_operation";
        string LANG = cCommon.getLanguage(Request);
        string USER = cCommon.getUserName(Session);

        Session["user_code"] = USER;
        Session["language_" + PageFunction] = LANG;

        string BO_ID = Request.QueryString["BO_ID"];
        Session["BO_ID"] = BO_ID;

        if (LANG == cCommon.Language_Thai)
        {
            //================ POPUP ==================================================
            string PageName = "รายละเอียดงบดำเนินการ";

            Session["pop_add_" + PageFunction] = "หน้าต่างจัดการ - เพิ่มข้อมูล" + PageName;
            Session["pop_edit_" + PageFunction] = "หน้าต่างจัดการ - แก้ไขข้อมูล" + PageName;
            Session["pop_sum_" + PageFunction] = "หน้าต่างจัดการ - สรุปข้อมูล" + PageName;
            Session["pop_confirms_" + PageFunction] = "คุณแน่ใจหรือไม่ที่จะยืนยันการตรวจสอบข้อมูล " + PageName + " : " + BO_ID + " นี้ ?";
            Session["pop_approve_" + PageFunction] = "คุณแน่ใจหรือไม่ที่จะอนุมัติข้อมูล " + PageName + " : " + BO_ID + " นี้ ?";
            Session["pop_confirm_" + PageFunction] = "ยืนยันการทำรายการ";

            Session["save_button_text"] = "บันทึก";
            Session["close_button_text"] = "ปิดหน้านี้";
            //================ POPUP ERROR ===========================================
            Session[PageFunction + "_ERROR_01"] = "โปรดระบุ รายการ";
            Session[PageFunction + "_ERROR_02"] = "โปรดเลือก ประเภทค่าใช้จ่าย";
            Session[PageFunction + "_ERROR_03"] = "โปรดระบุ จำนวน";
            Session[PageFunction + "_ERROR_04"] = "โปรดระบุ จำนวนเงิน";
            //================ TABLE ==================================================
            Session["HeaderText"] = PageName + " : " + BO_ID;
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
            Session["HeaderSubGroup"] = "ประวัติคำขอประมาณการ";
            Session["HeaderCurrent"] = PageName;
            //================ Column ==================================================   
            Session[PageFunction + "_ColumnSEQ"] = "ลำดับ";
            Session[PageFunction + "_ColumnEdit"] = "";
            Session[PageFunction + "_Column01"] = "รายการ";
            Session[PageFunction + "_Column02"] = "ประเภทค่าใช้จ่าย";
            Session[PageFunction + "_Column03"] = "จำนวน";
            Session[PageFunction + "_Column031"] = "จำนวนปรับแก้";
            Session[PageFunction + "_Column04"] = "จำนวนเงิน";
            Session[PageFunction + "_Column041"] = "จำนวนเงินปรับแก้";
            Session[PageFunction + "_Column05"] = "เหตุผล";

        }
        else
        {
            //================ POPUP ==================================================
            string PageName = "รายละเอียดงบดำเนินการ";

            Session["pop_add_" + PageFunction] = "หน้าต่างจัดการ - เพิ่มข้อมูล" + PageName;
            Session["pop_edit_" + PageFunction] = "หน้าต่างจัดการ - แก้ไขข้อมูล" + PageName;
            Session["pop_sum_" + PageFunction] = "หน้าต่างจัดการ - สรุปข้อมูล" + PageName;
            Session["pop_confirms_" + PageFunction] = "คุณแน่ใจหรือไม่ที่จะยืนยันการตรวจสอบข้อมูล " + PageName + " : " + BO_ID + " นี้ ?";
            Session["pop_approve_" + PageFunction] = "คุณแน่ใจหรือไม่ที่จะอนุมัติข้อมูล " + PageName + " : " + BO_ID + " นี้ ?";
            Session["pop_confirm_" + PageFunction] = "ยืนยันการทำรายการ";

            Session["save_button_text"] = "บันทึก";
            Session["close_button_text"] = "ปิดหน้านี้";
            //================ POPUP ERROR ===========================================
            Session[PageFunction + "_ERROR_01"] = "โปรดระบุ รายการ";
            Session[PageFunction + "_ERROR_02"] = "โปรดเลือก ประเภทค่าใช้จ่าย";
            Session[PageFunction + "_ERROR_03"] = "โปรดระบุ จำนวน";
            Session[PageFunction + "_ERROR_04"] = "โปรดระบุ จำนวนเงิน";
            //================ TABLE ==================================================
            Session["HeaderText"] = PageName + " : " + BO_ID;
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
            Session["HeaderSubGroup"] = "ประวัติคำขอประมาณการ";
            Session["HeaderCurrent"] = PageName;
            //================ Column ==================================================   
            Session[PageFunction + "_ColumnSEQ"] = "ลำดับ";
            Session[PageFunction + "_ColumnEdit"] = "";
            Session[PageFunction + "_Column01"] = "รายการ";
            Session[PageFunction + "_Column02"] = "ประเภทค่าใช้จ่าย";
            Session[PageFunction + "_Column03"] = "จำนวน";
            Session[PageFunction + "_Column031"] = "จำนวนปรับแก้";
            Session[PageFunction + "_Column04"] = "จำนวนเงิน";
            Session[PageFunction + "_Column041"] = "จำนวนเงินปรับแก้";
            Session[PageFunction + "_Column05"] = "เหตุผล";
        }
    }
}