<%@ Page Title="" Language="C#" MasterPageFile="~/master_page/popup.master" AutoEventWireup="true" CodeFile="pop_BudgetOperationSummary.aspx.cs" Inherits="budget_popup_pop_BudgetOperationSummary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script src="../Scripts/angular.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <center>
    <table id="dynamic-table" class="table table-striped table-bordered table-hover" ng-app="myApp" ng-controller="fnMain">
		<thead>
			<tr>
				<th class="center" style="width:50px"><%=Session["budget_operation_summary_ColumnSEQ"]%></th>
				<th class="center"><%=Session["budget_operation_summary_Column02"]%></th>
                <th class="center" style="width:50px"><%=Session["budget_operation_summary_Column03"]%></th>          
                <th class="center" style="width:100px"><%=Session["budget_operation_summary_Column04"]%></th> 
                <th class="center"><%=Session["budget_operation_summary_Column05"]%></th> 
			</tr>
		</thead>
        <tbody>
			<tr ng-repeat="x in DataSum">
				<td class="center">{{ x.RowID }}</td>
                <td>{{ x.BO_Type_Name }}</td>
                <td style="text-align:right">{{ x.BO_PRICE_MNT }}</td>          
                <td style="text-align:right">{{ x.BO_PRICE_YEAR }}</td>											
				<td><input type="text" id="BO_Remark{{x.RowID}}" style="width:100%" value="{{ x.BO_Remark }}"/>   </td>										
			</tr>										
		</tbody>  
        <tfoot >
      
        </tfoot>                                              
	</table>   
    </center>
    <script>
        function fnSave() {
            var User_Code = '<%=Session["user_code"]%>';
            var Lang = '<%=Session["language_budget_operation"]%>';

            $scope = $tmp_scope;
            $http = $tmp_http;

            var data = $.param({
                Command: 'BudgetOperationSummary',
                Function: 'Select',
                User_Code: User_Code,
                Lang: Lang
            });

            $http.post("../server/Server_Budget_Operation.aspx", data, config)
            .success(function (data, status, headers, config) {
                if (data!="") {
                    for (var i = 0; i < data.records.length; i++) {
                        fnSubmit(data.records[i].KeyID, data.records[i].BO_ID, data.records[i].BO_Type_ID, data.records[i].RowID);
                    }
                    setTimeout(window.parent.fnRefresh(), 1000);
                }
                window.parent.fnRefresh();
            })
            .error(function (data, status, header, config) {
                fnErrorMessage("ข้อผิดพลาด / Error", data[0].message);
            });

        }

        function fnSubmit(KeyID, BO_ID, BO_Type_ID, RowID) {
            var BO_Remark = '';
            if ($('#BO_Remark' + RowID).val() != null) {
                BO_Remark = $('#BO_Remark' + RowID).val();
            }

            $.post("../server/Server_Budget_Operation.aspx",
               {
                   Command: 'BudgetOperationSummary',
                   Function: 'Save',
                   KeyID: KeyID,
                   BO_ID: BO_ID,
                   BO_Type_ID: BO_Type_ID,
                   BO_Remark: BO_Remark
               },
               function (data, status) {
                   var data = eval(data);
                   if (data[0].output == "OK") {
                       window.parent.fnRefresh();
                   } else {
                       fnErrorMessage("ข้อผิดพลาด / Error", data[0].message);
                   }
               }
           );
        }

        
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

            $scope.fnEdit = function (KeyID) {           //,BO_ID,BO_Type_ID
               fnOpenPopup('<%=Session["pop_sum_budget_operation"]%>', "../pop_BudgetOperationRemark.aspx?KeyID=" + KeyID, null, "450");
            }

            fnGetBudgetOperationSummary($scope, $http);
        }
        function fnGetBudgetOperationSummary($scope, $http) {
            var User_Code = '<%=Session["user_code"]%>';
            var Lang = '<%=Session["language_budget_operation"]%>';
            $scope = $tmp_scope;
            $http = $tmp_http;

            var data = $.param({
                Command: 'BudgetOperationSummary',
                Function: 'Select',
                User_Code: User_Code,
                Lang: Lang
            });

            $http.post("../server/Server_Budget_Operation.aspx", data, config)
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

