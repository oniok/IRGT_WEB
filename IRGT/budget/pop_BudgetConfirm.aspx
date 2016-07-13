<%@ Page Title="" Language="C#" MasterPageFile="~/master_page/popup.master" AutoEventWireup="true" CodeFile="pop_BudgetConfirm.aspx.cs" Inherits="budget_popup_pop_BudgetConfirm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script src="../Scripts/angular.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="main-content" ng-app="myApp" ng-controller="fnMain">
    <table style="width:500px" align="center">
        <tr>
            <td style="width:100px">หน่วยงาน</td>
            <td style="width:5px"></td>                                                   
            <td style="width:300px">
                <select class="chosen-select form-control" id="Loc_ID" data-placeholder="<%=Session["search_placeholder"] %>" style="width:250px">
			        <option value=""></option>	
                    <option ng-repeat="x in Data_Loc_ID" value="{{ x.Code }}" >{{ x.Name }}</option>				
		        </select>   
            </td>  
        </tr>
    </table>
    </div>
    <script>
        var isLoad = true;
        var PageSize = 10;
        var CurrentPageIndex = 1;
        var tmpKeyID;
        var $tmp_http;
        var $tmp_scope;
        var app = angular.module('myApp', []);
        var config = { headers: { 'Content-Type': 'application/x-www-form-urlencoded;charset=utf-8;' } }
        app.controller('fnMain', fnMain);

        //ข้อมูลผู้ใต้บังคับบัญชาระดับอื่น ๆ    
        function fnMain($scope, $http) {
            $('body').pleaseWait();

            $tmp_http = $http;
            $tmp_scope = $scope;

            fnGetLocNameList();
        }

        function fnGetLocNameList() {

            $scope = $tmp_scope;
            $http = $tmp_http;

            var data = $.param({
                Command: 'GetMasterData',
                Function: 'WorkCenter',
                PageName: 'budget_position_list'
            });

            $http.post("../server/Server_Budget_Position.aspx", data, config)
            .success(function (data, status, headers, config) {
                $scope.Data_Loc_ID = data.records;
                setTimeout(fnLoad, 100);

                
            })
            .error(function (data, status, header, config) {
                $('body').pleaseWait('stop');
            });
        }
        function fnLoad() {
            $('body').pleaseWait('stop');
            fnLoadCtrl();
        }
        function fnSave() {
            var Loc_ID = document.getElementById("Loc_ID").value;
            if (Loc_ID == "") {
                alert("ERRROR");
                return;
            }
            window.parent.fnSendNew(Loc_ID);
        }
        
    </script>
    
</asp:Content>

