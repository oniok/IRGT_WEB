<%@ Page Title="" Language="C#" MasterPageFile="~/master_page/main.master" AutoEventWireup="true" CodeFile="BudgetPositionByIDAction.aspx.cs" Inherits="budget_BudgetPositionByIDAction" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="../Scripts/angular.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <div class="main-content" ng-app="myApp" ng-controller="fnMain">
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
                        <div class="row">
									<div class="col-xs-12">
										<h3 id="header_id" class="header smaller lighter blue"><%=Session["HeaderText"]%></h3>									
										
										<!-- div.table-responsive -->
                                        <center>
                                            <table>
                                                <tr>                                                    
                                                    <td style="width:5px"></td>
                                                    <td><%=Session["budget_position_Column01"]%></td>
                                                    <td style="width:5px"></td>                                                   
                                                    <td>
                                                        <select class="chosen-select form-control" id="Position_Type_ID" data-placeholder="<%=Session["search_placeholder"] %>" style="width:250px">
					                                        <option value=""></option>	
                                                            <option ng-repeat="x in Position_Type" value="{{ x.Code }}" >{{ x.Name }}</option>				
				                                        </select>
                                                    </td>
                                                    <td style="width:10px"></td>
                                                    <td><%=Session["budget_position_Column02"]%></td>
                                                    <td style="width:5px"></td>                                                   
                                                    <td>
                                                        <select class="chosen-select form-control" id="Educate_Type_ID" data-placeholder="<%=Session["search_placeholder"] %>" style="width:150px">
					                                        <option value=""></option>	
                                                            <option ng-repeat="x in Educate_Type" value="{{ x.Code }}" >{{ x.Name }}</option>				
				                                        </select>
                                                    </td>
                                                    <td style="width:10px"></td>
                                                    <td><%=Session["budget_position_Column03"]%></td>
                                                    <td style="width:5px"></td>                                                   
                                                    <td>
                                                        <select class="chosen-select form-control" id="BP_Type_ID" data-placeholder="<%=Session["search_placeholder"] %>" style="width:150px">
					                                        <option value=""></option>	
                                                            <option ng-repeat="x in BP_Type" value="{{ x.Code }}" >{{ x.Name }}</option>				
				                                        </select>   
                                                    </td>   
                                                    <td style="width:15px"></td>                                              
                                                    <td>         
                                                        <button type="button" class="btn btn-success " id="btnSearch" ng-click="fnSearch()">  
											                <i class="ace-icon fa fa-search "></i>
											                <%=Session["text_search"]%>
										                </button>
                                                    </td>                                                
                                                </tr>                                             
                                            </table>
                                        </center>
										<!-- div.dataTables_borderWrap -->
										<div class="widget-box  widget-color-blue2">
                                            <div class="widget-header">
												<h5 class="widget-title bigger">
													<i class="ace-icon fa fa-table"></i>
                                                    <%=Session["HeaderTable"]%>
												</h5>			
                                                <div class="widget-toolbar no-border">
                                                    <button class="btn btn-white btn-sm" type="button" ng-click="fnSum()">
												        <i class="ace-icon glyphicon glyphicon-file bigger-120"></i>
												        <%=Session["sum_button"]%>
											        </button>
                                                    <button class="btn btn-white btn-sm" type="button" ng-click="fnConfirm()">
												        <i class="ace-icon glyphicon glyphicon-check bigger-120"></i>
												        <%=Session["confirm_button"]%>
											        </button>
                                                    <button class="btn btn-white btn-sm" type="button" ng-click="fnApprove()">
												        <i class="ace-icon glyphicon glyphicon-check bigger-120"></i>
												        <%=Session["approve_button"]%>
											        </button>
                                                </div>									
											</div>
                                            <div class="widget-body">
                                                <div class="widget-main no-padding">
                                            
											        <table id="dynamic-table" class="table table-striped table-bordered table-hover">
												        <thead>
													        <tr>
														        <th class="center" style="width:50px"><%=Session["budget_position_ColumnSEQ"]%></th>
														        <th class="center"><%=Session["budget_position_Column01"]%></th>
														        <th class="center" style="width:100px"><%=Session["budget_position_Column02"]%></th>
                                                                <th class="center" style="width:150px"><%=Session["budget_position_Column03"]%></th>          
                                                                <th class="center" style="width:80px"><%=Session["budget_position_Column04"]%></th>    
                                                                <th class="center" style="width:120px"><%=Session["budget_position_Column05"]%></th>
                                                                <th class="center"><%=Session["budget_position_Column06"]%></th>
														        <td class="center" style="width:80px"><%=Session["budget_position_ColumnEdit"]%></td>
													        </tr>
												        </thead>
                                                        <tbody>
													        <tr ng-repeat="x in Data">
														        <td class="center">{{ x.RowID }}</td>
                                                                <td><input type="hidden" id="BP_ID" value="{{ x.BP_ID }}"/>{{ x.Position_Type_Name }}</td>
                                                                <td>{{ x.Educate_Type_Name }}</td>
                                                                <td>{{ x.BP_Type_Name }}</td>
                                                                <td class="center">{{ x.BP_Qty_View }}</td>          
                                                                <td style="text-align:right">{{ x.BP_Price_View }}</td>														                                                            
                                                                <td>{{ x.BP_Reason }}</td>							
														        <td style="text-align:center">   
                                                                    <button type="button" class="btn btn-success btn-xs" ng-click="fnEdit(x.KeyID)">
												                        <i class="ace-icon fa fa-pencil  bigger-110 icon-only"></i>
											                        </button>
                                                                    
														        </td>											
													        </tr>										
												        </tbody>  
                                                        <tfoot >
                                                            <tr style="background-color:whitesmoke">                                                                
                                                                <td colspan="7" style="padding:0px;text-align:right">                                                                  
                                                                    <table style="margin:3px;width:100%">
                                                                        <tr>
                                                                            <td style="width:5px"></td>
                                                                            <td style="text-align:left">
                                                                                <span class="label label-lg label-primary"><%=Session["PageStart"]%> {{ RowCount }} <%=Session["PageRecord"]%></span>            
                                                                            </td>
                                                                            <td style="width:30px">
                                                                                <label class="btn btn-primary btn-sm" ng-click="fnPageBack()">
                                                                                    <i class="ace-icon fa fa-angle-double-left"></i>
											                                    </label>
                                                                            </td>                                                                              
                                                                            <td style="width:30px">
                                                                                <label class="btn btn-primary btn-sm" ng-click="fnPageBack()" ><%=Session["PageText"]%></label>
                                                                            </td>
                                                                            <td style="width:2px"></td>
                                                                            <td style="width:30px">                                                                             
                                                                                <select id="paging-select" onchange="fnPagingChange()" style="height:32px">
                                                                                    <option ng-repeat="x in Paging" value="{{x.Page}}">{{ x.Page }}</option>
                                                                                </select>                                                                                  
                                                                            </td>
                                                                            <td style="width:2px"></td>
                                                                            <td style="width:30px">
                                                                                <label class="btn btn-primary btn-sm" ng-click="fnPageBack()"> / {{ PageMax }} <%=Session["PageLast"]%></label>
                                                                            </td>
                                                                            <td style="width:30px">
                                                                                <label class="btn btn-primary btn-sm" ng-click="fnPageNext()">
                                                                                    <i class="ace-icon fa fa-angle-double-right"></i>
											                                    </label>
                                                                            </td>
                                                                            <td style="width:5px"></td>
                                                                        </tr>
                                                                    </table>                                                                                                                       
                                                                </td>
                                                            </tr>
                                                        </tfoot>                                              
											        </table>
                                                </div>
                                            </div>
										</div>
									</div>
								</div>

								
						<!-- PAGE CONTENT ENDS -->
					</div><!-- /.col -->
				</div><!-- /.row -->
			</div><!-- /.page-content -->
		</div>
	</div><!-- /.main-content -->
