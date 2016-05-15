using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class server_Server : System.Web.UI.Page
{
    IRGT_Service.Service IRGTService = new IRGT_Service.Service();

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
            case "Login":
                fnLogin();
                break;
            case "User":
                fnUser();
                break;
            case "GetMasterData":
                fnGetMasterData();
                break;
            case "Asset":
                fnAsset();
                break;
            case "AssetType":
                fnAssetType();
                break;
            case "AssetDepreciate":
                fnAssetDepreciate();
                break;
            case "AssetQuota":
                fnAssetQuota();
                break;
            case "AssetDetail":
                fnAssetDetail();
                break;
            case "WorkCenter":
                fnWorkCenter();
                break;
            case "FundType":
                fnFundType();
                break;
            case "MovementType":
                fnMovementType();
                break;
            case "ReportGroupType":
                fnReportGroupType();
                break;
            case "ReportGroup":
                fnReportGroup();
                break;
        }
    }

    private void fnLogin()
    {
        string User_Code = Request.Params["User_Code"];
        string Password = Request.Params["Password"];
        string ReturnMSG_TH = "";
        string ReturnMSG_EN = "";
        if (IRGTService.login(User_Code, Password, out ReturnMSG_TH, out ReturnMSG_EN))
        {
            cCommon.setUserName(Session, User_Code);
            Response.Write("[{\"output\":\"OK\",\"message\":\"\"}]");
        }
        else
        {
            Response.Write("[{output:\"ERROR\",message:\"" + ReturnMSG_TH + "\"}]");
        }
        return;
    }
    private void fnUser()
    {
        string FN = Request.Params["Function"];
        DataTable DT;
        string DT_JSON;
        int PageSize = 0;

        switch (FN)
        {
            case "Paging":
                PageSize = cCommon.Convert_Str_To_Int(Request.Params["PageSize"]);
                string User_Code = Request.Params["User_Code"];

                int Count = IRGTService.getUser_Count(User_Code);
                DT_JSON = genPaging(PageSize, Count);

                Response.Write(DT_JSON);
                return;
            case "Select":
                PageSize = cCommon.Convert_Str_To_Int(Request.Params["PageSize"]);
                User_Code = Request.Params["User_Code"];              
                int PageIndex = int.Parse(Request.Params["PageIndex"]);
                string lang = Request.Params["lang"];
                DT = IRGTService.getUser(PageSize, PageIndex, User_Code, lang);
                Session["Data_User"] = DT.Copy();
                DT_JSON = DataTableToJSON(DT);
                DT_JSON = "{\"records\": " + DT_JSON + "}";
                Response.Write(DT_JSON);
                return;
            case "Check":
                lang = "" + Session["language_asset"];
                if (lang == "") lang = "TH";
                User_Code = Request.Params["User_Code"];
                int User_ID = cCommon.Convert_Str_To_Int(Request.Params["User_ID"]);
                string ReturnMSG_TH = "";
                string ReturnMSG_EN = "";
                if (IRGTService.checkUser(User_ID, User_Code, out ReturnMSG_TH, out ReturnMSG_EN))
                    Response.Write("[{\"output\":\"OK\",\"message\":\"\"}]");
                else
                {
                    if (lang == "TH")
                        Response.Write("[{output:\"ERROR\",message:\"" + ReturnMSG_TH + "\"}]");
                    else
                        Response.Write("[{output:\"ERROR\",message:\"" + ReturnMSG_EN + "\"}]");
                }

                return;
            case "Save":
                lang = Request.Params["lang"];
                if (lang == "") lang = "TH";
                string User_Type_ID = Request.Params["User_Type_ID"];
                User_Code = Request.Params["User_Code"];
                string User_Password = Request.Params["User_Password"];
                string Status = Request.Params["Status"];
               
                User_ID = cCommon.Convert_Str_To_Int(Request.Params["User_ID"]);
                if (IRGTService.setUser(User_ID, User_Type_ID,User_Code,User_Password,Status
                    , cCommon.getUserName(Session), out ReturnMSG_TH, out ReturnMSG_EN))
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
                User_ID = cCommon.Convert_Str_To_Int(Request.Params["User_ID"]);
                DT = (DataTable)Session["Data_User"];
                DataRow[] dr_list = DT.Select("User_ID = " + User_ID);
                string OP = "[{";
                OP += "User_ID:\"" + dr_list[0]["User_ID"] + "\"";
                OP += ",User_Type_ID:\"" + dr_list[0]["User_Type_ID"] + "\"";
                OP += ",User_Code:\"" + dr_list[0]["User_Code"] + "\"";
                OP += ",User_Password:\"" + dr_list[0]["User_Password"] + "\"";
                OP += ",Status:\"" + dr_list[0]["Status"] + "\"";
                OP += "}]";
                Response.Write(OP);
                return;
            case "Delete":
                lang = Request.Params["lang"];
                if (lang == "") lang = "TH";
                User_ID = cCommon.Convert_Str_To_Int(Request.Params["User_ID"]);
                ReturnMSG_TH = "";
                ReturnMSG_EN = "";

                //"{\"records\": " + DT_JSON + "}";
                if (IRGTService.deleteUser(User_ID, out ReturnMSG_TH, out ReturnMSG_EN))
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
    private void fnAsset()
    {
        string FN = Request.Params["Function"];
        DataTable DT;
        string DT_JSON;
        int PageSize = 0;

        switch (FN)
        {
            case "Paging":
                PageSize = cCommon.Convert_Str_To_Int(Request.Params["PageSize"]);
                string Asset_ID = Request.Params["Asset_ID"];
                string Asset_Name_T = Request.Params["Asset_Name_T"];
                string Asset_Name_E = Request.Params["Asset_Name_E"];
                string Asset_Type_ID = Request.Params["Asset_Type_ID"];

                int Count = IRGTService.getAsset_Count(Asset_ID, Asset_Name_T, Asset_Name_E, Asset_Type_ID);
                DT_JSON = genPaging(PageSize, Count);

                Response.Write(DT_JSON);
                return;
            case "Select":
                PageSize = cCommon.Convert_Str_To_Int(Request.Params["PageSize"]);
                Asset_ID = Request.Params["Asset_ID"];
                Asset_Name_T = Request.Params["Asset_Name_T"];
                Asset_Name_E = Request.Params["Asset_Name_E"];
                Asset_Type_ID = Request.Params["Asset_Type_ID"];
                int PageIndex = int.Parse(Request.Params["PageIndex"]);
                string lang = Request.Params["lang"];
                DT = IRGTService.getAsset(PageSize, PageIndex, Asset_ID, Asset_Name_T, Asset_Name_E, Asset_Type_ID, lang);
                Session["Data_Asset"] = DT.Copy();
                DT_JSON = DataTableToJSON(DT);
                DT_JSON = "{\"records\": " + DT_JSON + "}";
                Response.Write(DT_JSON);
                return;
            case "Check":
                lang = "" + Session["language_asset"];
                if (lang == "") lang = "TH";
                Asset_ID = Request.Params["Asset_ID"];
                int KeyID = cCommon.Convert_Str_To_Int(Request.Params["KeyID"]);
                string ReturnMSG_TH = "";
                string ReturnMSG_EN = "";
                if (IRGTService.checkAsset(KeyID, Asset_ID, out ReturnMSG_TH, out ReturnMSG_EN))
                    Response.Write("[{\"output\":\"OK\",\"message\":\"\"}]");
                else
                {
                    if (lang == "TH")
                        Response.Write("[{output:\"ERROR\",message:\"" + ReturnMSG_TH + "\"}]");
                    else
                        Response.Write("[{output:\"ERROR\",message:\"" + ReturnMSG_EN + "\"}]");
                }

                return;
            case "Save":
                lang = Request.Params["lang"];
                if (lang == "") lang = "TH";
                Asset_ID = Request.Params["Asset_ID"];
                string Asset_Name1_T = Request.Params["Asset_Name1_T"];
                string Asset_Name1_E = Request.Params["Asset_Name1_E"];
                string Asset_Name2_T = Request.Params["Asset_Name2_T"];
                string Asset_Name2_E = Request.Params["Asset_Name2_E"];
                Asset_Type_ID = Request.Params["Asset_Type_ID"];
                string Asset_Ref_No = Request.Params["Asset_Ref_No"];
                string Unit_Name = Request.Params["Unit_Name"];
                string StartDate = Request.Params["StartDate"];
                string EndDate = Request.Params["EndDate"];
                KeyID = cCommon.Convert_Str_To_Int(Request.Params["KeyID"]);
                if (IRGTService.setAsset(KeyID, Asset_ID, Asset_Name1_T, Asset_Name1_E, Asset_Name2_T, Asset_Name2_E
                    , Asset_Type_ID, Asset_Ref_No, Unit_Name
                    , StartDate, EndDate, cCommon.getUserName(Session), out ReturnMSG_TH, out ReturnMSG_EN))
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
                DT = (DataTable)Session["Data_Asset"];
                DataRow[] dr_list = DT.Select("KeyID = " + KeyID);
                string OP = "[{";
                OP += "Asset_ID:\"" + dr_list[0]["Asset_ID"] + "\"";
                OP += ",Asset_Name1_T:\"" + dr_list[0]["Asset_Name1_T"] + "\"";
                OP += ",Asset_Name1_E:\"" + dr_list[0]["Asset_Name1_E"] + "\"";
                OP += ",Asset_Name2_T:\"" + dr_list[0]["Asset_Name2_T"] + "\"";
                OP += ",Asset_Name2_E:\"" + dr_list[0]["Asset_Name2_E"] + "\"";
                OP += ",Asset_Type_ID:\"" + dr_list[0]["Asset_Type_ID"] + "\"";
                OP += ",Asset_Ref_No:\"" + dr_list[0]["Asset_Ref_No"] + "\"";
                OP += ",Unit_Name:\"" + dr_list[0]["Unit_Name"] + "\"";
                
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
                if (IRGTService.deleteAsset(KeyID, out ReturnMSG_TH, out ReturnMSG_EN))
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
    private void fnAssetDepreciate()
    {
        string FN = Request.Params["Function"];
        DataTable DT;
        string DT_JSON;
        int PageSize = 0;

        switch (FN)
        {
            case "Paging":
                PageSize = cCommon.Convert_Str_To_Int(Request.Params["PageSize"]);
                string Asset_ID = Request.Params["Asset_ID"];
                int Count = IRGTService.getAsset_Depreciate_Count(Asset_ID);
                DT_JSON = genPaging(PageSize, Count);

                Response.Write(DT_JSON);
                return;
            case "Select":
                PageSize = cCommon.Convert_Str_To_Int(Request.Params["PageSize"]);
                Asset_ID = Request.Params["Asset_ID"];              
                int PageIndex = int.Parse(Request.Params["PageIndex"]);
                string lang = Request.Params["lang"];
                DT = IRGTService.getAsset_Depreciate(PageSize, PageIndex, Asset_ID, lang);
                Session["Data_Asset_Depreciate"] = DT.Copy();
                DT_JSON = DataTableToJSON(DT);
                DT_JSON = "{\"records\": " + DT_JSON + "}";
                Response.Write(DT_JSON);
                return;
            case "Check":
                lang = "" + Session["language_asset_depreciate"];
                if (lang == "") lang = "TH";
                Asset_ID = Request.Params["Asset_ID"];
                int KeyID = cCommon.Convert_Str_To_Int(Request.Params["KeyID"]);
                string ReturnMSG_TH = "";
                string ReturnMSG_EN = "";
                if (IRGTService.checkAsset_Depreciate(KeyID, Asset_ID, out ReturnMSG_TH, out ReturnMSG_EN))
                    Response.Write("[{\"output\":\"OK\",\"message\":\"\"}]");
                else
                {
                    if (lang == "TH")
                        Response.Write("[{output:\"ERROR\",message:\"" + ReturnMSG_TH + "\"}]");
                    else
                        Response.Write("[{output:\"ERROR\",message:\"" + ReturnMSG_EN + "\"}]");
                }

                return;
            case "Save":
                lang = Request.Params["lang"];
                if (lang == "") lang = "TH";
                Asset_ID = Request.Params["Asset_ID"];
                string Depreciate_Rate = Request.Params["Depreciate_Rate"];
                string Standard_Price = Request.Params["Standard_Price"];
                string Term_Use = Request.Params["Term_Use"];
                string StartDate = Request.Params["StartDate"];
                string EndDate = Request.Params["EndDate"];
                KeyID = cCommon.Convert_Str_To_Int(Request.Params["KeyID"]);
                if (IRGTService.setAsset_Depreciate(KeyID, Asset_ID, Depreciate_Rate, Standard_Price, Term_Use
                    , StartDate, EndDate, cCommon.getUserName(Session), out ReturnMSG_TH, out ReturnMSG_EN))
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
                DT = (DataTable)Session["Data_Asset_Depreciate"];
                DataRow[] dr_list = DT.Select("KeyID = " + KeyID);
                string OP = "[{";
                OP += "Asset_ID:\"" + dr_list[0]["Asset_ID"] + "\"";
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
                if (IRGTService.deleteAsset_Depreciate(KeyID, out ReturnMSG_TH, out ReturnMSG_EN))
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
    private void fnAssetType()
    {
        string FN = Request.Params["Function"];
        DataTable DT;
        string DT_JSON;
        int PageSize = 0;

        switch (FN)
        {
            case "Paging":
                PageSize = cCommon.Convert_Str_To_Int(Request.Params["PageSize"]);
                string AssetTypeID = Request.Params["AssetTypeID"];
                string AssetTypeTh = Request.Params["AssetTypeTh"];
                string AssetTypeEn = Request.Params["AssetTypeEn"];

                int Count = IRGTService.getAsset_Type_Count(AssetTypeID, AssetTypeTh, AssetTypeEn);
                DT_JSON = genPaging(PageSize, Count);

                Response.Write(DT_JSON);
                return;
            case "Select":
                PageSize = cCommon.Convert_Str_To_Int(Request.Params["PageSize"]);
                AssetTypeID = Request.Params["AssetTypeID"];
                AssetTypeTh = Request.Params["AssetTypeTh"];
                AssetTypeEn = Request.Params["AssetTypeEn"];

                int PageIndex = int.Parse(Request.Params["PageIndex"]);
                DT = IRGTService.getAsset_Type(PageSize, PageIndex,AssetTypeID,AssetTypeTh,AssetTypeEn);
                Session["Data_AssetType"] = DT.Copy();
                DT_JSON = DataTableToJSON(DT);
                DT_JSON = "{\"records\": " + DT_JSON + "}";
                Response.Write(DT_JSON);
                return;
            case "Check":
                string lang = "" + Session["language_asset_type"];
                if (lang == "") lang = "TH";
                AssetTypeID = Request.Params["AssetTypeID"];
                int KeyID = cCommon.Convert_Str_To_Int(Request.Params["KeyID"]);
                string ReturnMSG_TH = "";
                string ReturnMSG_EN = "";
                if (IRGTService.checkAsset_Type(KeyID, AssetTypeID, out ReturnMSG_TH, out ReturnMSG_EN))
                    Response.Write("[{output:\"OK\",message:\"\"}]");
                else
                {
                    if (lang == "TH")
                        Response.Write("[{output:\"ERROR\",message:\"" + ReturnMSG_TH + "\"}]");
                    else
                        Response.Write("[{output:\"ERROR\",message:\"" + ReturnMSG_EN + "\"}]");
                }

                return;
            case "Save":
                lang = Request.Params["lang"];
                if (lang == "") lang = "TH";
                AssetTypeID = Request.Params["AssetTypeID"];
                AssetTypeTh = Request.Params["AssetTypeTh"];
                AssetTypeEn = Request.Params["AssetTypeEn"];
                string StartDate = Request.Params["StartDate"];
                string EndDate = Request.Params["EndDate"];
                KeyID = cCommon.Convert_Str_To_Int(Request.Params["KeyID"]);
                if (IRGTService.setAsset_Type(KeyID, AssetTypeID, AssetTypeTh, AssetTypeEn, StartDate, EndDate, cCommon.getUserName(Session), out ReturnMSG_TH, out ReturnMSG_EN))
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
                DT = (DataTable)Session["Data_AssetType"];
                DataRow[] dr_list = DT.Select("KeyID = " + KeyID);
                string OP = "[{";
                OP += "Asset_Type_ID:\"" + dr_list[0]["Asset_Type_ID"] + "\"";
                OP += ",Asset_Type_Name_T:\"" + dr_list[0]["Asset_Type_Name_T"] + "\"";
                OP += ",Asset_Type_Name_E:\"" + dr_list[0]["Asset_Type_Name_E"] + "\"";
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
                if (IRGTService.deleteAsset_Type(KeyID, out ReturnMSG_TH, out ReturnMSG_EN))
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
    private void fnAssetQuota()
    {
        string FN = Request.Params["Function"];
        DataTable DT;
        string DT_JSON;
        int PageSize = 0;

        switch (FN)
        {
            case "Paging":
                PageSize = cCommon.Convert_Str_To_Int(Request.Params["PageSize"]);
                string Asset_ID = Request.Params["Asset_ID"];
                string Loc_ID = Request.Params["Loc_ID"];

                int Count = IRGTService.getAsset_Quota_Count(Asset_ID, Loc_ID);
                DT_JSON = genPaging(PageSize, Count);

                Response.Write(DT_JSON);
                return;
            case "Select":
                string lang = Request.Params["lang"];
                if (lang == "") lang = "TH";
                PageSize = cCommon.Convert_Str_To_Int(Request.Params["PageSize"]);
                Asset_ID = Request.Params["Asset_ID"];
                Loc_ID = Request.Params["Loc_ID"];
                int PageIndex = int.Parse(Request.Params["PageIndex"]);
                DT = IRGTService.getAsset_Quota(PageSize, PageIndex, Asset_ID, Loc_ID, lang);
                Session["Data_Asset_Quota"] = DT.Copy();
                DT_JSON = DataTableToJSON(DT);
                DT_JSON = "{\"records\": " + DT_JSON + "}";
                Response.Write(DT_JSON);
                return;
            case "Check":
                lang = "" + Session["language_asset_quota"];
                if (lang == "") lang = "TH";
                Asset_ID = Request.Params["Asset_ID"];
                Loc_ID = Request.Params["Loc_ID"];
                int KeyID = cCommon.Convert_Str_To_Int(Request.Params["KeyID"]);
                string ReturnMSG_TH = "";
                string ReturnMSG_EN = "";
                if (IRGTService.checkAsset_Quota(KeyID, Asset_ID, Loc_ID, out ReturnMSG_TH, out ReturnMSG_EN))
                    Response.Write("[{output:\"OK\",message:\"\"}]");
                else
                {
                    if (lang == "TH")
                        Response.Write("[{output:\"ERROR\",message:\"" + ReturnMSG_TH + "\"}]");
                    else
                        Response.Write("[{output:\"ERROR\",message:\"" + ReturnMSG_EN + "\"}]");
                }

                return;
            case "Save":
                lang = Request.Params["lang"];
                if (lang == "") lang = "TH";
                Asset_ID = Request.Params["Asset_ID"];
                Loc_ID = Request.Params["Loc_ID"];
                string Quota_Qty = Request.Params["Quota_Qty"];
                string Stock_Qty = Request.Params["Stock_Qty"];
                
                string StartDate = Request.Params["StartDate"];
                string EndDate = Request.Params["EndDate"];
                KeyID = cCommon.Convert_Str_To_Int(Request.Params["KeyID"]);
                if (IRGTService.setAsset_Quota(KeyID, Asset_ID, Loc_ID, Quota_Qty, Stock_Qty, StartDate, EndDate, cCommon.getUserName(Session), out ReturnMSG_TH, out ReturnMSG_EN))
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
                DT = (DataTable)Session["Data_Asset_Quota"];
                DataRow[] dr_list = DT.Select("KeyID = " + KeyID);
                string OP = "[{";
                OP += "Asset_ID:\"" + dr_list[0]["Asset_ID"] + "\"";
                OP += ",Loc_ID:\"" + dr_list[0]["Loc_ID"] + "\"";
                OP += ",Quota_Qty:\"" + dr_list[0]["Quota_Qty"] + "\"";
                OP += ",Stock_Qty:\"" + dr_list[0]["Stock_Qty"] + "\"";
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
                if (IRGTService.deleteAsset_Quota(KeyID, out ReturnMSG_TH, out ReturnMSG_EN))
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
    private void fnAssetDetail()
    {
        string FN = Request.Params["Function"];
        DataTable DT;
        string DT_JSON;
        int PageSize = 0;

        switch (FN)
        {
            case "Paging":
                PageSize = cCommon.Convert_Str_To_Int(Request.Params["PageSize"]);
                string Item_ID = Request.Params["Item_ID"];
                string Loc_ID = Request.Params["Loc_ID"];
                string Asset_Type_ID = Request.Params["Asset_Type_ID"];
                string Asset_ID = Request.Params["Asset_ID"];

                int Count = IRGTService.getAsset_Detail_Count(Item_ID, Loc_ID, Asset_Type_ID, Asset_ID);
                DT_JSON = genPaging(PageSize, Count);

                Response.Write(DT_JSON);
                return;
            case "Select":
                string lang = Request.Params["lang"];
                if (lang == "") lang = "TH";
                PageSize = cCommon.Convert_Str_To_Int(Request.Params["PageSize"]);
                int PageIndex = int.Parse(Request.Params["PageIndex"]);
                Item_ID = Request.Params["Item_ID"];
                Loc_ID = Request.Params["Loc_ID"];
                Asset_Type_ID = Request.Params["Asset_Type_ID"];
                Asset_ID = Request.Params["Asset_ID"];

                DT = IRGTService.getAsset_Detail(PageSize, PageIndex, Item_ID, Loc_ID, Asset_Type_ID, Asset_ID, lang);
                Session["Data_Asset_Detail"] = DT.Copy();
                DT_JSON = DataTableToJSON(DT);
                DT_JSON = "{\"records\": " + DT_JSON + "}";
                Response.Write(DT_JSON);
                return;
            case "Check":
                lang = "" + Session["language_asset_detail"];
                if (lang == "") lang = "TH";
                Item_ID = Request.Params["Item_ID"];
                int KeyID = cCommon.Convert_Str_To_Int(Request.Params["KeyID"]);
                string ReturnMSG_TH = "";
                string ReturnMSG_EN = "";
                if (IRGTService.checkAsset_Detail(KeyID, Item_ID, out ReturnMSG_TH, out ReturnMSG_EN))
                    Response.Write("[{output:\"OK\",message:\"\"}]");
                else
                {
                    if (lang == "TH")
                        Response.Write("[{output:\"ERROR\",message:\"" + ReturnMSG_TH + "\"}]");
                    else
                        Response.Write("[{output:\"ERROR\",message:\"" + ReturnMSG_EN + "\"}]");
                }
                return;
            case "Save":
                lang = Request.Params["lang"];
                if (lang == "") lang = "TH";
                KeyID = cCommon.Convert_Str_To_Int(Request.Params["KeyID"]);
                Item_ID = Request.Params["Item_ID"];
                Loc_ID = Request.Params["Loc_ID"];
                Asset_Type_ID = Request.Params["Asset_Type_ID"];
                Asset_ID = Request.Params["Asset_ID"];
                string Serial_No = Request.Params["Serial_No"];
                string Asset_Loc = Request.Params["Asset_Loc"];
                string Owner = Request.Params["Owner"];
                string Quality = Request.Params["Quality"];
                string Tranfer_Loc = Request.Params["Tranfer_Loc"];
                string Budget_Year = Request.Params["Budget_Year"];
                string Document_No = Request.Params["Document_No"];
                string Document_Date = Request.Params["Document_Date"];
                string Fund_Type_ID = Request.Params["Fund_Type_ID"];
                string Standard_Price = Request.Params["Standard_Price"];
                string Price_per_unit = Request.Params["Price_per_unit"];
                string Purchase_Doc_No = Request.Params["Purchase_Doc_No"];
                string Purchase_Doc_Date = Request.Params["Purchase_Doc_Date"];
                string Brand = Request.Params["Brand"];
                string Serie = Request.Params["Serie"];
                string Receive_from = Request.Params["Receive_from"];
                string Mvt_ID = Request.Params["Mvt_ID"];
                string Term_Use = Request.Params["Term_Use"];
                string Depriciate_accru = Request.Params["Depriciate_accru"];
                string Depriciate_in_year = Request.Params["Depriciate_in_year"];
                string Net_Price = Request.Params["Net_Price"];

                if (IRGTService.setAsset_Detail(KeyID, Item_ID, Loc_ID, Asset_Type_ID, Asset_ID, Serial_No, Asset_Loc, Owner, Quality, Tranfer_Loc
                    , Budget_Year, Document_No, Document_Date, Fund_Type_ID, Standard_Price, Price_per_unit, Purchase_Doc_No, Purchase_Doc_Date
                    , Brand, Serie, Receive_from, Mvt_ID, Term_Use, Depriciate_accru, Depriciate_in_year, Net_Price
                    , cCommon.getUserName(Session), out ReturnMSG_TH, out ReturnMSG_EN))
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
                DT = (DataTable)Session["Data_Asset_Detail"];
                DataRow[] dr_list = DT.Select("KeyID = " + KeyID);
                string OP = "[{";
                OP += "Item_ID:\"" + dr_list[0]["Item_ID"] + "\"";
                OP += ",Loc_ID:\"" + dr_list[0]["Loc_ID"] + "\"";
                OP += ",Asset_Type_ID:\"" + dr_list[0]["Asset_Type_ID"] + "\"";
                OP += ",Asset_ID:\"" + dr_list[0]["Asset_ID"] + "\"";
                OP += ",Serial_No:\"" + dr_list[0]["Serial_No"] + "\"";
                OP += ",Asset_Loc:\"" + dr_list[0]["Asset_Loc"] + "\"";
                OP += ",Owner:\"" + dr_list[0]["Owner"] + "\"";
                OP += ",Quality:\"" + dr_list[0]["Quality"] + "\"";
                OP += ",Tranfer_Loc:\"" + dr_list[0]["Tranfer_Loc"] + "\"";
                OP += ",Budget_Year:\"" + dr_list[0]["Budget_Year"] + "\"";
                OP += ",Document_No:\"" + dr_list[0]["Document_No"] + "\"";
                OP += ",Document_Date:\"" + dr_list[0]["Document_Date"] + "\"";
                OP += ",Fund_Type_ID:\"" + dr_list[0]["Fund_Type_ID"] + "\"";
                OP += ",Standard_Price:\"" + dr_list[0]["Standard_Price"] + "\"";
                OP += ",Price_per_unit:\"" + dr_list[0]["Price_per_unit"] + "\"";
                OP += ",Purchase_Doc_no:\"" + dr_list[0]["Purchase_Doc_no"] + "\"";
                OP += ",Purchase_Doc_date:\"" + dr_list[0]["Purchase_Doc_date"] + "\"";
                OP += ",Brand:\"" + dr_list[0]["Brand"] + "\"";
                OP += ",Serie:\"" + dr_list[0]["Serie"] + "\"";
                OP += ",Receive_from:\"" + dr_list[0]["Receive_from"] + "\"";
                OP += ",Mvt_ID:\"" + dr_list[0]["Mvt_ID"] + "\"";
                OP += ",Term_Use:\"" + dr_list[0]["Term_Use"] + "\"";
                OP += ",Depriciate_accru:\"" + dr_list[0]["Depriciate_accru"] + "\"";
                OP += ",Depriciate_in_year:\"" + dr_list[0]["Depriciate_in_year"] + "\"";
                OP += ",Net_Price:\"" + dr_list[0]["Net_Price"] + "\"";
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
                if (IRGTService.deleteAsset_Detail(KeyID, out ReturnMSG_TH, out ReturnMSG_EN))
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
    private void fnWorkCenter()
    {
        string FN = Request.Params["Function"];
        DataTable DT;
        string DT_JSON;
        int PageSize = 0;

        switch (FN)
        {
            case "Paging":
                PageSize = cCommon.Convert_Str_To_Int(Request.Params["PageSize"]);
                string Loc_ID = Request.Params["Loc_ID"];
                string Loc_Name_T = Request.Params["Loc_Name_T"];
                string Loc_Name_E = Request.Params["Loc_Name_E"];

                int Count = IRGTService.getWork_Center_Count(Loc_ID, Loc_Name_T, Loc_Name_E);
                DT_JSON = genPaging(PageSize, Count);

                Response.Write(DT_JSON);
                return;
            case "Select":
                PageSize = cCommon.Convert_Str_To_Int(Request.Params["PageSize"]);
                Loc_ID = Request.Params["Loc_ID"];
                Loc_Name_T = Request.Params["Loc_Name_T"];
                Loc_Name_E = Request.Params["Loc_Name_E"];

                int PageIndex = int.Parse(Request.Params["PageIndex"]);
                DT = IRGTService.getWork_Center(PageSize, PageIndex, Loc_ID, Loc_Name_T, Loc_Name_E);
                Session["Data_WorkCenter"] = DT.Copy();
                DT_JSON = DataTableToJSON(DT);
                DT_JSON = "{\"records\": " + DT_JSON + "}";
                Response.Write(DT_JSON);
                return;
            case "Check":
                 string lang = "" + Session["language_work_center"];
                if (lang == "") lang = "TH";
                Loc_ID = Request.Params["Loc_ID"];
                int KeyID = cCommon.Convert_Str_To_Int(Request.Params["KeyID"]);
                string ReturnMSG_TH = "";
                string ReturnMSG_EN = "";
                if (IRGTService.checkWork_Center(KeyID, Loc_ID, out ReturnMSG_TH, out ReturnMSG_EN))
                    Response.Write("[{output:\"OK\",message:\"\"}]");
                else
                {
                    if (lang == "TH")
                        Response.Write("[{output:\"ERROR\",message:\"" + ReturnMSG_TH + "\"}]");
                    else
                        Response.Write("[{output:\"ERROR\",message:\"" + ReturnMSG_EN + "\"}]");
                }

                return;
            case "Save":
                lang = Request.Params["lang"];
                if (lang == "") lang = "TH";
                Loc_ID = Request.Params["Loc_ID"];
                Loc_Name_T = Request.Params["Loc_Name_T"];
                Loc_Name_E = Request.Params["Loc_Name_E"];
                string Loc_Address_T = Request.Params["Loc_Address_T"];
                string Loc_Address_E = Request.Params["Loc_Address_E"];
                string Loc_Tel = Request.Params["Loc_Tel"];
                string Loc_Level = Request.Params["Loc_Level"];
                string StartDate = Request.Params["StartDate"];
                string EndDate = Request.Params["EndDate"];
                KeyID = cCommon.Convert_Str_To_Int(Request.Params["KeyID"]);
                if (IRGTService.setWork_Center(KeyID, Loc_ID, Loc_Name_T, Loc_Name_E,Loc_Address_T,Loc_Address_E,Loc_Tel,Loc_Level, StartDate, EndDate, cCommon.getUserName(Session), out ReturnMSG_TH, out ReturnMSG_EN))
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
                DT = (DataTable)Session["Data_WorkCenter"];
                DataRow[] dr_list = DT.Select("KeyID = " + KeyID);
                string OP = "[{";
                OP += "Loc_ID:\"" + dr_list[0]["Loc_ID"] + "\"";
                OP += ",Loc_Name_T:\"" + dr_list[0]["Loc_Name_T"] + "\"";
                OP += ",Loc_Name_E:\"" + dr_list[0]["Loc_Name_E"] + "\"";
                OP += ",Loc_Address_T:\"" + dr_list[0]["Loc_Address_T"] + "\"";
                OP += ",Loc_Address_E:\"" + dr_list[0]["Loc_Address_E"] + "\"";
                OP += ",Loc_Tel:\"" + dr_list[0]["Loc_Tel"] + "\"";
                OP += ",Loc_Level:\"" + dr_list[0]["Loc_Level"] + "\"";
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
                if (IRGTService.deleteWork_Center(KeyID, out ReturnMSG_TH, out ReturnMSG_EN))
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
    private void fnFundType()
    {
        string FN = Request.Params["Function"];
        DataTable DT;
        string DT_JSON;
        int PageSize = 0;

        switch (FN)
        {
            case "Paging":
                PageSize = cCommon.Convert_Str_To_Int(Request.Params["PageSize"]);
                string Fund_Type_ID = Request.Params["Fund_Type_ID"];
                string Fund_Type_Name_T = Request.Params["Fund_Type_Name_T"];
                string Fund_Type_Name_E = Request.Params["Fund_Type_Name_E"];

                int Count = IRGTService.getFund_Type_Count(Fund_Type_ID, Fund_Type_Name_T, Fund_Type_Name_E);
                DT_JSON = genPaging(PageSize, Count);

                Response.Write(DT_JSON);
                return;
            case "Select":
                PageSize = cCommon.Convert_Str_To_Int(Request.Params["PageSize"]);
                Fund_Type_ID = Request.Params["Fund_Type_ID"];
                Fund_Type_Name_T = Request.Params["Fund_Type_Name_T"];
                Fund_Type_Name_E = Request.Params["Fund_Type_Name_E"];

                int PageIndex = int.Parse(Request.Params["PageIndex"]);
                DT = IRGTService.getFund_Type(PageSize, PageIndex, Fund_Type_ID, Fund_Type_Name_T, Fund_Type_Name_E);
                Session["Data_FundType"] = DT.Copy();
                DT_JSON = DataTableToJSON(DT);
                DT_JSON = "{\"records\": " + DT_JSON + "}";
                Response.Write(DT_JSON);
                return;
            case "Check":
                string lang = "" + Session["language_fund_type"];
                if (lang == "") lang = "TH";
                Fund_Type_ID = Request.Params["Fund_Type_ID"];
                int KeyID = cCommon.Convert_Str_To_Int(Request.Params["KeyID"]);
                string ReturnMSG_TH = "";
                string ReturnMSG_EN = "";
                if (IRGTService.checkFund_Type(KeyID, Fund_Type_ID, out ReturnMSG_TH, out ReturnMSG_EN))
                    Response.Write("[{output:\"OK\",message:\"\"}]");
                else
                {
                    if (lang == "TH")
                        Response.Write("[{output:\"ERROR\",message:\"" + ReturnMSG_TH + "\"}]");
                    else
                        Response.Write("[{output:\"ERROR\",message:\"" + ReturnMSG_EN + "\"}]");
                }

                return;
            case "Save":
                lang = Request.Params["lang"];
                if (lang == "") lang = "TH";
                Fund_Type_ID = Request.Params["Fund_Type_ID"];
                Fund_Type_Name_T = Request.Params["Fund_Type_Name_T"];
                Fund_Type_Name_E = Request.Params["Fund_Type_Name_E"];
                string StartDate = Request.Params["StartDate"];
                string EndDate = Request.Params["EndDate"];
                KeyID = cCommon.Convert_Str_To_Int(Request.Params["KeyID"]);
                if (IRGTService.setFund_Type(KeyID, Fund_Type_ID, Fund_Type_Name_T, Fund_Type_Name_E, StartDate, EndDate, cCommon.getUserName(Session), out ReturnMSG_TH, out ReturnMSG_EN))
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
                DT = (DataTable)Session["Data_FundType"];
                DataRow[] dr_list = DT.Select("KeyID = " + KeyID);
                string OP = "[{";
                OP += "Fund_Type_ID:\"" + dr_list[0]["Fund_Type_ID"] + "\"";
                OP += ",Fund_Type_Name_T:\"" + dr_list[0]["Fund_Type_Name_T"] + "\"";
                OP += ",Fund_Type_Name_E:\"" + dr_list[0]["Fund_Type_Name_E"] + "\"";
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
                if (IRGTService.deleteFund_Type(KeyID, out ReturnMSG_TH, out ReturnMSG_EN))
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
    private void fnMovementType()
    {
        string FN = Request.Params["Function"];
        DataTable DT;
        string DT_JSON;
        int PageSize = 0;

        switch (FN)
        {
            case "Paging":
                PageSize = cCommon.Convert_Str_To_Int(Request.Params["PageSize"]);
                string Mvt_ID = Request.Params["Mvt_ID"];
                string Mvt_Name_T = Request.Params["Mvt_Name_T"];
                string Mvt_Name_E = Request.Params["Mvt_Name_E"];

                int Count = IRGTService.getMovement_Type_Count(Mvt_ID, Mvt_Name_T, Mvt_Name_E);
                DT_JSON = genPaging(PageSize, Count);

                Response.Write(DT_JSON);
                return;
            case "Select":
                PageSize = cCommon.Convert_Str_To_Int(Request.Params["PageSize"]);
                Mvt_ID = Request.Params["Mvt_ID"];
                Mvt_Name_T = Request.Params["Mvt_Name_T"];
                Mvt_Name_E = Request.Params["Mvt_Name_E"];

                int PageIndex = int.Parse(Request.Params["PageIndex"]);
                DT = IRGTService.getMovement_Type(PageSize, PageIndex, Mvt_ID, Mvt_Name_T, Mvt_Name_E);
                Session["Data_MovementType"] = DT.Copy();
                DT_JSON = DataTableToJSON(DT);
                DT_JSON = "{\"records\": " + DT_JSON + "}";
                Response.Write(DT_JSON);
                return;
            case "Check":
                string lang = "" + Session["language_movement_type"];
                if (lang == "") lang = "TH";
                Mvt_ID = Request.Params["Mvt_ID"];
                int KeyID = cCommon.Convert_Str_To_Int(Request.Params["KeyID"]);
                string ReturnMSG_TH = "";
                string ReturnMSG_EN = "";
                if (IRGTService.checkMovement_Type(KeyID, Mvt_ID, out ReturnMSG_TH, out ReturnMSG_EN))
                    Response.Write("[{output:\"OK\",message:\"\"}]");
                else
                {
                    if (lang == "TH")
                        Response.Write("[{output:\"ERROR\",message:\"" + ReturnMSG_TH + "\"}]");
                    else
                        Response.Write("[{output:\"ERROR\",message:\"" + ReturnMSG_EN + "\"}]");
                }

                return;
            case "Save":
                lang = Request.Params["lang"];
                if (lang == "") lang = "TH";
                Mvt_ID = Request.Params["Mvt_ID"];
                Mvt_Name_T = Request.Params["Mvt_Name_T"];
                Mvt_Name_E = Request.Params["Mvt_Name_E"];
                string Mvt_Value = Request.Params["Mvt_Value"];

                string StartDate = Request.Params["StartDate"];
                string EndDate = Request.Params["EndDate"];
                KeyID = cCommon.Convert_Str_To_Int(Request.Params["KeyID"]);
                if (IRGTService.setMovement_Type(KeyID, Mvt_ID, Mvt_Name_T, Mvt_Name_E, Mvt_Value, StartDate, EndDate, cCommon.getUserName(Session), out ReturnMSG_TH, out ReturnMSG_EN))
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
                DT = (DataTable)Session["Data_MovementType"];
                DataRow[] dr_list = DT.Select("KeyID = " + KeyID);
                string OP = "[{";
                OP += "Mvt_ID:\"" + dr_list[0]["Mvt_ID"] + "\"";
                OP += ",Mvt_Name_T:\"" + dr_list[0]["Mvt_Name_T"] + "\"";
                OP += ",Mvt_Name_E:\"" + dr_list[0]["Mvt_Name_E"] + "\"";
                OP += ",Mvt_Value:\"" + dr_list[0]["Mvt_Value"] + "\"";
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
                if (IRGTService.deleteMovement_Type(KeyID, out ReturnMSG_TH, out ReturnMSG_EN))
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
    private void fnReportGroupType()
    {
        string FN = Request.Params["Function"];
        DataTable DT;
        string DT_JSON;
        int PageSize = 0;

        switch (FN)
        {
            case "Paging":
                PageSize = cCommon.Convert_Str_To_Int(Request.Params["PageSize"]);
                string Type_Grp_ID = Request.Params["Type_Grp_ID"];
                string Type_Grp_Name_T = Request.Params["Type_Grp_Name_T"];
                string Type_Grp_Name_E = Request.Params["Type_Grp_Name_E"];

                int Count = IRGTService.getReport_Group_Type_Count(Type_Grp_ID, Type_Grp_Name_T, Type_Grp_Name_E);
                DT_JSON = genPaging(PageSize, Count);

                Response.Write(DT_JSON);
                return;
            case "Select":
                PageSize = cCommon.Convert_Str_To_Int(Request.Params["PageSize"]);
                Type_Grp_ID = Request.Params["Type_Grp_ID"];
                Type_Grp_Name_T = Request.Params["Type_Grp_Name_T"];
                Type_Grp_Name_E = Request.Params["Type_Grp_Name_E"];
                int PageIndex = int.Parse(Request.Params["PageIndex"]);
                DT = IRGTService.getReport_Group_Type(PageSize, PageIndex, Type_Grp_ID, Type_Grp_Name_T, Type_Grp_Name_E);
                Session["Data_ReportGroupType"] = DT.Copy();
                DT_JSON = DataTableToJSON(DT);
                DT_JSON = "{\"records\": " + DT_JSON + "}";
                Response.Write(DT_JSON);
                return;
            case "Check":
                string lang = "" + Session["language_report_group_type"];
                if (lang == "") lang = "TH";
                Type_Grp_ID = Request.Params["Type_Grp_ID"];
                int KeyID = cCommon.Convert_Str_To_Int(Request.Params["KeyID"]);
                string ReturnMSG_TH = "";
                string ReturnMSG_EN = "";
                if (IRGTService.checkReport_Group_Type(KeyID, Type_Grp_ID, out ReturnMSG_TH, out ReturnMSG_EN))
                    Response.Write("[{output:\"OK\",message:\"\"}]");
                else
                {
                    if (lang == "TH")
                        Response.Write("[{output:\"ERROR\",message:\"" + ReturnMSG_TH + "\"}]");
                    else
                        Response.Write("[{output:\"ERROR\",message:\"" + ReturnMSG_EN + "\"}]");
                }

                return;
            case "Save":
                lang = Request.Params["lang"];
                if (lang == "") lang = "TH";
                Type_Grp_ID = Request.Params["Type_Grp_ID"];
                Type_Grp_Name_T = Request.Params["Type_Grp_Name_T"];
                Type_Grp_Name_E = Request.Params["Type_Grp_Name_E"];
                string StartDate = Request.Params["StartDate"];
                string EndDate = Request.Params["EndDate"];
                KeyID = cCommon.Convert_Str_To_Int(Request.Params["KeyID"]);
                if (IRGTService.setReport_Group_Type(KeyID, Type_Grp_ID, Type_Grp_Name_T, Type_Grp_Name_E, StartDate, EndDate, cCommon.getUserName(Session), out ReturnMSG_TH, out ReturnMSG_EN))
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
                DT = (DataTable)Session["Data_ReportGroupType"];
                DataRow[] dr_list = DT.Select("KeyID = " + KeyID);
                string OP = "[{";
                OP += "Type_Grp_ID:\"" + dr_list[0]["Type_Grp_ID"] + "\"";
                OP += ",Type_Grp_Name_T:\"" + dr_list[0]["Type_Grp_Name_T"] + "\"";
                OP += ",Type_Grp_Name_E:\"" + dr_list[0]["Type_Grp_Name_E"] + "\"";
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
                if (IRGTService.deleteReport_Group_Type(KeyID, out ReturnMSG_TH, out ReturnMSG_EN))
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
    private void fnReportGroup()
    {
        string FN = Request.Params["Function"];
        DataTable DT;
        string DT_JSON;
        int PageSize = 0;

        switch (FN)
        {
            case "Paging":
                PageSize = cCommon.Convert_Str_To_Int(Request.Params["PageSize"]);
                string Type_Grp_ID = Request.Params["Type_Grp_ID"];
                string Loc_ID = Request.Params["Loc_ID"];

                int Count = IRGTService.getReport_Group_Count(Type_Grp_ID, Loc_ID);
                DT_JSON = genPaging(PageSize, Count);

                Response.Write(DT_JSON);
                return;
            case "Select":
                string lang = Request.Params["lang"];
                if (lang == "") lang = "TH";
                PageSize = cCommon.Convert_Str_To_Int(Request.Params["PageSize"]);
                Type_Grp_ID = Request.Params["Type_Grp_ID"];
                Loc_ID = Request.Params["Loc_ID"];
                int PageIndex = int.Parse(Request.Params["PageIndex"]);
                DT = IRGTService.getReport_Group(PageSize, PageIndex, Type_Grp_ID, Loc_ID, lang);
                Session["Data_ReportGroup"] = DT.Copy();
                DT_JSON = DataTableToJSON(DT);
                DT_JSON = "{\"records\": " + DT_JSON + "}";
                Response.Write(DT_JSON);
                return;
            case "Check":
                lang = "" + Session["language_report_group"];
                if (lang == "") lang = "TH";
                Type_Grp_ID = Request.Params["Type_Grp_ID"];
                Loc_ID = Request.Params["Loc_ID"];
                int KeyID = cCommon.Convert_Str_To_Int(Request.Params["KeyID"]);
                string ReturnMSG_TH = "";
                string ReturnMSG_EN = "";
                if (IRGTService.checkReport_Group(KeyID, Type_Grp_ID, Loc_ID, out ReturnMSG_TH, out ReturnMSG_EN))
                    Response.Write("[{output:\"OK\",message:\"\"}]");
                else
                {
                    if (lang == "TH")
                        Response.Write("[{output:\"ERROR\",message:\"" + ReturnMSG_TH + "\"}]");
                    else
                        Response.Write("[{output:\"ERROR\",message:\"" + ReturnMSG_EN + "\"}]");
                }

                return;
            case "Save":
                lang = Request.Params["lang"];
                if (lang == "") lang = "TH";
                Type_Grp_ID = Request.Params["Type_Grp_ID"];
                Loc_ID = Request.Params["Loc_ID"];
                string StartDate = Request.Params["StartDate"];
                string EndDate = Request.Params["EndDate"];
                KeyID = cCommon.Convert_Str_To_Int(Request.Params["KeyID"]);
                if (IRGTService.setReport_Group(KeyID, Type_Grp_ID, Loc_ID, StartDate, EndDate, cCommon.getUserName(Session), out ReturnMSG_TH, out ReturnMSG_EN))
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
                DT = (DataTable)Session["Data_ReportGroup"];
                DataRow[] dr_list = DT.Select("KeyID = " + KeyID);
                string OP = "[{";
                OP += "Type_Grp_ID:\"" + dr_list[0]["Type_Grp_ID"] + "\"";
                OP += ",Loc_ID:\"" + dr_list[0]["Loc_ID"] + "\"";
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
                if (IRGTService.deleteReport_Group(KeyID, out ReturnMSG_TH, out ReturnMSG_EN))
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