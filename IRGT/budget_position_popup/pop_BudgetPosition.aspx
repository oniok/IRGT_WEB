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
                <td><%=Session["budget_position_Column01"] %></td>
                <td colspan="3">
                    <input type="hidden" id="BP_ID"/>
                    <select class="chosen-select form-control" id="Position_Type_ID" data-placeholder="<%=Session["search_placeholder"] %>">
					    <option value=""></option>	
                        <option ng-repeat="x in Position_Type" value="{{ x.Code }}" >{{ x.Name }}</option>				
				    </select>
                </td>
            </tr>
            <tr><td colspan="4" style="height:10px"></td></tr>
            <tr>
                <td style="width:100px"><%=Session["budget_position_Column02"] %></td>
                <td colspan="3">
                    <select class="chosen-select form-control" id="Educate_Type_ID" data-placeholder="<%=Session["search_placeholder"] %>" style="width:200px">
					    <option value=""></option>	
                        <option ng-repeat="x in Educate_Type" value="{{ x.Code }}" >{{ x.Name }}</option>				
				    </select>   
                </td>
             </tr>    
            <tr><td colspan="4" style="height:10px"></td></tr>
            <tr>
                <td style="width:100px"><%=Session["budget_position_Column03"] %></td>
                <td colspan="3">
                    <select class="chosen-select form-control" id="BP_Type_ID" data-placeholder="<%=Session["search_placeholder"] %>" style="width:200px">
					    <option value=""></option>	
                        <option ng-repeat="x in BP_Type" value="{{ x.Code }}" >{{ x.Name }}</option>				
				    </select>  
                </td>
             </tr>  
        <tr><td colspan="4" style="height:10px"></td></tr>
        <tr>
            <td><%=Session["budget_position_Column04"] %></td>
            <td colspan="3">
                <input type="text" id="BP_Qty" />
            </td>
        </tr>
        <tr><td colspan="4" style="height:10px"></td></tr>
        <tr>
            <td><%=Session["budget_position_Column05"] %></td>
            <td colspan="3">
                <input type="text" id="BP_Price" />    
            </td>
        </tr>        
        <tr><td colspan="4" style="height:10px"></td></tr>
        <tr>
            <td><%=Session["budget_position_Column06"] %></td>
            <td colspan="3">
                <input type="text" id="BP_Reason" style="width:100%"/>    
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
                        Command: 'BudgetPosition',
                        Function: 'Load',
                        KeyID: KeyID
                    },
                    function (data, status) {
                        var data = eval(data);
                        document.getElementById('BP_ID').value = data[0].BP_ID.trim();
                        document.getElementById('Position_Type_ID').value = data[0].Position_Type_ID.trim();
                        document.getElementById('Educate_Type_ID').value = data[0].Educate_Type_ID.trim();
                        document.getElementById('BP_Type_ID').value = data[0].BP_Type_ID.trim();
                        document.getElementById('BP_Qty').value = data[0].BP_Qty.trim();
                        document.getElementById('BP_Price').value = data[0].BP_Price.trim();
                        document.getElementById('BP_Reason').value = data[0].BP_Reason.trim();

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
            var BP_ID = document.getElementById('BP_ID').value.trim();
            var Position_Type_ID = document.getElementById('Position_Type_ID').value.trim();
            var Educate_Type_ID = document.getElementById('Educate_Type_ID').value.trim();
            var BP_Type_ID = document.getElementById('BP_Type_ID').value.trim();
            var BP_Qty = document.getElementById('BP_Qty').value.trim();
            var BP_Price = document.getElementById('BP_Price').value.trim();
            var BP_Reason = document.getElementById('BP_Reason').value.trim();
        
            if (Position_Type_ID == "") {
                fnErrorMessage("ข้อผิดพลาด / Error", "<%=Session["budget_position_ERROR_02"]%>");
                return;
            }
            if (Educate_Type_ID == "") {
                fnErrorMessage("ข้อผิดพลาด / Error", "<%=Session["budget_position_ERROR_03"]%>");
                return;
            }
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

            var User_Code = '<%=Session["user_code"]%>';

            $.post("../server/Server_Budget_Position.aspx",
			    {
			        Command: 'BudgetPosition',
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
            var BP_Reason = document.getElementById('BP_Reason').value.trim();

            $.post("../server/Server_Budget_Position.aspx",
               {
                   Command: 'BudgetPosition',
                   Function: 'Save',
                   KeyID: KeyID,
                   BP_ID: BP_ID,
                   Position_Type_ID: Position_Type_ID,
                   Educate_Type_ID: Educate_Type_ID,
                   BP_Type_ID: BP_Type_ID,
                   BP_Qty: BP_Qty,
                   BP_Price: BP_Price,
                   BP_Reason: BP_Reason
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
            fnGetBP_PositionType($scope, $http);
        }
        function fnGetBP_PositionType($scope, $http) {

            $scope = $tmp_scope;
            $http = $tmp_http;

            var data = $.param({
                Command: 'GetMasterData',
                Function: 'PositionType',
                PageName: 'budget_position'
            });

            $http.post("../server/Server_Budget_Position.aspx", data, config)
            .success(function (data, status, headers, config) {
                $scope.Position_Type = data.records;
                setTimeout(fnGetBP_EducateType, 100);
            })
            .error(function (data, status, header, config) {
                $('body').pleaseWait('stop');
            });
        }
        function fnGetBP_EducateType() {

            $scope = $tmp_scope;
            $http = $tmp_http;

            var data = $.param({
                Command: 'GetMasterData',
                Function: 'EducateType',
                PageName: 'budget_position'
            });

            $http.post("../server/Server_Budget_Position.aspx", data, config)
            .success(function (data, status, headers, config) {
                $scope.Educate_Type = data.records;
                setTimeout(fnGetBP_BPType, 100);
            })
            .error(function (data, status, header, config) {
                $('body').pleaseWait('stop');
            });
        }
        function fnGetBP_BPType() {

            $scope = $tmp_scope;
            $http = $tmp_http;

            var data = $.param({
                Command: 'GetMasterData',
                Function: 'BudgetPositionType',
                PageName: 'budget_position'
            });

            $http.post("../server/Server_Budget_Position.aspx", data, config)
            .success(function (data, status, headers, config) {
                $scope.BP_Type = data.records;
                setTimeout(fnLoad, 100);
            })
            .error(function (data, status, header, config) {
                $('body').pleaseWait('stop');
            });
        }
       
    </script>
    
</asp:Content>

