<%@ Page Title="" Language="C#" MasterPageFile="~/master_page/popup.master" AutoEventWireup="true" CodeFile="pop_BudgetProjectSummaryByID.aspx.cs" Inherits="budget_popup_pop_BudgetProjectSummaryByID" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script src="../Scripts/angular.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <center>
    <table id="dynamic-table" class="table table-striped table-bordered table-hover" ng-app="myApp" ng-controller="fnMain">
		<thead>
			<tr>
				<th class="center" style="width:50px"><%=Session["budget_project_summary_ColumnSEQ"]%></th>
				<th class="center"><%=Session["budget_project_summary_Column02"]%></th>
                <th class="center" style="width:50px"><%=Session["budget_project_summary_Column03"]%></th>          
                <th class="center" style="width:100px"><%=Session["budget_project_summary_Column04"]%></th> 
                <th class="center"><%=Session["budget_project_summary_Column05"]%></th> 
			</tr>
		</thead>
        <tbody>
			<tr ng-repeat="x in DataSum">
				<td class="center">{{ x.RowID }}</td>
                <td>{{ x.BR_Type_Name }}</td>
                <td style="text-align:right">{{ x.BO_PRICE_MNT }}</td>          
                <td style="text-align:right">{{ x.BO_PRICE_YEAR }}</td>		
                <td style="text-align:left">{{ x.BO_Remark }}</td>	
			</tr>										
		</tbody>  
        <tfoot >
      
        </tfoot>                                              
	</table>   
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
       
            fnGetBudgetOperationSummary($scope, $http);
        }
        function fnGetBudgetOperationSummary($scope, $http) {
            var BR_ID = '<%=Session["BR_ID"]%>';
            var Lang = '<%=Session["language_budget_project"]%>';
            $scope = $tmp_scope;
            $http = $tmp_http;

            var data = $.param({
                Command: 'BudgetOperationSummaryByID',
                Function: Function,
                BR_ID: BR_ID,
                Lang: Lang
            });

            $http.post("../server/Server_Budget_Project.aspx", data, config)
            .success(function (data, status, headers, config) {
                $scope.DataSum = data.records;
                $('body').pleaseWait('stop');
            })
            .error(function (data, status, header, config) {
                $('body').pleaseWait('stop');
            });
        }
        
    </script>
    
</asp:Content>

