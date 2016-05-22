<%@ Page Title="" Language="C#" MasterPageFile="~/master_page/popup.master" AutoEventWireup="true" CodeFile="pop_BudgetPosition.aspx.cs" Inherits="budget_popup_pop_BudgetPosition" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script src="../Scripts/angular.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <center>
    <table style="width:600px" ng-app="myApp" ng-controller="fnMain">
        <tr>
            <tr><td colspan="4" style="height:10px"></td></tr>
            <tr>
                <td><%=Session["budget_operation_Column01"] %></td>
                <td colspan="3">
                    <input type="hidden" id="BO_ID"/>
                    <input type="text" id="BO_Name" style="width:100%"/>
                </td>
            </tr>
            <tr><td colspan="4" style="height:10px"></td></tr>
            <tr>
                <td style="width:100px"><%=Session["budget_operation_Column02"] %></td>
                <td colspan="3">
                    <select class="chosen-select form-control" id="BO_Type_ID" data-placeholder="<%=Session["search_placeholder"] %>">
					    <option value=""></option>	
                        <option ng-repeat="x in BudgetOperationType" value="{{ x.Code }}" >{{ x.Name }}</option>				
				    </select>   
                </td>
             </tr>    
        <tr><td colspan="4" style="height:10px"></td></tr>
        <tr>
            <td><%=Session["budget_operation_Column03"] %></td>
            <td colspan="3">
                <input type="text" id="BO_Qty" />
            </td>
        </tr>
        <tr><td colspan="4" style="height:10px"></td></tr>
        <tr>
            <td><%=Session["budget_operation_Column04"] %></td>
            <td colspan="3">
                <input type="text" id="BO_Price" />    
            </td>
        </tr>        
        <tr><td colspan="4" style="height:10px"></td></tr>
        <tr>
            <td><%=Session["budget_operation_Column05"] %></td>
            <td colspan="3">
                <input type="text" id="BO_Reason" style="width:100%"/>    
            </td>
        </tr>
    </table>   
    </center>
    <script>
        function fnLoad() {
            var KeyID = getParamValue("KeyID");
            if (KeyID != null) {
                $.post("../server/Server_Budget_Position.aspx",
                    {
                        Command: 'BudgetOperation',
                        Function: 'Load',
                        KeyID: KeyID
                    },
                    function (data, status) {
                        var data = eval(data);
                        document.getElementById('BO_ID').value = data[0].BO_ID.trim();
                        document.getElementById('BO_Name').value = data[0].BO_Name.trim();
                        document.getElementById('BO_Type_ID').value = data[0].BO_Type_ID.trim();
                        document.getElementById('BO_Qty').value = data[0].BO_Qty.trim();
                        document.getElementById('BO_Price').value = data[0].BO_Price.trim();
                        document.getElementById('BO_Reason').value = data[0].BO_Reason.trim();

                        $('body').pleaseWait('stop');
                        fnLoadCtrl();
                    }
                );
            } else {
                $('body').pleaseWait('stop');
                fnLoadCtrl();
            }

        }
        function fnSave() {
            var KeyID = getParamValue("KeyID");
            var BO_Name = document.getElementById('BO_Name').value.trim();
            var BO_Type_ID = document.getElementById('BO_Type_ID').value.trim();
            var BO_Qty = document.getElementById('BO_Qty').value.trim();
            var BO_Price = document.getElementById('BO_Price').value;
        
            if (BO_Name == "") {
                fnErrorMessage("ข้อผิดพลาด / Error", "<%=Session["budget_operation_ERROR_02"]%>");
                return;
            }
            if (BO_Type_ID == "") {
                fnErrorMessage("ข้อผิดพลาด / Error", "<%=Session["budget_operation_ERROR_03"]%>");
                return;
            }
            if (BO_Qty == "") {
                fnErrorMessage("ข้อผิดพลาด / Error", "<%=Session["budget_operation_ERROR_04"]%>");
                return;
            }
            if (BO_Price == "") {
                fnErrorMessage("ข้อผิดพลาด / Error", "<%=Session["budget_operation_ERROR_05"]%>");
                return;
            }

            var BO_Type_ID = document.getElementById('BO_Type_ID').value;
            var User_Code = '<%=Session["user_code"]%>';

            $.post("../server/Server_Budget_Position.aspx",
			    {
			        Command: 'BudgetOperation',
			        Function: 'Check',
			        User_Code: User_Code
			    },
			    function (data, status) {
			        var data = eval(data);
			        if (data[0].BO_ID != '') {
			            fnSubmit(data[0].BO_ID);
			        } else {
			            fnErrorMessage("ข้อผิดพลาด / Error", data[0].message);
			        }
			    }
            );
        }

        function fnSubmit(BO_ID) {
            var KeyID = getParamValue("KeyID");
            var BO_Name = document.getElementById('BO_Name').value.trim();
            var BO_Type_ID = document.getElementById('BO_Type_ID').value.trim();
            var BO_Qty = document.getElementById('BO_Qty').value.trim();
            var BO_Price = document.getElementById('BO_Price').value;
            var BO_Reason = document.getElementById('BO_Reason').value;

            $.post("../server/Server_Budget_Position.aspx",
               {
                   Command: 'BudgetOperation',
                   Function: 'Save',
                   KeyID: KeyID,
                   BO_ID: BO_ID,
                   BO_Name: BO_Name,
                   BO_Type_ID: BO_Type_ID,
                   BO_Qty: BO_Qty,
                   BO_Price: BO_Price,
                   BO_Reason: BO_Reason
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
            fnGetBudgetOperationType($scope, $http);
        }
        function fnGetBudgetOperationType($scope, $http) {

            $scope = $tmp_scope;
            $http = $tmp_http;

            var data = $.param({
                Command: 'GetMasterData',
                Function: 'BudgetOperationType',
                PageName: 'budget_operation'
            });

            $http.post("../server/Server_Budget_Position.aspx", data, config)
            .success(function (data, status, headers, config) {
                $scope.BudgetOperationType = data.records;
                setTimeout(fnLoad, 100);
            })
            .error(function (data, status, header, config) {
                $('body').pleaseWait('stop');
            });
        }
       
    </script>
    
</asp:Content>

