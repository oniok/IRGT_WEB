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

        string BA_Type_ID = Request.Params["BA_Type_ID"];
        string User_Code = Request.Params["User_Code"];
        string ReturnMSG_TH = "";
        string ReturnMSG_EN = "";
        string BR_ID = "";
        int KeyID = 0;
        string BR_Name = "";
        string BA_Qty = "";
        string BA_Price = "";
        string BA_Reason = "";

        switch (FN)
        {
            case "Paging":
                PageSize = cCommon.Convert_Str_To_Int(Request.Params["PageSize"]);

                int Count = IRGTService.getBudget_Project_Count(User_Code, BA_Type_ID);
                DT_JSON = genPaging(PageSize, Count);

                Response.Write(DT_JSON);
                return;
            case "Select":
                string lang = Request.Params["lang"];
                DT = IRGTService.getBudget_Project(User_Code, lang);
                Session["Data_Budget_Project"] = DT.Copy();
                DT_JSON = DataTableToJSON(DT);
                DT_JSON = "{\"records\": " + DT_JSON + "}";
                Response.Write(DT_JSON);
                return;
            case "Check":
                lang = "" + Session["language_budget_asset"];
                if (lang == "") lang = "TH";
                BR_ID = IRGTService.checkBudget_Project(User_Code);
                if (BR_ID != "")
                    Response.Write("[{\"output\":\"OK\",\"BR_ID\":\"" + BR_ID + "\"}]");
                else
                {
                    if (lang == "TH")
                        Response.Write("[{output:\"ERROR\",message:\"" + ReturnMSG_TH + "\"}]");
                    else
                        Response.Write("[{output:\"ERROR\",message:\"" + ReturnMSG_EN + "\"}]");
                }

                return;
            case "Save":
                lang = "" + Session["language_budget_asset"];
                if (lang == "") lang = "TH";
                BR_ID = Request.Params["BR_ID"];
                BR_Name = Request.Params["BR_Name"];
                BA_Type_ID = Request.Params["BA_Type_ID"];
                BA_Qty = Request.Params["BA_Qty"];
                BA_Price = Request.Params["BA_Price"];
                BA_Reason = Request.Params["BA_Reason"];

                KeyID = cCommon.Convert_Str_To_Int(Request.Params["KeyID"]);
                if (IRGTService.setBudget_Project(KeyID, BR_ID, BR_Name, BA_Type_ID, BA_Qty
                    , BA_Price, BA_Reason, cCommon.getUserName(Session), out ReturnMSG_TH, out ReturnMSG_EN))
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
                DataRow[] dr_list = DT.Select("KeyID = " + KeyID);
                string OP = "[{";
                OP += "BJ_ID:\"" + dr_list[0]["BJ_ID"] + "\"";
                OP += ",BJ_Issue:\"" + dr_list[0]["BJ_Issue"] + "\"";
                OP += ",BJ_Goal:\"" + dr_list[0]["BJ_Goal"] + "\"";
                OP += ",BJ_Strategy:\"" + dr_list[0]["BJ_Strategy"] + "\"";
                OP += ",BJ_ProjectName:\"" + dr_list[0]["BJ_ProjectName"] + "\"";
                OP += ",BJ_Reason:\"" + dr_list[0]["BJ_Reason"] + "\"";
                OP += ",BJ_Objective:\"" + dr_list[0]["BJ_Objective"] + "\"";
                OP += ",BJ_Place:\"" + dr_list[0]["BJ_Place"] + "\"";
                OP += ",BJ_Duration:\"" + dr_list[0]["BJ_Duration"] + "\"";
                OP += ",BJ_Amount:\"" + dr_list[0]["BJ_Amount"] + "\"";
                OP += ",BJ_Detail:\"" + dr_list[0]["BJ_Detail"] + "\"";
                OP += ",BJ_Measure:\"" + dr_list[0]["BJ_Measure"] + "\"";
                OP += ",BJ_Benefit:\"" + dr_list[0]["BJ_Benefit"] + "\"";
                OP += ",BJ_Responsible:\"" + dr_list[0]["BJ_Responsible"] + "\"";
                OP += "}]";
                Response.Write(OP);
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
                BR_ID = Request.Params["BR_ID"];
                ReturnMSG_TH = "";
                ReturnMSG_EN = "";

                //"{\"records\": " + DT_JSON + "}";
                if (IRGTService.sendBudget_Project(BR_ID,User_Code, out ReturnMSG_TH, out ReturnMSG_EN))
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

        string BA_Type_ID = Request.Params["BA_Type_ID"];
        string BR_ID = Request.Params["BR_ID"];
        string User_Code = Request.Params["User_Code"];
        string BA_Qty_Adj = Request.Params["BA_Qty_Adj"];
        string BA_Price_Adj = Request.Params["BA_Price_Adj"];
        string ReturnMSG_TH = "";
        string ReturnMSG_EN = "";
        int KeyID = 0;

        switch (FN)
        {
            case "Paging":
                PageSize = cCommon.Convert_Str_To_Int(Request.Params["PageSize"]);

                int Count = IRGTService.getBudget_ProjectByID_Count(BR_ID, BA_Type_ID);
                DT_JSON = genPaging(PageSize, Count);

                Response.Write(DT_JSON);
                return;
            case "Select":
                PageSize = cCommon.Convert_Str_To_Int(Request.Params["PageSize"]);

                int PageIndex = int.Parse(Request.Params["PageIndex"]);
                string lang = Request.Params["lang"];
                DT = IRGTService.getBudget_ProjectByID(PageSize, PageIndex, BR_ID, BA_Type_ID, lang);
                Session["Data_Budget_Project"] = DT.Copy();
                DT_JSON = DataTableToJSON(DT);
                DT_JSON = "{\"records\": " + DT_JSON + "}";
                Response.Write(DT_JSON);
                return;
            case "Load":
                KeyID = cCommon.Convert_Str_To_Int(Request.Params["KeyID"]);
                DT = (DataTable)Session["Data_budget_project"];
                DataRow[] dr_list = DT.Select("KeyID = " + KeyID);
                string OP = "[{";
                OP += "BR_ID:\"" + dr_list[0]["BR_ID"] + "\"";
                OP += ",BR_Name:\"" + dr_list[0]["BR_Name"] + "\"";
                OP += ",BA_Type_ID:\"" + dr_list[0]["BA_Type_ID"] + "\"";
                OP += ",BA_Qty:\"" + dr_list[0]["BA_Qty"] + "\"";
                OP += ",BA_Price:\"" + dr_list[0]["BA_Price"] + "\"";
                OP += ",BA_Qty_Adj:\"" + dr_list[0]["BA_Qty_Adj"] + "\"";
                OP += ",BA_Price_Adj:\"" + dr_list[0]["BA_Price_Adj"] + "\"";
                OP += ",BA_Reason:\"" + dr_list[0]["BA_Reason"] + "\"";
                OP += "}]";
                Response.Write(OP);
                return;
            case "Confirm":
                lang = Request.Params["lang"];
                if (lang == "") lang = "TH";

                //"{\"records\": " + DT_JSON + "}";
                if (IRGTService.confirmBudget_Project(BR_ID, User_Code, out ReturnMSG_TH, out ReturnMSG_EN))
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
                if (IRGTService.approveBudget_Project(BR_ID, User_Code, out ReturnMSG_TH, out ReturnMSG_EN))
                    Response.Write("{\"output\":\"OK\"}");
                else
                {
                    if (lang == "TH")
                        Response.Write("{\"output\":\"ERROR\",\"message\":\"" + ReturnMSG_TH + "\"}");
                    else
                        Response.Write("{\"output\":\"ERROR\",\"message\":\"" + ReturnMSG_EN + "\"}");
                }

                return;
            case "Adjust":
                lang = "" + Session["language_budget_asset"];
                if (lang == "") lang = "TH";
                BA_Qty_Adj = Request.Params["BA_Qty_Adj"];
                BA_Price_Adj = Request.Params["BA_Price_Adj"];

                KeyID = cCommon.Convert_Str_To_Int(Request.Params["KeyID"]);
                if (IRGTService.adjustBudget_Project(KeyID, BA_Qty_Adj
                    , BA_Price_Adj, User_Code, out ReturnMSG_TH, out ReturnMSG_EN))
                    Response.Write("[{output:\"OK\",message:\"\"}]");
                else
                {
                    if (lang == "TH")
                        Response.Write("[{output:\"ERROR\",message:\"" + ReturnMSG_TH + "\"}]");
                    else
                        Response.Write("[{output:\"ERROR\",message:\"" + ReturnMSG_EN + "\"}]");
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
        string BR_ID = Request.Params["BR_ID"];
        string lang = Request.Params["lang"];
        DT = IRGTService.getBudget_ProjectSummaryByID(BR_ID, lang);
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
        string BR_ID = Request.Params["BR_ID"];
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
            case "Detail":
                DT = IRGTService.getBudget_ProjectDetail(BR_ID, lang);
                Session["Data_Budget_Project_Detail"] = DT.Copy();
                DT_JSON = DataTableToJSON(DT);
                DT_JSON = "{\"records\": " + DT_JSON + "}";
                Response.Write(DT_JSON);
                return;
            case "Load":
                KeyID = cCommon.Convert_Str_To_Int(Request.Params["KeyID"]);
                DT = (DataTable)Session["Data_budget_project"];
                DataRow[] dr_list = DT.Select("KeyID = " + KeyID);
                string OP = "[{";
                OP += "BR_ID:\"" + dr_list[0]["BR_ID"] + "\"";
                OP += ",BR_Name:\"" + dr_list[0]["BR_Name"] + "\"";
                OP += ",BA_Type_ID:\"" + dr_list[0]["BA_Type_ID"] + "\"";
                OP += ",BA_Qty:\"" + dr_list[0]["BA_Qty"] + "\"";
                OP += ",BA_Price:\"" + dr_list[0]["BA_Price"] + "\"";
                OP += ",BA_Reason:\"" + dr_list[0]["BA_Reason"] + "\"";
                OP += "}]";
                Response.Write(OP);
                return;
            case "Send":
                lang = Request.Params["lang"];
                if (lang == "") lang = "TH";
                BR_ID = Request.Params["BR_ID"];
                ReturnMSG_TH = "";
                ReturnMSG_EN = "";

                //"{\"records\": " + DT_JSON + "}";
                if (IRGTService.sendBudget_Project(BR_ID, User_Code, out ReturnMSG_TH, out ReturnMSG_EN))
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