using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.Data.SqlClient;
using System.Globalization;

/// <summary>
/// Summary description for DestroyXmlParams
/// </summary>
public class DetroyXmlParams
{
    XmlDocument xmldoc = new XmlDocument();
    public DetroyXmlParams(string xmlParams)
    {
        if (xmlParams == "" || xmlParams == "") return;
        xmldoc.LoadXml(xmlParams);
    }

    public SqlParameterCollection GetParams(SqlCommand comm)
    {
        SqlParameterCollection sqlParams = comm.Parameters;
        if (xmldoc.OuterXml == string.Empty || xmldoc.OuterXml == "") return null;
        XmlNode root = xmldoc.FirstChild;
        foreach (XmlNode paramNode in root.ChildNodes)
        {
            SqlParameter param = new SqlParameter();
            bool isBinary = false;

            foreach (XmlNode itemNode in paramNode.ChildNodes)
            {
                string name = itemNode.Attributes.GetNamedItem("Name").Value;
                string itemVal = itemNode.Attributes.GetNamedItem("Value").Value;
                switch (name)
                {
                    case "ParameterName":
                        param.ParameterName = itemVal;
                        break;
                    case "SqlDbType":
                        {

                            param.SqlDbType = GetParamType(itemVal);
                            if (param.SqlDbType == SqlDbType.VarBinary)
                            {
                                isBinary = true;
                            }
                        }
                        break;
                    case "Direction":
                        param.Direction = GetDirection(itemVal);
                        break;
                    case "Precision":
                        if (itemVal != "")
                        {
                            param.Precision = Convert.ToByte(itemVal);
                        }
                        break;
                    case "Value":
                        {
                            if (param.SqlDbType == SqlDbType.DateTime)
                            {
                                param.SqlDbType = SqlDbType.VarChar;
                                //param.SqlValue = itemVal;//DateTime.Parse(GetParamValue(param.SqlDbType, itemVal).ToString());
                                param.Value = itemVal;//DateTime.Parse(GetParamValue(param.SqlDbType, itemVal).ToString());
                            }
                            else
                                param.Value = GetParamValue(param.SqlDbType, itemVal);

                        }

                        break;
                }
            }
            comm.Parameters.Add(param);
        }
        return sqlParams;
    }


    private object GetParamValue(SqlDbType dbType, string val)
    {
        switch (dbType)
        {
            case SqlDbType.VarBinary:
                {
                    return (Object)System.Text.Encoding.UTF8.GetBytes(val);
                }
                break;
            case SqlDbType.DateTime:
                {
                    //Modify by ngo
                    string values = val;//SplitDate(val);
                    //
                    //return (Object)GetDate(DateTime.Parse(val));
                    return (Object)GetDate(DateTime.Parse(values,new CultureInfo("fr-FR")));
                }
                break;
            default: return (Object)val;
                break;

        }
    }

    private string SplitDate(string date)
    {
        //Split
        string[] d = date.Split(new char[] { '/' });
        //
        //Merge String
        string dy = d[0];
        string mn = d[1];
        string yy = d[2].Substring(0, 4);
        //if (int.Parse(d[0]) > 12)
        //{
        //    dy = d[0];
        //    mn = d[1];
        //    yy = d[2].Substring(0, 4);
        //}
        //else if (int.Parse(d[0]) < 12)
        //{
        //    mn = d[0];
        //    dy = d[1];
        //    yy = d[2].Substring(0,4);
        //}
        //else
        //{
        //    yy = d[0];
        //    mn = d[1];
        //    dy = d[2].Substring(0, 4);
        //}
        string dates = mn + "/" + dy + "/" + yy;
        //
        return dates;
    }

    private SqlDbType GetParamType(string typeId)
    {
        SqlDbType type = (SqlDbType)Convert.ToSingle(typeId);
        return type;
    }


    private string GetDate(DateTime dt)
    {
        //System.Globalization.CultureInfo info = System.Threading.Thread.CurrentThread.CurrentCulture;
        //System.Globalization.CultureInfo newInfo = new System.Globalization.CultureInfo("en-US");
        string s = dt.ToString("dd/MM/yyyy"); //Edit by ngo
        // System.Threading.Thread.CurrentThread.CurrentCulture = info;
        return s;
    }


    private ParameterDirection GetDirection(string direction)
    {
        ParameterDirection pDirect = ParameterDirection.Input;
        switch (direction)
        {
            case "Input": { pDirect = ParameterDirection.Input; } break;
            case "InputOutput": { pDirect = ParameterDirection.InputOutput; } break;
            case "Output": { pDirect = ParameterDirection.Output; } break;
            case "ReturnValue": { pDirect = ParameterDirection.ReturnValue; } break;
        }
        return pDirect;
    }

}
