﻿<%@ Page Title="" Language="C#" MasterPageFile="~/master_page/popup.master" AutoEventWireup="true" CodeFile="pop_BudgetAsset.aspx.cs" Inherits="budget_popup_pop_BudgetAsset" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script src="../Scripts/angular.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <center>
    <table style="width:600px" ng-app="myApp" ng-controller="fnMain">
        <tr>
            <tr><td colspan="4" style="height:10px"></td></tr>
            <tr>
                <td style="width:100px"><%=Session["budget_asset_Column01"] %></td>
                <td colspan="3">
                    <input type="hidden" id="BA_ID"/>
                    <select class="chosen-select form-control" id="BA_Type_ID" data-placeholder="<%=Session["search_placeholder"] %>">
					    <option value=""></option>	
                        <option ng-repeat="x in BudgetAssetType" value="{{ x.Code }}" >{{ x.Name }}</option>				
				    </select>   
                </td>
             </tr>    
        <tr><td colspan="4" style="height:10px"></td></tr>
        <tr>
            <td><%=Session["budget_asset_Column02"] %></td>
            <td colspan="3">
                <input type="text" id="BA_Qty" />
            </td>
        </tr>
        <tr><td colspan="4" style="height:10px"></td></tr>
        <tr>
            <td><%=Session["budget_asset_Column03"] %></td>
            <td colspan="3">
                <input type="text" id="BA_Price" />    
            </td>
        </tr>  
        <tr><td colspan="4" style="height:10px"></td></tr>
        <tr>
            <td><%=Session["budget_asset_Column05"] %></td>
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
                $.post("../server/Server_Budget_Asset.aspx",
                    {
                        Command: 'BudgetAsset',
                        Function: 'Load',
                        KeyID: KeyID
                    },
                    function (data, status) {
                        var data = eval(data);
                        document.getElementById('BA_ID').value = data[0].BA_ID.trim();
                        document.getElementById('BA_Type_ID').value = data[0].BA_Type_ID.trim();
                        document.getElementById('BA_Qty').value = data[0].BA_Qty.trim();
                        document.getElementById('BA_Price').value = data[0].BA_Price.trim();
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
            var BA_Type_ID = document.getElementById('BA_Type_ID').value.trim();
            var BA_Qty = document.getElementById('BA_Qty').value.trim();
            var BA_Price = document.getElementById('BA_Price').value;
            var BA_Reason = document.getElementById('BA_Reason').value;
        
            if (BA_Type_ID == "") {
                fnErrorMessage("ข้อผิดพลาด / Error", "<%=Session["budget_asset_ERROR_03"]%>");
                return;
            }
            if (BA_Qty == "") {
                fnErrorMessage("ข้อผิดพลาด / Error", "<%=Session["budget_asset_ERROR_04"]%>");
                return;
            }
            if (BA_Price == "") {
                fnErrorMessage("ข้อผิดพลาด / Error", "<%=Session["budget_asset_ERROR_05"]%>");
                return;
            }
            

            var BA_Type_ID = document.getElementById('BA_Type_ID').value;
            var User_Code = '<%=Session["user_code"]%>';

            $.post("../server/Server_Budget_Asset.aspx",
			    {
			        Command: 'BudgetAsset',
			        Function: 'Check',
			        User_Code: User_Code
			    },
			    function (data, status) {
			        var data = eval(data);
			        if (data[0].BA_ID != '') {
			            fnSubmit(data[0].BA_ID);
			        } else {
			            fnErrorMessage("ข้อผิดพลาด / Error", data[0].message);
			        }
			    }
            );
        }

        function fnSubmit(BA_ID) {
            var KeyID = getParamValue("KeyID");
            var BA_Type_ID = document.getElementById('BA_Type_ID').value.trim();
            var BA_Qty = document.getElementById('BA_Qty').value.trim();
            var BA_Price = document.getElementById('BA_Price').value;
            var BA_Reason = document.getElementById('BA_Reason').value;

            $.post("../server/Server_Budget_Asset.aspx",
               {
                   Command: 'BudgetAsset',
                   Function: 'Save',
                   KeyID: KeyID,
                   BA_ID: BA_ID,
                   BA_Type_ID: BA_Type_ID,
                   BA_Qty: BA_Qty,
                   BA_Price: BA_Price,
                   BA_Reason: BA_Reason
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
            fnGetBudgetAssetType($scope, $http);
        }
        function fnGetBudgetAssetType($scope, $http) {

            $scope = $tmp_scope;
            $http = $tmp_http;

            var data = $.param({
                Command: 'GetMasterData',
                Function: 'BudgetAssetType',
                PageName: 'budget_asset'
            });

            $http.post("../server/Server_Budget_Asset.aspx", data, config)
            .success(function (data, status, headers, config) {
                $scope.BudgetAssetType = data.records;
                setTimeout(fnLoad, 100);
            })
            .error(function (data, status, header, config) {
                $('body').pleaseWait('stop');
            });
        }
       
    </script>
    
</asp:Content>

