using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class server_Budget_Position : System.Web.UI.Page
{
    IRGT_Service.Budget_Position IRGTService = new IRGT_Service.Budget_Position();

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
            case "BudgetPosition":
                fnBudgetPosition();
                break;
            case "BudgetPositionByID":
                fnBudgetPositionByID();
                break;
            case "BudgetPositionList":
                fnBudgetPositionList();
                break;
            case "BudgetPositionSummary":
                fnBudgetPositionSummary();
                break;
            case "BudgetPositionSummaryByID":
                fnBudgetPositionSummaryByID();
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

    private void fnBudgetPosition()
    {
        string FN = Request.Params["Function"];
        DataTable DT;
        string DT_JSON;
        int PageSize = 0;

        string Position_Type_ID = Request.Params["Position_Type_ID"];
        string Educate_Type_ID = Request.Params["Educate_Type_ID"];
        string BP_Type_ID = Request.Params["BP_Type_ID"];
        string User_Code = Request.Params["User_Code"];
        string ReturnMSG_TH = "";
        string ReturnMSG_EN = "";
        string BP_ID = "";
        int KeyID = 0;
        string BP_Qty = "";
        string BP_Price = "";
        string BP_Reason = "";

        switch (FN)
        {
            case "Paging":
                PageSize = cCommon.Convert_Str_To_Int(Request.Params["PageSize"]);

                int Count = IRGTService.getBudget_Position_Count(User_Code, Position_Type_ID, Educate_Type_ID, BP_Type_ID);
                DT_JSON = genPaging(PageSize, Count);

                Response.Write(DT_JSON);
                return;
            case "Select":
                PageSize = cCommon.Convert_Str_To_Int(Request.Params["PageSize"]);

                int PageIndex = int.Parse(Request.Params["PageIndex"]);
                string lang = Request.Params["lang"];
                DT = IRGTService.getBudget_Position(PageSize, PageIndex, User_Code, Position_Type_ID, Educate_Type_ID, BP_Type_ID, lang);
                Session["Data_Budget_Position"] = DT.Copy();
                DT_JSON = DataTableToJSON(DT);
                DT_JSON = "{\"records\": " + DT_JSON + "}";
                Response.Write(DT_JSON);
                return;
            case "Check":
                lang = "" + Session["language_Budget_Position"];
                if (lang == "") lang = "TH";
                BP_ID = IRGTService.checkBudget_Position(User_Code);
                if (BP_ID != "")
                    Response.Write("[{\"output\":\"OK\",\"BP_ID\":\"" + BP_ID + "\"}]");
                else
                {
                    if (lang == "TH")
                        Response.Write("[{output:\"ERROR\",message:\"" + ReturnMSG_TH + "\"}]");
                    else
                        Response.Write("[{output:\"ERROR\",message:\"" + ReturnMSG_EN + "\"}]");
                }

                return;
            case "Save":
                lang = "" + Session["language_Budget_Position"];
                if (lang == "") lang = "TH";
                BP_ID = Request.Params["BP_ID"];
                Position_Type_ID = Request.Params["Position_Type_ID"];
                Educate_Type_ID = Request.Params["Educate_Type_ID"];
                BP_Type_ID = Request.Params["BP_Type_ID"];
                BP_Qty = Request.Params["BP_Qty"];
                BP_Price = Request.Params["BP_Price"];
                BP_Reason = Request.Params["BP_Reason"];

                KeyID = cCommon.Convert_Str_To_Int(Request.Params["KeyID"]);
                if (IRGTService.setBudget_Position(KeyID, BP_ID, Position_Type_ID, Educate_Type_ID, BP_Type_ID, BP_Qty
                    , BP_Price, BP_Reason, cCommon.getUserName(Session), out ReturnMSG_TH, out ReturnMSG_EN))
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
                DT = (DataTable)Session["Data_Budget_Position"];
                DataRow[] dr_list = DT.Select("KeyID = " + KeyID);
                string OP = "[{";
                OP += "BP_ID:\"" + dr_list[0]["BP_ID"] + "\"";
                OP += ",Position_Type_ID:\"" + dr_list[0]["Position_Type_ID"] + "\"";
                OP += ",Educate_Type_ID:\"" + dr_list[0]["Educate_Type_ID"] + "\"";
                OP += ",BP_Type_ID:\"" + dr_list[0]["BP_Type_ID"] + "\"";
                OP += ",BP_Qty:\"" + dr_list[0]["BP_Qty"] + "\"";
                OP += ",BP_Price:\"" + dr_list[0]["BP_Price"] + "\"";
                OP += ",BP_Reason:\"" + dr_list[0]["BP_Reason"] + "\"";
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
                if (IRGTService.deleteBudget_Position(KeyID, out ReturnMSG_TH, out ReturnMSG_EN))
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
                BP_ID = Request.Params["BP_ID"];
                ReturnMSG_TH = "";
                ReturnMSG_EN = "";

                //"{\"records\": " + DT_JSON + "}";
                if (IRGTService.sendBudget_Position(BP_ID,User_Code, out ReturnMSG_TH, out ReturnMSG_EN))
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

    private void fnBudgetPositionByID()
    {
        string FN = Request.Params["Function"];
        DataTable DT;
        string DT_JSON;
        int PageSize = 0;

        string Position_Type_ID = Request.Params["Position_Type_ID"];
        string Educate_Type_ID = Request.Params["Educate_Type_ID"];
        string BP_Type_ID = Request.Params["BP_Type_ID"];
        string BP_ID = Request.Params["BP_ID"];
        string User_Code = Request.Params["User_Code"];
        string BP_Qty_Adj = Request.Params["BP_Qty_Adj"];
        string BP_Price_Adj = Request.Params["BP_Price_Adj"];
        string ReturnMSG_TH = "";
        string ReturnMSG_EN = "";
        int KeyID = 0;

        switch (FN)
        {
            case "Paging":
                PageSize = cCommon.Convert_Str_To_Int(Request.Params["PageSize"]);

                int Count = IRGTService.getBudget_PositionByID_Count(BP_ID, Position_Type_ID, Educate_Type_ID, BP_Type_ID);
                DT_JSON = genPaging(PageSize, Count);

                Response.Write(DT_JSON);
                return;
            case "Select":
                PageSize = cCommon.Convert_Str_To_Int(Request.Params["PageSize"]);

                int PageIndex = int.Parse(Request.Params["PageIndex"]);
                string lang = Request.Params["lang"];
                DT = IRGTService.getBudget_PositionByID(PageSize, PageIndex, BP_ID, Position_Type_ID, Educate_Type_ID, BP_Type_ID, lang);
                Session["Data_Budget_Position"] = DT.Copy();
                DT_JSON = DataTableToJSON(DT);
                DT_JSON = "{\"records\": " + DT_JSON + "}";
                Response.Write(DT_JSON);
                return;
            case "Load":
                KeyID = cCommon.Convert_Str_To_Int(Request.Params["KeyID"]);
                DT = (DataTable)Session["Data_Budget_Position"];
                DataRow[] dr_list = DT.Select("KeyID = " + KeyID);
                string OP = "[{";
                OP += "BP_ID:\"" + dr_list[0]["BP_ID"] + "\"";
                OP += ",Position_Type_ID:\"" + dr_list[0]["Position_Type_ID"] + "\"";
                OP += ",Educate_Type_ID:\"" + dr_list[0]["Educate_Type_ID"] + "\"";
                OP += ",BP_Type_ID:\"" + dr_list[0]["BP_Type_ID"] + "\"";
                OP += ",BP_Qty:\"" + dr_list[0]["BP_Qty"] + "\"";
                OP += ",BP_Price:\"" + dr_list[0]["BP_Price"] + "\"";
                OP += ",BP_Qty_Adj:\"" + dr_list[0]["BP_Qty_Adj"] + "\"";
                OP += ",BP_Price_Adj:\"" + dr_list[0]["BP_Price_Adj"] + "\"";
                OP += ",BP_Reason:\"" + dr_list[0]["BP_Reason"] + "\"";
                OP += "}]";
                Response.Write(OP);
                return;
            case "Confirm":
                lang = Request.Params["lang"];
                if (lang == "") lang = "TH";

                //"{\"records\": " + DT_JSON + "}";
                if (IRGTService.confirmBudget_Position(BP_ID, User_Code, out ReturnMSG_TH, out ReturnMSG_EN))
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
                if (IRGTService.approveBudget_Position(BP_ID, User_Code, out ReturnMSG_TH, out ReturnMSG_EN))
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
                lang = "" + Session["language_Budget_Position"];
                if (lang == "") lang = "TH";
                BP_Qty_Adj = Request.Params["BP_Qty_Adj"];
                BP_Price_Adj = Request.Params["BP_Price_Adj"];

                KeyID = cCommon.Convert_Str_To_Int(Request.Params["KeyID"]);
                if (IRGTService.adjustBudget_Position(KeyID, BP_Qty_Adj
                    , BP_Price_Adj, User_Code, out ReturnMSG_TH, out ReturnMSG_EN))
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

    private void fnBudgetPositionSummary()
    {
        DataTable DT;
        string DT_JSON;
        string User_Code = Request.Params["User_Code"];
        string lang = Request.Params["lang"];
        DT = IRGTService.getBudget_PositionSummary(User_Code, lang);
        Session["Data_Budget_Position_summary"] = DT.Copy();
        DT_JSON = DataTableToJSON(DT);
        DT_JSON = "{\"records\": " + DT_JSON + "}";
        Response.Write(DT_JSON);
        return;

    }

    private void fnBudgetPositionSummaryByID()
    {
        DataTable DT;
        string DT_JSON;
        string BP_ID = Request.Params["BP_ID"];
        string lang = Request.Params["lang"];
        DT = IRGTService.getBudget_PositionSummaryByID(BP_ID, lang);
        Session["Data_Budget_Position_summary"] = DT.Copy();
        DT_JSON = DataTableToJSON(DT);
        DT_JSON = "{\"records\": " + DT_JSON + "}";
        Response.Write(DT_JSON);
        return;

    }

    private void fnBudgetPositionList()
    {
        string FN = Request.Params["Function"];
        DataTable DT;
        string DT_JSON;
        int PageSize = 0;

        string Loc_ID = Request.Params["Loc_ID"];
        string User_Code = Request.Params["User_Code"];
        string ReturnMSG_TH = "";
        string ReturnMSG_EN = "";
        string BP_ID = Request.Params["BP_ID"];
        string lang = Request.Params["lang"];
        int KeyID = 0;

        switch (FN)
        {
            case "Paging":
                PageSize = cCommon.Convert_Str_To_Int(Request.Params["PageSize"]);

                int Count = IRGTService.getBudget_Position_List_Count(User_Code,Loc_ID);
                DT_JSON = genPaging(PageSize, Count);

                Response.Write(DT_JSON);
                return;
            case "Select":
                PageSize = cCommon.Convert_Str_To_Int(Request.Params["PageSize"]);

                int PageIndex = int.Parse(Request.Params["PageIndex"]);
                
                DT = IRGTService.getBudget_Position_List(PageSize, PageIndex, User_Code, Loc_ID, lang);
                Session["Data_Budget_Position"] = DT.Copy();
                DT_JSON = DataTableToJSON(DT);
                DT_JSON = "{\"records\": " + DT_JSON + "}";
                Response.Write(DT_JSON);
                return;
            case "Detail":
                DT = IRGTService.getBudget_PositionDetail(BP_ID, lang);
                Session["Data_Budget_Position_Detail"] = DT.Copy();
                DT_JSON = DataTableToJSON(DT);
                DT_JSON = "{\"records\": " + DT_JSON + "}";
                Response.Write(DT_JSON);
                return;
            case "Load":
                KeyID = cCommon.Convert_Str_To_Int(Request.Params["KeyID"]);
                DT = (DataTable)Session["Data_Budget_Position"];
                DataRow[] dr_list = DT.Select("KeyID = " + KeyID);
                string OP = "[{";
                OP += "BP_ID:\"" + dr_list[0]["BP_ID"] + "\"";
                OP += ",BO_Name:\"" + dr_list[0]["BO_Name"] + "\"";
                OP += ",BP_Type_ID:\"" + dr_list[0]["BP_Type_ID"] + "\"";
                OP += ",BP_Qty:\"" + dr_list[0]["BP_Qty"] + "\"";
                OP += ",BP_Price:\"" + dr_list[0]["BP_Price"] + "\"";
                OP += ",BP_Reason:\"" + dr_list[0]["BP_Reason"] + "\"";
                OP += "}]";
                Response.Write(OP);
                return;
            case "Send":
                lang = Request.Params["lang"];
                if (lang == "") lang = "TH";
                BP_ID = Request.Params["BP_ID"];
                ReturnMSG_TH = "";
                ReturnMSG_EN = "";

                //"{\"records\": " + DT_JSON + "}";
                if (IRGTService.sendBudget_Position(BP_ID, User_Code, out ReturnMSG_TH, out ReturnMSG_EN))
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