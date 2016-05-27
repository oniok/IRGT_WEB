<%@ Page Title="" Language="C#" MasterPageFile="~/master_page/popup.master" AutoEventWireup="true" CodeFile="pop_BudgetProjectSummary.aspx.cs" Inherits="budget_popup_pop_BudgetProjectSummary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script src="../Scripts/angular.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <center>
    <h3 class="header smaller lighter blue"><%=Session["HeaderText"]%></h3>	
    <div class="widget-body" style="margin-bottom:10px;">
		<div class="widget-main no-padding">
			<textarea id="BJ_Summary" name="BJ_Summary" data-provide="markdown" data-iconlibrary="fa" rows="50"></textarea>
		</div>
	</div>
    </center>
    <script>
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

            fnGetBudgetProjectSummary($scope, $http);
        }
        function fnGetBudgetOperationSummary($scope, $http) {
            var User_Code = '<%=Session["user_code"]%>';
            var Lang = '<%=Session["language_budget_project"]%>';
            $scope = $tmp_scope;
            $http = $tmp_http;

            var data = $.param({
                Command: 'BudgetProjectSummary',
                Function: Function,
                User_Code: User_Code,
                Lang: Lang
            });

            $http.post("../server/Server_Budget_Project.aspx", data, config)
            .success(function (data, status, headers, config) {
                if (data.records.length > 0) {
                    document.getElementById('BJ_Summary').value = data[0].BJ_Summary.trim();
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

