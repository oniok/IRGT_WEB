<%@ Page Title="" Language="C#" MasterPageFile="~/master_page/popup.master" AutoEventWireup="true" CodeFile="pop_BudgetOperation.aspx.cs" Inherits="budget_popup_pop_BudgetOperation" %>

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
                    <input type="text" id="Budget_Name" />%
                </td>
            </tr>
            <tr><td colspan="4" style="height:10px"></td></tr>
            <tr>
                <td><%=Session["budget_operation_Column02"] %></td>
                <td colspan="3">
                    <select class="chosen-select form-control" id="Budget_Type" data-placeholder="<%=Session["text_placeholder"] %>">
					    <option value=""></option>	
                        <option ng-repeat="x in Budget" value="{{ x.Code }}" >{{ x.Name }}</option>				
				    </select>    
                </td>
             </tr>    
        <tr><td colspan="4" style="height:10px"></td></tr>
        <tr>
            <td><%=Session["budget_operation_Column03"] %></td>
            <td colspan="3">
                <input type="text" id="Budget_Qty" />
            </td>
        </tr>
        <tr><td colspan="4" style="height:10px"></td></tr>
        <tr>
            <td><%=Session["budget_operation_Column04"] %></td>
            <td colspan="3">
                <input type="text" id="Budget_Price" />    
            </td>
        </tr>        
        <tr><td colspan="4" style="height:10px"></td></tr>
        <tr>
            <td><%=Session["budget_operation_Column05"] %></td>
            <td colspan="3">
                <input type="text" id="Budget_Reason" />    
            </td>
        </tr>
    </table>   
    </center>
    <script>
        function fnLoad() {
            var KeyID = getParamValue("KeyID");
            if (KeyID != null) {
                $.post("../server/Server.aspx",
                    {
                        Command: 'BudgetOperation',
                        Function: 'Load',
                        KeyID: KeyID
                    },
                    function (data, status) {
                        var data = eval(data);
                        document.getElementById('Budget_ID').value = data[0].Budget_ID.trim();
                        document.getElementById('Budget_Name').value = data[0].Budget_Name.trim();
                        document.getElementById('Budget_Type').value = data[0].Budget_Type.trim();
                        document.getElementById('Budget_Qty').value = data[0].Budget_Qty.trim();
                        document.getElementById('Budget_Price').value = data[0].Budget_Price.trim();
                        document.getElementById('Budget_Reason').value = data[0].Budget_Reason.trim();

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
            var Budget_ID = document.getElementById('Budget_ID').value.trim();
            var Budget_Name = document.getElementById('Budget_Name').value.trim();
            var Budget_Type = document.getElementById('Budget_Type').value.trim();
            var Budget_Qty = document.getElementById('Budget_Qty').value.trim();
            var Budget_Price = document.getElementById('Budget_Price').value;
            var Budget_Reason = document.getElementById('Budget_Reason').value;

            if (Budget_ID == "") {
                fnErrorMessage("ข้อผิดพลาด / Error", "<%=Session["budget_operation_ERROR_01"]%>");
                return;
            }            
            if (Budget_Name == "") {
                fnErrorMessage("ข้อผิดพลาด / Error", "<%=Session["budget_operation_ERROR_02"]%>");
                return;
            }
            if (Budget_Type == "") {
                fnErrorMessage("ข้อผิดพลาด / Error", "<%=Session["budget_operation_ERROR_03"]%>");
                return;
            }
            if (Budget_Qty == "") {
                fnErrorMessage("ข้อผิดพลาด / Error", "<%=Session["budget_operation_ERROR_04"]%>");
                return;
            }
            if (Budget_Price == "") {
                fnErrorMessage("ข้อผิดพลาด / Error", "<%=Session["budget_operation_ERROR_05"]%>");
                return;
            }

            $.post("../server/Server.aspx",
			    {
			        Command: 'BudgetOperation',
			        Function: 'Check',
			        KeyID: KeyID,
			        Budget_ID: Budget_ID,
			        Budget_Name: Budget_Name,
			        Budget_Type: Budget_Type,
			        Budget_Qty: Budget_Qty,
			        Budget_Price: Budget_Price
			    },
			    function (data, status) {
			        var data = eval(data);
			        if (data[0].output == 'OK') {
			            fnSubmit();
			        } else {
			            fnErrorMessage("ข้อผิดพลาด / Error", data[0].message);
			        }
			    }
            );
        }

        function fnSubmit() {
            var KeyID = getParamValue("KeyID");
            var Budget_ID = document.getElementById('Budget_ID').value.trim();
            var Budget_Name = document.getElementById('Budget_Name').value.trim();
            var Budget_Type = document.getElementById('Budget_Type').value.trim();
            var Budget_Qty = document.getElementById('Budget_Qty').value.trim();
            var Budget_Price = document.getElementById('Budget_Price').value;
            var lang = getParamValue("lang");

            $.post("../server/Server.aspx",
               {
                   Command: 'BudgetOperation',
                   Function: 'Save',
                   KeyID: KeyID,
                   Budget_ID: Budget_ID,
                   Budget_Name: Budget_Name,
                   Budget_Type: Budget_Type,
                   Budget_Qty: Budget_Qty,
                   Budget_Price: Budget_Price,
                   lang: lang
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
            fnGetBudgetType($scope, $http);
        }
        function fnGetBudgetType($scope, $http) {

            $scope = $tmp_scope;
            $http = $tmp_http;

            var data = $.param({
                Command: 'GetMasterData',
                Function: 'Budget',
                PageName: 'budget_operation'
            });

            $http.post("../server/Server.aspx", data, config)
            .success(function (data, status, headers, config) {
                $scope.Budget = data.records;
                setTimeout(fnLoad, 100);
            })
            .error(function (data, status, header, config) {
                $('body').pleaseWait('stop');
            });
        }
    </script>
</asp:Content>

