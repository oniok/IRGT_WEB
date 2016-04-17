<%@ Page Title="" Language="C#" MasterPageFile="~/master_page/popup.master" AutoEventWireup="true" CodeFile="pop_AssetDetail.aspx.cs" Inherits="admin_popup_pop_AssetDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="../Scripts/angular.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <center>
    <table style="width:100%" ng-app="myApp" ng-controller="fnMain">
        <tr>
            <td style="width:150px"><h4><%=Session["asset_detail_Column02"] %></h4></td>
            <td colspan="3">
                <select class="chosen-select form-control" id="Loc_ID" data-placeholder="<%=Session["text_placeholder"] %>">
					<option value=""></option>	
                    <option ng-repeat="x in WorkCenter" value="{{ x.Code }}" >{{ x.Name }}</option>				
				</select>                               
            </td>
        </tr>
        <tr><td colspan="4" style="height:5px"></td></tr>
        <tr>
            <td><h4><%=Session["asset_detail_Column03"] %></h4></td>
            <td colspan="3">
                <select class="chosen-select form-control" id="Asset_Type_ID" data-placeholder="<%=Session["text_placeholder"] %>">
					<option value=""></option>	
                    <option ng-repeat="x in AssetType" value="{{ x.Code }}" >{{ x.Name }}</option>				
				</select>                               
            </td>
        </tr>
        <tr><td colspan="4" style="height:5px"></td></tr>
        <tr>
            <td><h4><%=Session["asset_detail_Column04"] %></h4></td>
            <td colspan="3">
                <select class="chosen-select form-control" id="Asset_ID" data-placeholder="<%=Session["text_placeholder"] %>">
					<option value=""></option>	
                    <option ng-repeat="x in Asset" value="{{ x.Code }}" >{{ x.Name }}</option>				
				</select>              
            </td>
        </tr>  
        <tr><td colspan="4" style="height:5px"></td></tr>
        <tr>
            <td colspan="4">
                <div class="widget-box transparent" id="widget-box-13">
					<div class="widget-header">
						<h4 class="widget-title" style="color:black"></h4>
						<div class="widget-toolbar no-border">
							<ul class="nav nav-tabs" id="myTab2">
								<li class="active">
									<a data-toggle="tab" href="#home2">รายละเอียด</a>
								</li>
								<li>
									<a data-toggle="tab" href="#profile2">ข้อมูลครุภัณฑ์</a>
								</li>
							</ul>
						</div>
					</div>
					<div class="widget-body">
						<div class="widget-main padding-12 no-padding-left no-padding-right">
							<div class="tab-content padding-4">
								<div id="home2" class="tab-pane in active">
									<table style="width:100%">
                                        <tr>
                                            <td rowspan="11" style="width:30px"></td>
                                            <td><%=Session["asset_detail_Column01"] %></td>
                                            <td><input type="text" id="Item_ID" placeholder="<%=Session["text_placeholder"] %>" style="width:100%"/></td> 
                                            <td style="width:20px"></td>                                       
                                            <td><%=Session["asset_detail_Column05"] %></td>
                                            <td><input type="text" id="Owner" placeholder="<%=Session["text_placeholder"] %>" style="width:100%"/></td>
                                        </tr>
                                        <tr><td colspan="5" style="height:5px"></td></tr>
                                        <tr>
                                            <td><%=Session["asset_detail_Column06"] %></td>
                                            <td><input type="text" id="Budget_Year" placeholder="<%=Session["text_placeholder"] %>" style="width:100%"/></td>      
                                            <td style="width:5px"></td>                                       
                                            <td><%=Session["asset_detail_Column07"] %></td>
                                            <td>
                                                <select class="chosen-select form-control" id="Fund_Type_ID" data-placeholder="<%=Session["text_placeholder"] %>">
					                                <option value=""></option>	
                                                    <option ng-repeat="x in FundType" value="{{ x.Code }}" >{{ x.Name }}</option>				
				                                </select> 
                                            </td>
                                        </tr>
                                        <tr><td colspan="5" style="height:5px"></td></tr>
                                        <tr>
                                            <td><%=Session["asset_detail_Column08"] %></td>
                                            <td><input type="text" id="Purchase_Doc_No" placeholder="<%=Session["text_placeholder"] %>" style="width:100%"/></td>
                                            <td style="width:5px"></td>                                            
                                            <td><%=Session["asset_detail_Column09"] %></td>
                                            <td>
                                                <div class="input-group">
				                                    <input class="form-control date-picker" id="Purchase_Doc_Date" type="text" data-date-format="dd/mm/yyyy" placeholder="<%=Session["text_placeholder"] %>"/>
				                                    <span class="input-group-addon">
					                                    <i class="fa fa-calendar bigger-110"></i>
				                                    </span>
			                                    </div>
                                            </td>                                                
                                        </tr>
                                        <tr><td colspan="5" style="height:5px"></td></tr>
                                        <tr>
                                            <td><%=Session["asset_detail_Column10"] %></td>
                                            <td><input type="text" id="Document_No" placeholder="<%=Session["text_placeholder"] %>" style="width:100%"/></td>  
                                            <td style="width:5px"></td>                                          
                                            <td><%=Session["asset_detail_Column11"] %></td>
                                            <td>
                                                <div class="input-group">
				                                    <input class="form-control date-picker" id="Document_Date" type="text" data-date-format="dd/mm/yyyy" placeholder="<%=Session["text_placeholder"] %>"/>
				                                    <span class="input-group-addon">
					                                    <i class="fa fa-calendar bigger-110"></i>
				                                    </span>
			                                    </div>
                                            </td>
                                        </tr>
                                        <tr><td colspan="5" style="height:5px"></td></tr>
                                        <tr>
                                            <td><%=Session["asset_detail_Column12"] %></td>
                                            <td><input type="text" id="Asset_Loc" placeholder="<%=Session["text_placeholder"] %>" style="width:100%"/></td>  
                                            <td style="width:5px"></td>                                          
                                            <td><%=Session["asset_detail_Column13"] %></td>
                                            <td><input type="text" id="Receive_from" placeholder="<%=Session["text_placeholder"] %>" style="width:100%"/></td>
                                        </tr>
                                        <tr><td colspan="5" style="height:5px"></td></tr>
                                        <tr>
                                            <td><%=Session["asset_detail_Column14"] %></td>
                                            <td><input type="text" id="Tranfer_Loc" placeholder="<%=Session["text_placeholder"] %>" style="width:100%"/></td>  
                                            <td style="width:5px"></td>                                          
                                            <td><%=Session["asset_detail_Column15"] %></td>
                                            <td>
                                                <select class="chosen-select form-control" id="Mvt_ID" data-placeholder="<%=Session["text_placeholder"] %>">
					                                <option value=""></option>	
                                                    <option ng-repeat="x in Mvt" value="{{ x.Code }}" >{{ x.Name }}</option>				
				                                </select> 
                                            </td>
                                        </tr>
									</table>
								</div>
								<div id="profile2" class="tab-pane">
									<table style="width:100%">
                                        <tr>
                                            <td rowspan="9" style="width:30px"></td>
                                            <td><%=Session["asset_detail_Column16"] %></td>
                                            <td><input type="text" id="Serial_No" placeholder="<%=Session["text_placeholder"] %>" style="width:100%"/></td> 
                                            <td style="width:20px"></td>                                       
                                            <td><%=Session["asset_detail_Column17"] %></td>
                                            <td><input type="text" id="Serie" placeholder="<%=Session["text_placeholder"] %>" style="width:100%"/></td>
                                        </tr>
                                        <tr><td colspan="5" style="height:5px"></td></tr>
                                        <tr>
                                            <td><%=Session["asset_detail_Column18"] %></td>
                                            <td><input type="text" id="Brand" placeholder="<%=Session["text_placeholder"] %>" style="width:100%"/></td>      
                                            <td style="width:5px"></td>                                       
                                            <td><%=Session["asset_detail_Column19"] %></td>
                                            <td><input type="text" id="Term_Use" placeholder="<%=Session["text_placeholder"] %>" style="width:100%"/></td>      
                                        </tr> 
                                        <tr><td colspan="5" style="height:5px"></td></tr>
                                        <tr>
                                            <td><%=Session["asset_detail_Column20"] %></td>
                                            <td><input type="text" id="Quality" placeholder="<%=Session["text_placeholder"] %>" style="width:100%"/></td>      
                                            <td style="width:5px"></td>                                       
                                            <td><%=Session["asset_detail_Column21"] %></td>
                                            <td><input type="text" id="Depriciate_accru" placeholder="<%=Session["text_placeholder"] %>" style="width:100%"/></td>      
                                        </tr> 
                                        <tr><td colspan="5" style="height:5px"></td></tr>
                                        <tr>
                                            <td><%=Session["asset_detail_Column22"] %></td>
                                            <td><input type="text" id="Depriciate_in_year" placeholder="<%=Session["text_placeholder"] %>" style="width:100%"/></td>      
                                            <td style="width:5px"></td>                                       
                                            <td><%=Session["asset_detail_Column23"] %></td>
                                            <td><input type="text" id="Price_per_unit" placeholder="<%=Session["text_placeholder"] %>" style="width:100%"/></td>      
                                        </tr> 
                                        <tr><td colspan="5" style="height:5px"></td></tr>
                                        <tr>
                                            <td><%=Session["asset_detail_Column24"] %></td>
                                            <td><input type="text" id="Standard_Price" placeholder="<%=Session["text_placeholder"] %>" style="width:100%"/></td>      
                                            <td style="width:5px"></td>                                       
                                            <td><%=Session["asset_detail_Column25"] %></td>
                                            <td><input type="text" id="Net_Price" placeholder="<%=Session["text_placeholder"] %>" style="width:100%"/></td>      
                                        </tr>                                       
									</table>
								</div>								
							</div>
						</div>
					</div>
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
                        Command: 'AssetDetail',
                        Function: 'Load',
                        KeyID: KeyID
                    },
                    function (data, status) {
                        var data = eval(data);
                        document.getElementById('Loc_ID').value = data[0].Loc_ID.trim();
                        document.getElementById('Asset_Type_ID').value = data[0].Asset_Type_ID.trim();
                        document.getElementById('Asset_ID').value = data[0].Asset_ID.trim();

                        document.getElementById('Item_ID').value = data[0].Item_ID.trim();
                        document.getElementById('Owner').value = data[0].Owner.trim();
                        document.getElementById('Budget_Year').value = data[0].Budget_Year.trim();
                        document.getElementById('Fund_Type_ID').value = data[0].Fund_Type_ID.trim();
                        document.getElementById('Purchase_Doc_No').value = data[0].Purchase_Doc_no.trim();
                        document.getElementById('Purchase_Doc_Date').value = data[0].Purchase_Doc_date.trim();
                        document.getElementById('Document_No').value = data[0].Document_No.trim();
                        document.getElementById('Document_Date').value = data[0].Document_Date.trim();
                        document.getElementById('Asset_Loc').value = data[0].Asset_Loc.trim();
                        document.getElementById('Receive_from').value = data[0].Receive_from.trim();
                        document.getElementById('Tranfer_Loc').value = data[0].Tranfer_Loc.trim();
                        document.getElementById('Mvt_ID').value = data[0].Mvt_ID.trim();

                        document.getElementById('Serial_No').value = data[0].Serial_No.trim();
                        document.getElementById('Serie').value = data[0].Serie.trim();
                        document.getElementById('Brand').value = data[0].Brand.trim();
                        document.getElementById('Term_Use').value = data[0].Term_Use.trim();
                        document.getElementById('Quality').value = data[0].Quality.trim();
                        document.getElementById('Depriciate_accru').value = data[0].Depriciate_accru.trim();
                        document.getElementById('Depriciate_in_year').value = data[0].Depriciate_in_year.trim();
                        document.getElementById('Price_per_unit').value = data[0].Price_per_unit.trim();
                        document.getElementById('Standard_Price').value = data[0].Standard_Price.trim();
                        document.getElementById('Net_Price').value = data[0].Net_Price.trim();












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
            var Loc_ID = document.getElementById('Loc_ID').value.trim();
            var Asset_Type_ID = document.getElementById('Asset_Type_ID').value.trim();
            var Asset_ID = document.getElementById('Asset_ID').value.trim();
            var Item_ID = document.getElementById('Item_ID').value.trim();

            if (Loc_ID == "") {
                fnErrorMessage("ข้อผิดพลาด / Error", "<%=Session["asset_detail_ERROR_01"]%>");
                return;
            }
            if (Asset_Type_ID == "") {
                fnErrorMessage("ข้อผิดพลาด / Error", "<%=Session["asset_detail_ERROR_02"]%>");
                return;
            }
            if (Asset_ID == "") {
                fnErrorMessage("ข้อผิดพลาด / Error", "<%=Session["asset_detail_ERROR_03"]%>");
                return;
            }
            if (Item_ID == "") {
                fnErrorMessage("ข้อผิดพลาด / Error", "<%=Session["asset_detail_ERROR_04"]%>");
                return;
            }

            $.post("../server/Server.aspx",
			    {
			        Command: 'AssetDetail',
			        Function: 'Check',
			        KeyID: KeyID,
			        Loc_ID: Loc_ID,
			        Asset_Type_ID: Asset_Type_ID,
			        Asset_ID: Asset_ID,
			        Item_ID: Item_ID
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
            var lang = getParamValue("lang");

            var Loc_ID = document.getElementById('Loc_ID').value.trim();
            var Asset_Type_ID = document.getElementById('Asset_Type_ID').value.trim();
            var Asset_ID = document.getElementById('Asset_ID').value.trim();
            var Item_ID = document.getElementById('Item_ID').value.trim();
            
            var Item_ID = document.getElementById('Item_ID').value.trim();
            var Owner = document.getElementById('Owner').value.trim();
            var Budget_Year = document.getElementById('Budget_Year').value.trim();
            var Fund_Type_ID = document.getElementById('Fund_Type_ID').value.trim();
            var Purchase_Doc_No = document.getElementById('Purchase_Doc_No').value.trim();
            var Purchase_Doc_Date = document.getElementById('Purchase_Doc_Date').value.trim();
            var Document_No = document.getElementById('Document_No').value.trim();
            var Document_Date = document.getElementById('Document_Date').value.trim();
            var Asset_Loc = document.getElementById('Asset_Loc').value.trim();
            var Receive_from = document.getElementById('Receive_from').value.trim();
            var Tranfer_Loc = document.getElementById('Tranfer_Loc').value.trim();
            var Mvt_ID = document.getElementById('Mvt_ID').value.trim();

            var Serial_No = document.getElementById('Serial_No').value.trim();
            var Serie = document.getElementById('Serie').value.trim();
            var Brand = document.getElementById('Brand').value.trim();
            var Term_Use = document.getElementById('Term_Use').value.trim();
            var Quality = document.getElementById('Quality').value.trim();
            var Depriciate_accru = document.getElementById('Depriciate_accru').value.trim();
            var Depriciate_in_year = document.getElementById('Depriciate_in_year').value.trim();
            var Price_per_unit = document.getElementById('Price_per_unit').value.trim();
            var Standard_Price = document.getElementById('Standard_Price').value.trim();
            var Net_Price = document.getElementById('Net_Price').value.trim();


            $.post("../server/Server.aspx",
               {
                   Command: 'AssetDetail',
                   Function: 'Save',
                   KeyID: KeyID,
                   Loc_ID: Loc_ID,
                   Asset_Type_ID: Asset_Type_ID,
                   Asset_ID: Asset_ID,
                   Item_ID: Item_ID,
                   Owner: Owner,
                   Budget_Year: Budget_Year,
                   Fund_Type_ID: Fund_Type_ID,
                   Purchase_Doc_No: Purchase_Doc_No,
                   Purchase_Doc_Date: Purchase_Doc_Date,
                   Document_No: Document_No,
                   Document_Date: Document_Date,
                   Asset_Loc:Asset_Loc,
                   Receive_from: Receive_from,
                   Tranfer_Loc: Tranfer_Loc,
                   Mvt_ID: Mvt_ID,
                   Serial_No: Serial_No,
                   Serie: Serie,
                   Brand: Brand,
                   Term_Use: Term_Use,
                   Quality: Quality,
                   Depriciate_accru: Depriciate_accru,
                   Depriciate_in_year: Depriciate_in_year,
                   Price_per_unit: Price_per_unit,
                   Standard_Price: Standard_Price,
                   Net_Price: Net_Price,
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
            var data = $.param({
                Command: 'GetMasterData',
                Function: 'AssetType',
                PageName: 'asset_detail'
            });

            $http.post("../server/Server.aspx", data, config)
            .success(function (data, status, headers, config) {
                $scope.AssetType = data.records;
                setTimeout(fnGetAsset, 100);
            })
            .error(function (data, status, header, config) {
                $('body').pleaseWait('stop');
            });
        }
        function fnGetAsset() {

            $scope = $tmp_scope;
            $http = $tmp_http;

            var data = $.param({
                Command: 'GetMasterData',
                Function: 'Asset',
                PageName: 'asset_detail'
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
                PageName: 'asset_detail'
            });

            $http.post("../server/Server.aspx", data, config)
            .success(function (data, status, headers, config) {
                $scope.WorkCenter = data.records;
                setTimeout(fnGetFundType, 100);
            })
            .error(function (data, status, header, config) {
                $('body').pleaseWait('stop');
            });
        }
        function fnGetFundType() {

            $scope = $tmp_scope;
            $http = $tmp_http;

            var data = $.param({
                Command: 'GetMasterData',
                Function: 'FundType',
                PageName: 'asset_detail'
            });

            $http.post("../server/Server.aspx", data, config)
            .success(function (data, status, headers, config) {
                $scope.FundType = data.records;
                setTimeout(fnGetMvt, 100);
            })
            .error(function (data, status, header, config) {
                $('body').pleaseWait('stop');
            });
        }
        function fnGetMvt() {

            $scope = $tmp_scope;
            $http = $tmp_http;

            var data = $.param({
                Command: 'GetMasterData',
                Function: 'Mvt',
                PageName: 'asset_detail'
            });

            $http.post("../server/Server.aspx", data, config)
            .success(function (data, status, headers, config) {
                $scope.Mvt = data.records;
                setTimeout(fnLoad, 100);
            })
            .error(function (data, status, header, config) {
                $('body').pleaseWait('stop');
            });
        }


    </script>
</asp:Content>

