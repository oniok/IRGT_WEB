<%@ Page Title="" Language="C#" MasterPageFile="~/master_page/popup.master" AutoEventWireup="true" CodeFile="pop_User.aspx.cs" Inherits="admin_popup_pop_User" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="../Scripts/angular.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <center>
    <table style="width:600px" ng-app="myApp" ng-controller="fnMain">        
        <tr>
            <td style="width:170px"><%=Session["user_Column02"] %></td>
            <td colspan="3">
                <select class="chosen-select form-control" id="User_Type_ID" data-placeholder="<%=Session["text_placeholder"] %>">
					<option value=""></option>	
                    <option ng-repeat="x in UserType" value="{{ x.Code }}" >{{ x.Name }}</option>				
				</select>        
            </td>
        </tr>
        <tr><td colspan="4" style="height:10px"></td></tr>
        <tr>
            <td style="width:135px"><%=Session["user_Column01"] %></td>
            <td colspan="3"><input type="text" id="User_Code" style="width:100px" /></td>
        </tr>
        <tr><td colspan="4" style="height:10px"></td></tr>
        <tr>
            <td><%=Session["user_Column04"] %></td>
            <td colspan="3"><input type="password" id="User_Password" placeholder="<%=Session["text_placeholder"] %>" style="width:100%"/></td>
        </tr>     
        <tr><td colspan="4" style="height:10px"></td></tr>
        <tr>
            <td style="width:135px"><%=Session["user_Column03"] %></td>
            <td colspan="3">
                <select id="Status">
                    <option value="1" selected>ใช้งาน</option>
                    <option value="0" >ยกเลิก</option>
                </select>
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
                        Command: 'User',
                        Function: 'Load',
                        User_ID: KeyID
                    },
                    function (data, status) {
                        var data = eval(data);
                        document.getElementById('User_Code').value = data[0].User_Code.trim();
                        document.getElementById('User_Password').value = data[0].User_Password.trim();
                        document.getElementById('Status').value = data[0].Status.trim();
                        document.getElementById('User_Type_ID').value = data[0].User_Type_ID.trim();
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
            var User_Code = document.getElementById('User_Code').value.trim();
            var User_Type_ID = document.getElementById('User_Type_ID').value.trim();
            var User_Password = document.getElementById('User_Password').value.trim();
            var Status = document.getElementById('Status').value.trim();

            if (User_Type_ID == "") {
                fnErrorMessage("ข้อผิดพลาด / Error", "<%=Session["user_ERROR_01"]%>");
                return;
            }
            if (User_Code == "") {
                fnErrorMessage("ข้อผิดพลาด / Error", "<%=Session["user_ERROR_02"]%>");
                return;
            }
            if (User_Password == "") {
                fnErrorMessage("ข้อผิดพลาด / Error", "<%=Session["user_ERROR_03"]%>");
                return;
            }

            

            $.post("../server/Server.aspx",
			    {
			        Command: 'User',
			        Function: 'Check',
			        User_ID: KeyID,
			        User_Code: User_Code,
			        User_Type_ID: User_Type_ID,
			        User_Password: User_Password,
			        Status: Status
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
            var User_Code = document.getElementById('User_Code').value.trim();
            var User_Type_ID = document.getElementById('User_Type_ID').value.trim();
            var User_Password = document.getElementById('User_Password').value.trim();
            var Status = document.getElementById('Status').value.trim();
            var lang = getParamValue("lang");

            $.post("../server/Server.aspx",
               {
                   Command: 'User',
                   Function: 'Save',
                   User_ID: KeyID,
                   User_Code: User_Code,
                   User_Type_ID: User_Type_ID,
                   User_Password: User_Password,
                   Status: Status,
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
            fnGetUserType($scope, $http);
        }
        function fnGetUserType($scope, $http) {

            $scope = $tmp_scope;
            $http = $tmp_http;

            var data = $.param({
                Command: 'GetMasterData',
                Function: 'UserType',
                PageName: 'user'
            });

            $http.post("../server/Server.aspx", data, config)
            .success(function (data, status, headers, config) {
                $scope.UserType = data.records;
                setTimeout(fnLoad, 100);
            })
            .error(function (data, status, header, config) {
                $('body').pleaseWait('stop');
            });
        }
    </script>
</asp:Content>

