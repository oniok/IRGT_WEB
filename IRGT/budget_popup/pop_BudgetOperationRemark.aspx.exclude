<%@ Page Title="" Language="C#" MasterPageFile="~/master_page/popup.master" AutoEventWireup="true" CodeFile="pop_BudgetOperationRemark.aspx.cs" Inherits="budget_popup_pop_BudgetOperationRemark" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script src="../Scripts/angular.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <center>
    <table style="width:600px" ng-app="myApp" ng-controller="fnMain">
        <tr>
            <tr><td colspan="4" style="height:10px"></td></tr>
            <tr>
                <td style="width:100px"><%=Session["budget_operation_summary_Column02"] %></td>
                <td colspan="3">
                    <input type="hidden" id="BO_ID"/>
                    <input type="text" id="BO_Type_Name" />
                </td>
             </tr>    
        <tr><td colspan="4" style="height:10px"></td></tr>
        <tr>
            <td><%=Session["budget_operation_summary_Column03"] %></td>
            <td colspan="3">
                <input type="text" id="BO_PRICE_MNT" />
            </td>
        </tr>
        <tr><td colspan="4" style="height:10px"></td></tr>
        <tr>
            <td><%=Session["budget_operation_summary_Column04"] %></td>
            <td colspan="3">
                <input type="text" id="BO_PRICE_YEAR" />    
            </td>
        </tr>        
        <tr><td colspan="4" style="height:10px"></td></tr>
        <tr>
            <td><%=Session["budget_operation_summary_Column05"] %></td>
            <td colspan="3">
                <input type="text" id="BO_Remark" style="width:100%"/>    
            </td>
        </tr>
    </table>   
    </center>
    <div class="modal-body">
		<center><iframe id="frmPopup" style="width:100%;height:500px;border:none" frameborder="0" ></iframe></center>						
	</div>
    <script>
        function fnLoad() {
            var KeyID = getParamValue("KeyID");
            
            if (KeyID != null) {
                $.post("../server/Server_Budget.aspx",
                    {
                        Command: 'BudgetOperationRemark',
                        Function: 'Load',
                        KeyID: KeyID
                    },
                    function (data, status) {
                        var data = eval(data);
                        document.getElementById('BO_ID').value = data[0].BO_ID.trim();
                        document.getElementById('BO_Type_Name').value = data[0].BO_Type_Name.trim();
                        document.getElementById('BO_PRICE_MNT').value = data[0].BO_PRICE_MNT.trim();
                        document.getElementById('BO_PRICE_YEAR').value = data[0].BO_PRICE_YEAR.trim();
                        document.getElementById('BO_Remark').value = data[0].BO_Remark.trim();

                        $('body').pleaseWait('stop');
                        fnLoadCtrl();
                    }
                );
            } else {
                $('body').pleaseWait('stop');
                fnLoadCtrl();
            }

        }
        function fnOpenPopup(title, url, width, height) {
            document.getElementById('popHeader').innerHTML = title;
            document.getElementById('frmPopup').src = url;
            if (width != null) {
                document.getElementById('frmPopup').style.width = width + "px";
            }
            else {
                document.getElementById('frmPopup').style.width = "100%";
            }

            if (height != null) document.getElementById('frmPopup').style.height = height + "px";
            else document.getElementById('frmPopup').style.height = "500px";

            document.getElementById('btnPopSave').onclick = function () {
                try {
                    document.getElementById('frmPopup').contentWindow.fnSave();
                } catch (err) { }
            }

            document.getElementById('btnEdit').click();
        }
        function fnSave() {
            var KeyID = getParamValue("KeyID");
            var BO_Remark = document.getElementById('BO_Remark').value.trim();
        
            if (BO_Remark == "") {
                fnErrorMessage("ข้อผิดพลาด / Error", "<%=Session["budget_operation_ERROR_05"]%>");
                return;
            }

            var User_Code = '<%=Session["user_code"]%>';

            $.post("../server/Server_Budget.aspx",
			    {
			        Command: 'BudgetOperationRemark',
			        Function: 'Check',
			        User_Code: User_Code
			    },
			    function (data, status) {
			        var data = eval(data);
			        if (data[0].KeyID != '') {
			            fnSubmit(data[0].KeyID);
			        } else {
			            fnErrorMessage("ข้อผิดพลาด / Error", data[0].message);
			        }
			    }
            );
        }

        function fnSubmit(KeyID) {
            
            var BO_Reason = document.getElementById('BO_Remark').value;

            $.post("../server/Server_Budget.aspx",
               {
                   Command: 'BudgetOperationRemark',
                   Function: 'Save',
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
            document.getElementById('BO_ID').value = getParamValue("BO_ID");
            document.getElementById('BO_Type_Name').value = getParamValue("BO_Type_Name");
            setTimeout(fnLoad, 100);
        }
        
       
    </script>
    
</asp:Content>

