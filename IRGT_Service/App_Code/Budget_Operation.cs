using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Services;


/// <summary>
/// Summary description for Budget
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Budget_Operation : System.Web.Services.WebService
{

    public Budget_Operation()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }


    SqlParameter newParam(string Name, object Value)
    {
        SetLog("[Parameter][" + Name + "]=>" + Value);
        return new SqlParameter(Name, Value);
    }
    SqlParameter newParam(string Name, SqlDbType DbType)
    {
        return new SqlParameter(Name, DbType);
    }
    SqlParameter newParam(string Name, SqlDbType DbType, int Size)
    {
        return new SqlParameter(Name, DbType, Size);
    }

    void SetLog(string Message)
    {

    }
    void SetErrorLog(string Message)
    {

    }

    public SqlConnection GetDBConnection()
    {
        SqlConnection DbConnect = new SqlConnection();
        DbConnect.ConnectionString = ConfigurationManager.AppSettings["DBConnection"];
        return DbConnect;
    }

    #region Master Data
    [WebMethod]
    public DataTable getMasterData(string FN, string Lang, string Param)
    {
        string StoreProcedureName = "sp_getMasterData_Budget";
        SetLog("========================START==============================");
        SetLog("[@Time][Store:" + StoreProcedureName + "]");

        SqlConnection DBConnect = GetDBConnection();
        SqlCommand DBCommand = new SqlCommand();
        DBCommand.Connection = DBConnect;
        DBCommand.CommandType = CommandType.StoredProcedure;
        DBCommand.CommandText = StoreProcedureName;

        DBCommand.Parameters.Add(newParam("@Function", FN));
        DBCommand.Parameters.Add(newParam("@Language", Lang));
        DBCommand.Parameters.Add(newParam("@Param", Param));

        SqlDataReader DBReader;
        DataTable TableResult = null;

        try
        {
            DBConnect.Open();
            SetLog("[Open Connection]");
            DBReader = DBCommand.ExecuteReader();

            TableResult = new DataTable("Data");
            TableResult.Load(DBReader);

            DBConnect.Close();
            SetLog("[Close Connection]");
            SetLog("[@Time]");
            SetLog("========================END==============================");
            return TableResult;
        }
        catch (Exception ex)
        {
            SetLog("[ERROR]=>" + ex.Message);
            SetLog("========================END==============================");
            SetErrorLog("[@Time][Store:" + StoreProcedureName + "]=>ERROR:" + ex.Message);
            DBConnect.Close();
            return null;
        }
    }
    #endregion

    #region Budget_OperationByID
    [WebMethod]
    public DataTable getBudget_OperationByID(int PageSize, int PageIndex, string BO_ID, string BO_Type_ID, string Lang)
    {
        string StoreProcedureName = "sp_getBudget_OperationByID";
        SetLog("========================START==============================");
        SetLog("[@Time][Store:" + StoreProcedureName + "]");

        SqlConnection DBConnect = GetDBConnection();
        SqlCommand DBCommand = new SqlCommand();
        DBCommand.Connection = DBConnect;
        DBCommand.CommandType = CommandType.StoredProcedure;
        DBCommand.CommandText = StoreProcedureName;

        DBCommand.Parameters.Add(newParam("@PageSize", PageSize));
        DBCommand.Parameters.Add(newParam("@PageIndex", PageIndex));
        DBCommand.Parameters.Add(newParam("@BO_ID", BO_ID));
        DBCommand.Parameters.Add(newParam("@BO_Type_ID", BO_Type_ID));
        DBCommand.Parameters.Add(newParam("@Language", Lang));

        SqlDataReader DBReader;
        DataTable TableResult = null;

        try
        {
            DBConnect.Open();
            SetLog("[Open Connection]");
            DBReader = DBCommand.ExecuteReader();

            TableResult = new DataTable("Data");
            TableResult.Load(DBReader);

            DBConnect.Close();
            SetLog("[Close Connection]");
            SetLog("[@Time]");
            SetLog("========================END==============================");
            return TableResult;
        }
        catch (Exception ex)
        {
            SetLog("[ERROR]=>" + ex.Message);
            SetLog("========================END==============================");
            SetErrorLog("[@Time][Store:" + StoreProcedureName + "]=>ERROR:" + ex.Message);
            DBConnect.Close();
            return null;
        }
    }

    [WebMethod]
    public int getBudget_OperationByID_Count(string BO_ID, string BO_Type_ID)
    {
        string StoreProcedureName = "sp_getBudget_OperationByID_Count";
        SetLog("========================START==============================");
        SetLog("[@Time][Store:" + StoreProcedureName + "]");
        SqlConnection DBConnect = GetDBConnection();
        SqlCommand DBCommand = new SqlCommand();
        DBCommand.Connection = DBConnect;
        DBCommand.CommandType = CommandType.StoredProcedure;
        DBCommand.CommandText = StoreProcedureName;

        DBCommand.Parameters.Add(newParam("@BO_ID", BO_ID));
        DBCommand.Parameters.Add(newParam("@BO_Type_ID", BO_Type_ID));

        SqlDataReader DBReader;
        DataTable TableResult = null;

        try
        {
            DBConnect.Open();
            SetLog("[Open Connection]");

            DBReader = DBCommand.ExecuteReader();
            TableResult = new DataTable("Data");
            TableResult.Load(DBReader);
            DBConnect.Close();

            SetLog("[Close Connection]");
            SetLog("[@Time]");
            SetLog("========================END==============================");
            return int.Parse(TableResult.Rows[0][0].ToString());
        }
        catch (Exception ex)
        {
            SetLog("[ERROR]=>" + ex.Message);
            SetLog("========================END==============================");
            SetErrorLog("[@Time][Store:" + StoreProcedureName + "]=>ERROR:" + ex.Message);
            DBConnect.Close();
            return 0;
        }
    }

    [WebMethod]
    public DataTable getBudget_OperationSummaryByID(string BO_ID, string Lang)
    {
        string StoreProcedureName = "sp_getBudget_OperationSummaryByID";
        SetLog("========================START==============================");
        SetLog("[@Time][Store:" + StoreProcedureName + "]");

        SqlConnection DBConnect = GetDBConnection();
        SqlCommand DBCommand = new SqlCommand();
        DBCommand.Connection = DBConnect;
        DBCommand.CommandType = CommandType.StoredProcedure;
        DBCommand.CommandText = StoreProcedureName;

        DBCommand.Parameters.Add(newParam("@BO_ID", BO_ID));
        DBCommand.Parameters.Add(newParam("@Language", Lang));

        SqlDataReader DBReader;
        DataTable TableResult = null;

        try
        {
            DBConnect.Open();
            SetLog("[Open Connection]");
            DBReader = DBCommand.ExecuteReader();

            TableResult = new DataTable("Data");
            TableResult.Load(DBReader);

            DBConnect.Close();
            SetLog("[Close Connection]");
            SetLog("[@Time]");
            SetLog("========================END==============================");
            return TableResult;
        }
        catch (Exception ex)
        {
            SetLog("[ERROR]=>" + ex.Message);
            SetLog("========================END==============================");
            SetErrorLog("[@Time][Store:" + StoreProcedureName + "]=>ERROR:" + ex.Message);
            DBConnect.Close();
            return null;
        }
    }

    [WebMethod]
    public bool adjustBudget_Operation(int KeyID, string BO_Qty_Adj
        , string BO_Price_Adj, string User_Code
        , out string ReturnMSG_TH, out string ReturnMSG_EN)
    {
        bool ReturnOutput = false;
        ReturnMSG_TH = "";
        ReturnMSG_EN = "";

        string StoreProcedureName = "sp_adjustBudget_Operation";
        SetLog("========================START==============================");
        SetLog("[@Time][Store:" + StoreProcedureName + "]");
        SqlConnection DBConnect = GetDBConnection();
        SqlCommand DBCommand = new SqlCommand();
        DBCommand.Connection = DBConnect;
        DBCommand.CommandType = CommandType.StoredProcedure;
        DBCommand.CommandText = StoreProcedureName;

        DBCommand.Parameters.Add(newParam("@KeyID", KeyID));
        DBCommand.Parameters.Add(newParam("@BO_Qty_Adj", BO_Qty_Adj));
        DBCommand.Parameters.Add(newParam("@BO_Price_Adj", BO_Price_Adj));
        DBCommand.Parameters.Add(newParam("@USER_CODE", User_Code));

        //================================= RETURN OUTPUT ===========================
        DBCommand.Parameters.Add(newParam("@ReturnCode", SqlDbType.Int));
        DBCommand.Parameters["@ReturnCode"].Direction = ParameterDirection.Output;
        DBCommand.Parameters.Add(newParam("@ReturnMSG_TH", SqlDbType.VarChar, 200));
        DBCommand.Parameters["@ReturnMSG_TH"].Direction = ParameterDirection.Output;
        DBCommand.Parameters.Add(newParam("@ReturnMSG_EN", SqlDbType.VarChar, 200));
        DBCommand.Parameters["@ReturnMSG_EN"].Direction = ParameterDirection.Output;

        try
        {
            DBConnect.Open();
            SetLog("[Open Connection]");
            DBCommand.ExecuteNonQuery();
            string ReturnCode = DBCommand.Parameters["@ReturnCode"].Value.ToString();
            if (ReturnCode == "100") ReturnOutput = false;
            else ReturnOutput = true;

            ReturnMSG_TH = DBCommand.Parameters["@ReturnMSG_TH"].Value.ToString();
            ReturnMSG_EN = DBCommand.Parameters["@ReturnMSG_EN"].Value.ToString();
            DBConnect.Close();

            SetLog("[Close Connection]");
            SetLog("[@Time]");
            SetLog("========================END==============================");
        }
        catch (Exception ex)
        {
            SetLog("[ERROR]=>" + ex.Message);
            SetLog("========================END==============================");
            SetErrorLog("[@Time][Store:" + StoreProcedureName + "]=>ERROR:" + ex.Message);
            DBConnect.Close();

            ReturnOutput = false;
            ReturnMSG_TH = "เกิดข้อผิดพลาดจากระบบ:" + ex.Message;
            ReturnMSG_EN = "System Error:" + ex.Message;
        }
        return ReturnOutput;
    }

    #endregion

    #region Budget_Operation_List
    [WebMethod]
    public DataTable getBudget_Operation_List(int PageSize, int PageIndex, string User_Code, string Loc_ID, string Lang)
    {
        string StoreProcedureName = "sp_getBudget_Operation_List";
        SetLog("========================START==============================");
        SetLog("[@Time][Store:" + StoreProcedureName + "]");

        SqlConnection DBConnect = GetDBConnection();
        SqlCommand DBCommand = new SqlCommand();
        DBCommand.Connection = DBConnect;
        DBCommand.CommandType = CommandType.StoredProcedure;
        DBCommand.CommandText = StoreProcedureName;

        DBCommand.Parameters.Add(newParam("@PageSize", PageSize));
        DBCommand.Parameters.Add(newParam("@PageIndex", PageIndex));
        DBCommand.Parameters.Add(newParam("@User_Code", User_Code));
        DBCommand.Parameters.Add(newParam("@Loc_ID", Loc_ID));
        DBCommand.Parameters.Add(newParam("@Language", Lang));

        SqlDataReader DBReader;
        DataTable TableResult = null;

        try
        {
            DBConnect.Open();
            SetLog("[Open Connection]");
            DBReader = DBCommand.ExecuteReader();

            TableResult = new DataTable("Data");
            TableResult.Load(DBReader);

            DBConnect.Close();
            SetLog("[Close Connection]");
            SetLog("[@Time]");
            SetLog("========================END==============================");
            return TableResult;
        }
        catch (Exception ex)
        {
            SetLog("[ERROR]=>" + ex.Message);
            SetLog("========================END==============================");
            SetErrorLog("[@Time][Store:" + StoreProcedureName + "]=>ERROR:" + ex.Message);
            DBConnect.Close();
            return null;
        }
    }

    [WebMethod]
    public int getBudget_Operation_List_Count(string User_Code, string Loc_ID)
    {
        string StoreProcedureName = "sp_getBudget_Operation_List_Count";
        SetLog("========================START==============================");
        SetLog("[@Time][Store:" + StoreProcedureName + "]");
        SqlConnection DBConnect = GetDBConnection();
        SqlCommand DBCommand = new SqlCommand();
        DBCommand.Connection = DBConnect;
        DBCommand.CommandType = CommandType.StoredProcedure;
        DBCommand.CommandText = StoreProcedureName;

        DBCommand.Parameters.Add(newParam("@Loc_ID", Loc_ID));
        DBCommand.Parameters.Add(newParam("@User_Code", User_Code));

        SqlDataReader DBReader;
        DataTable TableResult = null;

        try
        {
            DBConnect.Open();
            SetLog("[Open Connection]");

            DBReader = DBCommand.ExecuteReader();
            TableResult = new DataTable("Data");
            TableResult.Load(DBReader);
            DBConnect.Close();

            SetLog("[Close Connection]");
            SetLog("[@Time]");
            SetLog("========================END==============================");
            return int.Parse(TableResult.Rows[0][0].ToString());
        }
        catch (Exception ex)
        {
            SetLog("[ERROR]=>" + ex.Message);
            SetLog("========================END==============================");
            SetErrorLog("[@Time][Store:" + StoreProcedureName + "]=>ERROR:" + ex.Message);
            DBConnect.Close();
            return 0;
        }
    }

    [WebMethod]
    public DataTable getBudget_OperationDetail(string BO_ID, string Lang)
    {
        string StoreProcedureName = "sp_getBudget_OperationDetail";
        SetLog("========================START==============================");
        SetLog("[@Time][Store:" + StoreProcedureName + "]");

        SqlConnection DBConnect = GetDBConnection();
        SqlCommand DBCommand = new SqlCommand();
        DBCommand.Connection = DBConnect;
        DBCommand.CommandType = CommandType.StoredProcedure;
        DBCommand.CommandText = StoreProcedureName;
        
        DBCommand.Parameters.Add(newParam("@BO_ID", BO_ID));
        DBCommand.Parameters.Add(newParam("@Language", Lang));

        SqlDataReader DBReader;
        DataTable TableResult = null;

        try
        {
            DBConnect.Open();
            SetLog("[Open Connection]");
            DBReader = DBCommand.ExecuteReader();

            TableResult = new DataTable("Data");
            TableResult.Load(DBReader);

            DBConnect.Close();
            SetLog("[Close Connection]");
            SetLog("[@Time]");
            SetLog("========================END==============================");
            return TableResult;
        }
        catch (Exception ex)
        {
            SetLog("[ERROR]=>" + ex.Message);
            SetLog("========================END==============================");
            SetErrorLog("[@Time][Store:" + StoreProcedureName + "]=>ERROR:" + ex.Message);
            DBConnect.Close();
            return null;
        }
    }
    #endregion

    #region Budget_Operation
    [WebMethod]
    public DataTable getBudget_Operation(int PageSize, int PageIndex, string User_Code, string BO_Type_ID, string Lang)
    {
        string StoreProcedureName = "sp_getBudget_Operation";
        SetLog("========================START==============================");
        SetLog("[@Time][Store:" + StoreProcedureName + "]");

        SqlConnection DBConnect = GetDBConnection();
        SqlCommand DBCommand = new SqlCommand();
        DBCommand.Connection = DBConnect;
        DBCommand.CommandType = CommandType.StoredProcedure;
        DBCommand.CommandText = StoreProcedureName;

        DBCommand.Parameters.Add(newParam("@PageSize", PageSize));
        DBCommand.Parameters.Add(newParam("@PageIndex", PageIndex));
        DBCommand.Parameters.Add(newParam("@User_Code", User_Code));
        DBCommand.Parameters.Add(newParam("@BO_Type_ID", BO_Type_ID));
        DBCommand.Parameters.Add(newParam("@Language", Lang));

        SqlDataReader DBReader;
        DataTable TableResult = null;

        try
        {
            DBConnect.Open();
            SetLog("[Open Connection]");
            DBReader = DBCommand.ExecuteReader();

            TableResult = new DataTable("Data");
            TableResult.Load(DBReader);

            DBConnect.Close();
            SetLog("[Close Connection]");
            SetLog("[@Time]");
            SetLog("========================END==============================");
            return TableResult;
        }
        catch (Exception ex)
        {
            SetLog("[ERROR]=>" + ex.Message);
            SetLog("========================END==============================");
            SetErrorLog("[@Time][Store:" + StoreProcedureName + "]=>ERROR:" + ex.Message);
            DBConnect.Close();
            return null;
        }
    }

    [WebMethod]
    public int getBudget_Operation_Count(string User_Code, string BO_Type_ID)
    {
        string StoreProcedureName = "sp_getBudget_Operation_Count";
        SetLog("========================START==============================");
        SetLog("[@Time][Store:" + StoreProcedureName + "]");
        SqlConnection DBConnect = GetDBConnection();
        SqlCommand DBCommand = new SqlCommand();
        DBCommand.Connection = DBConnect;
        DBCommand.CommandType = CommandType.StoredProcedure;
        DBCommand.CommandText = StoreProcedureName;

        DBCommand.Parameters.Add(newParam("@User_Code", User_Code));
        DBCommand.Parameters.Add(newParam("@BO_Type_ID", BO_Type_ID));

        SqlDataReader DBReader;
        DataTable TableResult = null;

        try
        {
            DBConnect.Open();
            SetLog("[Open Connection]");

            DBReader = DBCommand.ExecuteReader();
            TableResult = new DataTable("Data");
            TableResult.Load(DBReader);
            DBConnect.Close();

            SetLog("[Close Connection]");
            SetLog("[@Time]");
            SetLog("========================END==============================");
            return int.Parse(TableResult.Rows[0][0].ToString());
        }
        catch (Exception ex)
        {
            SetLog("[ERROR]=>" + ex.Message);
            SetLog("========================END==============================");
            SetErrorLog("[@Time][Store:" + StoreProcedureName + "]=>ERROR:" + ex.Message);
            DBConnect.Close();
            return 0;
        }
    }

    [WebMethod]
    public string checkBudget_Operation(string User_Code)
    {
        bool ReturnOutput = false;
        string ReturnCode = "";

        string StoreProcedureName = "sp_checkBudget_Operation";
        SetLog("========================START==============================");
        SetLog("[@Time][Store:" + StoreProcedureName + "]");
        SqlConnection DBConnect = GetDBConnection();
        SqlCommand DBCommand = new SqlCommand();
        DBCommand.Connection = DBConnect;
        DBCommand.CommandType = CommandType.StoredProcedure;
        DBCommand.CommandText = StoreProcedureName;

        DBCommand.Parameters.Add(newParam("@User_Code", User_Code));

        //================================= RETURN OUTPUT ===========================
        DBCommand.Parameters.Add(newParam("@ReturnCode", SqlDbType.VarChar, 50));
        DBCommand.Parameters["@ReturnCode"].Direction = ParameterDirection.Output;

        try
        {
            DBConnect.Open();
            SetLog("[Open Connection]");
            DBCommand.ExecuteNonQuery();
            ReturnCode = DBCommand.Parameters["@ReturnCode"].Value.ToString();
            if (ReturnCode == "") ReturnOutput = false;
            else ReturnOutput = true;

            DBConnect.Close();

            SetLog("[Close Connection]");
            SetLog("[@Time]");
            SetLog("========================END==============================");
        }
        catch (Exception ex)
        {
            SetLog("[ERROR]=>" + ex.Message);
            SetLog("========================END==============================");
            SetErrorLog("[@Time][Store:" + StoreProcedureName + "]=>ERROR:" + ex.Message);
            DBConnect.Close();

            ReturnOutput = false;
        }
        return ReturnCode;
    }

    [WebMethod]
    public bool setBudget_Operation(int KeyID, string BO_ID, string BO_Name, string BO_Type_ID, string BO_Qty
        , string BO_Price, string BO_Reason, string User_Code
        , out string ReturnMSG_TH, out string ReturnMSG_EN)
    {
        bool ReturnOutput = false;
        ReturnMSG_TH = "";
        ReturnMSG_EN = "";

        string StoreProcedureName = "sp_setBudget_Operation";
        SetLog("========================START==============================");
        SetLog("[@Time][Store:" + StoreProcedureName + "]");
        SqlConnection DBConnect = GetDBConnection();
        SqlCommand DBCommand = new SqlCommand();
        DBCommand.Connection = DBConnect;
        DBCommand.CommandType = CommandType.StoredProcedure;
        DBCommand.CommandText = StoreProcedureName;

        DBCommand.Parameters.Add(newParam("@KeyID", KeyID));
        DBCommand.Parameters.Add(newParam("@BO_ID", BO_ID));
        DBCommand.Parameters.Add(newParam("@BO_Name", BO_Name));
        DBCommand.Parameters.Add(newParam("@BO_Type_ID", BO_Type_ID));
        DBCommand.Parameters.Add(newParam("@BO_Qty", BO_Qty));
        DBCommand.Parameters.Add(newParam("@BO_Price", BO_Price));
        DBCommand.Parameters.Add(newParam("@BO_Reason", BO_Reason));
        DBCommand.Parameters.Add(newParam("@USER_CODE", User_Code));

        //================================= RETURN OUTPUT ===========================
        DBCommand.Parameters.Add(newParam("@ReturnCode", SqlDbType.Int));
        DBCommand.Parameters["@ReturnCode"].Direction = ParameterDirection.Output;
        DBCommand.Parameters.Add(newParam("@ReturnMSG_TH", SqlDbType.VarChar, 200));
        DBCommand.Parameters["@ReturnMSG_TH"].Direction = ParameterDirection.Output;
        DBCommand.Parameters.Add(newParam("@ReturnMSG_EN", SqlDbType.VarChar, 200));
        DBCommand.Parameters["@ReturnMSG_EN"].Direction = ParameterDirection.Output;

        try
        {
            DBConnect.Open();
            SetLog("[Open Connection]");
            DBCommand.ExecuteNonQuery();
            string ReturnCode = DBCommand.Parameters["@ReturnCode"].Value.ToString();
            if (ReturnCode == "100") ReturnOutput = false;
            else ReturnOutput = true;

            ReturnMSG_TH = DBCommand.Parameters["@ReturnMSG_TH"].Value.ToString();
            ReturnMSG_EN = DBCommand.Parameters["@ReturnMSG_EN"].Value.ToString();
            DBConnect.Close();

            SetLog("[Close Connection]");
            SetLog("[@Time]");
            SetLog("========================END==============================");
        }
        catch (Exception ex)
        {
            SetLog("[ERROR]=>" + ex.Message);
            SetLog("========================END==============================");
            SetErrorLog("[@Time][Store:" + StoreProcedureName + "]=>ERROR:" + ex.Message);
            DBConnect.Close();

            ReturnOutput = false;
            ReturnMSG_TH = "เกิดข้อผิดพลาดจากระบบ:" + ex.Message;
            ReturnMSG_EN = "System Error:" + ex.Message;
        }
        return ReturnOutput;
    }

    [WebMethod]
    public bool deleteBudget_Operation(int KeyID, out string ReturnMSG_TH, out string ReturnMSG_EN)
    {
        bool ReturnOutput = false;
        ReturnMSG_TH = "";
        ReturnMSG_EN = "";

        string StoreProcedureName = "sp_deleteBudget_Operation";
        SetLog("========================START==============================");
        SetLog("[@Time][Store:" + StoreProcedureName + "]");
        SqlConnection DBConnect = GetDBConnection();
        SqlCommand DBCommand = new SqlCommand();
        DBCommand.Connection = DBConnect;
        DBCommand.CommandType = CommandType.StoredProcedure;
        DBCommand.CommandText = StoreProcedureName;

        DBCommand.Parameters.Add(newParam("@KeyID", KeyID));
        //================================= RETURN OUTPUT ===========================
        DBCommand.Parameters.Add(newParam("@ReturnCode", SqlDbType.Int));
        DBCommand.Parameters["@ReturnCode"].Direction = ParameterDirection.Output;
        DBCommand.Parameters.Add(newParam("@ReturnMSG_TH", SqlDbType.VarChar, 200));
        DBCommand.Parameters["@ReturnMSG_TH"].Direction = ParameterDirection.Output;
        DBCommand.Parameters.Add(newParam("@ReturnMSG_EN", SqlDbType.VarChar, 200));
        DBCommand.Parameters["@ReturnMSG_EN"].Direction = ParameterDirection.Output;

        try
        {
            DBConnect.Open();
            SetLog("[Open Connection]");
            DBCommand.ExecuteNonQuery();
            string ReturnCode = DBCommand.Parameters["@ReturnCode"].Value.ToString();
            if (ReturnCode == "100") ReturnOutput = false;
            else ReturnOutput = true;

            ReturnMSG_TH = DBCommand.Parameters["@ReturnMSG_TH"].Value.ToString();
            ReturnMSG_EN = DBCommand.Parameters["@ReturnMSG_EN"].Value.ToString();
            DBConnect.Close();

            SetLog("[Close Connection]");
            SetLog("[@Time]");
            SetLog("========================END==============================");
        }
        catch (Exception ex)
        {
            SetLog("[ERROR]=>" + ex.Message);
            SetLog("========================END==============================");
            SetErrorLog("[@Time][Store:" + StoreProcedureName + "]=>ERROR:" + ex.Message);
            DBConnect.Close();

            ReturnOutput = false;
            ReturnMSG_TH = "เกิดข้อผิดพลาดจากระบบ:" + ex.Message;
            ReturnMSG_EN = "System Error:" + ex.Message;
        }
        return ReturnOutput;
    }

    [WebMethod]
    public bool sendBudget_Operation(string BO_ID, string User_Code, out string ReturnMSG_TH, out string ReturnMSG_EN)
    {
        bool ReturnOutput = false;
        ReturnMSG_TH = "";
        ReturnMSG_EN = "";

        string StoreProcedureName = "sp_sendBudget_Operation";
        SetLog("========================START==============================");
        SetLog("[@Time][Store:" + StoreProcedureName + "]");
        SqlConnection DBConnect = GetDBConnection();
        SqlCommand DBCommand = new SqlCommand();
        DBCommand.Connection = DBConnect;
        DBCommand.CommandType = CommandType.StoredProcedure;
        DBCommand.CommandText = StoreProcedureName;

        DBCommand.Parameters.Add(newParam("@BO_ID", BO_ID));
        DBCommand.Parameters.Add(newParam("@User_Code", User_Code));
        //================================= RETURN OUTPUT ===========================
        DBCommand.Parameters.Add(newParam("@ReturnCode", SqlDbType.Int));
        DBCommand.Parameters["@ReturnCode"].Direction = ParameterDirection.Output;
        DBCommand.Parameters.Add(newParam("@ReturnMSG_TH", SqlDbType.VarChar, 200));
        DBCommand.Parameters["@ReturnMSG_TH"].Direction = ParameterDirection.Output;
        DBCommand.Parameters.Add(newParam("@ReturnMSG_EN", SqlDbType.VarChar, 200));
        DBCommand.Parameters["@ReturnMSG_EN"].Direction = ParameterDirection.Output;

        try
        {
            DBConnect.Open();
            SetLog("[Open Connection]");
            DBCommand.ExecuteNonQuery();
            string ReturnCode = DBCommand.Parameters["@ReturnCode"].Value.ToString();
            if (ReturnCode == "100") ReturnOutput = false;
            else ReturnOutput = true;

            ReturnMSG_TH = DBCommand.Parameters["@ReturnMSG_TH"].Value.ToString();
            ReturnMSG_EN = DBCommand.Parameters["@ReturnMSG_EN"].Value.ToString();
            DBConnect.Close();

            SetLog("[Close Connection]");
            SetLog("[@Time]");
            SetLog("========================END==============================");
        }
        catch (Exception ex)
        {
            SetLog("[ERROR]=>" + ex.Message);
            SetLog("========================END==============================");
            SetErrorLog("[@Time][Store:" + StoreProcedureName + "]=>ERROR:" + ex.Message);
            DBConnect.Close();

            ReturnOutput = false;
            ReturnMSG_TH = "เกิดข้อผิดพลาดจากระบบ:" + ex.Message;
            ReturnMSG_EN = "System Error:" + ex.Message;
        }
        return ReturnOutput;
    }

    [WebMethod]
    public bool confirmBudget_Operation(string BO_ID, string User_Code, out string ReturnMSG_TH, out string ReturnMSG_EN)
    {
        bool ReturnOutput = false;
        ReturnMSG_TH = "";
        ReturnMSG_EN = "";

        string StoreProcedureName = "sp_confirmBudget_Operation";
        SetLog("========================START==============================");
        SetLog("[@Time][Store:" + StoreProcedureName + "]");
        SqlConnection DBConnect = GetDBConnection();
        SqlCommand DBCommand = new SqlCommand();
        DBCommand.Connection = DBConnect;
        DBCommand.CommandType = CommandType.StoredProcedure;
        DBCommand.CommandText = StoreProcedureName;

        DBCommand.Parameters.Add(newParam("@BO_ID", BO_ID));
        DBCommand.Parameters.Add(newParam("@User_Code", User_Code));
        //================================= RETURN OUTPUT ===========================
        DBCommand.Parameters.Add(newParam("@ReturnCode", SqlDbType.Int));
        DBCommand.Parameters["@ReturnCode"].Direction = ParameterDirection.Output;
        DBCommand.Parameters.Add(newParam("@ReturnMSG_TH", SqlDbType.VarChar, 200));
        DBCommand.Parameters["@ReturnMSG_TH"].Direction = ParameterDirection.Output;
        DBCommand.Parameters.Add(newParam("@ReturnMSG_EN", SqlDbType.VarChar, 200));
        DBCommand.Parameters["@ReturnMSG_EN"].Direction = ParameterDirection.Output;

        try
        {
            DBConnect.Open();
            SetLog("[Open Connection]");
            DBCommand.ExecuteNonQuery();
            string ReturnCode = DBCommand.Parameters["@ReturnCode"].Value.ToString();
            if (ReturnCode == "100") ReturnOutput = false;
            else ReturnOutput = true;

            ReturnMSG_TH = DBCommand.Parameters["@ReturnMSG_TH"].Value.ToString();
            ReturnMSG_EN = DBCommand.Parameters["@ReturnMSG_EN"].Value.ToString();
            DBConnect.Close();

            SetLog("[Close Connection]");
            SetLog("[@Time]");
            SetLog("========================END==============================");
        }
        catch (Exception ex)
        {
            SetLog("[ERROR]=>" + ex.Message);
            SetLog("========================END==============================");
            SetErrorLog("[@Time][Store:" + StoreProcedureName + "]=>ERROR:" + ex.Message);
            DBConnect.Close();

            ReturnOutput = false;
            ReturnMSG_TH = "เกิดข้อผิดพลาดจากระบบ:" + ex.Message;
            ReturnMSG_EN = "System Error:" + ex.Message;
        }
        return ReturnOutput;
    }

    [WebMethod]
    public bool approveBudget_Operation(string BO_ID, string User_Code, out string ReturnMSG_TH, out string ReturnMSG_EN)
    {
        bool ReturnOutput = false;
        ReturnMSG_TH = "";
        ReturnMSG_EN = "";

        string StoreProcedureName = "sp_approveBudget_Operation";
        SetLog("========================START==============================");
        SetLog("[@Time][Store:" + StoreProcedureName + "]");
        SqlConnection DBConnect = GetDBConnection();
        SqlCommand DBCommand = new SqlCommand();
        DBCommand.Connection = DBConnect;
        DBCommand.CommandType = CommandType.StoredProcedure;
        DBCommand.CommandText = StoreProcedureName;

        DBCommand.Parameters.Add(newParam("@BO_ID", BO_ID));
        DBCommand.Parameters.Add(newParam("@User_Code", User_Code));
        //================================= RETURN OUTPUT ===========================
        DBCommand.Parameters.Add(newParam("@ReturnCode", SqlDbType.Int));
        DBCommand.Parameters["@ReturnCode"].Direction = ParameterDirection.Output;
        DBCommand.Parameters.Add(newParam("@ReturnMSG_TH", SqlDbType.VarChar, 200));
        DBCommand.Parameters["@ReturnMSG_TH"].Direction = ParameterDirection.Output;
        DBCommand.Parameters.Add(newParam("@ReturnMSG_EN", SqlDbType.VarChar, 200));
        DBCommand.Parameters["@ReturnMSG_EN"].Direction = ParameterDirection.Output;

        try
        {
            DBConnect.Open();
            SetLog("[Open Connection]");
            DBCommand.ExecuteNonQuery();
            string ReturnCode = DBCommand.Parameters["@ReturnCode"].Value.ToString();
            if (ReturnCode == "100") ReturnOutput = false;
            else ReturnOutput = true;

            ReturnMSG_TH = DBCommand.Parameters["@ReturnMSG_TH"].Value.ToString();
            ReturnMSG_EN = DBCommand.Parameters["@ReturnMSG_EN"].Value.ToString();
            DBConnect.Close();

            SetLog("[Close Connection]");
            SetLog("[@Time]");
            SetLog("========================END==============================");
        }
        catch (Exception ex)
        {
            SetLog("[ERROR]=>" + ex.Message);
            SetLog("========================END==============================");
            SetErrorLog("[@Time][Store:" + StoreProcedureName + "]=>ERROR:" + ex.Message);
            DBConnect.Close();

            ReturnOutput = false;
            ReturnMSG_TH = "เกิดข้อผิดพลาดจากระบบ:" + ex.Message;
            ReturnMSG_EN = "System Error:" + ex.Message;
        }
        return ReturnOutput;
    }

    [WebMethod]
    public DataTable getBudget_OperationSummary(string User_Code, string Lang)
    {
        string StoreProcedureName = "sp_getBudget_OperationSummary";
        SetLog("========================START==============================");
        SetLog("[@Time][Store:" + StoreProcedureName + "]");

        SqlConnection DBConnect = GetDBConnection();
        SqlCommand DBCommand = new SqlCommand();
        DBCommand.Connection = DBConnect;
        DBCommand.CommandType = CommandType.StoredProcedure;
        DBCommand.CommandText = StoreProcedureName;
        
        DBCommand.Parameters.Add(newParam("@User_Code", User_Code));
        DBCommand.Parameters.Add(newParam("@Language", Lang));

        SqlDataReader DBReader;
        DataTable TableResult = null;

        try
        {
            DBConnect.Open();
            SetLog("[Open Connection]");
            DBReader = DBCommand.ExecuteReader();

            TableResult = new DataTable("Data");
            TableResult.Load(DBReader);

            DBConnect.Close();
            SetLog("[Close Connection]");
            SetLog("[@Time]");
            SetLog("========================END==============================");
            return TableResult;
        }
        catch (Exception ex)
        {
            SetLog("[ERROR]=>" + ex.Message);
            SetLog("========================END==============================");
            SetErrorLog("[@Time][Store:" + StoreProcedureName + "]=>ERROR:" + ex.Message);
            DBConnect.Close();
            return null;
        }
    }
    
    #endregion
    

}
