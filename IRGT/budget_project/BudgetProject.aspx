<%@ Page Title="" Language="C#" MasterPageFile="~/master_page/main.master" AutoEventWireup="true" CodeFile="BudgetProject.aspx.cs" Inherits="budget_BudgetProject" %>

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
                                            <button class="btn btn-white btn-sm" type="button" ng-click="fnSave()">
												<i class="ace-icon fa fa-floppy-o bigger-120"></i>
												<%=Session["save_button"]%>
											</button>
                                            <button class="btn btn-white btn-sm" type="button" ng-click="fnSend()">
												<i class="ace-icon glyphicon glyphicon-check bigger-120"></i>
												<%=Session["confirm_button"]%>
											</button>
                                        </div>											
									</div>               
								</div>
							</div>
						</div>

                        <div class="btn-group pull-left" style="margin-left:10px;"></div>
                        <div class="widget-body" style="margin-bottom:10px;">
                            <div class="widget-toolbox padding-4 clearfix"><span class="label label-xlg label-primary arrowed arrowed-right"><%=Session["budget_project_Column01"]%></span>
							</div>
							<div class="widget-main no-padding">
                                <input type="hidden" id="BJ_ID"/>
								<textarea id="BJ_Issue" name="BJ_Issue" data-provide="markdown" data-iconlibrary="fa" rows="2"></textarea>
							</div>
						</div>

                        <div class="btn-group pull-left" style="margin-left:10px;"></div>
                        <div class="widget-body" style="margin-bottom:10px;">
                            <div class="widget-toolbox padding-4 clearfix"><span class="label label-xlg label-primary arrowed arrowed-right"><%=Session["budget_project_Column02"]%></span>
							</div>
							<div class="widget-main no-padding">
								<textarea id="BJ_Goal" name="BJ_Goal" data-provide="markdown" data-iconlibrary="fa" rows="2"></textarea>
							</div>
						</div>

                        <div class="btn-group pull-left" style="margin-left:10px;"></div>
                        <div class="widget-body" style="margin-bottom:10px;">
                            <div class="widget-toolbox padding-4 clearfix"><span class="label label-xlg label-primary arrowed arrowed-right"><%=Session["budget_project_Column03"]%></span>
							</div>
							<div class="widget-main no-padding">
								<textarea id="BJ_Strategy" name="BJ_Strategy" data-provide="markdown" data-iconlibrary="fa" rows="2"></textarea>
							</div>
						</div>

                        <div class="btn-group pull-left" style="margin-left:10px;"></div>
                        <div class="widget-body" style="margin-bottom:10px;">
                            <div class="widget-toolbox padding-4 clearfix"><span class="label label-xlg label-primary arrowed arrowed-right"><%=Session["budget_project_Column04"]%></span>
							</div>
							<div class="widget-main no-padding">
								<textarea id="BJ_ProjectName" name="BJ_ProjectName" data-provide="markdown" data-iconlibrary="fa" rows="2"></textarea>
							</div>
						</div>

                        <div class="btn-group pull-left" style="margin-left:10px;"></div>
                        <div class="widget-body" style="margin-bottom:10px;">
                            <div class="widget-toolbox padding-4 clearfix"><span class="label label-xlg label-primary arrowed arrowed-right"><%=Session["budget_project_Column05"]%></span>
							</div>
							<div class="widget-main no-padding">
								<textarea id="BJ_Reason" name="BJ_Reason" data-provide="markdown" data-iconlibrary="fa" rows="8"></textarea>
							</div>
						</div>

                        <div class="btn-group pull-left" style="margin-left:10px;"></div>
                        <div class="widget-body" style="margin-bottom:10px;">
                            <div class="widget-toolbox padding-4 clearfix"><span class="label label-xlg label-primary arrowed arrowed-right"><%=Session["budget_project_Column06"]%></span>
							</div>
							<div class="widget-main no-padding">
								<textarea id="BJ_Objective" name="BJ_Objective" data-provide="markdown" data-iconlibrary="fa" rows="3"></textarea>
							</div>
						</div>

                        <div class="btn-group pull-left" style="margin-left:10px;"></div>
                        <div class="widget-body" style="margin-bottom:10px;">
                            <div class="widget-toolbox padding-4 clearfix"><span class="label label-xlg label-primary arrowed arrowed-right"><%=Session["budget_project_Column07"]%></span>
							</div>
							<div class="widget-main no-padding">
								<textarea id="BJ_Place" name="BJ_Place" data-provide="markdown" data-iconlibrary="fa" rows="2"></textarea>
							</div>
						</div>

                        <div class="btn-group pull-left" style="margin-left:10px;"></div>
                        <div class="widget-body" style="margin-bottom:10px;">
                            <div class="widget-toolbox padding-4 clearfix"><span class="label label-xlg label-primary arrowed arrowed-right"><%=Session["budget_project_Column08"]%></span>
							</div>
							<div class="widget-main no-padding">
								<textarea id="BJ_Duration" name="BJ_Duration" data-provide="markdown" data-iconlibrary="fa" rows="2"></textarea>
							</div>
						</div>

                        <div class="btn-group pull-left" style="margin-left:10px;"></div>
                        <div class="widget-body" style="margin-bottom:10px;">
                            <div class="widget-toolbox padding-4 clearfix"><span class="label label-xlg label-primary arrowed arrowed-right"><%=Session["budget_project_Column09"]%></span>
							</div>
							<div class="widget-main no-padding">
								<textarea id="BJ_Amount" name="BJ_Amount" data-provide="markdown" data-iconlibrary="fa" rows="2"></textarea>
							</div>
						</div>

                        <div class="btn-group pull-left" style="margin-left:10px;"></div>
                        <div class="widget-body" style="margin-bottom:10px;">
                            <div class="widget-toolbox padding-4 clearfix"><span class="label label-xlg label-primary arrowed arrowed-right"><%=Session["budget_project_Column10"]%></span>
							</div>
							<div class="widget-main no-padding">
								<textarea id="BJ_Detail" name="BJ_Detail" data-provide="markdown" data-iconlibrary="fa" rows="5"></textarea>
							</div>
						</div>

                        <div class="btn-group pull-left" style="margin-left:10px;"></div>
                        <div class="widget-body" style="margin-bottom:10px;">
                            <div class="widget-toolbox padding-4 clearfix"><span class="label label-xlg label-primary arrowed arrowed-right"><%=Session["budget_project_Column11"]%></span>
							</div>
							<div class="widget-main no-padding">
								<textarea id="BJ_Measure" name="BJ_Measure" data-provide="markdown" data-iconlibrary="fa" rows="5"></textarea>
							</div>
						</div>

                        <div class="btn-group pull-left" style="margin-left:10px;"></div>
                        <div class="widget-body" style="margin-bottom:10px;">
                            <div class="widget-toolbox padding-4 clearfix"><span class="label label-xlg label-primary arrowed arrowed-right"><%=Session["budget_project_Column12"]%></span>
							</div>
							<div class="widget-main no-padding">
								<textarea id="BJ_Benefit" name="BJ_Benefit" data-provide="markdown" data-iconlibrary="fa" rows="5"></textarea>
							</div>
						</div>

                        <div class="btn-group pull-left" style="margin-left:10px;"></div>
                        <div class="widget-body" style="margin-bottom:10px;">
                            <div class="widget-toolbox padding-4 clearfix"><span class="label label-xlg label-primary arrowed arrowed-right"><%=Session["budget_project_Column13"]%></span>
							</div>
							<div class="widget-main no-padding">
								<textarea id="BJ_Responsible" name="BJ_Responsible" data-provide="markdown" data-iconlibrary="fa" rows="5"></textarea>
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
        fnLoad(tmpKeyID);
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
        $scope.fnSave = function () {
            fnConfirmMessage('<%=Session["pop_confirm_budget_project"]%>', '<%=Session["pop_save_budget_project"]%>', fnSave);
        }
        fnGetData($scope, $http);
    }
    function fnSendYes() {
        $http = $tmp_http;
        $scope = $tmp_scope;
        var User_Code = '<%=Session["user_code"]%>';
        var BJ_ID = document.getElementById('BJ_ID').value;
        $('body').pleaseWait();
        var data = $.param({
            Command: 'BudgetProject',
            Function: 'Send',
            BJ_ID: BJ_ID,
            User_Code: User_Code
        });

        $http.post("../server/Server_Budget_Project.aspx", data, config)
        .success(function (data, status, headers, config) {
            document.getElementById('btnConfirm').click();
            fnLoad(tmpKeyID);
        })
        .error(function (data, status, header, config) {
            $('body').pleaseWait('stop');
        });
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
                document.getElementById('BJ_ID').value = data[0].BJ_ID.trim();
                document.getElementById('BJ_Issue').value = data[0].BJ_Issue.trim();
                document.getElementById('BJ_Goal').value = data[0].BJ_Goal.trim();
                document.getElementById('BJ_Strategy').value = data[0].BJ_Strategy.trim();
                document.getElementById('BJ_ProjectName').value = data[0].BJ_ProjectName.trim();
                document.getElementById('BJ_Reason').value = data[0].BJ_Reason.trim();
                document.getElementById('BJ_Objective').value = data[0].BJ_Objective.trim();
                document.getElementById('BJ_Place').value = data[0].BJ_Place.trim();
                document.getElementById('BJ_Duration').value = data[0].BJ_Duration.trim();
                document.getElementById('BJ_Amount').value = data[0].BJ_Amount.trim();
                document.getElementById('BJ_Detail').value = data[0].BJ_Detail.trim();
                document.getElementById('BJ_Measure').value = data[0].BJ_Measure.trim();
                document.getElementById('BJ_Benefit').value = data[0].BJ_Benefit.trim();
                document.getElementById('BJ_Responsible').value = data[0].BJ_Responsible.trim();
                $('body').pleaseWait('stop');
                fnLoadCtrl();
            }
        );

    }

    function fnSave() {
            var BJ_ID = document.getElementById('BJ_ID').value.trim();
            var BJ_Issue = document.getElementById('BJ_Issue').value.trim();
            var BJ_Goal = document.getElementById('BJ_Goal').value.trim();
            var BJ_Strategy = document.getElementById('BJ_Strategy').value.trim();
            var BJ_ProjectName = document.getElementById('BJ_ProjectName').value.trim();
        
            if (BJ_Issue == "") {
                fnErrorMessage("ข้อผิดพลาด / Error", "<%=Session["budget_project_ERROR_02"]%>");
                return;
            }
            if (BJ_Goal == "") {
                fnErrorMessage("ข้อผิดพลาด / Error", "<%=Session["budget_project_ERROR_03"]%>");
                return;
            }
            if (BJ_Strategy == "") {
                fnErrorMessage("ข้อผิดพลาด / Error", "<%=Session["budget_project_ERROR_04"]%>");
                return;
            }
            if (BJ_ProjectName == "") {
                fnErrorMessage("ข้อผิดพลาด / Error", "<%=Session["budget_project_ERROR_05"]%>");
                return;
            }

            var User_Code = '<%=Session["user_code"]%>';

            $.post("../server/Server_Budget_Project.aspx",
			    {
			        Command: 'BudgetProject',
			        Function: 'Check',
			        User_Code: User_Code,
			        BJ_Issue: BJ_Issue,
			        BJ_Goal: BJ_Goal,
			        BJ_Strategy: BJ_Strategy,
			        BJ_ProjectName: BJ_ProjectName
			    },
			    function (data, status) {
			        var data = eval(data);
			        if (data[0].BJ_ID != '') {
			            fnSubmit(data[0].BJ_ID);
			        } else {
			            fnErrorMessage("ข้อผิดพลาด / Error", data[0].message);
			        }
			    }
            );
    }

    function fnErrorMessage(title, message) {
        document.getElementById('img-error').style.display = "";
        document.getElementById('img-information').style.display = "none";
        document.getElementById('popHeader').innerHTML = "<font color='red'>" + title + "</font>";
        document.getElementById('popMessage').innerHTML = message;
        document.getElementById('btnEdit').click();
    }

    function fnSubmit(BJ_ID) {
        var KeyID = tmpKeyID;
        var BJ_Issue = document.getElementById('BJ_Issue').value.trim();
        var BJ_Goal = document.getElementById('BJ_Goal').value.trim();
        var BJ_Strategy = document.getElementById('BJ_Strategy').value.trim();
        var BJ_ProjectName = document.getElementById('BJ_ProjectName').value.trim();

        $.post("../server/Server_Budget_Project.aspx",
           {
               Command: 'BudgetProject',
               Function: 'Save',
               KeyID: KeyID,
               BJ_ID: BJ_ID,
               BJ_Issue: BJ_Issue,
               BJ_Goal: BJ_Goal,
               BJ_Strategy: BJ_Strategy,
               BJ_ProjectName: BJ_ProjectName
           },
           function (data, status) {
               var data = eval(data);
               if (data[0].output == "OK") {
                   document.getElementById('btnConfirm').click();
                   window.parent.fnRefresh();
               } else {
                   fnErrorMessage("ข้อผิดพลาด / Error", data[0].message);
               }
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
           if (data.records.length > 0) {
               tmpKeyID = data.records[0].KeyID;
               setTimeout(fnLoad(tmpKeyID), 100);
           } else {
               $('body').pleaseWait('stop');
           }
           
       })
       .error(function (data, status, header, config) {
           $('body').pleaseWait('stop');
       });

    } 

</script>
</asp:Content>

