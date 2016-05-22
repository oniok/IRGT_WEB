<%@ Page Title="" Language="C#" MasterPageFile="~/master_page/popup.master" AutoEventWireup="true" CodeFile="pop_BudgetPositionSummary.aspx.cs" Inherits="budget_popup_pop_BudgetPositionSummary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script src="../Scripts/angular.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <center>
    <table id="dynamic-table" class="table table-striped table-bordered table-hover" ng-app="myApp" ng-controller="fnMain">
		<thead>
			<tr>
				<th class="center" style="width:50px"><%=Session["budget_position_summary_ColumnSEQ"]%></th>
				<th class="center"><%=Session["budget_position_summary_Column01"]%></th>
				<th class="center" style="width:100px"><%=Session["budget_position_summary_Column02"]%></th>
                <th class="center" style="width:150px"><%=Session["budget_position_summary_Column03"]%></th>          
                <th class="center" style="width:80px"><%=Session["budget_position_summary_Column04"]%></th>    
                <th class="center" style="width:120px"><%=Session["budget_position_summary_Column05"]%></th>
                <th class="center"><%=Session["budget_position_summary_Column06"]%></th>
			</tr>
		</thead>
        <tbody>
			<tr ng-repeat="x in DataSum">
				<td class="center">{{ x.RowID }}</td>
                <td>{{ x.Position_Type_Name }}</td>
                <td>{{ x.Educate_Type_Name }}</td>
                <td>{{ x.BP_Type_Name }}</td>
                <td style="text-align:right">{{ x.BP_PRICE_MNT }}</td>          
                <td style="text-align:right">{{ x.BP_PRICE_YEAR }}</td>														                                                            
                <td><input type="text" id="BP_Reason" style="width:100%" value="{{ x.BP_Remark }}"/>   </td>
														
			</tr>										
		</tbody>  
        <tfoot >
      
        </tfoot>                                              
	</table>   
    </center>
    <script>
        function fnSave() {
            var KeyID = getParamValue("KeyID");
            var Position_Type_ID = document.getElementById('Position_Type_ID').value.trim();
            var Educate_Type_ID = document.getElementById('Educate_Type_ID').value.trim();
            var BP_Type_ID = document.getElementById('BP_Type_ID').value.trim();
            var BP_Qty = document.getElementById('BP_Qty').value.trim();
            var BP_Price = document.getElementById('BP_Price').value.trim();
            var BP_Remark = document.getElementById('BP_Remark').value.trim();
            
            if (BP_Type_ID == "") {
                fnErrorMessage("ข้อผิดพลาด / Error", "<%=Session["budget_position_ERROR_03"]%>");
                return;
            }
            if (BP_Qty == "") {
                fnErrorMessage("ข้อผิดพลาด / Error", "<%=Session["budget_position_ERROR_04"]%>");
                return;
            }
            if (BP_Price == "") {
                fnErrorMessage("ข้อผิดพลาด / Error", "<%=Session["budget_position_ERROR_05"]%>");
                return;
            }
            if (BP_Remark == "") {
                fnErrorMessage("ข้อผิดพลาด / Error", "<%=Session["budget_position_ERROR_05"]%>");
                return;
            }

            var User_Code = '<%=Session["user_code"]%>';

            $.post("../server/Server_Budget_Position.aspx",
			    {
			        Command: 'BudgetPositionSummary',
			        Function: 'Check',
			        User_Code: User_Code
			    },
			    function (data, status) {
			        var data = eval(data);
			        if (data[0].BP_ID != '') {
			            fnSubmit(data[0].BP_ID);
			        } else {
			            fnErrorMessage("ข้อผิดพลาด / Error", data[0].message);
			        }
			    }
            );
        }

        function fnSubmit(BP_ID) {
            var KeyID = getParamValue("KeyID");
            var Position_Type_ID = document.getElementById('Position_Type_ID').value.trim();
            var Educate_Type_ID = document.getElementById('Educate_Type_ID').value.trim();
            var BP_Type_ID = document.getElementById('BP_Type_ID').value.trim();
            var BP_Qty = document.getElementById('BP_Qty').value.trim();
            var BP_Price = document.getElementById('BP_Price').value.trim();
            var BP_Remark = document.getElementById('BP_Remark').value.trim();

            $.post("../server/Server_Budget_Position.aspx",
               {
                   Command: 'BudgetPositionSummary',
                   Function: 'Save',
                   KeyID: KeyID,
                   BP_ID: BP_ID,
                   Position_Type_ID: Position_Type_ID,
                   Educate_Type_ID: Educate_Type_ID,
                   Educate_Type_ID: Educate_Type_ID,
                   BP_Qty: BP_Qty,
                   BP_Price: BP_Price,
                   BP_Remark: BP_Remark
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

            $scope.fnEdit = function (KeyID) {           
               fnOpenPopup('<%=Session["pop_sum_budget_position"]%>', "../pop_BudgetOperationRemark.aspx?KeyID=" + KeyID, null, "450");
            }

            fnGetBudgetPositionSummary($scope, $http);
        }
        function fnGetBudgetPositionSummary($scope, $http) {
            var User_Code = '<%=Session["user_code"]%>';
            var Lang = '<%=Session["language_budget_position"]%>';
            $scope = $tmp_scope;
            $http = $tmp_http;

            var data = $.param({
                Command: 'BudgetPositionSummary',
                Function: Function,
                User_Code: User_Code,
                Lang: Lang
            });

            $http.post("../server/Server_Budget_Position.aspx", data, config)
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

