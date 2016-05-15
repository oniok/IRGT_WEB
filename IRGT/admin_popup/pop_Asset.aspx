<%@ Page Title="" Language="C#" MasterPageFile="~/master_page/popup.master" AutoEventWireup="true" CodeFile="pop_Asset.aspx.cs" Inherits="admin_popup_pop_Asset" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="../Scripts/angular.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <center>
    <table style="width:600px" ng-app="myApp" ng-controller="fnMain">
        <tr>
            <td style="width:135px"><%=Session["asset_Column01"] %></td>
            <td colspan="3"><input type="text" id="Asset_ID" style="width:100px" /></td>
        </tr>
        <tr><td colspan="4" style="height:10px"></td></tr>
        <tr>
            <td style="width:170px"><%=Session["asset_Column06"] %></td>
            <td colspan="3">
                <select class="chosen-select form-control" id="Asset_Type_ID" data-placeholder="<%=Session["text_placeholder"] %>">
					<option value=""></option>	
                    <option ng-repeat="x in AssetType" value="{{ x.Code }}" >{{ x.Name }}</option>				
				</select>        
            </td>
        </tr>
        <tr><td colspan="4" style="height:10px"></td></tr>
        <tr>
            <td><%=Session["asset_Column02"] %></td>
            <td colspan="3"><input type="text" id="Asset_Name1_T" placeholder="<%=Session["text_placeholder"] %>" style="width:100%"/></td>
        </tr>
        <tr><td colspan="4" style="height:10px"></td></tr>
        <tr>
            <td><%=Session["asset_Column07"] %></td>
            <td colspan="3"><input type="text" id="Asset_Name2_T" placeholder="<%=Session["text_placeholder"] %>" style="width:100%"/></td>
        </tr>
        <tr><td colspan="4" style="height:10px"></td></tr>
        <tr>
            <td><%=Session["asset_Column03"] %></td>
            <td colspan="3"><input type="text" id="Asset_Name1_E" placeholder="<%=Session["text_placeholder"] %>" style="width:100%"/></td>
        </tr>
        <tr><td colspan="4" style="height:10px"></td></tr>
        <tr>
            <td><%=Session["asset_Column08"] %></td>
            <td colspan="3"><input type="text" id="Asset_Name2_E" placeholder="<%=Session["text_placeholder"] %>" style="width:100%"/></td>
        </tr>
        <tr><td colspan="4" style="height:10px"></td></tr>
        <tr>
            <td><%=Session["asset_Column09"] %></td>
            <td colspan="3"><input type="text" id="Unit_Name" placeholder="<%=Session["text_placeholder"] %>" style="width:100%"/></td>
        </tr> 
        <tr><td colspan="4" style="height:10px"></td></tr>
        <tr>
            <td><%=Session["asset_Column10"] %></td>
            <td colspan="3"><input type="text" id="Asset_Ref_No" placeholder="<%=Session["text_placeholder"] %>" style="width:100%"/></td>
        </tr>        
        <tr><td colspan="4" style="height:10px"></td></tr>
        <tr>
            <td><%=Session["asset_Column04"] %></td>
            <td>
                <div class="input-group">
				    <input class="form-control date-picker" id="StartDate" type="text" data-date-format="dd/mm/yyyy" placeholder="<%=Session["text_placeholder"] %>"/>
				    <span class="input-group-addon">
					    <i class="fa fa-calendar bigger-110"></i>
				    </span>
			    </div>
            </td>
            <td style="width:80px;text-align:right"><%=Session["asset_Column05"] %>&nbsp;</td>
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
                        Command: 'Asset',
                        Function: 'Load',
                        KeyID: KeyID
                    },
                    function (data, status) {
                        var data = eval(data);
                        document.getElementById('Asset_ID').value = data[0].Asset_ID.trim();
                        document.getElementById('Asset_Name1_T').value = data[0].Asset_Name1_T.trim();
                        document.getElementById('Asset_Name2_T').value = data[0].Asset_Name2_T.trim();
                        document.getElementById('Asset_Name1_E').value = data[0].Asset_Name1_E.trim();
                        document.getElementById('Asset_Name2_E').value = data[0].Asset_Name2_E.trim();
                        document.getElementById('Unit_Name').value = data[0].Unit_Name.trim();
                        document.getElementById('Asset_Ref_No').value = data[0].Asset_Ref_No.trim();
                        document.getElementById('Asset_Type_ID').value = data[0].Asset_Type_ID.trim();
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
            var Asset_Name1_T = document.getElementById('Asset_Name1_T').value.trim();
            var Asset_Name2_T = document.getElementById('Asset_Name2_T').value.trim();
            var Asset_Name1_E = document.getElementById('Asset_Name1_E').value.trim();
            var Asset_Name2_E = document.getElementById('Asset_Name2_E').value.trim();
            var Unit_Name = document.getElementById('Unit_Name').value.trim();
            var Asset_Ref_No = document.getElementById('Asset_Ref_No').value.trim();
            var Asset_Type_ID = document.getElementById('Asset_Type_ID').value.trim();

            var StartDate = document.getElementById('StartDate').value;
            var EndDate = document.getElementById('EndDate').value;
           
            //if (Asset_ID == "") {
            //    fnErrorMessage("ข้อผิดพลาด / Error", "<%=Session["asset_ERROR_01"]%>");
            //    return;
            //}
            if (Asset_Name1_T == "") {
                fnErrorMessage("ข้อผิดพลาด / Error", "<%=Session["asset_ERROR_02"]%>");
                return;
            }
            if (Asset_Name1_E == "") {
                fnErrorMessage("ข้อผิดพลาด / Error", "<%=Session["asset_ERROR_03"]%>");
                return;
            }

            if (Asset_Type_ID == "") {
                fnErrorMessage("ข้อผิดพลาด / Error", "<%=Session["asset_ERROR_07"]%>");
                 return;
             }

            

            if (EndDate != "") {
                if (StartDate == "") {
                    fnErrorMessage("ข้อผิดพลาด / Error", "<%=Session["asset_ERROR_04"]%>");
                    return;
                }

                var SP = StartDate.split("/");
                var SP_Start = parseInt(SP[2]) * 365 + parseInt(SP[1]) * 30 + parseInt(SP[0]);
                var SP2 = EndDate.split("/");
                var SP_END = parseInt(SP2[2]) * 365 + parseInt(SP2[1]) * 30 + parseInt(SP2[0]);
                if (SP_Start > SP_END) {
                    fnErrorMessage("ข้อผิดพลาด / Error", "<%=Session["asset_ERROR_06"]%>");
                    return;
                }
            }

            $.post("../server/Server.aspx",
			    {
			        Command: 'Asset',
			        Function: 'Check',
			        KeyID: KeyID,
			        Asset_ID: Asset_ID,
			        Asset_Name1_T: Asset_Name1_T,
			        Asset_Name2_T: Asset_Name2_T,
			        Asset_Name1_E: Asset_Name1_E,
			        Asset_Name2_E: Asset_Name2_E,
			        Unit_Name: Unit_Name,
			        Asset_Ref_No: Asset_Ref_No,
			        Asset_Type_ID: Asset_Type_ID,
			        StartDate: StartDate,
			        EndDate: EndDate
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
            var Asset_ID = document.getElementById('Asset_ID').value.trim();
            var Asset_Name1_T = document.getElementById('Asset_Name1_T').value.trim();
            var Asset_Name2_T = document.getElementById('Asset_Name2_T').value.trim();
            var Asset_Name1_E = document.getElementById('Asset_Name1_E').value.trim();
            var Asset_Name2_E = document.getElementById('Asset_Name2_E').value.trim();
            var Unit_Name = document.getElementById('Unit_Name').value.trim();
            var Asset_Ref_No = document.getElementById('Asset_Ref_No').value.trim();
            var Asset_Type_ID = document.getElementById('Asset_Type_ID').value.trim();
            var StartDate = document.getElementById('StartDate').value;
            var EndDate = document.getElementById('EndDate').value;
            var lang = getParamValue("lang");

            $.post("../server/Server.aspx",
               {
                   Command: 'Asset',
                   Function: 'Save',
                   KeyID: KeyID,
                   Asset_ID: Asset_ID,
                   Asset_Name1_T: Asset_Name1_T,
                   Asset_Name2_T: Asset_Name2_T,
                   Asset_Name1_E: Asset_Name1_E,
                   Asset_Name2_E: Asset_Name2_E,
                   Unit_Name: Unit_Name,
                   Asset_Ref_No: Asset_Ref_No,
                   Asset_Type_ID: Asset_Type_ID,
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
            fnGetAssetType($scope, $http);
        }      
        function fnGetAssetType($scope, $http) {

            $scope = $tmp_scope;
            $http = $tmp_http;

            var data = $.param({
                Command: 'GetMasterData',
                Function: 'AssetType',
                PageName: 'asset'
            });

            $http.post("../server/Server.aspx", data, config)
            .success(function (data, status, headers, config) {
                $scope.AssetType = data.records;
                setTimeout(fnLoad, 100);
            })
            .error(function (data, status, header, config) {
                $('body').pleaseWait('stop');
            });
        }


    </script>
</asp:Content>

