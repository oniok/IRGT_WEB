using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class master_page_main : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string UserCode = cCommon.getUserName(Session);
        if (UserCode == "")
        {
            Response.Redirect("../page/login.html");
            return;
        }

        labUserLogin.Text = UserCode;

        XmlDocument xmlMenu = new XmlDocument();
        xmlMenu.Load(Server.MapPath("../config/Menu.xml"));

        string MenuText = "Menu";
        string PageID = Request.QueryString["PageID"];
        if (PageID == null) PageID = "XXXXXXXXXXXXXXXX";
        if (PageID == "") PageID = "XXXXXXXXXXXXXXXX";
      

        string LANG = cCommon.getLanguage(Request);
        

        if (LANG == cCommon.Language_Thai)
        {
            Session["text_placeholder"] = "โปรดระบุ";
            Session["search_placeholder"] = "ทั้งหมด";
            Session["text_search"] = "ค้นหา";
        }
        else
        {
            Session["text_placeholder"] = "please specify";
            Session["search_placeholder"] = "All";
            Session["text_search"] = "Search";
        }



        string SMenu = "";
        XmlNodeList listMenu = xmlMenu.SelectNodes("//Menus/Menu");
        for (int i = 0; i < listMenu.Count; i++)
        {
            string ID = listMenu[i].Attributes["ID"].Value;
            string Name = listMenu[i].Attributes["Name"].Value;
            string TextTH = listMenu[i].Attributes["TextTH"].Value;
            string TextEN = listMenu[i].Attributes["TextEN"].Value;

                string Text = "";
            if (LANG.Trim().ToUpper() == cCommon.Language_Thai)
            {
                MenuText = "เมนูระบบ";
                Text = TextTH;
            }
            else
            {
                MenuText = "Menu";
                Text = TextEN;
            }

            string li_class = "";
           
            if (PageID == ID)
                li_class = "active";
            if (PageID.Substring(0, ID.Length) == ID)
                li_class += " open";

            XmlNodeList listSubMenu = listMenu[i].SelectNodes("./Menu");
            string Url = "";
            if (listSubMenu.Count == 0)
                Url = listSubMenu[i].Attributes["Url"].Value + "?PageID=" + ID + "&lang=" + LANG;

            SMenu += "<li class='" + li_class + "'>";
            if (listSubMenu.Count > 0)
                SMenu += "<a href='" + Url + "' class='dropdown-toggle'>";
            else
                SMenu += "<a href='" + Url + "'>";
           
            SMenu += "<i class='menu-icon fa fa-cog'></i>";
            SMenu += "<span class='menu-text'>" + Text + "</span>";
            if (listSubMenu.Count > 0)
                SMenu += "<b class='arrow fa fa-angle-down'></b>";
            SMenu += "</a>";
            if (listSubMenu.Count > 0)
            {
                 SMenu += "<b class='arrow'></b>";
                 SMenu += "<ul class='submenu'>";
                 for (int j = 0; j < listSubMenu.Count; j++)
                 {
                     string subID = listSubMenu[j].Attributes["ID"].Value;
                     string subName = listSubMenu[j].Attributes["Name"].Value;
                     string subTextTH = listSubMenu[j].Attributes["TextTH"].Value;
                     string subTextEN = listSubMenu[j].Attributes["TextEN"].Value;
                     string subText = "";
                     if (LANG.Trim().ToUpper() == "TH")
                         subText = subTextTH;
                     else
                         subText = subTextEN;

                    //string RoleShow = listMenu[i].Attributes["RoleShow"].Value;
                    //string RoleHide = listMenu[i].Attributes["RoleHide"].Value;

                    string RoleShow = (listMenu[i].ChildNodes[j]).Attributes["RoleShow"].Value;
                    //if RoleShow <> All find Loc_ID
                    if (RoleShow != "All")
                    {
                        if (RoleShow.Contains("-"))
                        {
                            //if RoleShow match with Loc_ID
                            if (RoleShow.Contains(((System.Data.DataTable)(Session["Data_User"])).Rows[0][8].ToString()))
                            {
                                //hide li
                                continue;
                            }

                        }
                        else
                        {
                            if (!RoleShow.Contains(((System.Data.DataTable)(Session["Data_User"])).Rows[0][8].ToString()))
                            {
                                //hide li
                                continue;
                            }
                        }
                    }

                    li_class = "";
                     if (PageID == ID + "-" + subID)
                         li_class = "active";
                     if(PageID.Substring(0, (ID + "-" + subID).Length) == ID + "-" + subID)
                         li_class += " open";


                     XmlNodeList listSubSubMenu = listSubMenu[j].SelectNodes("./Menu");
                     string subUrl = "";
                     if (listSubSubMenu.Count == 0)
                         subUrl = listSubSubMenu[j].Attributes["Url"].Value + "?PageID=" + ID + "-" + subID + "&lang=" + LANG;

                     SMenu += "<li class='" + li_class + "'>";
                     if (listSubSubMenu.Count > 0)
                         SMenu += "<a href='" + subUrl + "' class='dropdown-toggle'>";
                     else
                         SMenu += "<a href='" + subUrl + "'>";

                     SMenu += "<i class='menu-icon fa fa-caret-right'></i>";
                     SMenu += subText;
                     if (listSubSubMenu.Count > 0)
                         SMenu += "<b class='arrow fa fa-angle-down'></b>";
					 SMenu += "</a>";
                     if (listSubSubMenu.Count > 0)
                     {
                         SMenu += "<b class='arrow'></b>";
                         SMenu += "<ul class='submenu'>";


                         for (int k = 0; k < listSubSubMenu.Count; k++)
                         {
                             string sub_subID = listSubSubMenu[k].Attributes["ID"].Value;
                             string sub_subName = listSubSubMenu[k].Attributes["Name"].Value;
                             string sub_subTextTH = listSubSubMenu[k].Attributes["TextTH"].Value;
                             string sub_subTextEN = listSubSubMenu[k].Attributes["TextEN"].Value;
                             string sub_subUrl = listSubSubMenu[k].Attributes["Url"].Value + "?PageID=" + ID + "-" + subID + "-" + sub_subID + "&lang=" + LANG;
                             string sub_subText = "";
                             if (LANG.Trim().ToUpper() == "TH")
                                 sub_subText = sub_subTextTH;
                             else
                                 sub_subText = sub_subTextEN;

                             li_class = "";
                             if (PageID == ID + "-" + subID + "-" + sub_subID)
                                 li_class = "active open";

                            /////////////////////
                            XmlNodeList listSubSubSubMenu = listSubSubMenu[j].SelectNodes("./Menu");

                            if (listSubSubSubMenu.Count > 0)
                            {
                                string subsubID = listSubMenu[j].Attributes["ID"].Value;
                                string subsubName = listSubMenu[j].Attributes["Name"].Value;
                                string subsubTextTH = listSubMenu[j].Attributes["TextTH"].Value;
                                string subsubTextEN = listSubMenu[j].Attributes["TextEN"].Value;
                                string subsubText = "";
                                if (LANG.Trim().ToUpper() == "TH")
                                    subsubText = subsubTextTH;
                                else
                                    subsubText = subsubTextEN;

                                string SubSubUrl = "";
                                if (listSubSubSubMenu.Count == 0)
                                    SubSubUrl = listSubSubSubMenu[j].Attributes["Url"].Value + "?PageID=" + ID + "-" + subsubID + "&lang=" + LANG;

                                SMenu += "<li class='" + li_class + "'>";
                                if (listSubSubSubMenu.Count > 0)
                                    SMenu += "<a href='" + SubSubUrl + "' class='dropdown-toggle'>";
                                else
                                    SMenu += "<a href='" + SubSubUrl + "'>";

                                SMenu += "<i class='menu-icon fa fa-caret-right'></i>";
                                SMenu += subsubText;
                                if (listSubSubSubMenu.Count > 0)
                                    SMenu += "<b class='arrow fa fa-angle-down'></b>";
                                SMenu += "</a>";
                                if (listSubSubSubMenu.Count > 0)
                                {
                                    SMenu += "<b class='arrow'></b>";
                                    SMenu += "<ul class='submenu'>";


                                    for (int l = 0; l < listSubSubSubMenu.Count; l++)
                                    {
                                        string sub_subsubID = listSubSubSubMenu[l].Attributes["ID"].Value;
                                        string sub_subsubName = listSubSubSubMenu[l].Attributes["Name"].Value;
                                        string sub_subsubTextTH = listSubSubSubMenu[l].Attributes["TextTH"].Value;
                                        string sub_subsubTextEN = listSubSubSubMenu[l].Attributes["TextEN"].Value;
                                        string sub_subsubUrl = listSubSubSubMenu[l].Attributes["Url"].Value + "?PageID=" + ID + "-" + subsubID + "-" + sub_subsubID + "&lang=" + LANG;
                                        string sub_subsubText = "";
                                        if (LANG.Trim().ToUpper() == "TH")
                                            sub_subsubText = sub_subsubTextTH;
                                        else
                                            sub_subsubText = sub_subsubTextEN;

                                        li_class = "";
                                        if (PageID == ID + "-" + subsubID + "-" + sub_subsubID)
                                            li_class = "active open";

                                        SMenu += "<li class='" + li_class + "'>";
                                        SMenu += "<a href='" + sub_subsubUrl + "'>";
                                        SMenu += "<i class='menu-icon fa fa-caret-right'></i>";
                                        SMenu += sub_subsubText;
                                        SMenu += "</a>";
                                        SMenu += "<b class='arrow'></b>";
                                        SMenu += "</li>";

                                    }
                                    SMenu += "</ul>";

                                }

                                SMenu += "</li>";
                            }
                            
                            ////////////////

                            SMenu += "<li class='" + li_class + "'>";
                             SMenu += "<a href='" + sub_subUrl + "'>";
                             SMenu += "<i class='menu-icon fa fa-caret-right'></i>";
                             SMenu += sub_subText;
                             SMenu += "</a>";
                             SMenu += "<b class='arrow'></b>";
                             SMenu += "</li>";

                         }
                         SMenu += "</ul>";
                        
                     }

					 SMenu += "</li>";
                 }
                 SMenu += "</ul>";
            }


            SMenu += "</li>";
        }

        


        labMenu.Text = @"<ul class='nav nav-list'>
                <li class='green'>
					<a href='#'>
						<i class='menu-icon fa fa-list'></i>
						<span class='menu-text'>" + MenuText + @"</span>
					</a>						
				</li>
				" + SMenu + "</ul>";

    }
}
