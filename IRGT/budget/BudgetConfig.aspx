<%@ Page ClientTarget="Uplevel" Title="" Language="C#" MasterPageFile="~/master_page/main.Master" AutoEventWireup="true" CodeFile="BudgetConfig.aspx.cs" Inherits="BudgetConfig" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="main-content">
		<div class="main-content-inner">
			<!-- #section:basics/content.breadcrumbs -->
			<div class="breadcrumbs ace-save-state" id="breadcrumbs">
				<ul class="breadcrumb">          
                    <li>
						<i class="ace-icon fa fa-cog home-icon"></i>
						<a href="#"><%=Session["HeaderGroup"]%></a>
					</li>	
                    <li><a href="#"><%=Session["HeaderSubGroup"]%></a></li>			
					<li class="active"><%=Session["HeaderCurrent"]%></li>  
				</ul><!-- /.breadcrumb -->
				<!-- /section:basics/content.searchbox -->
			</div>
			<!-- /section:basics/content.breadcrumbs -->
			<div class="page-content">
				<!-- /section:settings.box -->
				<div class="row">
					<div class="col-xs-12" >
						<!-- PAGE CONTENT BEGINS -->
                        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="row">
									<div class="col-xs-12">
										<h3 class="header smaller lighter blue"><%=Session["HeaderText"]%></h3>		
										<!-- div.table-responsive -->
                                        <center>
                                            <table ng-app="myApp" ng-controller="fnMain">
                                                <tr>
                                                    <td>หน่วยงาน</td>
                                                    <td style="width:5px"></td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlLoc" runat="server" CssClass="chosen-select form-control" Width="250px"></asp:DropDownList>
                                                    </td>
                                                    <td style="width:5px"></td>
                                                    <td><asp:Button ID="btnSave" runat="server" Text="กำหนดค่า" OnClick="btnSave_Click"></asp:Button>
                                                        
                                                    </td>
                                                </tr>
                                            </table>
                                        </center>
									</div>
								</div>
                                
                            </ContentTemplate>
                        </asp:UpdatePanel> 
								
						<!-- PAGE CONTENT ENDS -->
					</div><!-- /.col -->
				</div><!-- /.row -->
			</div><!-- /.page-content -->
		</div>
	</div><!-- /.main-content -->
    
</asp:Content>

