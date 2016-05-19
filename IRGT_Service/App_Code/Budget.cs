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
public class Budget : System.Web.Services.WebService
{

    public Budget()
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

    #region User

    [WebMethod]
    public bool login(string User_Code, string Password, out string ReturnMSG_TH, out string ReturnMSG_EN)
    {
        bool ReturnOutput = false;
        ReturnMSG_TH = "";
        ReturnMSG_EN = "";

        string StoreProcedureName = "sp_login";
        SetLog("========================START==============================");
        SetLog("[@Time][Store:" + StoreProcedureName + "]");
        SqlConnection DBConnect = GetDBConnection();
        SqlCommand DBCommand = new SqlCommand();
        DBCommand.Connection = DBConnect;
        DBCommand.CommandType = CommandType.StoredProcedure;
        DBCommand.CommandText = StoreProcedureName;

        DBCommand.Parameters.Add(newParam("@User_Code", User_Code));
        DBCommand.Parameters.Add(newParam("@Password", Password));

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
    public DataTable getUser(int PageSize, int PageIndex, string User_Code, string Lang)
    {
        string StoreProcedureName = "sp_getUser";
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
    public int getUser_Count(string User_Code)
    {
        string StoreProcedureName = "sp_getUser_Count";
        SetLog("========================START==============================");
        SetLog("[@Time][Store:" + StoreProcedureName + "]");
        SqlConnection DBConnect = GetDBConnection();
        SqlCommand DBCommand = new SqlCommand();
        DBCommand.Connection = DBConnect;
        DBCommand.CommandType = CommandType.StoredProcedure;
        DBCommand.CommandText = StoreProcedureName;

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
    public bool checkUser(int User_ID, string User_Code, out string ReturnMSG_TH, out string ReturnMSG_EN)
    {
        bool ReturnOutput = false;
        ReturnMSG_TH = "";
        ReturnMSG_EN = "";

        string StoreProcedureName = "sp_checkUser";
        SetLog("========================START==============================");
        SetLog("[@Time][Store:" + StoreProcedureName + "]");
        SqlConnection DBConnect = GetDBConnection();
        SqlCommand DBCommand = new SqlCommand();
        DBCommand.Connection = DBConnect;
        DBCommand.CommandType = CommandType.StoredProcedure;
        DBCommand.CommandText = StoreProcedureName;

        DBCommand.Parameters.Add(newParam("@User_ID", User_ID));
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
    public bool setUser(int User_ID, string User_Type_ID, string User_Code, string User_Password, string Status, string USER_UPDATE
        , out string ReturnMSG_TH, out string ReturnMSG_EN)
    {
        bool ReturnOutput = false;
        ReturnMSG_TH = "";
        ReturnMSG_EN = "";

        string StoreProcedureName = "sp_setUser";
        SetLog("========================START==============================");
        SetLog("[@Time][Store:" + StoreProcedureName + "]");
        SqlConnection DBConnect = GetDBConnection();
        SqlCommand DBCommand = new SqlCommand();
        DBCommand.Connection = DBConnect;
        DBCommand.CommandType = CommandType.StoredProcedure;
        DBCommand.CommandText = StoreProcedureName;

        DBCommand.Parameters.Add(newParam("@User_ID", User_ID));
        DBCommand.Parameters.Add(newParam("@User_Type_ID", User_Type_ID));
        DBCommand.Parameters.Add(newParam("@User_Code", User_Code));
        DBCommand.Parameters.Add(newParam("@User_Password", User_Password));
        DBCommand.Parameters.Add(newParam("@Status", Status));
        DBCommand.Parameters.Add(newParam("@USER_UPDATE", USER_UPDATE));

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
    public bool deleteUser(int User_ID, out string ReturnMSG_TH, out string ReturnMSG_EN)
    {
        bool ReturnOutput = false;
        ReturnMSG_TH = "";
        ReturnMSG_EN = "";

        string StoreProcedureName = "sp_deleteUser";
        SetLog("========================START==============================");
        SetLog("[@Time][Store:" + StoreProcedureName + "]");
        SqlConnection DBConnect = GetDBConnection();
        SqlCommand DBCommand = new SqlCommand();
        DBCommand.Connection = DBConnect;
        DBCommand.CommandType = CommandType.StoredProcedure;
        DBCommand.CommandText = StoreProcedureName;

        DBCommand.Parameters.Add(newParam("@User_ID", User_ID));
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

    #region Report Group
    [WebMethod]
    public DataTable getReport_Group(int PageSize, int PageIndex, string Type_Grp_ID, string Loc_ID, string Lang)
    {
        string StoreProcedureName = "sp_getReport_Group";
        SetLog("========================START==============================");
        SetLog("[@Time][Store:" + StoreProcedureName + "]");

        SqlConnection DBConnect = GetDBConnection();
        SqlCommand DBCommand = new SqlCommand();
        DBCommand.Connection = DBConnect;
        DBCommand.CommandType = CommandType.StoredProcedure;
        DBCommand.CommandText = StoreProcedureName;

        DBCommand.Parameters.Add(newParam("@PageSize", PageSize));
        DBCommand.Parameters.Add(newParam("@PageIndex", PageIndex));
        DBCommand.Parameters.Add(newParam("@Type_Grp_ID", Type_Grp_ID));
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
    public int getReport_Group_Count(string Type_Grp_ID, string Loc_ID)
    {
        string StoreProcedureName = "sp_getReport_Group_Count";
        SetLog("========================START==============================");
        SetLog("[@Time][Store:" + StoreProcedureName + "]");
        SqlConnection DBConnect = GetDBConnection();
        SqlCommand DBCommand = new SqlCommand();
        DBCommand.Connection = DBConnect;
        DBCommand.CommandType = CommandType.StoredProcedure;
        DBCommand.CommandText = StoreProcedureName;

        DBCommand.Parameters.Add(newParam("@Type_Grp_ID", Type_Grp_ID));
        DBCommand.Parameters.Add(newParam("@Loc_ID", Loc_ID));

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
    public bool checkReport_Group(int KeyID, string Type_Grp_ID, string Loc_ID, out string ReturnMSG_TH, out string ReturnMSG_EN)
    {
        bool ReturnOutput = false;
        ReturnMSG_TH = "";
        ReturnMSG_EN = "";

        string StoreProcedureName = "sp_checkReport_Group";
        SetLog("========================START==============================");
        SetLog("[@Time][Store:" + StoreProcedureName + "]");
        SqlConnection DBConnect = GetDBConnection();
        SqlCommand DBCommand = new SqlCommand();
        DBCommand.Connection = DBConnect;
        DBCommand.CommandType = CommandType.StoredProcedure;
        DBCommand.CommandText = StoreProcedureName;

        DBCommand.Parameters.Add(newParam("@KeyID", KeyID));
        DBCommand.Parameters.Add(newParam("@Type_Grp_ID", Type_Grp_ID));
        DBCommand.Parameters.Add(newParam("@Loc_ID", Loc_ID));

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
    public bool setReport_Group(int KeyID, string Type_Grp_ID, string Loc_ID, string Start_Date, string End_Date, string UserCode
        , out string ReturnMSG_TH, out string ReturnMSG_EN)
    {
        bool ReturnOutput = false;
        ReturnMSG_TH = "";
        ReturnMSG_EN = "";

        string StoreProcedureName = "sp_setReport_Group";
        SetLog("========================START==============================");
        SetLog("[@Time][Store:" + StoreProcedureName + "]");
        SqlConnection DBConnect = GetDBConnection();
        SqlCommand DBCommand = new SqlCommand();
        DBCommand.Connection = DBConnect;
        DBCommand.CommandType = CommandType.StoredProcedure;
        DBCommand.CommandText = StoreProcedureName;

        DBCommand.Parameters.Add(newParam("@KeyID", KeyID));
        DBCommand.Parameters.Add(newParam("@Type_Grp_ID", Type_Grp_ID));
        DBCommand.Parameters.Add(newParam("@Loc_ID", Loc_ID));
        DBCommand.Parameters.Add(newParam("@Start_Date", Start_Date));
        DBCommand.Parameters.Add(newParam("@End_Date", End_Date));
        DBCommand.Parameters.Add(newParam("@USER_CODE", UserCode));

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
    public bool deleteReport_Group(int KeyID, out string ReturnMSG_TH, out string ReturnMSG_EN)
    {
        bool ReturnOutput = false;
        ReturnMSG_TH = "";
        ReturnMSG_EN = "";

        string StoreProcedureName = "sp_deleteReport_Group";
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
    #endregion

    #region Report Type Group
    [WebMethod]
    public DataTable getReport_Group_Type(int PageSize, int PageIndex, string Type_Grp_ID, string Type_Grp_Name_T, string Type_Grp_Name_E)
    {
        string StoreProcedureName = "sp_getReport_Group_Type";
        SetLog("========================START==============================");
        SetLog("[@Time][Store:" + StoreProcedureName + "]");

        SqlConnection DBConnect = GetDBConnection();
        SqlCommand DBCommand = new SqlCommand();
        DBCommand.Connection = DBConnect;
        DBCommand.CommandType = CommandType.StoredProcedure;
        DBCommand.CommandText = StoreProcedureName;

        DBCommand.Parameters.Add(newParam("@PageSize", PageSize));
        DBCommand.Parameters.Add(newParam("@PageIndex", PageIndex));
        DBCommand.Parameters.Add(newParam("@Type_Grp_ID", Type_Grp_ID));
        DBCommand.Parameters.Add(newParam("@Type_Grp_Name_T", Type_Grp_Name_T));
        DBCommand.Parameters.Add(newParam("@Type_Grp_Name_E", Type_Grp_Name_E));


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
    public int getReport_Group_Type_Count(string Type_Grp_ID, string Type_Grp_Name_T, string Type_Grp_Name_E)
    {
        string StoreProcedureName = "sp_getReport_Group_Type_Count";
        SetLog("========================START==============================");
        SetLog("[@Time][Store:" + StoreProcedureName + "]");
        SqlConnection DBConnect = GetDBConnection();
        SqlCommand DBCommand = new SqlCommand();
        DBCommand.Connection = DBConnect;
        DBCommand.CommandType = CommandType.StoredProcedure;
        DBCommand.CommandText = StoreProcedureName;

        DBCommand.Parameters.Add(newParam("@Type_Grp_ID", Type_Grp_ID));
        DBCommand.Parameters.Add(newParam("@Type_Grp_Name_T", Type_Grp_Name_T));
        DBCommand.Parameters.Add(newParam("@Type_Grp_Name_E", Type_Grp_Name_E));

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
    public bool checkReport_Group_Type(int KeyID, string Type_Grp_ID, out string ReturnMSG_TH, out string ReturnMSG_EN)
    {
        bool ReturnOutput = false;
        ReturnMSG_TH = "";
        ReturnMSG_EN = "";

        string StoreProcedureName = "sp_checkReport_Group_Type";
        SetLog("========================START==============================");
        SetLog("[@Time][Store:" + StoreProcedureName + "]");
        SqlConnection DBConnect = GetDBConnection();
        SqlCommand DBCommand = new SqlCommand();
        DBCommand.Connection = DBConnect;
        DBCommand.CommandType = CommandType.StoredProcedure;
        DBCommand.CommandText = StoreProcedureName;

        DBCommand.Parameters.Add(newParam("@KeyID", KeyID));
        DBCommand.Parameters.Add(newParam("@Type_Grp_ID", Type_Grp_ID));

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
    public bool setReport_Group_Type(int KeyID, string Type_Grp_ID, string Type_Grp_Name_T, string Type_Grp_Name_E, string Start_Date, string End_Date, string UserCode
        , out string ReturnMSG_TH, out string ReturnMSG_EN)
    {
        bool ReturnOutput = false;
        ReturnMSG_TH = "";
        ReturnMSG_EN = "";

        string StoreProcedureName = "sp_setReport_Group_Type";
        SetLog("========================START==============================");
        SetLog("[@Time][Store:" + StoreProcedureName + "]");
        SqlConnection DBConnect = GetDBConnection();
        SqlCommand DBCommand = new SqlCommand();
        DBCommand.Connection = DBConnect;
        DBCommand.CommandType = CommandType.StoredProcedure;
        DBCommand.CommandText = StoreProcedureName;

        DBCommand.Parameters.Add(newParam("@KeyID", KeyID));
        DBCommand.Parameters.Add(newParam("@Type_Grp_ID", Type_Grp_ID));
        DBCommand.Parameters.Add(newParam("@Type_Grp_Name_T", Type_Grp_Name_T));
        DBCommand.Parameters.Add(newParam("@Type_Grp_Name_E", Type_Grp_Name_E));
        DBCommand.Parameters.Add(newParam("@Start_Date", Start_Date));
        DBCommand.Parameters.Add(newParam("@End_Date", End_Date));
        DBCommand.Parameters.Add(newParam("@USER_CODE", UserCode));

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
    public bool deleteReport_Group_Type(int KeyID, out string ReturnMSG_TH, out string ReturnMSG_EN)
    {
        bool ReturnOutput = false;
        ReturnMSG_TH = "";
        ReturnMSG_EN = "";

        string StoreProcedureName = "sp_deleteReport_Group_Type";
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
    #endregion

    #region Movement Type
    [WebMethod]
    public DataTable getMovement_Type(int PageSize, int PageIndex, string Mvt_ID, string Mvt_Name_T, string Mvt_Name_E)
    {
        string StoreProcedureName = "sp_getMovement_Type";
        SetLog("========================START==============================");
        SetLog("[@Time][Store:" + StoreProcedureName + "]");

        SqlConnection DBConnect = GetDBConnection();
        SqlCommand DBCommand = new SqlCommand();
        DBCommand.Connection = DBConnect;
        DBCommand.CommandType = CommandType.StoredProcedure;
        DBCommand.CommandText = StoreProcedureName;

        DBCommand.Parameters.Add(newParam("@PageSize", PageSize));
        DBCommand.Parameters.Add(newParam("@PageIndex", PageIndex));
        DBCommand.Parameters.Add(newParam("@Mvt_ID", Mvt_ID));
        DBCommand.Parameters.Add(newParam("@Mvt_Name_T", Mvt_Name_T));
        DBCommand.Parameters.Add(newParam("@Mvt_Name_E", Mvt_Name_E));


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
    public int getMovement_Type_Count(string Mvt_ID, string Mvt_Name_T, string Mvt_Name_E)
    {
        string StoreProcedureName = "sp_getMovement_Type_Count";
        SetLog("========================START==============================");
        SetLog("[@Time][Store:" + StoreProcedureName + "]");
        SqlConnection DBConnect = GetDBConnection();
        SqlCommand DBCommand = new SqlCommand();
        DBCommand.Connection = DBConnect;
        DBCommand.CommandType = CommandType.StoredProcedure;
        DBCommand.CommandText = StoreProcedureName;

        DBCommand.Parameters.Add(newParam("@Mvt_ID", Mvt_ID));
        DBCommand.Parameters.Add(newParam("@Mvt_Name_T", Mvt_Name_T));
        DBCommand.Parameters.Add(newParam("@Mvt_Name_E", Mvt_Name_E));

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
    public bool checkMovement_Type(int KeyID, string Mvt_ID, out string ReturnMSG_TH, out string ReturnMSG_EN)
    {
        bool ReturnOutput = false;
        ReturnMSG_TH = "";
        ReturnMSG_EN = "";

        string StoreProcedureName = "sp_checkMovement_Type";
        SetLog("========================START==============================");
        SetLog("[@Time][Store:" + StoreProcedureName + "]");
        SqlConnection DBConnect = GetDBConnection();
        SqlCommand DBCommand = new SqlCommand();
        DBCommand.Connection = DBConnect;
        DBCommand.CommandType = CommandType.StoredProcedure;
        DBCommand.CommandText = StoreProcedureName;

        DBCommand.Parameters.Add(newParam("@KeyID", KeyID));
        DBCommand.Parameters.Add(newParam("@Mvt_ID", Mvt_ID));

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
    public bool setMovement_Type(int KeyID, string Mvt_ID, string Mvt_Name_T, string Mvt_Name_E, string Mvt_Value, string Start_Date, string End_Date, string UserCode
        , out string ReturnMSG_TH, out string ReturnMSG_EN)
    {
        bool ReturnOutput = false;
        ReturnMSG_TH = "";
        ReturnMSG_EN = "";

        string StoreProcedureName = "sp_setMovement_Type";
        SetLog("========================START==============================");
        SetLog("[@Time][Store:" + StoreProcedureName + "]");
        SqlConnection DBConnect = GetDBConnection();
        SqlCommand DBCommand = new SqlCommand();
        DBCommand.Connection = DBConnect;
        DBCommand.CommandType = CommandType.StoredProcedure;
        DBCommand.CommandText = StoreProcedureName;

        DBCommand.Parameters.Add(newParam("@KeyID", KeyID));
        DBCommand.Parameters.Add(newParam("@Mvt_ID", Mvt_ID));
        DBCommand.Parameters.Add(newParam("@Mvt_Name_T", Mvt_Name_T));
        DBCommand.Parameters.Add(newParam("@Mvt_Name_E", Mvt_Name_E));
        DBCommand.Parameters.Add(newParam("@Mvt_Value", Mvt_Value));
        DBCommand.Parameters.Add(newParam("@Start_Date", Start_Date));
        DBCommand.Parameters.Add(newParam("@End_Date", End_Date));
        DBCommand.Parameters.Add(newParam("@USER_CODE", UserCode));

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
    public bool deleteMovement_Type(int KeyID, out string ReturnMSG_TH, out string ReturnMSG_EN)
    {
        bool ReturnOutput = false;
        ReturnMSG_TH = "";
        ReturnMSG_EN = "";

        string StoreProcedureName = "sp_deleteMovement_Type";
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
    #endregion

    #region Fund Type
    [WebMethod]
    public DataTable getFund_Type(int PageSize, int PageIndex, string Fund_Type_ID, string Fund_Type_Name_T, string Fund_Type_Name_E)
    {
        string StoreProcedureName = "sp_getFund_Type";
        SetLog("========================START==============================");
        SetLog("[@Time][Store:" + StoreProcedureName + "]");

        SqlConnection DBConnect = GetDBConnection();
        SqlCommand DBCommand = new SqlCommand();
        DBCommand.Connection = DBConnect;
        DBCommand.CommandType = CommandType.StoredProcedure;
        DBCommand.CommandText = StoreProcedureName;

        DBCommand.Parameters.Add(newParam("@PageSize", PageSize));
        DBCommand.Parameters.Add(newParam("@PageIndex", PageIndex));
        DBCommand.Parameters.Add(newParam("@Fund_Type_ID", Fund_Type_ID));
        DBCommand.Parameters.Add(newParam("@Fund_Type_Name_T", Fund_Type_Name_T));
        DBCommand.Parameters.Add(newParam("@Fund_Type_Name_E", Fund_Type_Name_E));


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
    public int getFund_Type_Count(string Fund_Type_ID, string Fund_Type_Name_T, string Fund_Type_Name_E)
    {
        string StoreProcedureName = "sp_getFund_Type_Count";
        SetLog("========================START==============================");
        SetLog("[@Time][Store:" + StoreProcedureName + "]");
        SqlConnection DBConnect = GetDBConnection();
        SqlCommand DBCommand = new SqlCommand();
        DBCommand.Connection = DBConnect;
        DBCommand.CommandType = CommandType.StoredProcedure;
        DBCommand.CommandText = StoreProcedureName;

        DBCommand.Parameters.Add(newParam("@Fund_Type_ID", Fund_Type_ID));
        DBCommand.Parameters.Add(newParam("@Fund_Type_Name_T", Fund_Type_Name_T));
        DBCommand.Parameters.Add(newParam("@Fund_Type_Name_E", Fund_Type_Name_E));

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
    public bool checkFund_Type(int KeyID, string Fund_Type_ID, out string ReturnMSG_TH, out string ReturnMSG_EN)
    {
        bool ReturnOutput = false;
        ReturnMSG_TH = "";
        ReturnMSG_EN = "";

        string StoreProcedureName = "sp_checkFund_Type";
        SetLog("========================START==============================");
        SetLog("[@Time][Store:" + StoreProcedureName + "]");
        SqlConnection DBConnect = GetDBConnection();
        SqlCommand DBCommand = new SqlCommand();
        DBCommand.Connection = DBConnect;
        DBCommand.CommandType = CommandType.StoredProcedure;
        DBCommand.CommandText = StoreProcedureName;

        DBCommand.Parameters.Add(newParam("@KeyID", KeyID));
        DBCommand.Parameters.Add(newParam("@Fund_Type_ID", Fund_Type_ID));

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
    public bool setFund_Type(int KeyID, string Fund_Type_ID, string Fund_Type_Name_T, string Fund_Type_Name_E, string Start_Date, string End_Date, string UserCode
        , out string ReturnMSG_TH, out string ReturnMSG_EN)
    {
        bool ReturnOutput = false;
        ReturnMSG_TH = "";
        ReturnMSG_EN = "";

        string StoreProcedureName = "sp_setFund_Type";
        SetLog("========================START==============================");
        SetLog("[@Time][Store:" + StoreProcedureName + "]");
        SqlConnection DBConnect = GetDBConnection();
        SqlCommand DBCommand = new SqlCommand();
        DBCommand.Connection = DBConnect;
        DBCommand.CommandType = CommandType.StoredProcedure;
        DBCommand.CommandText = StoreProcedureName;

        DBCommand.Parameters.Add(newParam("@KeyID", KeyID));
        DBCommand.Parameters.Add(newParam("@Fund_Type_ID", Fund_Type_ID));
        DBCommand.Parameters.Add(newParam("@Fund_Type_Name_T", Fund_Type_Name_T));
        DBCommand.Parameters.Add(newParam("@Fund_Type_Name_E", Fund_Type_Name_E));
        DBCommand.Parameters.Add(newParam("@Start_Date", Start_Date));
        DBCommand.Parameters.Add(newParam("@End_Date", End_Date));
        DBCommand.Parameters.Add(newParam("@USER_CODE", UserCode));

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
    public bool deleteFund_Type(int KeyID, out string ReturnMSG_TH, out string ReturnMSG_EN)
    {
        bool ReturnOutput = false;
        ReturnMSG_TH = "";
        ReturnMSG_EN = "";

        string StoreProcedureName = "sp_deleteFund_Type";
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
    #endregion

    #region Work Center
    [WebMethod]
    public DataTable getWork_Center(int PageSize, int PageIndex, string Loc_ID, string Loc_Name_T, string Loc_Name_E)
    {
        string StoreProcedureName = "sp_getWork_Center";
        SetLog("========================START==============================");
        SetLog("[@Time][Store:" + StoreProcedureName + "]");

        SqlConnection DBConnect = GetDBConnection();
        SqlCommand DBCommand = new SqlCommand();
        DBCommand.Connection = DBConnect;
        DBCommand.CommandType = CommandType.StoredProcedure;
        DBCommand.CommandText = StoreProcedureName;

        DBCommand.Parameters.Add(newParam("@PageSize", PageSize));
        DBCommand.Parameters.Add(newParam("@PageIndex", PageIndex));
        DBCommand.Parameters.Add(newParam("@Loc_ID", Loc_ID));
        DBCommand.Parameters.Add(newParam("@Loc_Name_T", Loc_Name_T));
        DBCommand.Parameters.Add(newParam("@Loc_Name_E", Loc_Name_E));

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
    public int getWork_Center_Count(string Loc_ID, string Loc_Name_T, string Loc_Name_E)
    {
        string StoreProcedureName = "sp_getWork_Center_Count";
        SetLog("========================START==============================");
        SetLog("[@Time][Store:" + StoreProcedureName + "]");
        SqlConnection DBConnect = GetDBConnection();
        SqlCommand DBCommand = new SqlCommand();
        DBCommand.Connection = DBConnect;
        DBCommand.CommandType = CommandType.StoredProcedure;
        DBCommand.CommandText = StoreProcedureName;

        DBCommand.Parameters.Add(newParam("@Loc_ID", Loc_ID));
        DBCommand.Parameters.Add(newParam("@Loc_Name_T", Loc_Name_T));
        DBCommand.Parameters.Add(newParam("@Loc_Name_E", Loc_Name_E));

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
    public bool checkWork_Center(int KeyID, string Loc_ID, out string ReturnMSG_TH, out string ReturnMSG_EN)
    {
        bool ReturnOutput = false;
        ReturnMSG_TH = "";
        ReturnMSG_EN = "";

        string StoreProcedureName = "sp_checkWork_Center";
        SetLog("========================START==============================");
        SetLog("[@Time][Store:" + StoreProcedureName + "]");
        SqlConnection DBConnect = GetDBConnection();
        SqlCommand DBCommand = new SqlCommand();
        DBCommand.Connection = DBConnect;
        DBCommand.CommandType = CommandType.StoredProcedure;
        DBCommand.CommandText = StoreProcedureName;

        DBCommand.Parameters.Add(newParam("@KeyID", KeyID));
        DBCommand.Parameters.Add(newParam("@Loc_ID", Loc_ID));

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
    public bool setWork_Center(int KeyID, string Loc_ID, string Loc_Name_T, string Loc_Name_E, string Loc_Address_T, string Loc_Address_E
        , string Loc_Tel, string Loc_Level, string Start_Date, string End_Date, string UserCode
        , out string ReturnMSG_TH, out string ReturnMSG_EN)
    {
        bool ReturnOutput = false;
        ReturnMSG_TH = "";
        ReturnMSG_EN = "";

        string StoreProcedureName = "sp_setWork_Center";
        SetLog("========================START==============================");
        SetLog("[@Time][Store:" + StoreProcedureName + "]");
        SqlConnection DBConnect = GetDBConnection();
        SqlCommand DBCommand = new SqlCommand();
        DBCommand.Connection = DBConnect;
        DBCommand.CommandType = CommandType.StoredProcedure;
        DBCommand.CommandText = StoreProcedureName;

        DBCommand.Parameters.Add(newParam("@KeyID", KeyID));
        DBCommand.Parameters.Add(newParam("@Loc_ID", Loc_ID));
        DBCommand.Parameters.Add(newParam("@Loc_Name_T", Loc_Name_T));
        DBCommand.Parameters.Add(newParam("@Loc_Name_E", Loc_Name_E));
        DBCommand.Parameters.Add(newParam("@Loc_Address_T", Loc_Address_T));
        DBCommand.Parameters.Add(newParam("@Loc_Address_E", Loc_Address_E));
        DBCommand.Parameters.Add(newParam("@Loc_Tel", Loc_Tel));
        DBCommand.Parameters.Add(newParam("@Loc_Level", Loc_Level));
        DBCommand.Parameters.Add(newParam("@Start_Date", Start_Date));
        DBCommand.Parameters.Add(newParam("@End_Date", End_Date));
        DBCommand.Parameters.Add(newParam("@USER_CODE", UserCode));

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
    public bool deleteWork_Center(int KeyID, out string ReturnMSG_TH, out string ReturnMSG_EN)
    {
        bool ReturnOutput = false;
        ReturnMSG_TH = "";
        ReturnMSG_EN = "";

        string StoreProcedureName = "sp_deleteWork_Center";
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
    #endregion

    #region Asset_Detail
    [WebMethod]
    public DataTable getAsset_Detail(int PageSize, int PageIndex, string Item_ID, string Loc_ID, string Asset_Type_ID, string Asset_ID, string Lang)
    {
        string StoreProcedureName = "sp_getAsset_Detail";
        SetLog("========================START==============================");
        SetLog("[@Time][Store:" + StoreProcedureName + "]");

        SqlConnection DBConnect = GetDBConnection();
        SqlCommand DBCommand = new SqlCommand();
        DBCommand.Connection = DBConnect;
        DBCommand.CommandType = CommandType.StoredProcedure;
        DBCommand.CommandText = StoreProcedureName;

        DBCommand.Parameters.Add(newParam("@PageSize", PageSize));
        DBCommand.Parameters.Add(newParam("@PageIndex", PageIndex));
        DBCommand.Parameters.Add(newParam("@Item_ID", Item_ID));
        DBCommand.Parameters.Add(newParam("@Loc_ID", Loc_ID));
        DBCommand.Parameters.Add(newParam("@Asset_Type_ID", Asset_Type_ID));
        DBCommand.Parameters.Add(newParam("@Asset_ID", Asset_ID));
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
    public int getAsset_Detail_Count(string Item_ID, string Loc_ID, string Asset_Type_ID, string Asset_ID)
    {
        string StoreProcedureName = "sp_getAsset_Detail_Count";
        SetLog("========================START==============================");
        SetLog("[@Time][Store:" + StoreProcedureName + "]");
        SqlConnection DBConnect = GetDBConnection();
        SqlCommand DBCommand = new SqlCommand();
        DBCommand.Connection = DBConnect;
        DBCommand.CommandType = CommandType.StoredProcedure;
        DBCommand.CommandText = StoreProcedureName;

        DBCommand.Parameters.Add(newParam("@Item_ID", Item_ID));
        DBCommand.Parameters.Add(newParam("@Loc_ID", Loc_ID));
        DBCommand.Parameters.Add(newParam("@Asset_Type_ID", Asset_Type_ID));
        DBCommand.Parameters.Add(newParam("@Asset_ID", Asset_ID));

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
    public bool checkAsset_Detail(int KeyID, string Item_ID, out string ReturnMSG_TH, out string ReturnMSG_EN)
    {
        bool ReturnOutput = false;
        ReturnMSG_TH = "";
        ReturnMSG_EN = "";

        string StoreProcedureName = "sp_checkAsset_Detail";
        SetLog("========================START==============================");
        SetLog("[@Time][Store:" + StoreProcedureName + "]");
        SqlConnection DBConnect = GetDBConnection();
        SqlCommand DBCommand = new SqlCommand();
        DBCommand.Connection = DBConnect;
        DBCommand.CommandType = CommandType.StoredProcedure;
        DBCommand.CommandText = StoreProcedureName;

        DBCommand.Parameters.Add(newParam("@KeyID", KeyID));
        DBCommand.Parameters.Add(newParam("@Item_ID", Item_ID));
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
    public bool setAsset_Detail(int KeyID, string Item_ID, string Loc_ID, string Asset_Type_ID, string Asset_ID
        , string Serial_No, string Asset_Loc, string Owner, string Quality, string Tranfer_Loc, string Budget_Year
        , string Document_No, string Document_Date, string Fund_Type_ID, string Standard_Price, string Price_per_unit
        , string Purchase_Doc_no, string Purchase_Doc_date, string Brand, string Serie, string Receive_from
        , string Mvt_ID, string Term_Use, string Depriciate_accru, string Depriciate_in_year, string Net_Price
        , string UserCode, out string ReturnMSG_TH, out string ReturnMSG_EN)
    {
        bool ReturnOutput = false;
        ReturnMSG_TH = "";
        ReturnMSG_EN = "";

        string StoreProcedureName = "sp_setAsset_Detail";
        SetLog("========================START==============================");
        SetLog("[@Time][Store:" + StoreProcedureName + "]");
        SqlConnection DBConnect = GetDBConnection();
        SqlCommand DBCommand = new SqlCommand();
        DBCommand.Connection = DBConnect;
        DBCommand.CommandType = CommandType.StoredProcedure;
        DBCommand.CommandText = StoreProcedureName;

        DBCommand.Parameters.Add(newParam("@KeyID", KeyID));
        DBCommand.Parameters.Add(newParam("@Item_ID", Item_ID));
        DBCommand.Parameters.Add(newParam("@Loc_ID", Loc_ID));
        DBCommand.Parameters.Add(newParam("@Asset_Type_ID", Asset_Type_ID));
        DBCommand.Parameters.Add(newParam("@Asset_ID", Asset_ID));
        DBCommand.Parameters.Add(newParam("@Serial_No", Serial_No));
        DBCommand.Parameters.Add(newParam("@Asset_Loc", Asset_Loc));
        DBCommand.Parameters.Add(newParam("@Owner", Owner));
        DBCommand.Parameters.Add(newParam("@Quality", Quality));
        DBCommand.Parameters.Add(newParam("@Tranfer_Loc", Tranfer_Loc));
        DBCommand.Parameters.Add(newParam("@Budget_Year", Budget_Year));
        DBCommand.Parameters.Add(newParam("@Document_No", Document_No));
        DBCommand.Parameters.Add(newParam("@Document_Date", Document_Date));
        DBCommand.Parameters.Add(newParam("@Fund_Type_ID", Fund_Type_ID));
        DBCommand.Parameters.Add(newParam("@Standard_Price", Standard_Price));
        DBCommand.Parameters.Add(newParam("@Price_per_unit", Price_per_unit));
        DBCommand.Parameters.Add(newParam("@Purchase_Doc_no", Purchase_Doc_no));
        DBCommand.Parameters.Add(newParam("@Purchase_Doc_date", Purchase_Doc_date));
        DBCommand.Parameters.Add(newParam("@Brand", Brand));
        DBCommand.Parameters.Add(newParam("@Serie", Serie));
        DBCommand.Parameters.Add(newParam("@Receive_from", Receive_from));
        DBCommand.Parameters.Add(newParam("@Mvt_ID", Mvt_ID));
        DBCommand.Parameters.Add(newParam("@Term_Use", Term_Use));
        DBCommand.Parameters.Add(newParam("@Depriciate_accru", Depriciate_accru));
        DBCommand.Parameters.Add(newParam("@Depriciate_in_year", Depriciate_in_year));
        DBCommand.Parameters.Add(newParam("@Net_Price", Net_Price));
        DBCommand.Parameters.Add(newParam("@USER_CODE", UserCode));

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
    public bool deleteAsset_Detail(int KeyID, out string ReturnMSG_TH, out string ReturnMSG_EN)
    {
        bool ReturnOutput = false;
        ReturnMSG_TH = "";
        ReturnMSG_EN = "";

        string StoreProcedureName = "sp_deleteAsset_Detail";
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
    #endregion

    #region Asset_Quota
    [WebMethod]
    public DataTable getAsset_Quota(int PageSize, int PageIndex, string Asset_ID, string Loc_ID, string Lang)
    {
        string StoreProcedureName = "sp_getAsset_Quota";
        SetLog("========================START==============================");
        SetLog("[@Time][Store:" + StoreProcedureName + "]");

        SqlConnection DBConnect = GetDBConnection();
        SqlCommand DBCommand = new SqlCommand();
        DBCommand.Connection = DBConnect;
        DBCommand.CommandType = CommandType.StoredProcedure;
        DBCommand.CommandText = StoreProcedureName;

        DBCommand.Parameters.Add(newParam("@PageSize", PageSize));
        DBCommand.Parameters.Add(newParam("@PageIndex", PageIndex));
        DBCommand.Parameters.Add(newParam("@Asset_ID", Asset_ID));
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
    public int getAsset_Quota_Count(string Asset_ID, string Loc_ID)
    {
        string StoreProcedureName = "sp_getAsset_Quota_Count";
        SetLog("========================START==============================");
        SetLog("[@Time][Store:" + StoreProcedureName + "]");
        SqlConnection DBConnect = GetDBConnection();
        SqlCommand DBCommand = new SqlCommand();
        DBCommand.Connection = DBConnect;
        DBCommand.CommandType = CommandType.StoredProcedure;
        DBCommand.CommandText = StoreProcedureName;

        DBCommand.Parameters.Add(newParam("@Asset_ID", Asset_ID));
        DBCommand.Parameters.Add(newParam("@Loc_ID", Loc_ID));

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
    public bool checkAsset_Quota(int KeyID, string Asset_ID, string Loc_ID, out string ReturnMSG_TH, out string ReturnMSG_EN)
    {
        bool ReturnOutput = false;
        ReturnMSG_TH = "";
        ReturnMSG_EN = "";

        string StoreProcedureName = "sp_checkAsset_Quota";
        SetLog("========================START==============================");
        SetLog("[@Time][Store:" + StoreProcedureName + "]");
        SqlConnection DBConnect = GetDBConnection();
        SqlCommand DBCommand = new SqlCommand();
        DBCommand.Connection = DBConnect;
        DBCommand.CommandType = CommandType.StoredProcedure;
        DBCommand.CommandText = StoreProcedureName;

        DBCommand.Parameters.Add(newParam("@KeyID", KeyID));
        DBCommand.Parameters.Add(newParam("@Asset_ID", Asset_ID));
        DBCommand.Parameters.Add(newParam("@Loc_ID", Loc_ID));
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
    public bool setAsset_Quota(int KeyID, string Asset_ID, string Loc_ID, string Quota_Qty, string Stock_Qty
        , string Start_Date, string End_Date, string UserCode
        , out string ReturnMSG_TH, out string ReturnMSG_EN)
    {
        bool ReturnOutput = false;
        ReturnMSG_TH = "";
        ReturnMSG_EN = "";

        string StoreProcedureName = "sp_setAsset_Quota";
        SetLog("========================START==============================");
        SetLog("[@Time][Store:" + StoreProcedureName + "]");
        SqlConnection DBConnect = GetDBConnection();
        SqlCommand DBCommand = new SqlCommand();
        DBCommand.Connection = DBConnect;
        DBCommand.CommandType = CommandType.StoredProcedure;
        DBCommand.CommandText = StoreProcedureName;

        DBCommand.Parameters.Add(newParam("@KeyID", KeyID));
        DBCommand.Parameters.Add(newParam("@Asset_ID", Asset_ID));
        DBCommand.Parameters.Add(newParam("@Quota_Qty", Quota_Qty));
        DBCommand.Parameters.Add(newParam("@Stock_Qty", Stock_Qty));
        DBCommand.Parameters.Add(newParam("@Loc_ID", Loc_ID));
        DBCommand.Parameters.Add(newParam("@Start_Date", Start_Date));
        DBCommand.Parameters.Add(newParam("@End_Date", End_Date));
        DBCommand.Parameters.Add(newParam("@USER_CODE", UserCode));

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
    public bool deleteAsset_Quota(int KeyID, out string ReturnMSG_TH, out string ReturnMSG_EN)
    {
        bool ReturnOutput = false;
        ReturnMSG_TH = "";
        ReturnMSG_EN = "";

        string StoreProcedureName = "sp_deleteAsset_Quota";
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
    #endregion

    #region Asset_Depreciate
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
    #endregion

    #region Asset Type
    [WebMethod]
    public DataTable getAsset_Type(int PageSize, int PageIndex, string Asset_Type_ID, string Asset_Type_Name_T, string Asset_Type_Name_E)
    {
        string StoreProcedureName = "sp_getAsset_Type";
        SetLog("========================START==============================");
        SetLog("[@Time][Store:" + StoreProcedureName + "]");

        SqlConnection DBConnect = GetDBConnection();
        SqlCommand DBCommand = new SqlCommand();
        DBCommand.Connection = DBConnect;
        DBCommand.CommandType = CommandType.StoredProcedure;
        DBCommand.CommandText = StoreProcedureName;

        DBCommand.Parameters.Add(newParam("@PageSize", PageSize));
        DBCommand.Parameters.Add(newParam("@PageIndex", PageIndex));
        DBCommand.Parameters.Add(newParam("@Asset_Type_ID", Asset_Type_ID));
        DBCommand.Parameters.Add(newParam("@Asset_Type_Name_T", Asset_Type_Name_T));
        DBCommand.Parameters.Add(newParam("@Asset_Type_Name_E", Asset_Type_Name_E));


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
    public int getAsset_Type_Count(string Asset_Type_ID, string Asset_Type_Name_T, string Asset_Type_Name_E)
    {
        string StoreProcedureName = "sp_getAsset_Type_Count";
        SetLog("========================START==============================");
        SetLog("[@Time][Store:" + StoreProcedureName + "]");
        SqlConnection DBConnect = GetDBConnection();
        SqlCommand DBCommand = new SqlCommand();
        DBCommand.Connection = DBConnect;
        DBCommand.CommandType = CommandType.StoredProcedure;
        DBCommand.CommandText = StoreProcedureName;

        DBCommand.Parameters.Add(newParam("@Asset_Type_ID", Asset_Type_ID));
        DBCommand.Parameters.Add(newParam("@Asset_Type_Name_T", Asset_Type_Name_T));
        DBCommand.Parameters.Add(newParam("@Asset_Type_Name_E", Asset_Type_Name_E));

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
    public bool checkAsset_Type(int KeyID, string Asset_Type_ID, out string ReturnMSG_TH, out string ReturnMSG_EN)
    {
        bool ReturnOutput = false;
        ReturnMSG_TH = "";
        ReturnMSG_EN = "";

        string StoreProcedureName = "sp_checkAsset_Type";
        SetLog("========================START==============================");
        SetLog("[@Time][Store:" + StoreProcedureName + "]");
        SqlConnection DBConnect = GetDBConnection();
        SqlCommand DBCommand = new SqlCommand();
        DBCommand.Connection = DBConnect;
        DBCommand.CommandType = CommandType.StoredProcedure;
        DBCommand.CommandText = StoreProcedureName;

        DBCommand.Parameters.Add(newParam("@KeyID", KeyID));
        DBCommand.Parameters.Add(newParam("@Asset_Type_ID", Asset_Type_ID));

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
    public bool setAsset_Type(int KeyID, string Asset_Type_ID, string Asset_Type_Name_T, string Asset_Type_Name_E, string Start_Date, string End_Date, string UserCode
        , out string ReturnMSG_TH, out string ReturnMSG_EN)
    {
        bool ReturnOutput = false;
        ReturnMSG_TH = "";
        ReturnMSG_EN = "";

        string StoreProcedureName = "sp_setAsset_Type";
        SetLog("========================START==============================");
        SetLog("[@Time][Store:" + StoreProcedureName + "]");
        SqlConnection DBConnect = GetDBConnection();
        SqlCommand DBCommand = new SqlCommand();
        DBCommand.Connection = DBConnect;
        DBCommand.CommandType = CommandType.StoredProcedure;
        DBCommand.CommandText = StoreProcedureName;

        DBCommand.Parameters.Add(newParam("@KeyID", KeyID));
        DBCommand.Parameters.Add(newParam("@Asset_Type_ID", Asset_Type_ID));
        DBCommand.Parameters.Add(newParam("@Asset_Type_Name_T", Asset_Type_Name_T));
        DBCommand.Parameters.Add(newParam("@Asset_Type_Name_E", Asset_Type_Name_E));
        DBCommand.Parameters.Add(newParam("@Start_Date", Start_Date));
        DBCommand.Parameters.Add(newParam("@End_Date", End_Date));
        DBCommand.Parameters.Add(newParam("@USER_CODE", UserCode));

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
    public bool deleteAsset_Type(int KeyID, out string ReturnMSG_TH, out string ReturnMSG_EN)
    {
        bool ReturnOutput = false;
        ReturnMSG_TH = "";
        ReturnMSG_EN = "";

        string StoreProcedureName = "sp_deleteAsset_Type";
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
    #endregion

    #region Asset
    [WebMethod]
    public DataTable getAsset(int PageSize, int PageIndex, string Asset_ID, string Asset_Name_T, string Asset_Name_E, string Asset_Type_ID, string Lang)
    {
        string StoreProcedureName = "sp_getAsset";
        SetLog("========================START==============================");
        SetLog("[@Time][Store:" + StoreProcedureName + "]");

        SqlConnection DBConnect = GetDBConnection();
        SqlCommand DBCommand = new SqlCommand();
        DBCommand.Connection = DBConnect;
        DBCommand.CommandType = CommandType.StoredProcedure;
        DBCommand.CommandText = StoreProcedureName;

        DBCommand.Parameters.Add(newParam("@PageSize", PageSize));
        DBCommand.Parameters.Add(newParam("@PageIndex", PageIndex));
        DBCommand.Parameters.Add(newParam("@Asset_ID", Asset_ID));
        DBCommand.Parameters.Add(newParam("@Asset_Name_T", Asset_Name_T));
        DBCommand.Parameters.Add(newParam("@Asset_Name_E", Asset_Name_E));
        DBCommand.Parameters.Add(newParam("@Asset_Type_ID", Asset_Type_ID));
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
    public int getAsset_Count(string Asset_ID, string Asset_Name_T, string Asset_Name_E, string Asset_Type_ID)
    {
        string StoreProcedureName = "sp_getAsset_Count";
        SetLog("========================START==============================");
        SetLog("[@Time][Store:" + StoreProcedureName + "]");
        SqlConnection DBConnect = GetDBConnection();
        SqlCommand DBCommand = new SqlCommand();
        DBCommand.Connection = DBConnect;
        DBCommand.CommandType = CommandType.StoredProcedure;
        DBCommand.CommandText = StoreProcedureName;

        DBCommand.Parameters.Add(newParam("@Asset_ID", Asset_ID));
        DBCommand.Parameters.Add(newParam("@Asset_Name_T", Asset_Name_T));
        DBCommand.Parameters.Add(newParam("@Asset_Name_E", Asset_Name_E));
        DBCommand.Parameters.Add(newParam("@Asset_Type_ID", Asset_Type_ID));

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
    public bool checkAsset(int KeyID, string Asset_ID, out string ReturnMSG_TH, out string ReturnMSG_EN)
    {
        bool ReturnOutput = false;
        ReturnMSG_TH = "";
        ReturnMSG_EN = "";

        string StoreProcedureName = "sp_checkAsset";
        SetLog("========================START==============================");
        SetLog("[@Time][Store:" + StoreProcedureName + "]");
        SqlConnection DBConnect = GetDBConnection();
        SqlCommand DBCommand = new SqlCommand();
        DBCommand.Connection = DBConnect;
        DBCommand.CommandType = CommandType.StoredProcedure;
        DBCommand.CommandText = StoreProcedureName;

        DBCommand.Parameters.Add(newParam("@KeyID", KeyID));
        DBCommand.Parameters.Add(newParam("@Asset_ID", Asset_ID));

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
    public bool setAsset(int KeyID, string Asset_ID, string Asset_Name1_T, string Asset_Name1_E, string Asset_Name2_T, string Asset_Name2_E
        , string Asset_Type_ID, string Asset_Ref_No, string Unit_Name
        , string Start_Date, string End_Date, string UserCode
        , out string ReturnMSG_TH, out string ReturnMSG_EN)
    {
        bool ReturnOutput = false;
        ReturnMSG_TH = "";
        ReturnMSG_EN = "";

        string StoreProcedureName = "sp_setAsset";
        SetLog("========================START==============================");
        SetLog("[@Time][Store:" + StoreProcedureName + "]");
        SqlConnection DBConnect = GetDBConnection();
        SqlCommand DBCommand = new SqlCommand();
        DBCommand.Connection = DBConnect;
        DBCommand.CommandType = CommandType.StoredProcedure;
        DBCommand.CommandText = StoreProcedureName;

        DBCommand.Parameters.Add(newParam("@KeyID", KeyID));
        DBCommand.Parameters.Add(newParam("@Asset_ID", Asset_ID));
        DBCommand.Parameters.Add(newParam("@Asset_Name1_T", Asset_Name1_T));
        DBCommand.Parameters.Add(newParam("@Asset_Name1_E", Asset_Name1_E));
        DBCommand.Parameters.Add(newParam("@Asset_Name2_T", Asset_Name2_T));
        DBCommand.Parameters.Add(newParam("@Asset_Name2_E", Asset_Name2_E));
        DBCommand.Parameters.Add(newParam("@Asset_Type_ID", Asset_Type_ID));
        DBCommand.Parameters.Add(newParam("@Unit_Name", Unit_Name));
        DBCommand.Parameters.Add(newParam("@Asset_Ref_No", Asset_Ref_No));
        DBCommand.Parameters.Add(newParam("@Start_Date", Start_Date));
        DBCommand.Parameters.Add(newParam("@End_Date", End_Date));
        DBCommand.Parameters.Add(newParam("@USER_CODE", UserCode));

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
    public bool deleteAsset(int KeyID, out string ReturnMSG_TH, out string ReturnMSG_EN)
    {
        bool ReturnOutput = false;
        ReturnMSG_TH = "";
        ReturnMSG_EN = "";

        string StoreProcedureName = "sp_deleteAsset";
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
    #endregion

}
