﻿<%@ Page Title="" Language="C#" MasterPageFile="~/master_page/popup.master" AutoEventWireup="true" CodeFile="pop_ReportGroup.aspx.cs" Inherits="admin_popup_pop_ReportGroup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="../Scripts/angular.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <center>
    <table style="width:600px" ng-app="myApp" ng-controller="fnMain">
        <tr>
            <td style="width:170px"><%=Session["report_group_Column01"] %></td>
            <td colspan="3">
                <select class="chosen-select form-control" id="Type_Grp_ID" data-placeholder="<%=Session["text_placeholder"] %>">
					<option value=""></option>	
                    <option ng-repeat="x in ReportGroupType" value="{{ x.Code }}" >{{ x.Name }}</option>				
				</select>                
            </td>
        </tr>
        <tr><td colspan="4" style="height:10px"></td></tr>
        <tr>
            <td><%=Session["report_group_Column02"] %></td>
            <td colspan="3">
                <select class="chosen-select form-control" id="Loc_ID" data-placeholder="<%=Session["text_placeholder"] %>">
					<option value=""></option>	
                    <option ng-repeat="x in WorkCenter" value="{{ x.Code }}" >{{ x.Name }}</option>				
				</select>               
            </td>
        </tr>        
        <tr><td colspan="4" style="height:10px"></td></tr>
        <tr>
            <td><%=Session["report_group_Column04"] %></td>
            <td>
                <div class="input-group">
				    <input class="form-control date-picker" id="StartDate" type="text" data-date-format="dd/mm/yyyy" placeholder="<%=Session["text_placeholder"] %>"/>
				    <span class="input-group-addon">
					    <i class="fa fa-calendar bigger-110"></i>
				    </span>
			    </div>
            </td>
            <td style="width:80px;text-align:right"><%=Session["report_group_Column05"] %>&nbsp;</td>
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
                        Command: 'ReportGroup',
                        Function: 'Load',
                        KeyID: KeyID
                    },
                    function (data, status) {
                        var data = eval(data);
                        document.getElementById('Type_Grp_ID').value = data[0].Type_Grp_ID.trim();
                        document.getElementById('Loc_ID').value = data[0].Loc_ID.trim();
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
            var Type_Grp_ID = document.getElementById('Type_Grp_ID').value.trim();
            var Loc_ID = document.getElementById('Loc_ID').value.trim();           
            var StartDate = document.getElementById('StartDate').value;
            var EndDate = document.getElementById('EndDate').value;

            if (Type_Grp_ID == "") {
                fnErrorMessage("ข้อผิดพลาด / Error", "<%=Session["report_group_ERROR_01"]%>");
                return;
            }
            if (Loc_ID == "") {
                fnErrorMessage("ข้อผิดพลาด / Error", "<%=Session["report_group_ERROR_02"]%>");
                return;
            }           
                  

            if (EndDate != "") {
                if (StartDate == "") {
                    fnErrorMessage("ข้อผิดพลาด / Error", "<%=Session["report_group_ERROR_04"]%>");
                     return;
                 }

                var SP = StartDate.split("/");
                var SP_Start = parseInt(SP[2]) * 365 + parseInt(SP[1]) * 30 + parseInt(SP[0]);
                var SP2 = EndDate.split("/");
                var SP_END = parseInt(SP2[2]) * 365 + parseInt(SP2[1]) * 30 + parseInt(SP2[0]);
                if (SP_Start > SP_END) {
                    fnErrorMessage("ข้อผิดพลาด / Error", "<%=Session["report_group_type_ERROR_06"]%>");
                    return;
                }
            }

            $.post("../server/Server.aspx",
			    {
			        Command: 'ReportGroup',
			        Function: 'Check',                    
			        KeyID: KeyID,
			        Type_Grp_ID: Type_Grp_ID,
			        Loc_ID: Loc_ID,
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
            var Type_Grp_ID = document.getElementById('Type_Grp_ID').value.trim();
            var Loc_ID = document.getElementById('Loc_ID').value.trim();
            var StartDate = document.getElementById('StartDate').value;
            var EndDate = document.getElementById('EndDate').value;
            var lang = getParamValue("lang");

            $.post("../server/Server.aspx",
               {
                   Command: 'ReportGroup',
                   Function: 'Save',
                   KeyID: KeyID,
                   Type_Grp_ID: Type_Grp_ID,
                   Loc_ID: Loc_ID,
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
            fnGetReportGroupType($scope, $http);
        }

        function fnGetReportGroupType($scope, $http) {   
            var data = $.param({
                Command: 'GetMasterData',
                Function: 'ReportGroupType',
                PageName: 'report_group'
            });

            $http.post("../server/Server.aspx", data, config)
            .success(function (data, status, headers, config) {
                $scope.ReportGroupType = data.records;                
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

