using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using CrystalDecisions.CrystalReports.Engine;
using System.Data.SqlClient;
using System.Configuration;

public partial class rpt_BudgetAnnualPosition : System.Web.UI.Page
{  
    protected void Page_Load(object sender, EventArgs e)
    {
        string PageFunction = "report_budget_annual";
        string LANG = cCommon.getLanguage(Request);
        Session["language_" + PageFunction] = LANG;

        if (LANG == cCommon.Language_Thai)
        {
            string PageName = "รายงานประจำปีงบบุคลากร";
            Session["HeaderText"] = PageName;
            Session["HeaderGroup"] = "งบประมาณเงินทุนหมุนเวียน";
            Session["HeaderSubGroup"] = "รายงานประมาณการ";
            Session["HeaderCurrent"] = PageName;
        }
        else
        {

            string PageName = "รายงานประจำปีงบบุคลากร";
            Session["HeaderText"] = PageName;
            Session["HeaderGroup"] = "งบประมาณเงินทุนหมุนเวียน";
            Session["HeaderSubGroup"] = "รายงานประมาณการ";
            Session["HeaderCurrent"] = PageName;
        }

        if (!Page.IsPostBack)
        {          
            fnLoadLoc();
            fnLoadBudgetType();
            fnLoadBudgetYear();
            //fnSearch();
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
    void fnLoadBudgetType()
    {
        IRGT_Service.Budget_Operation IRGTService = new IRGT_Service.Budget_Operation();
        DataTable dtTemp = IRGTService.getMasterData("BudgetPositionType", "TH", "");
        ddlType.DataSource = dtTemp;
        ddlType.DataTextField = "Name";
        ddlType.DataValueField = "Code";
        ddlType.DataBind();
    }
    void fnLoadBudgetYear()
    {
        IRGT_Service.Budget_Operation IRGTService = new IRGT_Service.Budget_Operation();
        DataTable dtTemp = IRGTService.getMasterData("BudgetYear", "TH", "");
        ddlYear.DataSource = dtTemp;
        ddlYear.DataTextField = "Name";
        ddlYear.DataValueField = "Code";
        ddlYear.DataBind();
    }

    public void fnSearch()
    {

        try
        {
            //validate select budget type
            String sel_budgetType = ddlType.SelectedValue;

            if (sel_budgetType == string.Empty || sel_budgetType == null)
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "error_req_budgetType", "fnErrorMessage('ข้อผิดพลาด / Error', 'กรุณาเลือกประเภทงบประมาณการ')", true);
                return;
            }

            //check budget type
            String sql_conn = ConfigurationManager.AppSettings["DBConn_Budget"];

            String sel_command = "EXEC [dbo].[sp_getBudget_Annual_Position] @Loc_ID = N'" + ddlLoc.SelectedValue
                        + "',@Budget_Type = N'" + ddlType.SelectedValue
                        + "',@Budget_Year = N'" + ddlYear.SelectedValue
                        + "',@Language = N'" + Session["language_report_budget_annual"] + "'";

            SqlDataAdapter sda = new SqlDataAdapter(sel_command, sql_conn);
            ReportDocument rpt = new ReportDocument();
            DataSet ds = new DataSet();
            DataTable dt = null;

            sda.Fill(ds, "BP_DataTable");
            dt = ds.Tables[0];
            rpt.Load(Server.MapPath("../report/rpt_BudgetAnnualPosition.rpt"));

            rpt.SetDataSource(dt);
            crv_BudgetAnnual.ReportSource = rpt;
            crv_BudgetAnnual.RefreshReport();

        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fnSearch();
    }
}