<%@ Page Title="" Language="C#" MasterPageFile="~/master_page/popup.master" AutoEventWireup="true" CodeFile="pop_AssetQuota.aspx.cs" Inherits="admin_popup_pop_AssetQuota" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="../Scripts/angular.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <center>
    <table style="width:600px" ng-app="myApp" ng-controller="fnMain">
        <tr>
            <td style="width:170px"><%=Session["asset_quota_Column01"] %></td>
            <td colspan="3">
                <select class="chosen-select form-control" id="Asset_ID" data-placeholder="<%=Session["text_placeholder"] %>">
					<option value=""></option>	
                    <option ng-repeat="x in Asset" value="{{ x.Code }}" >{{ x.Name }}</option>				
				</select>                
            </td>
        </tr>
        <tr><td colspan="4" style="height:10px"></td></tr>
        <tr>
            <td><%=Session["asset_quota_Column02"] %></td>
            <td colspan="3">
                <select class="chosen-select form-control" id="Loc_ID" data-placeholder="<%=Session["text_placeholder"] %>">
					<option value=""></option>	
                    <option ng-repeat="x in WorkCenter" value="{{ x.Code }}" >{{ x.Name }}</option>				
				</select>               
            </td>
        </tr>       
        <tr><td colspan="4" style="height:10px"></td></tr>
        <tr>
            <td><%=Session["asset_quota_Column03"] %></td>
            <td colspan="3">
                <input type="text" id="Quota_Qty" />%
            </td>
        </tr> 
        <tr><td colspan="4" style="height:10px"></td></tr>
        <tr>
            <td><%=Session["asset_quota_Column06"] %></td>
            <td colspan="3">
                <input type="text" id="Stock_Qty" />%
            </td>
        </tr> 
        <tr><td colspan="4" style="height:10px"></td></tr>
        <tr>
            <td><%=Session["asset_quota_Column04"] %></td>
            <td>
                <div class="input-group">
				    <input class="form-control date-picker" id="StartDate" type="text" data-date-format="dd/mm/yyyy" placeholder="<%=Session["text_placeholder"] %>"/>
				    <span class="input-group-addon">
					    <i class="fa fa-calendar bigger-110"></i>
				    </span>
			    </div>
            </td>
            <td style="width:80px;text-align:right"><%=Session["asset_quota_Column05"] %>&nbsp;</td>
            <td>
                <div class="input-group">
				    <input class="form-control date-picker" id="EndDate" type="text" data-date-format="dd/mm/yyyy" placeholder="<%=Session["text_placeholder"] %>"/>
				    <span class="input-group-addon">
					    <i class="fa fa-calendar bigger-110"></i>
				    </span>
			    </div>
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
                        Command: 'AssetQuota',
                        Function: 'Load',
                        KeyID: KeyID
                    },
                    function (data, status) {
                        var data = eval(data);
                        document.getElementById('Asset_ID').value = data[0].Asset_ID.trim();
                        document.getElementById('Loc_ID').value = data[0].Loc_ID.trim();
                        document.getElementById('Quota_Qty').value = data[0].Quota_Qty.trim();
                        document.getElementById('Stock_Qty').value = data[0].Stock_Qty.trim();
                        document.getElementById('StartDate').value = data[0].Start_Date.trim();
                        document.getElementById('EndDate').value = data[0].End_Date.trim();

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
            var Asset_ID = document.getElementById('Asset_ID').value.trim();
            var Loc_ID = document.getElementById('Loc_ID').value.trim();
            var Quota_Qty = document.getElementById('Quota_Qty').value.trim();
            var Stock_Qty = document.getElementById('Stock_Qty').value.trim();
            var StartDate = document.getElementById('StartDate').value;
            var EndDate = document.getElementById('EndDate').value;

            if (Asset_ID == "") {
                fnErrorMessage("ข้อผิดพลาด / Error", "<%=Session["asset_quota_ERROR_01"]%>");
                return;
            }
            if (Loc_ID == "") {
                fnErrorMessage("ข้อผิดพลาด / Error", "<%=Session["asset_quota_ERROR_02"]%>");
                return;
            }
            if (StartDate == "") {
                fnErrorMessage("ข้อผิดพลาด / Error", "<%=Session["asset_quota_ERROR_04"]%>");
                return;
            }

            if (EndDate != "") {
                var SP = StartDate.split("/");
                var SP_Start = parseInt(SP[2]) * 365 + parseInt(SP[1]) * 30 + parseInt(SP[0]);
                var SP2 = EndDate.split("/");
                var SP_END = parseInt(SP2[2]) * 365 + parseInt(SP2[1]) * 30 + parseInt(SP2[0]);
                if (SP_Start > SP_END) {
                    fnErrorMessage("ข้อผิดพลาด / Error", "<%=Session["asset_quota_ERROR_06"]%>");
                    return;
                }
            }

            $.post("../server/Server.aspx",
			    {
			        Command: 'AssetQuota',
			        Function: 'Check',
			        KeyID: KeyID,
			        Asset_ID: Asset_ID,
			        Loc_ID: Loc_ID,
			        Quota_Qty: Quota_Qty,
			        Stock_Qty: Stock_Qty,
			        StartDate: StartDate,
			        EndDate: EndDate
			    },
			    function (data, status) {
			        var data = eval(data);
			        if (data[0].output == "OK") {
			            fnSubmit();
			        } else {
			            fnErrorMessage("ข้อผิดพลาด / Error", data[0].message);
			        }
			    }
            );
        }

        function fnSubmit() {
            var KeyID = getParamValue("KeyID");
            var Asset_ID = document.getElementById('Asset_ID').value.trim();
            var Loc_ID = document.getElementById('Loc_ID').value.trim();
            var Quota_Qty = document.getElementById('Quota_Qty').value.trim();
            var Stock_Qty = document.getElementById('Stock_Qty').value.trim();
            var StartDate = document.getElementById('StartDate').value;
            var EndDate = document.getElementById('EndDate').value;
            var lang = getParamValue("lang");

            $.post("../server/Server.aspx",
               {
                   Command: 'AssetQuota',
                   Function: 'Save',
                   KeyID: KeyID,
                   Asset_ID: Asset_ID,
                   Loc_ID: Loc_ID,
                   Quota_Qty: Quota_Qty,
                   Stock_Qty: Stock_Qty,
                   StartDate: StartDate,
                   EndDate: EndDate,
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
            fnGetAsset($scope, $http);
        }

        function fnGetAsset($scope, $http) {
            var data = $.param({
                Command: 'GetMasterData',
                Function: 'Asset',
                PageName: 'report_group'
            });

            $http.post("../server/Server.aspx", data, config)
            .success(function (data, status, headers, config) {
                $scope.Asset = data.records;
                setTimeout(fnGetWorkCenter, 100);
            })
            .error(function (data, status, header, config) {
                $('body').pleaseWait('stop');
            });
        }
        function fnGetWorkCenter() {

            $scope = $tmp_scope;
            $http = $tmp_http;

            var data = $.param({
                Command: 'GetMasterData',
                Function: 'WorkCenter',
                PageName: 'report_group'
            });

            $http.post("../server/Server.aspx", data, config)
            .success(function (data, status, headers, config) {
                $scope.WorkCenter = data.records;
                setTimeout(fnLoad, 100);
            })
            .error(function (data, status, header, config) {
                $('body').pleaseWait('stop');
            });
        }


    </script>
</asp:Content>

