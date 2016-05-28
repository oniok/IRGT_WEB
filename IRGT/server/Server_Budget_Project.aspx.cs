using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Server_Budget_Project : System.Web.UI.Page
{
    IRGT_Service.Budget_Project IRGTService = new IRGT_Service.Budget_Project();

    public static string DataTableToJSON(DataTable table)
    {
        List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();

        foreach (DataRow row in table.Rows)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();

            foreach (DataColumn col in table.Columns)
            {
                dict[col.ColumnName] = row[col];
            }
            list.Add(dict);
        }
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        return serializer.Serialize(list);
    }
    private string genPaging(int PageSize, int Count)
    {
        int sum = Count / PageSize;
        DataTable dtTemp = new DataTable("Data");
        dtTemp.Columns.Add("Page");
        dtTemp.Columns.Add("Class");

        if ((sum * PageSize) != Count)
            sum = sum + 1;

        if (sum == 0) sum = 1;

        for (int i = 1; i <= sum; i++)
        {
            DataRow dr = dtTemp.NewRow();
            dr.BeginEdit();
            dr["Page"] = "" + i;
            if (i == 1)
                dr["Class"] = "active";
            else
                dr["Class"] = "";
            dr.EndEdit();
            dtTemp.Rows.Add(dr);
        }

        string DT_JSON = DataTableToJSON(dtTemp);
        DT_JSON = "{\"records\": " + DT_JSON + ",\"pagemax\":\"" + sum + "\",\"rowcount\":\"" + Count + "\"}";

        return DT_JSON;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string Command = Request.Params["Command"];
        switch (Command)
        {
            case "GetMasterData":
                fnGetMasterData();
                break;
            case "BudgetProject":
                fnBudgetProject();
                break;
            case "BudgetProjectByID":
                fnBudgetProjectByID();
                break;
            case "BudgetProjectList":
                fnBudgetProjectList();
                break;
            case "BudgetProjectSummary":
                fnBudgetProjectSummary();
                break;
            case "BudgetProjectSummaryByID":
                fnBudgetProjectSummaryByID();
                break;
        }
    }

    private void fnGetMasterData()
    {
        string FN = Request.Params["Function"];
        string PageName = Request.Params["PageName"].ToString();
        string LANG = "" + Session["language_" + PageName];
        string Param = "" + Request.Params["Param"];
        if (LANG == "") LANG = "TH";
        DataTable DT = IRGTService.getMasterData(FN, LANG, Param);
        string DT_JSON = DataTableToJSON(DT);
        DT_JSON = "{\"records\": " + DT_JSON + "}";
        Response.Write(DT_JSON);
        return;
    }

    private void fnBudgetProject()
    {
        string FN = Request.Params["Function"];
        DataTable DT;
        string DT_JSON;
        int PageSize = 0;

        string BJ_Type_ID = Request.Params["BJ_Type_ID"];
        string User_Code = Request.Params["User_Code"];
        string ReturnMSG_TH = "";
        string ReturnMSG_EN = "";
        string BJ_ID = "";
        int KeyID = 0;
        string BJ_Issue = "";
        string BJ_Goal = "";
        string BJ_Strategy = "";
        string BJ_ProjectName = "";

        string BJ_Reason = "";
        string BJ_Objective = "";
        string BJ_Place = "";
        string BJ_Duration = "";
        string BJ_Amount = "";
        string BJ_Detail = "";
        string BJ_Measure = "";
        string BJ_Benefit = "";
        string BJ_Responsible = "";
        
        switch (FN)
        {
            case "Select":
                string lang = Request.Params["lang"];
                DT = IRGTService.getBudget_Project(User_Code, lang);
                Session["Data_Budget_Project"] = DT.Copy();
                DT_JSON = DataTableToJSON(DT);
                DT_JSON = "{\"records\": " + DT_JSON + "}";
                Response.Write(DT_JSON);
                return;
            case "Check":
                lang = "" + Session["language_budget_project"];
                if (lang == "") lang = "TH";
                BJ_ID = IRGTService.checkBudget_Project(User_Code);
                if (BJ_ID != "")
                    Response.Write("[{\"output\":\"OK\",\"BJ_ID\":\"" + BJ_ID + "\"}]");
                else
                {
                    if (lang == "TH")
                        Response.Write("[{output:\"ERROR\",message:\"" + ReturnMSG_TH + "\"}]");
                    else
                        Response.Write("[{output:\"ERROR\",message:\"" + ReturnMSG_EN + "\"}]");
                }

                return;
            case "Save":
                lang = "" + Session["language_budget_project"];
                if (lang == "") lang = "TH";
                BJ_ID = Request.Params["BJ_ID"];
                BJ_Issue = Request.Params["BJ_Issue"];
                BJ_Goal = Request.Params["BJ_Goal"];
                BJ_Strategy = Request.Params["BJ_Strategy"];
                BJ_ProjectName = Request.Params["BJ_ProjectName"];

                BJ_Reason = Request.Params["BJ_Reason"];
                BJ_Objective = Request.Params["BJ_Objective"];
                BJ_Place = Request.Params["BJ_Place"];
                BJ_Duration = Request.Params["BJ_Duration"];
                BJ_Amount = Request.Params["BJ_Amount"];
                BJ_Detail = Request.Params["BJ_Detail"];
                BJ_Measure = Request.Params["BJ_Measure"];
                BJ_Benefit = Request.Params["BJ_Benefit"];
                BJ_Responsible = Request.Params["BJ_Responsible"];

                KeyID = cCommon.Convert_Str_To_Int(Request.Params["KeyID"]);
                if (IRGTService.setBudget_Project(KeyID, BJ_ID, BJ_Issue, BJ_Goal, BJ_Strategy
                    , BJ_ProjectName, BJ_Reason, BJ_Objective, BJ_Place, BJ_Duration, BJ_Amount
                    , BJ_Detail, BJ_Measure, BJ_Benefit, BJ_Responsible, cCommon.getUserName(Session), out ReturnMSG_TH, out ReturnMSG_EN))
                    Response.Write("[{output:\"OK\",message:\"\"}]");
                else
                {
                    if (lang == "TH")
                        Response.Write("[{output:\"ERROR\",message:\"" + ReturnMSG_TH + "\"}]");
                    else
                        Response.Write("[{output:\"ERROR\",message:\"" + ReturnMSG_EN + "\"}]");
                }
                return;
            case "Load":
                KeyID = cCommon.Convert_Str_To_Int(Request.Params["KeyID"]);
                DT = (DataTable)Session["Data_budget_project"];
                DT_JSON = DataTableToJSON(DT);
                DT_JSON = "{\"records\": " + DT_JSON + "}";
                Response.Write(DT_JSON);
                return;
            case "Delete":
                lang = Request.Params["lang"];
                if (lang == "") lang = "TH";
                KeyID = cCommon.Convert_Str_To_Int(Request.Params["KeyID"]);
                ReturnMSG_TH = "";
                ReturnMSG_EN = "";

                //"{\"records\": " + DT_JSON + "}";
                if (IRGTService.deleteBudget_Project(KeyID, out ReturnMSG_TH, out ReturnMSG_EN))
                    Response.Write("{\"output\":\"OK\"}");
                else
                {
                    if (lang == "TH")
                        Response.Write("{\"output\":\"ERROR\",\"message\":\"" + ReturnMSG_TH + "\"}");
                    else
                        Response.Write("{\"output\":\"ERROR\",\"message\":\"" + ReturnMSG_EN + "\"}");
                }

                return;
            case "Send":
                lang = Request.Params["lang"];
                if (lang == "") lang = "TH";
                BJ_ID = Request.Params["BJ_ID"];
                ReturnMSG_TH = "";
                ReturnMSG_EN = "";

                //"{\"records\": " + DT_JSON + "}";
                if (IRGTService.sendBudget_Project(BJ_ID, User_Code, out ReturnMSG_TH, out ReturnMSG_EN))
                    Response.Write("{\"output\":\"OK\"}");
                else
                {
                    if (lang == "TH")
                        Response.Write("{\"output\":\"ERROR\",\"message\":\"" + ReturnMSG_TH + "\"}");
                    else
                        Response.Write("{\"output\":\"ERROR\",\"message\":\"" + ReturnMSG_EN + "\"}");
                }

                return;
        }
    }

    private void fnBudgetProjectByID()
    {
        string FN = Request.Params["Function"];
        DataTable DT;
        string DT_JSON;
        int PageSize = 0;

        string BJ_ID = Request.Params["BJ_ID"];
        string User_Code = Request.Params["User_Code"];
        string ReturnMSG_TH = "";
        string ReturnMSG_EN = "";
        int KeyID = 0;

        switch (FN)
        {
            case "Select":
                string lang = Request.Params["lang"];
                DT = IRGTService.getBudget_ProjectByID(BJ_ID, lang);
                Session["Data_Budget_Project"] = DT.Copy();
                DT_JSON = DataTableToJSON(DT);
                DT_JSON = "{\"records\": " + DT_JSON + "}";
                Response.Write(DT_JSON);
                return;
            case "Load":
                KeyID = cCommon.Convert_Str_To_Int(Request.Params["KeyID"]);
                DT = (DataTable)Session["Data_budget_project"];
                DT_JSON = DataTableToJSON(DT);
                DT_JSON = "{\"records\": " + DT_JSON + "}";
                Response.Write(DT_JSON);
                return;
            case "Confirm":
                lang = Request.Params["lang"];
                if (lang == "") lang = "TH";

                //"{\"records\": " + DT_JSON + "}";
                if (IRGTService.confirmBudget_Project(BJ_ID, User_Code, out ReturnMSG_TH, out ReturnMSG_EN))
                    Response.Write("{\"output\":\"OK\"}");
                else
                {
                    if (lang == "TH")
                        Response.Write("{\"output\":\"ERROR\",\"message\":\"" + ReturnMSG_TH + "\"}");
                    else
                        Response.Write("{\"output\":\"ERROR\",\"message\":\"" + ReturnMSG_EN + "\"}");
                }

                return;
            case "Approve":
                lang = Request.Params["lang"];
                if (lang == "") lang = "TH";

                //"{\"records\": " + DT_JSON + "}";
                if (IRGTService.approveBudget_Project(BJ_ID, User_Code, out ReturnMSG_TH, out ReturnMSG_EN))
                    Response.Write("{\"output\":\"OK\"}");
                else
                {
                    if (lang == "TH")
                        Response.Write("{\"output\":\"ERROR\",\"message\":\"" + ReturnMSG_TH + "\"}");
                    else
                        Response.Write("{\"output\":\"ERROR\",\"message\":\"" + ReturnMSG_EN + "\"}");
                }

                return;
        }
    }

    private void fnBudgetProjectSummary()
    {
        DataTable DT;
        string DT_JSON;
        string User_Code = Request.Params["User_Code"];
        string lang = Request.Params["lang"];
        DT = IRGTService.getBudget_ProjectSummary(User_Code, lang);
        Session["Data_budget_project_summary"] = DT.Copy();
        DT_JSON = DataTableToJSON(DT);
        DT_JSON = "{\"records\": " + DT_JSON + "}";
        Response.Write(DT_JSON);
        return;

    }

    private void fnBudgetProjectSummaryByID()
    {
        DataTable DT;
        string DT_JSON;
        string BJ_ID = Request.Params["BJ_ID"];
        string lang = Request.Params["lang"];
        DT = IRGTService.getBudget_ProjectSummaryByID(BJ_ID, lang);
        Session["Data_budget_project_summary"] = DT.Copy();
        DT_JSON = DataTableToJSON(DT);
        DT_JSON = "{\"records\": " + DT_JSON + "}";
        Response.Write(DT_JSON);
        return;

    }

    private void fnBudgetProjectList()
    {
        string FN = Request.Params["Function"];
        DataTable DT;
        string DT_JSON;
        int PageSize = 0;

        string Loc_ID = Request.Params["Loc_ID"];
        string User_Code = Request.Params["User_Code"];
        string ReturnMSG_TH = "";
        string ReturnMSG_EN = "";
        string BJ_ID = Request.Params["BJ_ID"];
        string lang = Request.Params["lang"];
        int KeyID = 0;

        switch (FN)
        {
            case "Paging":
                PageSize = cCommon.Convert_Str_To_Int(Request.Params["PageSize"]);

                int Count = IRGTService.getBudget_Project_List_Count(User_Code,Loc_ID);
                DT_JSON = genPaging(PageSize, Count);

                Response.Write(DT_JSON);
                return;
            case "Select":
                PageSize = cCommon.Convert_Str_To_Int(Request.Params["PageSize"]);

                int PageIndex = int.Parse(Request.Params["PageIndex"]);

                DT = IRGTService.getBudget_Project_List(PageSize, PageIndex, User_Code, Loc_ID, lang);
                Session["Data_Budget_Project"] = DT.Copy();
                DT_JSON = DataTableToJSON(DT);
                DT_JSON = "{\"records\": " + DT_JSON + "}";
                Response.Write(DT_JSON);
                return;
            case "Load":
                KeyID = cCommon.Convert_Str_To_Int(Request.Params["KeyID"]);
                DT = (DataTable)Session["Data_budget_project"];
                DT_JSON = DataTableToJSON(DT);
                DT_JSON = "{\"records\": " + DT_JSON + "}";
                Response.Write(DT_JSON);
                return;
            case "Detail":
                DT = IRGTService.getBudget_ProjectDetail(BJ_ID, lang);
                Session["Data_Budget_Project_Detail"] = DT.Copy();
                DT_JSON = DataTableToJSON(DT);
                DT_JSON = "{\"records\": " + DT_JSON + "}";
                Response.Write(DT_JSON);
                return;
            case "Send":
                lang = Request.Params["lang"];
                if (lang == "") lang = "TH";
                BJ_ID = Request.Params["BJ_ID"];
                ReturnMSG_TH = "";
                ReturnMSG_EN = "";

                //"{\"records\": " + DT_JSON + "}";
                if (IRGTService.sendBudget_Project(BJ_ID, User_Code, out ReturnMSG_TH, out ReturnMSG_EN))
                    Response.Write("{\"output\":\"OK\"}");
                else
                {
                    if (lang == "TH")
                        Response.Write("{\"output\":\"ERROR\",\"message\":\"" + ReturnMSG_TH + "\"}");
                    else
                        Response.Write("{\"output\":\"ERROR\",\"message\":\"" + ReturnMSG_EN + "\"}");
                }

                return;
        }
    }
}