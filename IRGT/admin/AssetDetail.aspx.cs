using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_AssetDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string PageFunction = "asset_detail";
        string LANG = cCommon.getLanguage(Request);
        Session["language_" + PageFunction] = LANG;

        if (LANG == cCommon.Language_Thai)
        {
            //================ POPUP ==================================================
            string PageName = "ข้อมูลครุภัณฑ์";

            Session["pop_add_" + PageFunction] = "หน้าต่างจัดการ - เพิ่มข้อมูล" + PageName;
            Session["pop_edit_" + PageFunction] = "หน้าต่างจัดการ - แก้ไขข้อมูล" + PageName;
            Session["pop_delete_" + PageFunction] = "คุณแน่ใจหรือไม่ที่จะลบข้อมูล" + PageName + "นี้ ?";
            Session["pop_confirm_" + PageFunction] = "ยืนยันการลบข้อมูล";

            Session["save_button_text"] = "บันทึก";
            Session["close_button_text"] = "ปิดหน้านี้";
            //================ POPUP ERROR ===========================================
            Session[PageFunction + "_ERROR_01"] = "โปรดเลือก หน่วยงาน";
            Session[PageFunction + "_ERROR_02"] = "โปรดเลือก หมวดครุภัณฑ์";
            Session[PageFunction + "_ERROR_03"] = "โปรดเลือก ครุภัณฑ์";
            Session[PageFunction + "_ERROR_04"] = "โปรดระบุ หมายเลขครุภัณฑ์";
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
            Session["HeaderSubGroup"] = "ข้อมูลครุภัณฑ์";
            Session["HeaderCurrent"] = PageName;
            //================ Column ==================================================   
            Session[PageFunction + "_ColumnSEQ"] = "ลำดับ";
            Session[PageFunction + "_ColumnEdit"] = "";
            Session[PageFunction + "_Column01"] = "หมายเลขครุภัณฑ์";
            Session[PageFunction + "_Column02"] = "หน่วยงาน";
            Session[PageFunction + "_Column03"] = "หมวดหมู่ครุภัณฑ์";
            Session[PageFunction + "_Column04"] = "ครุภัณฑ์";

            Session[PageFunction + "_Column05"] = "ผู้รับผิดชอบ";
            Session[PageFunction + "_Column06"] = "ปีงบประมาณ";
            Session[PageFunction + "_Column07"] = "ประเภทเงินทุน";
            Session[PageFunction + "_Column08"] = "เลขที่เอกสารซื้อ";
            Session[PageFunction + "_Column09"] = "วันที่เอกสารซืั้อ";
            Session[PageFunction + "_Column10"] = "เลขที่เอกสารรับ";
            Session[PageFunction + "_Column11"] = "วันที่เอกสารรับ";
            Session[PageFunction + "_Column12"] = "หน่วยงานที่ตั้ง";
            Session[PageFunction + "_Column13"] = "รับมาจาก";
            Session[PageFunction + "_Column14"] = "โอนให้หน่วยงาน";
            Session[PageFunction + "_Column15"] = "ความเคลื่อนไหว";            

            Session[PageFunction + "_Column16"] = "หมายเลขเครื่อง";
            Session[PageFunction + "_Column17"] = "รุ่น";
            Session[PageFunction + "_Column18"] = "ยี่ห้อ";
            Session[PageFunction + "_Column19"] = "อายุการใช้งาน";
            Session[PageFunction + "_Column20"] = "สภาพ";
            Session[PageFunction + "_Column21"] = "ค่าเสื่อมสะสม";
            Session[PageFunction + "_Column22"] = "ค่าเสื่อมปีนี้";
            Session[PageFunction + "_Column23"] = "ราคาซื้อต่อหน่วย";
            Session[PageFunction + "_Column24"] = "ราคามาตราฐาน";
            Session[PageFunction + "_Column25"] = "ราคาสุทธิ์";

        }
        else
        {
            //================ POPUP ==================================================
            string PageName = "Asset Detail";
            Session["pop_add_asset_type"] = "window management - New " + PageName;
            Session["pop_edit_asset_type"] = "window management - Edit " + PageName;
            Session["pop_delete_asset_type"] = "Are you sure you want to delete this " + PageName + "?";
            Session["pop_confirm_asset_type"] = "Confirm Delete Data";

            Session["save_button_text"] = "Save";
            Session["close_button_text"] = "Close";
            //================ POPUP ERROR ===========================================
            Session[PageFunction + "_ERROR_01"] = "Please specify Work Center";
            Session[PageFunction + "_ERROR_02"] = "Please specify Asset Type";
            Session[PageFunction + "_ERROR_03"] = "Please specify Asset";
            Session[PageFunction + "_ERROR_04"] = "Please specify Asset Number";            
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
            Session[PageFunction + "_Column01"] = "Asset Number";
            Session[PageFunction + "_Column02"] = "Work Center";
            Session[PageFunction + "_Column03"] = "Asset Type";
            Session[PageFunction + "_Column04"] = "Asset";

            Session[PageFunction + "_Column05"] = "Owner";
            Session[PageFunction + "_Column06"] = "Budget Year";
            Session[PageFunction + "_Column07"] = "Fund Type";
            Session[PageFunction + "_Column08"] = "Purchase Doc No";
            Session[PageFunction + "_Column09"] = "Purchase Doc Date";
            Session[PageFunction + "_Column10"] = "Recive Document No";
            Session[PageFunction + "_Column11"] = "Recive Docuemnt Date";
            Session[PageFunction + "_Column12"] = "Department Set";
            Session[PageFunction + "_Column13"] = "Receive from";
            Session[PageFunction + "_Column14"] = "Tranfer Depratment";
            Session[PageFunction + "_Column15"] = "Movement";

            Session[PageFunction + "_Column16"] = "Serial No";
            Session[PageFunction + "_Column17"] = "Serie";
            Session[PageFunction + "_Column18"] = "Brand";
            Session[PageFunction + "_Column19"] = "Term User";
            Session[PageFunction + "_Column20"] = "Quality";
            Session[PageFunction + "_Column21"] = "Depriciate accru";
            Session[PageFunction + "_Column22"] = "Depriciate in year";
            Session[PageFunction + "_Column23"] = "Price per unit";
            Session[PageFunction + "_Column24"] = "Standard Price";
            Session[PageFunction + "_Column25"] = "New Price";
        }
    }
}