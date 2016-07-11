using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class server_FileUpload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string FileName = Request.Files[0].FileName;
            string FileType = Request.Files[0].ContentType;

            Request.Files[0].SaveAs(Server.MapPath("../upload/budget_project/" + FileName));
        }
        catch (Exception ex){ }
    }
}