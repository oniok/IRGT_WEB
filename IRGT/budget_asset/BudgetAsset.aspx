﻿<%@ Page Title="" Language="C#" MasterPageFile="~/master_page/main.master" AutoEventWireup="true" CodeFile="BudgetAsset.aspx.cs" Inherits="budget_BudgetAsset" %>

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
										<h3 class="header smaller lighter blue"><%=Session["HeaderText"]%></h3>									
										
										<!-- div.table-responsive -->
                                        <center>
                                            <table>
                                                <tr>     
                                                    <td><%=Session["budget_asset_Column06"]%></td>
                                                    <td style="width:5px"></td>
                                                    <td>
                                                        <select class="chosen-select form-control" id="Asset_Type_ID" data-placeholder="<%=Session["search_placeholder"] %>" style="width:200px" >
					                                        <option value=""></option>	
                                                            <option ng-repeat="x in AssetType" value="{{ x.Code }}" >{{ x.Name }}</option>				
				                                        </select>   
                                                    </td>                                                 
                                                    <td style="width:5px"></td>
                                                    <td><%=Session["budget_asset_Column01"]%></td>
                                                    <td style="width:5px"></td>                                                   
                                                    <td>
                                                        <select class="chosen-select form-control" id="BA_Type_ID" data-placeholder="<%=Session["search_placeholder"] %>" style="width:250px">
					                                        <option value=""></option>	
                                                            <option ng-repeat="x in BudgetAssetType" value="{{ x.Code }}" >{{ x.Name }}</option>				
				                                        </select>
                                                        <!--<select class="chosen-select form-control" id="BA_Type_ID" data-ng-options="x.Name for x in BudgetAssetType" ng-model="referral.BudgetAssetType" data-placeholder="<%=Session["search_placeholder"] %>" style="width:250px">
                                                            
                                                        </select>-->
                                                    
                                                    </td>   
                                                    <td style="width:5px"></td>                                              
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
                                                    <button class="btn btn-white btn-sm" type="button" ng-click="fnNew()">
												        <i class="ace-icon fa fa-plus bigger-120"></i>
												        <%=Session["add_button"]%>
											        </button>
                                                    <button class="btn btn-white btn-sm" type="button" ng-click="fnSum()">
												        <i class="ace-icon glyphicon glyphicon-file bigger-120"></i>
												        <%=Session["sum_button"]%>
											        </button>
                                                    <button class="btn btn-white btn-sm" type="button" ng-click="fnSend()">
												        <i class="ace-icon glyphicon glyphicon-check bigger-120"></i>
												        <%=Session["confirm_button"]%>
											        </button>
                                                </div>									
											</div>
                                            <div class="widget-body">
                                                <div class="widget-main no-padding">
                                            
											        <table id="dynamic-table" class="table table-striped table-bordered table-hover">
												        <thead>
													        <tr>
														        <th class="center" style="width:50px"><%=Session["budget_asset_ColumnSEQ"]%></th>
														        <th class="center"><%=Session["budget_asset_Column01"]%></th>
														        <th class="center" style="width:80px"><%=Session["budget_asset_Column02"]%></th>
                                                                <th class="center" style="width:120px"><%=Session["budget_asset_Column03"]%></th>          
                                                                <th class="center" style="width:120px"><%=Session["budget_asset_Column04"]%></th>    
                                                                <th class="center"><%=Session["budget_asset_Column05"]%></th>
														        <td class="center" style="width:80px"><%=Session["budget_asset_ColumnEdit"]%></td>
													        </tr>
												        </thead>
                                                        <tbody>
													        <tr ng-repeat="x in Data">
														        <td class="center">{{ x.RowID }}</td>
                                                                <td><input type="hidden" id="BA_ID" value="{{ x.BA_ID }}"/>{{ x.BA_Type_Name }}</td>
                                                                <td class="center">{{ x.BA_Qty }}</td>          
                                                                <td style="text-align:right">{{ x.BA_Price }}</td>
                                                                <td style="text-align:right">{{ x.Total_Amount }}</td>  														                                                            
                                                                <td>{{ x.BA_Reason }}</td>
														        <td style="text-align:center">   
                                                                    <button type="button" class="btn btn-success btn-xs" ng-click="fnEdit(x.KeyID)">
												                        <i class="ace-icon fa fa-pencil  bigger-110 icon-only"></i>
											                        </button>
                                                                    <button type="button" class="btn btn-danger btn-xs" ng-click="fnDelete(x.KeyID)">
												                        <i class="ace-icon fa fa-trash-o  bigger-110 icon-only"></i>
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
    function fnDeleteYes() {
        $http = $tmp_http;
        $scope = $tmp_scope;
        $('body').pleaseWait();
        var data = $.param({
            Command: 'BudgetAsset',
            Function: 'Delete',
            KeyID: tmpKeyID
        });

        $http.post("../server/Server_Budget_Asset.aspx", data, config)
        .success(function (data, status, headers, config) {
            document.getElementById('btnConfirm').click();
            GetPaging($scope, $http);
        })
        .error(function (data, status, header, config) {
            $('body').pleaseWait('stop');
        });
    }
    function fnSendYes() {
        $http = $tmp_http;
        $scope = $tmp_scope;
        var User_Code = '<%=Session["user_code"]%>';
        var BA_ID = document.getElementById('BA_ID').value;
        $('body').pleaseWait();
        var data = $.param({
            Command: 'BudgetAsset',
            Function: 'Send',
            BA_ID: BA_ID,
            User_Code: User_Code
        });

        $http.post("../server/Server_Budget_Asset.aspx", data, config)
        .success(function (data, status, headers, config) {
            document.getElementById('btnConfirm').click();
            GetPaging($scope, $http);
        })
        .error(function (data, status, header, config) {
            $('body').pleaseWait('stop');
        });
    }

    function fnSendNew(LOC_ID) {
        $http = $tmp_http;
        $scope = $tmp_scope;
        var User_Code = '<%=Session["user_code"]%>';
        var BA_ID = document.getElementById('BA_ID').value;
        $('body').pleaseWait();
        var data = $.param({
            Command: 'BudgetAsset',
            Function: 'Send',
            BA_ID: BA_ID,
            LOC_ID: LOC_ID,
            User_Code: User_Code
        });

        $http.post("../server/Server_Budget_Asset.aspx", data, config)
        .success(function (data, status, headers, config) {
            document.getElementById('btnPopClose').click();
            GetPaging($scope, $http);
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

        $scope.fnNew = function () {
            fnOpenPopup('<%=Session["pop_add_budget_asset"]%>', "../budget_asset_popup/pop_BudgetAsset.aspx", null, "450");
        }
        $scope.fnEdit = function (KeyID) {           
            fnOpenPopup('<%=Session["pop_edit_budget_asset"]%>', "../budget_asset_popup/pop_BudgetAsset.aspx?KeyID=" + KeyID, null, "450");
        }
        $scope.fnSum = function () {
            fnOpenPopup('<%=Session["pop_sum_budget_asset"]%>', "../budget_asset_popup/pop_BudgetAssetSummary.aspx?", "1300", "450");
        }
        $scope.fnSend = function () {
            fnOpenPopup('<%=Session["pop_confirm_budget_asset"]%>', "../budget/pop_BudgetConfirm.aspx?",null, "450");
        }
        $scope.fnDelete = function (KeyID) {
            tmpKeyID = KeyID;
            fnConfirmMessage('<%=Session["pop_confirm_budget_asset"]%>', '<%=Session["pop_delete_budget_asset"]%>', fnDeleteYes);
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

        $('#Asset_Type_ID').on('change', function () {
            $('body').pleaseWait();
            fnGetBudgetAssetType();
        });
    }
   
    function GetPaging($scope, $http) {

        var BA_Type_ID = document.getElementById('BA_Type_ID').value;
        var User_Code = '<%=Session["user_code"]%>';
        var data = $.param({
            Command: 'BudgetAsset',
            Function: 'Paging',
            PageSize: PageSize,
            BA_Type_ID: BA_Type_ID,
            User_Code: User_Code
        });

        $http.post("../server/Server_Budget_Asset.aspx", data, config)
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
        var BA_Type_ID = document.getElementById('BA_Type_ID').value;
        var User_Code = '<%=Session["user_code"]%>';
        var lang = getParamValue("lang");
        var data = $.param({
            Command: 'BudgetAsset',
            Function: 'Select',
            PageIndex: PageIndex,
            PageSize: PageSize,
            BA_Type_ID: BA_Type_ID,
            User_Code: User_Code,            
            lang: lang
        });

        $http.post("../server/Server_Budget_Asset.aspx", data, config)
        .success(function (data, status, headers, config) {
            $scope.Data = data.records;
            if (isLoad){
                setTimeout(fnGetAssetType, 100);
            }                
            else
                $('body').pleaseWait('stop');
        })
        .error(function (data, status, header, config) {
            $('body').pleaseWait('stop');
        });
    }
    function fnGetAssetType() {

        $scope = $tmp_scope;
        $http = $tmp_http;

        var data = $.param({
            Command: 'GetMasterData',
            Function: 'AssetType',
            PageName: 'asset'
        });

        $http.post("../server/Server.aspx", data, config)
        .success(function (data, status, headers, config) {
            $scope.AssetType = data.records;
            fnGetBudgetAssetType()
        })
        .error(function (data, status, header, config) {
            $('body').pleaseWait('stop');
        });
    }
    function fnGetBudgetAssetType() {

        $scope = $tmp_scope;
        $http = $tmp_http;

        var data = $.param({
            Command: 'GetMasterData',
            Function: 'BudgetAssetType',
            PageName: 'budget_asset',
            Param: document.getElementById('Asset_Type_ID').value
        });

        $http.post("../server/Server_Budget_Asset.aspx", data, config)
        .success(function (data, status, headers, config) {
            $scope.BudgetAssetType = data.records;
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

