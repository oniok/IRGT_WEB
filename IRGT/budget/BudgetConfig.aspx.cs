using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using System.Data.SqlClient;
using System.Configuration;

public partial class BudgetConfig : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string PageFunction = "budget_config";
        string LANG = cCommon.getLanguage(Request);
        Session["language_" + PageFunction] = LANG;

        if (LANG == cCommon.Language_Thai)
        {
            string PageName = "หน่วยงานจัดทำคำขอ";
            Session["HeaderText"] = PageName;
            Session["HeaderGroup"] = "งบประมาณเงินทุนหมุนเวียน";
            Session["HeaderSubGroup"] = "กำหนดค่าเริ่มต้น";
            Session["HeaderCurrent"] = PageName;
        }
        else
        {
            string PageName = "หน่วยงานจัดทำคำขอ";
            Session["HeaderText"] = PageName;
            Session["HeaderGroup"] = "งบประมาณเงินทุนหมุนเวียน";
            Session["HeaderSubGroup"] = "กำหนดค่าเริ่มต้น";
            Session["HeaderCurrent"] = PageName;
        }

        if (!Page.IsPostBack)
        {
            fnLoadLoc();
        }
    }


    void fnLoadLoc()
    {
        IRGT_Service.Budget_Operation IRGTService = new IRGT_Service.Budget_Operation();
        DataTable dtTemp = IRGTService.getMasterData("WorkCenter", "TH", "");
        ddlLoc.DataSource = dtTemp;
        ddlLoc.DataTextField = "Name";
        ddlLoc.DataValueField = "Code";
        ddlLoc.DataBind();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        Session["LocationID"] = ddlLoc.SelectedValue;
  
    }
}