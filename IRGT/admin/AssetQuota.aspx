<%@ Page Title="" Language="C#" MasterPageFile="~/master_page/main.master" AutoEventWireup="true" CodeFile="AssetQuota.aspx.cs" Inherits="admin_AssetQuota" %>

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
                                                    <td><%=Session["asset_quota_Column01"]%></td>
                                                    <td style="width:5px"></td>
                                                    <td>
                                                        <select class="chosen-select form-control" id="Asset_ID" data-placeholder="<%=Session["search_placeholder"] %>" style="width:200px">
					                                        <option value=""></option>	
                                                            <option ng-repeat="x in Asset" value="{{ x.Code }}" >{{ x.Name }}</option>				
				                                        </select>   
                                                    </td>
                                                    <td style="width:5px"></td>
                                                    <td><%=Session["asset_quota_Column02"]%></td>
                                                    <td style="width:5px"></td>
                                                    <td>
                                                        <select class="chosen-select form-control" id="Loc_ID" data-placeholder="<%=Session["search_placeholder"] %>"  style="width:200px">
					                                        <option value=""></option>	
                                                            <option ng-repeat="x in WorkCenter" value="{{ x.Code }}" >{{ x.Name }}</option>				
				                                        </select>     
                                                    </td>
                                                    <td style="width:5px"></td>                                                    
                                                    <td>         
                                                        <button type="button" class="btn btn-success btn-bold" id="btnSearch" ng-click="fnSearch()">  
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
                                                </div>									
											</div>
                                            <div class="widget-body">
                                                <div class="widget-main no-padding">
                                            
											        <table id="dynamic-table" class="table table-striped table-bordered table-hover">
												        <thead>
													        <tr>
														        <th class="center" style="width:50px"><%=Session["asset_quota_ColumnSEQ"]%></th>
														        <th class="center"><%=Session["asset_quota_Column01"]%></th>
														        <th class="center"><%=Session["asset_quota_Column02"]%></th>
                                                                <th class="center" style="width:100px"><%=Session["asset_quota_Column03"]%></th>
                                                                <th class="center" style="width:100px"><%=Session["asset_quota_Column06"]%></th>
                                                                <th class="center" style="width:120px">
                                                                    <i class="ace-icon fa fa-clock-o bigger-110 hidden-480"></i>
                                                                    <%=Session["asset_quota_Column04"]%>
                                                                </th>
                                                                <th class="center" style="width:100px">
                                                                    <i class="ace-icon fa fa-clock-o bigger-110 hidden-480"></i>
                                                                    <%=Session["asset_quota_Column05"]%>
                                                                </th>														      
														       <td class="center" style="width:80px"><%=Session["asset_quota_ColumnEdit"]%></td>
													        </tr>
												        </thead>
                                                        <tbody>
													        <tr ng-repeat="x in Data">
														        <td class="center">{{ x.RowID }}</td>
                                                                <td>{{ x.Asset_Name }}</td>
														        <td>{{ x.Loc_Name }}</td>
                                                                <td>{{ x.Quota_Qty }}</td>
                                                                <td>{{ x.Stock_Qty }}</td>
                                                                <td style="text-align:center">{{ x.Start_Date }}</td>
                                                                <td style="text-align:center">{{ x.End_Date }}</td>														        
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
                                                                <td colspan="9" style="padding:0px;text-align:right">                                                                  
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
            Command: 'AssetQuota',
            Function: 'Delete',
            KeyID: tmpKeyID
        });

        $http.post("../server/Server.aspx", data, config)
        .success(function (data, status, headers, config) {
            document.getElementById('btnConfirm').click();
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
            fnOpenPopup('<%=Session["pop_add_asset_quota"]%>', "../admin_popup/pop_AssetQuota.aspx", null, "450");
        }
        $scope.fnEdit = function (KeyID) {
            fnOpenPopup('<%=Session["pop_edit_asset_quota"]%>', "../admin_popup/pop_AssetQuota.aspx?KeyID=" + KeyID, null, "450");
        }
        $scope.fnDelete = function (KeyID) {
            tmpKeyID = KeyID;
            fnConfirmMessage('<%=Session["pop_confirm_asset_quota"]%>', '<%=Session["pop_delete_asset_quota"]%>', fnDeleteYes);
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

        var Asset_ID = document.getElementById('Asset_ID').value;
        var Loc_ID = document.getElementById('Loc_ID').value;

        var data = $.param({
            Command: 'AssetQuota',
            Function: 'Paging',
            PageSize: PageSize,
            Asset_ID: Asset_ID,
            Loc_ID: Loc_ID
        });

        $http.post("../server/Server.aspx", data, config)
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
        var Asset_ID = document.getElementById('Asset_ID').value;
        var Loc_ID = document.getElementById('Loc_ID').value;
        var lang = getParamValue("lang");
        var data = $.param({
            Command: 'AssetQuota',
            Function: 'Select',
            PageIndex: PageIndex,
            PageSize: PageSize,
            Asset_ID: Asset_ID,
            Loc_ID: Loc_ID,
            lang: lang
        });

        $http.post("../server/Server.aspx", data, config)
        .success(function (data, status, headers, config) {
            $scope.Data = data.records;
            if (isLoad)
                setTimeout(fnGetAsset, 100);
            else
                $('body').pleaseWait('stop');
        })
        .error(function (data, status, header, config) {
            $('body').pleaseWait('stop');
        });
    }
    function fnGetAsset() {
        $scope = $tmp_scope;
        $http = $tmp_http;

        var data = $.param({
            Command: 'GetMasterData',
            Function: 'Asset',
            PageName: 'asset_quota'
        });

        $http.post("../server/Server.aspx", data, config)
        .success(function (data, status, headers, config) {
            $scope.Asset = data.records;
            setTimeout(fnGetWorkCenter, 100);
        })
        .error(function (data, status, header, config) {
            $('body').pleaseWait('stop');
        });
    }
    function fnGetWorkCenter() {

        $scope = $tmp_scope;
        $http = $tmp_http;

        var data = $.param({
            Command: 'GetMasterData',
            Function: 'WorkCenter',
            PageName: 'asset_quota'
        });

        $http.post("../server/Server.aspx", data, config)
        .success(function (data, status, headers, config) {
            $scope.WorkCenter = data.records;
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

