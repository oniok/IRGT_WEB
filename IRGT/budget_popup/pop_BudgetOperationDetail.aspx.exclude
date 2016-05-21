<%@ Page Title="" Language="C#" MasterPageFile="~/master_page/popup.master" AutoEventWireup="true" CodeFile="pop_BudgetOperationDetail.aspx.cs" Inherits="budget_popup_pop_BudgetOperationDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script src="../Scripts/angular.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <center>
     <h3 class="header smaller lighter blue"><%=Session["HeaderText_Summary"]%></h3>	
    <table id="dynamic-table" class="table table-striped table-bordered table-hover" ng-app="myApp" ng-controller="fnMain">
		<thead>
			<tr>
				<th class="center" style="width:50px"><%=Session["budget_operation_summary_ColumnSEQ"]%></th>
				<th class="center"><%=Session["budget_operation_summary_Column02"]%></th>
                <th class="center" style="width:50px"><%=Session["budget_operation_summary_Column03"]%></th>          
                <th class="center" style="width:100px"><%=Session["budget_operation_summary_Column04"]%></th> 
                <th class="center"><%=Session["budget_operation_summary_Column05"]%></th> 
			</tr>
		</thead>
        <tbody>
			<tr ng-repeat="x in DataSum">
				<td class="center">{{ x.RowID }}</td>
                <td>{{ x.BO_Type_Name }}</td>
                <td style="text-align:right">{{ x.BO_PRICE_MNT }}</td>          
                <td style="text-align:right">{{ x.BO_PRICE_YEAR }}</td>	
                <td style="text-align:left">{{ x.BO_Remark }}</td>		
			</tr>										
		</tbody>  
        <tfoot >
      
        </tfoot>                                              
	</table> 
    <h3 class="header smaller lighter blue"><%=Session["HeaderText_Detail"]%></h3>
    <table id="dynamic-table2" class="table table-striped table-bordered table-hover" ng-app="myApp" ng-controller="fnMain">
		<thead>
			<tr>
				<th class="center" style="width:50px"><%=Session["budget_operation_ColumnSEQ"]%></th>
				<th class="center"><%=Session["budget_operation_Column01"]%></th>
				<th class="center"><%=Session["budget_operation_Column02"]%></th>
                <th class="center" style="width:50px"><%=Session["budget_operation_Column03"]%></th>          
                <th class="center" style="width:100px"><%=Session["budget_operation_Column04"]%></th>    
                <th class="center"><%=Session["budget_operation_Column05"]%></th>
				<td class="center" style="width:80px"><%=Session["budget_operation_ColumnEdit"]%></td>
			</tr>
		</thead>
        <tbody>
			<tr ng-repeat="x in Data">
				<td class="center">{{ x.RowID }}</td>
                <td><input type="hidden" id="BO_ID" value="{{ x.BO_ID }}"/>{{ x.BO_Name }}</td>
                <td>{{ x.BO_Type_Name }}</td>
                <td class="center">{{ x.BO_Qty }}</td>          
                <td style="text-align:right">{{ x.BO_Price }}</td>														                                                            
                <td>{{ x.BO_Reason }}</td>		
			</tr>										
		</tbody>  
        <tfoot >
            <tr style="background-color:whitesmoke">                                                                
                <td colspan="7" style="padding:0px;text-align:right">                                                                  
                    <table style="margin:3px;width:100%">
                        <tr>
                            <td style="width:5px"></td>
                            <td style="text-align:left">
                                <span class="label label-lg label-primary"><%=Session["PageStart"]%> {{ RowCount }} <%=Session["PageRecord"]%></span>            
                            </td>
                            <td style="width:30px">
                                <label class="btn btn-primary btn-sm" ng-click="fnPageBack()">
                                    <i class="ace-icon fa fa-angle-double-left"></i>
								</label>
                            </td>                                                                              
                            <td style="width:30px">
                                <label class="btn btn-primary btn-sm" ng-click="fnPageBack()" ><%=Session["PageText"]%></label>
                            </td>
                            <td style="width:2px"></td>
                            <td style="width:30px">                                                                             
                                <select id="paging-select" onchange="fnPagingChange()" style="height:32px">
                                    <option ng-repeat="x in Paging" value="{{x.Page}}">{{ x.Page }}</option>
                                </select>                                                                                  
                            </td>
                            <td style="width:2px"></td>
                            <td style="width:30px">
                                <label class="btn btn-primary btn-sm" ng-click="fnPageBack()"> / {{ PageMax }} <%=Session["PageLast"]%></label>
                            </td>
                            <td style="width:30px">
                                <label class="btn btn-primary btn-sm" ng-click="fnPageNext()">
                                    <i class="ace-icon fa fa-angle-double-right"></i>
								</label>
                            </td>
                            <td style="width:5px"></td>
                        </tr>
                    </table>                                                                                                                       
                </td>
            </tr>
        </tfoot>                                              
	</table>
          
    </center>
    <script>
        function fnLoad() {
            var KeyID = getParamValue("KeyID");
            if (KeyID != null) {
                $.post("../server/Server_Budget.aspx",
                    {
                        Command: 'BudgetOperationDetail',
                        Function: 'Load',
                        KeyID: KeyID
                    },
                    function (data, status) {
                        var data = eval(data);
                        document.getElementById('BO_ID').value = data[0].BO_ID.trim();
                        document.getElementById('BO_Type_ID').value = data[0].BO_Type_ID.trim();
                        document.getElementById('BO_PRICE_MNT').value = data[0].BO_Qty.trim();
                        document.getElementById('BO_PRICE_YEAR').value = data[0].BO_Price.trim();
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

            fnGetBudgetOperationSum($scope, $http);
            
        }
        function fnGetBudgetOperationSum($scope, $http) {
            var Lang = '<%=Session["language_budget_operation"]%>';
            var BO_ID = getParamValue("BO_ID");
            $scope = $tmp_scope;
            $http = $tmp_http;

            var data = $.param({
                Command: 'BudgetOperationSummaryList',
                Function: 'fnBudgetOperationSummaryList',
                BO_ID: BO_ID,
                Lang: Lang
            });

            $http.post("../server/Server_Budget.aspx", data, config)
            .success(function (data, status, headers, config) {
                $scope.DataSum = data.records;
                fnGetBudgetOperationDetail($scope, $http);
            })
            .error(function (data, status, header, config) {
                $('body').pleaseWait('stop');
            });
        }
        function fnGetBudgetOperationDetail($scope, $http) {
            var Lang = '<%=Session["language_budget_operation"]%>';
            var BO_ID = getParamValue("BO_ID");
            $scope = $tmp_scope;
            $http = $tmp_http;

            var data = $.param({
                Command: 'BudgetOperationList',
                Function: 'detail',
                BO_ID: BO_ID,
                lang: lang
            });

            $http.post("../server/Server_Budget.aspx", data, config)
            .success(function (data, status, headers, config) {
                $scope.Data = data.records;
                $('body').pleaseWait('stop');
            })
            .error(function (data, status, header, config) {
                $('body').pleaseWait('stop');
            });
        }
        
    </script>
    
</asp:Content>

