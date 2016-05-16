using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Common
/// </summary>
public static class cCommon
{
   

    public static string Language_Thai = "TH";
    public static string Language_Eng = "EN";
    public static string getLanguage(HttpRequest Request)
    {
        string LANG = Request.QueryString["lang"];
        if (LANG == "" || LANG == null)
            LANG = Language_Thai;

        return LANG;
    }

    public static string getUserName(System.Web.SessionState.HttpSessionState Session)
    {
        return "" + Session["profile_usercode"];
    }
    public static void setUserName(System.Web.SessionState.HttpSessionState Session, string User_Code)
    {
        Session["profile_usercode"] = User_Code;
    }

    public static int Convert_Str_To_Int(string str)
    {
        try
        {
            return int.Parse(str);
        }
        catch
        {
            return 0;
        }
    }

}