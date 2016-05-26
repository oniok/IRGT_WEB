﻿<%@ Page Title="" Language="C#" MasterPageFile="~/master_page/main.master" AutoEventWireup="true" CodeFile="BudgetProject.aspx.cs" Inherits="budget_BudgetProject" %>

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
								<!-- div.dataTables_borderWrap -->
								<div class="widget-box  widget-color-blue2">
                                    <div class="widget-header">
										<h5 class="widget-title bigger">
											<i class="ace-icon fa fa-table"></i>
                                            <%=Session["HeaderTable"]%>
										</h5>			
                                        <div class="widget-toolbar no-border">
                                            <button class="btn btn-white btn-sm" type="button" ng-click="fnSend()">
												<i class="ace-icon glyphicon glyphicon-check bigger-120"></i>
												<%=Session["confirm_button"]%>
											</button>
                                        </div>									
									</div>               
								</div>
							</div>
						</div>

                        <div class="widget-box widget-color-blue">
							<div class="widget-header widget-header-small">
                                <h5 class="widget-title bigger"><%=Session["budget_project_Column01"]%></h5>
							</div>
							<div class="widget-body">
								<div class="widget-main no-padding">
									<textarea id="BJ_Issue" name="BJ_Issue" data-provide="markdown" data-iconlibrary="fa" rows="10"></textarea>
								</div>
								<div class="widget-toolbox padding-4 clearfix">
									<div class="btn-group pull-left">
										<button class="btn btn-sm btn-info">
											<i class="ace-icon fa fa-times bigger-125"></i>
											Cancel
										</button>
									</div>
									<div class="btn-group pull-right">
										<button class="btn btn-sm btn-purple">
											<i class="ace-icon fa fa-floppy-o bigger-125"></i>
											Save
										</button>
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
    
    function fnRefresh() {
        document.getElementById('btnPopClose').click();
        document.getElementById('btnSearch').click();
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
            fnOpenPopup('<%=Session["pop_edit_budget_project"]%>', "../budget_project_popup/pop_BudgetProject.aspx?KeyID=" + KeyID, null, "450");
        }
        $scope.fnSend = function () {
            fnConfirmMessage('<%=Session["pop_confirm_budget_project"]%>', '<%=Session["pop_send_budget_project"]%>', fnSendYes);
        }
        fnGetData($scope, $http);
    }
    function fnLoad(KeyID) {
        var User_Code = '<%=Session["user_code"]%>';
        var lang = '<%=Session["language_budget_project"]%>';
       
        $.post("../server/Server_Budget_Project.aspx",
            {
                Command: 'BudgetProject',
                Function: 'Load',
                KeyID: KeyID,
                lang: lang
            },
            function (data, status) {
                var data = eval(data);
                document.getElementById('BJ_Issue').value = data[0].BJ_Issue.trim();

                $('body').pleaseWait('stop');
                fnLoadCtrl();
            }
        );

    }
    
    function fnGetData($scope, $http) {
        var User_Code = '<%=Session["user_code"]%>';
        var lang = '<%=Session["language_budget_project"]%>';

        var data = $.param({
            Command: 'BudgetProject',
            Function: 'Select',
            User_Code: User_Code,            
            lang: lang
        });

        $http.post("../server/Server_Budget_Project.aspx", data, config)
       .success(function (data, status, headers, config) {
           $scope.Data = data.records;
           setTimeout(fnLoad(data.records[0].KeyID), 100);
       })
       .error(function (data, status, header, config) {
           $('body').pleaseWait('stop');
       });

    } 
        //var data = eval(data);
        //document.getElementById('BJ_ID').value = data[0].BJ_ID.trim();
        //document.getElementById('BJ_Issue').value = data[0].BJ_Issue.trim();
        //document.getElementById('BJ_Goal').value = data[0].BJ_Goal.trim();
        //document.getElementById('BJ_Strategy').value = data[0].BJ_Strategy.trim();
        //document.getElementById('BJ_ProjectName').value = data[0].BJ_ProjectName.trim();
        //document.getElementById('BJ_Reason').value = data[0].BJ_Reason.trim();
        //document.getElementById('BJ_Objective').value = data[0].BJ_Objective.trim();
        //document.getElementById('BJ_Place').value = data[0].BJ_Place.trim();
        //document.getElementById('BJ_Duration').value = data[0].BJ_Duration.trim();
        //document.getElementById('BJ_Amount').value = data[0].BJ_Amount.trim();
        //document.getElementById('BJ_Detail').value = data[0].BJ_Detail.trim();
        //document.getElementById('BJ_Measure').value = data[0].BJ_Measure.trim();
        //document.getElementById('BJ_Benefit').value = data[0].BJ_Benefit.trim();
        //document.getElementById('BJ_Responsible').value = data[0].BJ_Responsible.trim();

</script>
</asp:Content>

