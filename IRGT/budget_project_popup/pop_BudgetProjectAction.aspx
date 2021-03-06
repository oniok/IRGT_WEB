﻿<%@ Page Title="" Language="C#" MasterPageFile="~/master_page/popup.master" AutoEventWireup="true" CodeFile="pop_BudgetProjectAction.aspx.cs" Inherits="budget_popup_pop_BudgetProjectAction" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script src="../Scripts/angular.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <center>
    <table style="width:600px" ng-app="myApp" ng-controller="fnMain">
        <tr>
            <tr><td colspan="4" style="height:10px"></td></tr>
            <tr>
                <td><%=Session["budget_project_Column01"] %></td>
                <td colspan="3">
                    <input type="hidden" id="BR_ID"/>
                    <input type="text" id="BR_Name" style="width:100%"/>
                </td>
            </tr>
            <tr><td colspan="4" style="height:10px"></td></tr>
            <tr>
                <td style="width:100px"><%=Session["budget_project_Column02"] %></td>
                <td colspan="3">
                    <select class="chosen-select form-control" id="BA_Type_ID" data-placeholder="<%=Session["search_placeholder"] %>">
					    <option value=""></option>	
                        <option ng-repeat="x in BudgetProjectType" value="{{ x.Code }}" >{{ x.Name }}</option>				
				    </select>   
                </td>
             </tr>    
        <tr><td colspan="4" style="height:10px"></td></tr>
        <tr>
            <td><%=Session["budget_project_Column03"] %></td>
            <td colspan="3">
                <input type="text" id="BA_Qty" />
            </td>
        </tr>
        <tr><td colspan="4" style="height:10px"></td></tr>
        <tr>
            <td><%=Session["budget_project_Column031"] %></td>
            <td colspan="3">
                <input type="text" id="BA_Qty_Adj" />
            </td>
        </tr>
        <tr><td colspan="4" style="height:10px"></td></tr>
        <tr>
            <td><%=Session["budget_project_Column04"] %></td>
            <td colspan="3">
                <input type="text" id="BA_Price" />    
            </td>
        </tr> 
        <tr><td colspan="4" style="height:10px"></td></tr>
        <tr>
            <td><%=Session["budget_project_Column041"] %></td>
            <td colspan="3">
                <input type="text" id="BA_Price_Adj" />    
            </td>
        </tr>        
        <tr><td colspan="4" style="height:10px"></td></tr>
        <tr>
            <td><%=Session["budget_project_Column05"] %></td>
            <td colspan="3">
                <input type="text" id="BA_Reason" style="width:100%"/>    
            </td>
        </tr>
    </table>   
    </center>
    <script>
        function fnLoad() {
            var KeyID = getParamValue("KeyID");
            if (KeyID != null) {
                $.post("../server/Server_Budget_Project.aspx",
                    {
                        Command: 'BudgetOperationByID',
                        Function: 'Load',
                        KeyID: KeyID
                    },
                    function (data, status) {
                        var data = eval(data);
                        document.getElementById('BR_ID').value = data[0].BR_ID.trim();
                        document.getElementById('BR_Name').value = data[0].BR_Name.trim();
                        document.getElementById('BA_Type_ID').value = data[0].BA_Type_ID.trim();
                        document.getElementById('BA_Qty').value = data[0].BA_Qty.trim();
                        document.getElementById('BA_Price').value = data[0].BA_Price.trim();
                        document.getElementById('BA_Qty_Adj').value = data[0].BA_Qty_Adj.trim();
                        document.getElementById('BA_Price_Adj').value = data[0].BA_Price_Adj.trim();
                        document.getElementById('BA_Reason').value = data[0].BA_Reason.trim();

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
            
            var BA_Qty_Adj = document.getElementById('BA_Qty_Adj').value.trim();
            var BA_Price_Adj = document.getElementById('BA_Price_Adj').value.trim();
        
            if (BA_Qty_Adj == "") {
                fnErrorMessage("ข้อผิดพลาด / Error", "<%=Session["budget_project_ERROR_04"]%>");
                return;
            }
            if (BA_Price_Adj == "") {
                fnErrorMessage("ข้อผิดพลาด / Error", "<%=Session["budget_project_ERROR_05"]%>");
                return;
            }

            var User_Code = '<%=Session["user_code"]%>';

            $.post("../server/Server_Budget_Project.aspx",
			    {
			        Command: 'BudgetOperationByID',
			        Function: 'Adjust',
			        BA_Qty_Adj: BA_Qty_Adj,
			        BA_Price_Adj: BA_Price_Adj,
			        KeyID: KeyID,
			        User_Code: User_Code
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
            fnGetBudgetProjectType($scope, $http);

            $("#BR_Name").prop("disabled", true);
            $("#BA_Type_ID").prop("disabled", true);
            $("#BA_Reason").prop("disabled", true);
            $("#BA_Qty").prop("disabled", true);
            $("#BA_Price").prop("disabled", true);
        }
        function fnGetBudgetProjectType($scope, $http) {

            $scope = $tmp_scope;
            $http = $tmp_http;

            var data = $.param({
                Command: 'GetMasterData',
                Function: 'BudgetProjectType',
                PageName: 'Budget_Project'
            });

            $http.post("../server/Server_Budget_Project.aspx", data, config)
            .success(function (data, status, headers, config) {
                $scope.BudgetProjectType = data.records;
                setTimeout(fnLoad, 100);
            })
            .error(function (data, status, header, config) {
                $('body').pleaseWait('stop');
            });
        }
       
    </script>
    
</asp:Content>

