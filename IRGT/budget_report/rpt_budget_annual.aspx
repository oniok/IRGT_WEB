<%@ Page Title="" Language="C#" MasterPageFile="~/master_page/main.Master" AutoEventWireup="true" CodeFile="rpt_budget_annual.aspx.cs" Inherits="rpt_budget_annual" %>



<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

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
                                                        <asp:DropDownList ID="ddlLoc" runat="server" CssClass="chosen-select form-control" Width="300px"></asp:DropDownList>
                                                    </td>
                                                    <td style="width:5px"></td>
                                                    <td>ประเภทประมาณการ</td>
                                                    <td style="width:5px"></td>
                                                    <td><asp:DropDownList ID="ddlType" runat="server" CssClass="chosen-select form-control" Width="150px"></asp:DropDownList>
                                                    </td>
                                                    <td style="width:5px"></td>
                                                    <td>ปีงบประมาณ</td>
                                                    <td style="width:5px"></td>
                                                    <td><asp:DropDownList ID="ddlYear" runat="server" CssClass="chosen-select form-control" Width="100px"></asp:DropDownList>
                                                    </td>
                                                    <td style="width:5px"></td>
                                                    <td><asp:Button ID="btnSearch" runat="server" Text="เริ่มแสดงรายงาน" OnClick="btnSearch_Click"></asp:Button>
                                                        
                                                    </td>
                                                </tr>
                                            </table>
                                        </center>
										<!-- div.dataTables_borderWrap -->
										<div class="widget-box  widget-color-blue2">                                            
                                            <div class="widget-body">
                                                <div class="widget-main no-padding">                                                        
                                                      <CR:CrystalReportViewer ID="crv_BudgetAnnual" runat="server" AutoDataBind="True" GroupTreeImagesFolderUrl="" Height="1202px" ToolbarImagesFolderUrl="" ToolPanelWidth="300px" Width="903px" ReuseParameterValuesOnRefresh="True" ToolPanelView="None" HasCrystalLogo="False" HasDrilldownTabs="False" HasToggleGroupTreeButton="False" HasToggleParameterPanelButton="False" HasZoomFactorList="False" PageZoomFactor="125" />
                                                      
                                                      <asp:SqlDataSource ID="sds_BudgetAnnualOperator" runat="server" ConnectionString="<%$ ConnectionStrings:IRGT_MASTERConnectionString %>" SelectCommand="SELECT * FROM [HIR_Work_Center]"></asp:SqlDataSource>
                                                      <asp:SqlDataSource ID="sds_BudgetAnnualPosition" runat="server" ConnectionString="<%$ ConnectionStrings:IRGT_BUDGETConnectionString %>" SelectCommand="SELECT * FROM [MS_Approve_Type]"></asp:SqlDataSource>
                                                      <asp:SqlDataSource ID="sds_BudgetAnnualAsset" runat="server" ConnectionString="<%$ ConnectionStrings:IRGT_BUDGETConnectionString %>" SelectCommand="SELECT * FROM [MS_Approve_Type]"></asp:SqlDataSource>
                                                      <asp:SqlDataSource ID="sds_BudgetAnnualProject" runat="server" ConnectionString="Data Source=localhost;Initial Catalog=IRGT_BUDGET;Integrated Security=True" SelectCommand="sp_getBudget_Annual_Project" ProviderName="System.Data.SqlClient" SelectCommandType="StoredProcedure">
                                                          <SelectParameters>
                                                              <asp:FormParameter DefaultValue="" FormField="ddlLoc" Name="Loc_ID" Type="String" />
                                                              <asp:SessionParameter DefaultValue="th" Name="Language" SessionField="language_report_budget_annual" Type="String" />
                                                          </SelectParameters>
                                                      </asp:SqlDataSource>
                                                </div>
                                            </div>
										</div>
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

    <script>
        /*
        var $tmp_scope;
        var $tmp_http;
        var app = angular.module('myApp', []);
        var config = { headers: { 'Content-Type': 'application/x-www-form-urlencoded;charset=utf-8;' } }
        app.controller('fnMain', fnMain);

        //ข้อมูลผู้ใต้บังคับบัญชาระดับอื่น ๆ    
        function fnMain($scope, $http) {
            $('body').pleaseWait();
            $tmp_scope = $scope;
            $tmp_http = $http;
            fnLoadLoc();
            fnLoadBudgetType();
        }

        function fnLoadLoc($scope, $http) {

            $scope = $tmp_scope;
            $http = $tmp_http;

            var data = $.param({
                Command: 'GetMasterData',
                Function: 'WorkCenter',
                PageName: 'budget_annual'
            });

            $http.post("../server/Server_Budget_Operation.aspx", data, config)
            .success(function (data, status, headers, config) {
                $scope.Data_Loc_ID = data.records;
                setTimeout(fnLoad, 100);
            })
            .error(function (data, status, header, config) {
                $('body').pleaseWait('stop');
            });
        }

        function fnLoadBudgetType($scope, $http) {

            $scope = $tmp_scope;
            $http = $tmp_http;

            var data = $.param({
                Command: 'GetMasterData',
                Function: 'BudgetType',
                PageName: 'budget_annual'
            });

            $http.post("../server/Server_Budget_Operation.aspx", data, config)
            .success(function (data, status, headers, config) {
                $scope.Data_Budget_ID = data.records;
                setTimeout(fnLoad, 100);
            })
            .error(function (data, status, header, config) {
                $('body').pleaseWait('stop');
            });
        }

        function fnLoad() {
            $('body').pleaseWait('stop');
            fnLoadCtrl();
            isLoad = false;
        }
        */
    </script>
    
</asp:Content>

