using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using CrystalDecisions.CrystalReports.Engine;

public partial class rpt_budget_annual : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string PageFunction = "report_asset";
        string LANG = cCommon.getLanguage(Request);
        Session["language_" + PageFunction] = LANG;

        if (LANG == cCommon.Language_Thai)
        {
            string PageName = "รายงานประจำปีบัญชีพัสดุประเภทครุภัณฑ์";
            Session["HeaderText"] = PageName;
            Session["HeaderGroup"] = "ผู้ดูแลระบบ";
            Session["HeaderSubGroup"] = "รายงาน";
            Session["HeaderCurrent"] = PageName;
        }
        else
        {

            string PageName = "รายงานประจำปีบัญชีพัสดุประเภทครุภัณฑ์";
            Session["HeaderText"] = PageName;
            Session["HeaderGroup"] = "ผู้ดูแลระบบ";
            Session["HeaderSubGroup"] = "รายงาน";
            Session["HeaderCurrent"] = PageName;
        }

        if (!Page.IsPostBack)
        {          
            fnLoadLoc();
            fnSearch();

        }
    }

    
    void fnLoadLoc()
    {
        IRGT_Service.Service IRGTService = new IRGT_Service.Service();
        DataTable dtTemp = IRGTService.getMasterData("WorkCenter", "TH", "");
        ddlLoc.DataSource = dtTemp;
        ddlLoc.DataTextField = "Name";
        ddlLoc.DataValueField = "Code";
        ddlLoc.DataBind();
    }
    public void fnSearch()
    {
        //rpvMain.LocalReport.ReportPath = Server.MapPath("../report/RPT01.rdl");
        //rpvMain.LocalReport.DataSources.Clear();

        //IRGT_Service.Service IRGTService = new IRGT_Service.Service();
        //DataTable dtTemp = IRGTService.report_01(ddlLoc.SelectedValue);

        //ReportParameter[] p = new ReportParameter[1];
        //p[0] = new ReportParameter("LOC_NAME", ddlLoc.SelectedItem.Text);

        //rpvMain.LocalReport.SetParameters(p);
        //rpvMain.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dtTemp));
        //rpvMain.DataBind();
        

        try
        {
            ReportDocument rpt = new ReportDocument();
            rpt.Load(Server.MapPath("../report/rpt_budget_annual.rpt"));
            CrystalReportViewer1.ReportSource = rpt;
            //CrystalReportViewer1.Refresh();
        }
        catch (Exception ex)
        {

            throw;
        }

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fnSearch();

     //   ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "fnReStart()", true);
    }
}