<script>
    var isLoad = true;
    var PageSize = 10;
    var CurrentPageIndex = 1;
    var tmpKeyID;
    var $tmp_http;
    var $tmp_scope;
    function fnPagingChange() {
        var PageIndex = document.getElementById('paging-select').value;
        $('body').pleaseWait();
        GetData($tmp_scope, $tmp_http, PageIndex);
    }
    function fnRefresh() {
        document.getElementById('btnPopClose').click();
        document.getElementById('btnSearch').click();
    }
    
    function fnConfirmYes() {
        $http = $tmp_http;
        $scope = $tmp_scope;
        var User_Code = '<%=Session["user_code"]%>';
        var BP_ID = '<%=Session["BP_ID"]%>';
        $('body').pleaseWait();
        var data = $.param({
            Command: 'BudgetPositionByID',
            Function: 'Confirm',
            BP_ID: BP_ID,
            User_Code: User_Code
        });

        $http.post("../server/Server_Budget_Position.aspx", data, config)
        .success(function (data, status, headers, config) {
            document.getElementById('btnConfirm').click();
            window.open(
              "../budget_position/BudgetPositionListAction.aspx",
              "_self"
            );
        })
        .error(function (data, status, header, config) {
            $('body').pleaseWait('stop');
        });
    }
    function fnApproveYes() {
        $http = $tmp_http;
        $scope = $tmp_scope;
        var User_Code = '<%=Session["user_code"]%>';
        var BP_ID = '<%=Session["BP_ID"]%>';
        $('body').pleaseWait();
        var data = $.param({
            Command: 'BudgetPositionByID',
            Function: 'Approve',
            BP_ID: BP_ID,
            User_Code: User_Code
        });

        $http.post("../server/Server_Budget_Position.aspx", data, config)
        .success(function (data, status, headers, config) {
            document.getElementById('btnConfirm').click();
            window.open(
              "../budget_position/BudgetPositionListAction.aspx",
              "_self"
            );
        })
        .error(function (data, status, header, config) {
            $('body').pleaseWait('stop');
        });
    }

    var app = angular.module('myApp', []);
    var config = { headers: { 'Content-Type': 'application/x-www-form-urlencoded;charset=utf-8;' } }
    app.controller('fnMain', fnMain);

    //ข้อมูลผู้ใต้บังคับบัญชาระดับอื่น ๆ    
    function fnMain($scope, $http) {
        $('body').pleaseWait();

        $tmp_http = $http;
        $tmp_scope = $scope;

        $scope.fnSearch = function () {
            $('body').pleaseWait();
            document.getElementById('paging-select').value = 1;
            GetPaging($scope, $http);
        }
       
        $scope.fnEdit = function (KeyID) {
            $('#btnPopSave').toggle(true);
            fnOpenPopup('<%=Session["pop_edit_budget_position"]%>', "../budget_position_popup/pop_BudgetPositionAction.aspx?KeyID=" + KeyID, null, "450");
        }
        $scope.fnSum = function () {
            $('#btnPopSave').toggle(false);
            var BP_ID = '<%=Session["BP_ID"]%>';
            fnOpenPopup('<%=Session["pop_sum_budget_position"]%>', "../budget_position_popup/pop_BudgetPositionSummaryByID.aspx?BP_ID=" + BP_ID, null, "450");
        }
        $scope.fnConfirm = function () {
            fnConfirmMessage('<%=Session["pop_confirm_budget_position"]%>', '<%=Session["pop_confirms_budget_position"]%>', fnConfirmYes);
        }
        $scope.fnApprove = function () {
            fnConfirmMessage('<%=Session["pop_confirm_budget_position"]%>', '<%=Session["pop_approve_budget_position"]%>', fnApproveYes);
        }
        
        $scope.fnPageBack = function () {
            var currentPageIndex = parseInt(document.getElementById('paging-select').value);
            if (currentPageIndex != 1) {
                var PageIndex = currentPageIndex - 1;
                document.getElementById('paging-select').value = PageIndex;
                $('body').pleaseWait();
                GetData($scope, $http, PageIndex);
            }
        }
        $scope.fnPageNext = function () {
            var currentPageIndex = parseInt(document.getElementById('paging-select').value);
            var PageMax = $scope.PageMax;
            if (PageMax == currentPageIndex) {
                return;
            }
            var PageIndex = currentPageIndex + 1;
            document.getElementById('paging-select').value = PageIndex;
            $('body').pleaseWait();
            GetData($scope, $http, PageIndex);
        }
        GetPaging($scope, $http);
        
    }
   
    function GetPaging($scope, $http) {
        var Position_Type_ID = document.getElementById('Position_Type_ID').value;
        var Educate_Type_ID = document.getElementById('Educate_Type_ID').value;
        var BP_Type_ID = document.getElementById('BP_Type_ID').value;
        var BP_ID = '<%=Session["BP_ID"]%>';
        var data = $.param({
            Command: 'BudgetPositionByID',
            Function: 'Paging',
            PageSize: PageSize,
            Position_Type_ID: Position_Type_ID,
            Educate_Type_ID: Educate_Type_ID,
            BP_Type_ID: BP_Type_ID,
            BP_ID: BP_ID
        });

        $http.post("../server/Server_Budget_Position.aspx", data, config)
        .success(function (data, status, headers, config) {
            $scope.Paging = data.records;
            $scope.PageMax = data.pagemax;
            $scope.RowCount = data.rowcount;
            GetData($scope, $http, 1);
        })
        .error(function (data, status, header, config) {
            $('body').pleaseWait('stop');
        });
    }
    function GetData($scope, $http, PageIndex) {
        CurrentPageIndex = PageIndex;
        var Position_Type_ID = document.getElementById('Position_Type_ID').value;
        var Educate_Type_ID = document.getElementById('Educate_Type_ID').value;
        var BP_Type_ID = document.getElementById('BP_Type_ID').value;
        var User_Code = '<%=Session["user_code"]%>';
        var BP_ID = '<%=Session["BP_ID"]%>';
        var lang = '<%=Session["language_budget_position"]%>';
        var data = $.param({
            Command: 'BudgetPositionByID',
            Function: 'Select',
            PageIndex: PageIndex,
            PageSize: PageSize,
            Position_Type_ID: Position_Type_ID,
            Educate_Type_ID: Educate_Type_ID,
            BP_Type_ID: BP_Type_ID,
            BP_ID: BP_ID,
            lang: lang
        });

        $http.post("../server/Server_Budget_Position.aspx", data, config)
        .success(function (data, status, headers, config) {
            $scope.Data = data.records;
            if (isLoad)
                setTimeout(fnGetBP_PositionType, 100);
            else
                $('body').pleaseWait('stop');
        })
        .error(function (data, status, header, config) {
            $('body').pleaseWait('stop');
        });
    }
    function fnGetBP_PositionType() {

        $scope = $tmp_scope;
        $http = $tmp_http;

        var data = $.param({
            Command: 'GetMasterData',
            Function: 'PositionType',
            PageName: 'budget_position'
        });

        $http.post("../server/Server_Budget_Position.aspx", data, config)
        .success(function (data, status, headers, config) {
            $scope.Position_Type = data.records;
            setTimeout(fnGetBP_EducateType, 100);
        })
        .error(function (data, status, header, config) {
            $('body').pleaseWait('stop');
        });
    }
    function fnGetBP_EducateType() {

        $scope = $tmp_scope;
        $http = $tmp_http;

        var data = $.param({
            Command: 'GetMasterData',
            Function: 'EducateType',
            PageName: 'budget_position'
        });

        $http.post("../server/Server_Budget_Position.aspx", data, config)
        .success(function (data, status, headers, config) {
            $scope.Educate_Type = data.records;
            setTimeout(fnGetBP_BPType, 100);
        })
        .error(function (data, status, header, config) {
            $('body').pleaseWait('stop');
        });
    }
    function fnGetBP_BPType() {

        $scope = $tmp_scope;
        $http = $tmp_http;

        var data = $.param({
            Command: 'GetMasterData',
            Function: 'BudgetPositionType',
            PageName: 'budget_position'
        });

        $http.post("../server/Server_Budget_Position.aspx", data, config)
        .success(function (data, status, headers, config) {
            $scope.BP_Type = data.records;
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


</script>
</asp:Content>

