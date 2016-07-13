<%@ Page Title="" Language="C#" MasterPageFile="~/master_page/main2.master" AutoEventWireup="true" CodeFile="BudgetProjectByID.aspx.cs" Inherits="budget_BudgetProjectByID" %>

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
                                            <button class="btn btn-white btn-sm" type="button" ng-click="fnSum()">
												<i class="ace-icon glyphicon glyphicon-file bigger-120"></i>
												<%=Session["sum_button"]%>
											</button>
                                        </div>											
									</div>               
								</div>
							</div>
						</div>

                        <div class="widget-body" style="margin-bottom:10px;margin-top:10px;">
                            <div class="clearfix"><input type="hidden" id="BJ_ID"/><span class="label label-lg label-purple arrowed-right"><%=Session["budget_project_Column01"]%></span></div>
                            <input  disabled type="text" id="BJ_Issue" style="width:100%"/>
                        </div>

                        <div class="widget-body" style="margin-bottom:10px;">
                            <div class="clearfix"><span class="label label-lg label-purple arrowed-right"><%=Session["budget_project_Column02"]%></span></div>
                            <input  disabled type="text" id="BJ_Goal" style="width:100%"/>
                        </div>

                        <div class="widget-body" style="margin-bottom:10px;">
                            <div class="clearfix"><span class="label label-lg label-purple arrowed-right"><%=Session["budget_project_Column03"]%></span></div>
                            <input  disabled type="text" id="BJ_Strategy" style="width:100%"/>
                        </div>

                        <div class="widget-body" style="margin-bottom:10px;">
                            <div class="clearfix"><span class="label label-lg label-purple arrowed-right"><%=Session["budget_project_Column04"]%></span></div>
                            <input  disabled type="text" id="BJ_ProjectName" style="width:100%"/>
                        </div>

                        <div class="widget-body" style="margin-bottom:10px;">
                            <div class="clearfix"><span class="label label-lg label-purple arrowed-right"><%=Session["budget_project_Column05"]%></span></div>
                            <div class="wysiwyg-editor" id="BJ_Reason"></div>
                        </div>

                        <div class="widget-body" style="margin-bottom:10px;">
                            <div class="clearfix"><span class="label label-lg label-purple arrowed-right"><%=Session["budget_project_Column06"]%></span></div>
                            <div class="wysiwyg-editor" id="BJ_Objective"></div>
                        </div>

                        <div class="widget-body" style="margin-bottom:10px;">
                            <div class="clearfix"><span class="label label-lg label-purple arrowed-right"><%=Session["budget_project_Column07"]%></span></div>
                            <div class="wysiwyg-editor" id="BJ_Place"></div>
                        </div>

                        <div class="widget-body" style="margin-bottom:10px;">
                            <div class="clearfix"><span class="label label-lg label-purple arrowed-right"><%=Session["budget_project_Column08"]%></span></div>
                            <input  disabled type="text" id="BJ_Duration" style="width:100%"/>
                        </div>

                        <div class="widget-body" style="margin-bottom:10px;">
                            <div class="clearfix"><span class="label label-lg label-purple arrowed-right"><%=Session["budget_project_Column09"]%></span></div>
                            <input  disabled type="text" id="BJ_Amount" style="width:100%"/>
                        </div>

                        <div class="widget-body" style="margin-bottom:10px;">
                            <div class="clearfix"><span class="label label-lg label-purple arrowed-right"><%=Session["budget_project_Column10"]%></span></div>
                            <div class="wysiwyg-editor" id="BJ_Detail"></div>
                        </div>

                        <div class="widget-body" style="margin-bottom:10px;">
                            <div class="clearfix"><span class="label label-lg label-purple arrowed-right"><%=Session["budget_project_Column11"]%></span></div>
                            <div class="wysiwyg-editor" id="BJ_Measure"></div>
                        </div>

                        <div class="widget-body" style="margin-bottom:10px;">
                            <div class="clearfix"><span class="label label-lg label-purple arrowed-right"><%=Session["budget_project_Column12"]%></span></div>
                            <div class="wysiwyg-editor" id="BJ_Benefit"></div>
                        </div>

                        <div class="widget-body" style="margin-bottom:10px;">
                            <div class="clearfix"><span class="label label-lg label-purple arrowed-right"><%=Session["budget_project_Column13"]%></span></div>
                            <div class="wysiwyg-editor" id="BJ_Responsible"></div>
                        </div>

                        <a id="btnEdit2" href="#modal-profile-edit" class="btn btn-primary" data-toggle="modal" style="display:none">แก้ไขข้อมูล(ถ้ามี)</a>
                        <div class="modal fade" id="modal-profile-edit2" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
			                <div class="modal-dialog modal-lg">
				                <div class="modal-content" id="pop-content2">
					                <div class="modal-header">
                                        <table>
                                            <tr>
                                                <td>
                                                    <i id="img-error2" class="ace-icon fa fa-exclamation-circle icon-only fa-lg red"></i>
                                                    <i id="img-information2" class="ace-icon fa fa-exclamation-circle icon-only fa-lg blue"></i>
                                                </td>
                                                <td style="width:10px"></td>
                                                <td><h4 class="modal-title" id="popHeader2"></h4></td>
                                            </tr>
                                        </table>                	
					                </div>
					                <div class="modal-body">
					                    <center><label id="popMessage2"></label></center>						
					                </div>	
                                    <div class="modal-footer">							 
						                <button type="button" class="btn btn-info" data-dismiss="modal">
							                OK
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

        $scope.fnSum = function () {
            var BJ_ID = '<%=Session["BJ_ID"]%>';
            $('#btnPopSave').toggle(false);
            $('#btnPopPrint').toggle(true);
            fnOpenPopup('<%=Session["pop_sum_budget_project"]%>', "../budget_project_popup/pop_BudgetProjectSummaryByID.aspx?BJ_ID=" + BJ_ID, null, "960");
        }
       
        fnGetData($scope, $http);
    }
    
    function fnLoad(KeyID) {
        var User_Code = '<%=Session["user_code"]%>';
        var lang = '<%=Session["language_budget_project"]%>';
        $http = $tmp_http;
        $scope = $tmp_scope;

        
        var data = $.param({
            Command: 'BudgetProjectByID',
            Function: 'Load',
            KeyID: KeyID,
            lang: lang
        });

        $http.post("../server/Server_Budget_Project.aspx", data, config)
        .success(function (data, status, headers, config) {
            $scope.Data = data.records;
            if (data.records.length > 0) {
                document.getElementById('BJ_ID').value = data.records[0].BJ_ID.trim();
                $('#BJ_Issue').val(data.records[0].BJ_Issue.trim());
                $('#BJ_Goal').val(data.records[0].BJ_Goal.trim());
                $('#BJ_Strategy').val(data.records[0].BJ_Strategy.trim());
                $('#BJ_ProjectName').val(data.records[0].BJ_ProjectName.trim());
                $('#BJ_Reason').html(data.records[0].BJ_Reason.trim());
                $('#BJ_Objective').html(data.records[0].BJ_Objective.trim());
                $('#BJ_Place').html(data.records[0].BJ_Place.trim());
                $('#BJ_Duration').val(data.records[0].BJ_Duration.trim());
                $('#BJ_Amount').val((data.records[0].BJ_Amount).toString().trim());
                $('#BJ_Detail').html(data.records[0].BJ_Detail.trim());
                $('#BJ_Measure').html(data.records[0].BJ_Measure.trim());
                $('#BJ_Benefit').html(data.records[0].BJ_Benefit.trim());
                $('#BJ_Responsible').html(data.records[0].BJ_Responsible.trim());
                $('body').pleaseWait('stop');
            } else {
                $('body').pleaseWait('stop');
            }
        })
        .error(function (data, status, header, config) {
            $('body').pleaseWait('stop');
        });


        setEditorReadonly('BJ_Reason');
        setEditorReadonly('BJ_Objective');
        setEditorReadonly('BJ_Place');
        setEditorReadonly('BJ_Detail');
        setEditorReadonly('BJ_Measure');
        setEditorReadonly('BJ_Benefit');
        setEditorReadonly('BJ_Responsible');

        $(".wysiwyg-editor").css("background-color", "#DDD");
        $(".wysiwyg-toolbar").css("display", "none");

    }

    function setEditorReadonly(editor_name) {
        $('#' + editor_name).prop("contenteditable", false);
    }
    
    function fnGetData($scope, $http) {
        var BJ_ID = '<%=Session["BJ_ID"]%>';
        var lang = '<%=Session["language_budget_project"]%>';

        var data = $.param({
            Command: 'BudgetProjectByID',
            Function: 'Select',
            BJ_ID: BJ_ID,
            lang: lang
        });

        $http.post("../server/Server_Budget_Project.aspx", data, config)
        .success(function (data, status, headers, config) {
           $scope.Data = data.records;
           if (data.records.length > 0) {
               tmpKeyID = data.records[0].KeyID;
               setTimeout(fnRefresh(), 100);
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

