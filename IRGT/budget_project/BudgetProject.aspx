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
								</div>
							</div>
						</div>

                        <div class="widget-body" style="margin-bottom:10px;margin-top:10px;">
                            <div class="clearfix"><input type="hidden" id="BJ_ID"/><span class="label label-lg label-purple arrowed-right"><%=Session["budget_project_Column01"]%></span></div>
                            <input type="text" id="BJ_Issue" style="width:100%"/>
                        </div>

                        <div class="widget-body" style="margin-bottom:10px;">
                            <div class="clearfix"><span class="label label-lg label-purple arrowed-right"><%=Session["budget_project_Column02"]%></span></div>
                            <input type="text" id="BJ_Goal" style="width:100%"/>
                        </div>

                        <div class="widget-body" style="margin-bottom:10px;">
                            <div class="clearfix"><span class="label label-lg label-purple arrowed-right"><%=Session["budget_project_Column03"]%></span></div>
                            <input type="text" id="BJ_Strategy" style="width:100%"/>
                        </div>

                        <div class="widget-body" style="margin-bottom:10px;">
                            <div class="clearfix"><span class="label label-lg label-purple arrowed-right"><%=Session["budget_project_Column04"]%></span></div>
                            <input type="text" id="BJ_ProjectName" style="width:100%"/>
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
                            <input type="text" id="BJ_Duration" style="width:100%"/>
                        </div>

                        <div class="widget-body" style="margin-bottom:10px;">
                            <div class="clearfix"><span class="label label-lg label-purple arrowed-right"><%=Session["budget_project_Column09"]%></span></div>
                            <input type="text" id="BJ_Amount" style="width:100%"/>
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

       
        $scope.fnSave = function () {
            fnConfirmMessage('<%=Session["pop_confirm_budget_project"]%>', '<%=Session["pop_save_budget_project"]%>', fnSave);
        }
        $scope.fnSum = function () {
            $('#btnPopSave').toggle(false);
            fnOpenPopup('<%=Session["pop_sum_budget_project"]%>', "../budget_project_popup/pop_BudgetProjectSummary.aspx?KeyID=" + tmpKeyID, null, "960");
        }
        $scope.fnSend = function () {
            fnConfirmMessage('<%=Session["pop_confirm_budget_project"]%>', '<%=Session["pop_send_budget_project"]%>', fnSendYes);
        }
        fnGetData($scope, $http);
    }
    
    function fnLoad(KeyID) {
        var User_Code = '<%=Session["user_code"]%>';
        var lang = '<%=Session["language_budget_project"]%>';
        $http = $tmp_http;
        $scope = $tmp_scope;

        
        var data = $.param({
            Command: 'BudgetProject',
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
                $('#BJ_Amount').val(data.records[0].BJ_Amount.trim());
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
            fnGetData($scope, $http);
            //location.reload();
        })
        .error(function (data, status, header, config) {
            $('body').pleaseWait('stop');
        });
    }

    function fnSave() {
            var BJ_ID = document.getElementById('BJ_ID').value.trim();
            var BJ_Issue = $('#BJ_Issue').val();
            var BJ_Goal = $('#BJ_Goal').val();
            var BJ_Strategy = $('#BJ_Strategy').val();
            var BJ_ProjectName = $('#BJ_ProjectName').val();

            var BJ_Reason = $('#BJ_Reason').html();
            var BJ_Objective = $('#BJ_Objective').html();
            var BJ_Place = $('#BJ_Place').html();
            var BJ_Duration = $('#BJ_Duration').val();
            var BJ_Amount = $('#BJ_Amount').val();
            var BJ_Detail = $('#BJ_Detail').html();
            var BJ_Measure = $('#BJ_Measure').html();
            var BJ_Benefit = $('#BJ_Benefit').html();
            var BJ_Responsible = $('#BJ_Responsible').html();
        
            if (BJ_Issue == "") {
                fnErrorMessage("ข้อผิดพลาด / Error", "<%=Session["budget_project_ERROR_01"]%>");
                return;
            }
            if (BJ_Goal == "") {
                    fnErrorMessage("ข้อผิดพลาด / Error", "<%=Session["budget_project_ERROR_02"]%>");
                    return;
            }
            if (BJ_Strategy == "") {
                    fnErrorMessage("ข้อผิดพลาด / Error", "<%=Session["budget_project_ERROR_03"]%>");
                    return;
            }
            if (BJ_ProjectName == "") {
                    fnErrorMessage("ข้อผิดพลาด / Error", "<%=Session["budget_project_ERROR_04"]%>");
                    return;
            }
            if (BJ_Reason == "") {
                    fnErrorMessage("ข้อผิดพลาด / Error", "<%=Session["budget_project_ERROR_05"]%>");
                    return;
            }
            if (BJ_Objective == "") {
                    fnErrorMessage("ข้อผิดพลาด / Error", "<%=Session["budget_project_ERROR_06"]%>");
                    return;
            }
            if (BJ_Place == "") {
                    fnErrorMessage("ข้อผิดพลาด / Error", "<%=Session["budget_project_ERROR_07"]%>");
                    return;
            }
            if (BJ_Duration == "") {
                    fnErrorMessage("ข้อผิดพลาด / Error", "<%=Session["budget_project_ERROR_08"]%>");
                    return;
            }
            if (BJ_Amount == "") {
                    fnErrorMessage("ข้อผิดพลาด / Error", "<%=Session["budget_project_ERROR_09"]%>");
                    return;
            }
            if (BJ_Detail == "") {
                    fnErrorMessage("ข้อผิดพลาด / Error", "<%=Session["budget_project_ERROR_10"]%>");
                    return;
            }
            if (BJ_Measure == "") {
                    fnErrorMessage("ข้อผิดพลาด / Error", "<%=Session["budget_project_ERROR_11"]%>");
                    return;
            }
            if (BJ_Benefit == "") {
                    fnErrorMessage("ข้อผิดพลาด / Error", "<%=Session["budget_project_ERROR_12"]%>");
                    return;
            }
            if (BJ_Responsible == "") {
                    fnErrorMessage("ข้อผิดพลาด / Error", "<%=Session["budget_project_ERROR_13"]%>");
                    return;
            }
        
            var User_Code = '<%=Session["user_code"]%>';

            $.post("../server/Server_Budget_Project.aspx",
			    {
			        Command: 'BudgetProject',
			        Function: 'Check',
			        User_Code: User_Code
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

    function fnSubmit(BJ_ID) {
        var KeyID = tmpKeyID;
        var $http = $tmp_http;
        var $scope = $tmp_scope;


        var BJ_Issue = $('#BJ_Issue').val();
        var BJ_Goal = $('#BJ_Goal').val();
        var BJ_Strategy = $('#BJ_Strategy').val();
        var BJ_ProjectName = $('#BJ_ProjectName').val();

        var BJ_Reason = $('#BJ_Reason').html();
        var BJ_Objective = $('#BJ_Objective').html();
        var BJ_Place = $('#BJ_Place').html();
        var BJ_Duration = $('#BJ_Duration').val();
        var BJ_Amount = $('#BJ_Amount').val();
        var BJ_Detail = $('#BJ_Detail').html();
        var BJ_Measure = $('#BJ_Measure').html();
        var BJ_Benefit = $('#BJ_Benefit').html();
        var BJ_Responsible = $('#BJ_Responsible').html();

        var data = $.param({
            Command: 'BudgetProject',
            Function: 'Save',
            KeyID: KeyID,
            BJ_ID: BJ_ID,
            BJ_Issue: BJ_Issue,
            BJ_Goal: BJ_Goal,
            BJ_Strategy: BJ_Strategy,
            BJ_ProjectName: BJ_ProjectName,
            BJ_Reason: BJ_Reason,
            BJ_Objective: BJ_Objective,
            BJ_Place: BJ_Place,
            BJ_Duration: BJ_Duration,
            BJ_Amount: BJ_Amount,
            BJ_Detail: BJ_Detail,
            BJ_Measure: BJ_Measure,
            BJ_Benefit: BJ_Benefit,
            BJ_Responsible: BJ_Responsible
        });
        /*
        $http.post("../server/Server_Budget_Project.aspx", data, config)
        .success(function (data, status, headers, config) {
            $scope.Data = data.records;
            if (data.records.length > 0) {
                if (data[0].output == "OK") {
                    fnGetData($scope, $http);

                    //document.getElementById('btnConfirm').click();
                    //fnRefresh();
                } else {
                    fnErrorMessage("ข้อผิดพลาด / Error", data[0].message);
                }
            } else {
                $('body').pleaseWait('stop');
                fnRefresh();
            }
        })
        .error(function (data, status, header, config) {
            $('body').pleaseWait('stop');
            fnRefresh();
        });
        */
        $.ajax({
            type: 'POST',
            url: "../server/Server_Budget_Project.aspx",
            data:data,
            success: function (data) {
                var data = eval(data);
                if (data[0].output == "OK") {
                    //document.getElementById('btnConfirm').click();
                    document.getElementById('popMessage').innerHTML = "<%=Session["info_success_budget_project"]%>";
                        $('#div_footerYN').hide();
                        $('#div_footerOK').show();
                       //fnGetData($tmp_scope, $tmp_http);
                } else {
                       fnErrorMessage("ข้อผิดพลาด / Error", data[0].message);
                }
            },
            error: function (req, status, error) {
                fnErrorMessage("ข้อผิดพลาด / Error", data[0].message);
            }
        });
    }

    function fnClearState() {
        $('#div_footerYN').show();
        $('#div_footerOK').hide();
        document.getElementById('btnConfirm').click();
    }
    
    function fnErrorMessage(title, message) {
        document.getElementById('popError').innerHTML = "<font color='navy'>" + title + "</font>";
        document.getElementById('popMessage').innerHTML = message;
        $('#div_footerYN').hide();
        $('#div_footerOK').show();
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
           } else {
               tmpKeyID = 0;
           }
           setTimeout(fnRefresh(), 100);
        })
        .error(function (data, status, header, config) {
           $('body').pleaseWait('stop');
        });

    } 

</script>
</asp:Content>

