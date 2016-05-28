<%@ Page Title="" Language="C#" MasterPageFile="~/master_page/popup.master" AutoEventWireup="true" CodeFile="pop_BudgetAssetSummary.aspx.cs" Inherits="budget_popup_pop_BudgetAssetSummary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script src="../Scripts/angular.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <center>
    <table id="dynamic-table-sum" class="table table-striped table-bordered table-hover" ng-app="myApp" ng-controller="fnMain">
		<thead>
			<tr>
				<th class="center" style="width:50px"><%=Session["budget_asset_summary_ColumnSEQ"]%></th>
				<th class="center" style="width:300px"><%=Session["budget_asset_summary_Column01"]%></th>
                <th class="center" style="width:80px"><%=Session["budget_asset_summary_Column02"]%></th>
                <th class="center" style="width:80px"><%=Session["budget_asset_summary_Column03"]%></th>
                <th class="center" style="width:80px"><%=Session["budget_asset_summary_Column04"]%></th>
                <th class="center" style="width:80px"><%=Session["budget_asset_summary_Column05"]%></th>
                <th class="center" style="width:80px"><%=Session["budget_asset_summary_Column06"]%></th>
                <th class="center" style="width:80px"><%=Session["budget_asset_summary_Column07"]%></th>
                <th class="center" style="width:80px"><%=Session["budget_asset_summary_Column08"]%></th>
                <th class="center" style="width:80px"><%=Session["budget_asset_summary_Column09"]%></th>
                <th class="center" style="width:80px"><%=Session["budget_asset_summary_Column10"]%></th>
                <th class="center" style="width:80px"><%=Session["budget_asset_summary_Column11"]%></th>
				<th class="center" style="width:80px"><%=Session["budget_asset_summary_Column12"]%></th>
                <th class="center" style="width:120px"><%=Session["budget_asset_summary_Column13"]%></th>          
                <th class="center" style="width:120px"><%=Session["budget_asset_summary_Column14"]%></th>    
                <th class="center" style="width:500px"><%=Session["budget_asset_summary_Column15"]%></th>
			</tr>
		</thead>
        <tbody>
			<tr ng-repeat="x in DataSum">
				<td class="center">{{ x.RowID }}</td>
                <td>{{ x.BA_Type_Name }}</td>
                <td style="text-align:right">{{ x.Quota_Qty }}</td> 
                <td style="text-align:right">{{ x.Unit_Name }}</td> 
                <td style="text-align:right">{{ x.Stock_Qty }}</td> 
                <td style="text-align:right">{{ x.BA_Qty }}</td> 
                <td style="text-align:right">{{ x.BA_Qty }}</td> 
                <td style="text-align:right">{{ x.BA_Qty }}</td> 
                <td style="text-align:right">{{ x.BA_Qty }}</td> 
                <td style="text-align:right">{{ x.BA_Qty }}</td> 
                <td style="text-align:right">{{ x.BA_Qty }}</td> 
                <td style="text-align:right">{{ x.BA_Qty }}</td> 
                <td style="text-align:right">{{ x.BA_Qty }}</td>          
                <td style="text-align:right">{{ x.BA_Price }}</td>	
                <td style="text-align:right">{{ x.Total_Amount }}</td>														                                                            
                <td><input type="text" id="BA_Remark{{x.RowID}}" style="width:100%" value="{{ x.BA_Remark }}"/>   </td>
														
			</tr>										
		</tbody>  
        <tfoot >
      
        </tfoot>                                              
	</table>   
    </center>
    <script>
        function fnSave() {
            var User_Code = '<%=Session["user_code"]%>';
            var Lang = '<%=Session["language_budget_asset"]%>';

            $scope = $tmp_scope;
            $http = $tmp_http;

            var data = $.param({
                Command: 'BudgetAssetSummary',
                Function: 'Select',
                User_Code: User_Code,
                Lang: Lang
            });

            $http.post("../server/Server_Budget_Asset.aspx", data, config)
            .success(function (data, status, headers, config) {
                if (data!="") {
                    for (var i = 0; i < data.records.length; i++) {
                        fnSubmit(data.records[i].KeyID, data.records[i].BA_ID, data.records[i].BA_Type_ID, data.records[i].RowID);
                    }
                    setTimeout(window.parent.fnRefresh(), 1000);
                }
                window.parent.fnRefresh();
            })
            .error(function (data, status, header, config) {
                fnErrorMessage("ข้อผิดพลาด / Error", data[0].message);
            });

        }

        function fnSubmit(KeyID,BA_ID,BA_Type_ID,RowID) {
            var BA_Remark = '';
            if ($('#BA_Remark' + RowID).val() != null) {
                BA_Remark = $('#BA_Remark' + RowID).val();
            }

            $.post("../server/Server_Budget_Asset.aspx",
               {
                   Command: 'BudgetAssetSummary',
                   Function: 'Save',
                   KeyID: KeyID,
                   BA_ID: BA_ID,
                   BA_Type_ID: BA_Type_ID,
                   BA_Remark: BA_Remark
               },
               function (data, status) {
                   var data = eval(data);
                   if (data[0].output == "OK") {
                       //window.parent.fnRefresh();
                   } else {
                       //fnErrorMessage("ข้อผิดพลาด / Error", data[0].message);
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

            $scope.fnEdit = function (KeyID) {           //,BA_ID,BA_Type_ID
               fnOpenPopup('<%=Session["pop_sum_budget_asset"]%>', "../pop_BudgetAssetRemark.aspx?KeyID=" + KeyID, null, "450");
            }
            fnGetBudgetAssetSummary($scope, $http);
        }
        function fnGetBudgetAssetSummary($scope, $http) {
            var User_Code = '<%=Session["user_code"]%>';
            var Lang = '<%=Session["language_budget_asset"]%>';
            $scope = $tmp_scope;
            $http = $tmp_http;

            var data = $.param({
                Command: 'BudgetAssetSummary',
                Function: 'Select',
                User_Code: User_Code,
                Lang: Lang
            });

            $http.post("../server/Server_Budget_Asset.aspx", data, config)
            .success(function (data, status, headers, config) {
                $scope.DataSum = data.records;
                $('body').pleaseWait('stop');
            })
            .error(function (data, status, header, config) {
                $('body').pleaseWait('stop');
            });
        }
        
    </script>
    
</asp:Content>

