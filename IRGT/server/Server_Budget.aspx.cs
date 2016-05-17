using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class server_Budget : System.Web.UI.Page
{
    IRGT_Service.Budget IRGTService = new IRGT_Service.Budget();

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
            case "BudgetOperation":
                fnBudgetOperation();
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

    private void fnBudgetOperation()
    {
        string FN = Request.Params["Function"];
        DataTable DT;
        string DT_JSON;
        int PageSize = 0;

        string BO_Type_ID = Request.Params["BO_Type_ID"];
        string User_Code = Request.Params["User_Code"];
        string ReturnMSG_TH = "";
        string ReturnMSG_EN = "";
        string BO_ID = "";

        switch (FN)
        {
            case "Paging":
                PageSize = cCommon.Convert_Str_To_Int(Request.Params["PageSize"]);
               
                int Count = IRGTService.getBudget_Operation_Count(User_Code,BO_Type_ID);
                DT_JSON = genPaging(PageSize, Count);

                Response.Write(DT_JSON);
                return;
            case "Select":
                PageSize = cCommon.Convert_Str_To_Int(Request.Params["PageSize"]);
                
                int PageIndex = int.Parse(Request.Params["PageIndex"]);
                string lang = Request.Params["lang"];
                DT = IRGTService.getBudget_Operation(PageSize, PageIndex, User_Code, BO_Type_ID, lang);
                Session["Data_Budget_Operation"] = DT.Copy();
                DT_JSON = DataTableToJSON(DT);
                DT_JSON = "{\"records\": " + DT_JSON + "}";
                Response.Write(DT_JSON);
                return;
            case "Check":
                lang = "" + Session["language_budget_operation"];
                if (lang == "") lang = "TH";
                BO_ID = IRGTService.checkBudget_Operation(User_Code);
                if (BO_ID != "")
                    Response.Write("[{\"output\":\"OK\",\"BO_ID\":\"" + BO_ID + "\"}]");
                else
                {
                    if (lang == "TH")
                        Response.Write("[{output:\"ERROR\",message:\"" + ReturnMSG_TH + "\"}]");
                    else
                        Response.Write("[{output:\"ERROR\",message:\"" + ReturnMSG_EN + "\"}]");
                }

                return;
            //case "Save":
            //    lang = Request.Params["lang"];
            //    if (lang == "") lang = "TH";
            //    BO_ID = Request.Params["BO_ID"];
            //    string Depreciate_Rate = Request.Params["Depreciate_Rate"];
            //    string Standard_Price = Request.Params["Standard_Price"];
            //    string Term_Use = Request.Params["Term_Use"];
            //    string StartDate = Request.Params["StartDate"];
            //    string EndDate = Request.Params["EndDate"];
            //    KeyID = cCommon.Convert_Str_To_Int(Request.Params["KeyID"]);
            //    if (IRGTService.setBudget_Operation(KeyID, BO_ID, Depreciate_Rate, Standard_Price, Term_Use
            //        , StartDate, EndDate, cCommon.getUserName(Session), out ReturnMSG_TH, out ReturnMSG_EN))
            //        Response.Write("[{output:\"OK\",message:\"\"}]");
            //    else
            //    {
            //        if (lang == "TH")
            //            Response.Write("[{output:\"ERROR\",message:\"" + ReturnMSG_TH + "\"}]");
            //        else
            //            Response.Write("[{output:\"ERROR\",message:\"" + ReturnMSG_EN + "\"}]");
            //    }
            //    return;
            case "Load":
                int KeyID = cCommon.Convert_Str_To_Int(Request.Params["KeyID"]);
                DT = (DataTable)Session["Data_budget_operation"];
                DataRow[] dr_list = DT.Select("KeyID = " + KeyID);
                string OP = "[{";
                OP += "BO_ID:\"" + dr_list[0]["BO_ID"] + "\"";
                OP += ",Depreciate_Rate:\"" + dr_list[0]["Depreciate_Rate"] + "\"";
                OP += ",Standard_Price:\"" + dr_list[0]["Standard_Price"] + "\"";
                OP += ",Term_Use:\"" + dr_list[0]["Term_Use"] + "\"";    
                OP += ",Start_Date:\"" + dr_list[0]["Start_Date"] + "\"";
                OP += ",End_Date:\"" + dr_list[0]["End_Date"] + "\"";
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
                if (IRGTService.deleteBudget_Operation(KeyID, out ReturnMSG_TH, out ReturnMSG_EN))
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