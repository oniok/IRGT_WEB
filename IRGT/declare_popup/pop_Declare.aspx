<%@ Page Title="" Language="C#" MasterPageFile="~/master_page/popup.master" AutoEventWireup="true" CodeFile="pop_Declare.aspx.cs" Inherits="declare_popup_pop_Declare" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="../Scripts/angular.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <center>
    <table style="width:600px" ng-app="myApp" ng-controller="fnMain">           
        <tr>
            <td><%=Session["declare_Column07"] %></td>
            <td colspan="3"><input type="text" id="DC_YEAR" placeholder="<%=Session["text_placeholder"] %>" style="width:100%"/></td>
        </tr>
        <tr><td colspan="4" style="height:10px"></td></tr>      
        <tr>
            <td style="width:120px"><%=Session["declare_Column06"] %></td>
            <td colspan="3">
                <select class="chosen-select form-control" id="DC_Type_ID" data-placeholder="<%=Session["text_placeholder"] %>">
					<option value=""></option>	
                    <option ng-repeat="x in DC_Type" value="{{ x.Code }}" >{{ x.Name }}</option>				
				</select>        
            </td>
        </tr>
        <tr>
            <td></td>
            <td colspan="3" style="height:30px;vertical-align:middle">
                <label><input type="radio" id="DC_GROUP_01" name="DC_GROUP" value="ม.5" />&nbsp;ม.5</label>
                &nbsp;&nbsp;
                <label><input type="radio" id="DC_GROUP_02" name="DC_GROUP" value="ม.8" />&nbsp;ม.8</label>
            </td>
        </tr>
        <tr>
            <td style="width:120px"><%=Session["declare_Column16"] %></td>
            <td colspan="2">
                <input type="text" id="fileValue" style="width:100%" readonly />               
            </td>
            <td style="vertical-align:middle;width:170px">
                <table style="width:100%">
                    <tr>
                        <td style="width:5px"></td>
                        <td><a herf="#" id="fileView" target="_blank">VIEW</a></td>
                        <td style="width:5px"></td>
                        <td><input type="file" id="fileUpload" style="width:73px;" onchange="fnFilechange()" /></td>
                        <td style="width:5px"></td>
                        <td><input type="button" value="Clear" onclick="fnClear()" /></td>
                    </tr>
                </table>
            </td>
        </tr>

        <tr><td colspan="4" style="height:10px"></td></tr>

        <tr>
            <td colspan="4">               
                <div class="widget-box transparent" id="widget-box-13">
					<div class="widget-header">
						<h4 class="widget-title" style="color:black"></h4>
						<div class="widget-toolbar no-border">
							<ul class="nav nav-tabs" id="myTab2">
								<li class="active">
									<a data-toggle="tab" href="#home2"><%=Session["declare_Column14"] %></a>
								</li>
								<li>
									<a data-toggle="tab" href="#profile2"><%=Session["declare_Column15"] %></a>
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
                                            <td style="width:120px"><%=Session["declare_Column01"] %></td>
                                            <td colspan="3"><input type="text" id="DC_Name_T" placeholder="<%=Session["text_placeholder"] %>" style="width:100%"/></td>
                                        </tr>
                                        <tr><td colspan="4" style="height:10px"></td></tr>
                                        <tr>
                                            <td><%=Session["declare_Column02"] %></td>
                                            <td colspan="3"><input type="text" id="DC_FORM_T" placeholder="<%=Session["text_placeholder"] %>" style="width:100%"/></td>
                                        </tr>
                                        <tr><td colspan="4" style="height:10px"></td></tr>
                                        <tr>
                                            <td><%=Session["declare_Column03"] %></td>
                                            <td colspan="3"><input type="text" id="DC_TO_T" placeholder="<%=Session["text_placeholder"] %>" style="width:100%"/></td>
                                        </tr> 
                                    </table>
								</div>
								<div id="profile2" class="tab-pane">
									<table style="width:100%">
                                        <tr>
                                            <td style="width:120px"><%=Session["declare_Column01"] %></td>
                                            <td colspan="3"><input type="text" id="DC_Name_E" placeholder="<%=Session["text_placeholder"] %>" style="width:100%"/></td>
                                        </tr>
                                        <tr><td colspan="4" style="height:10px"></td></tr>
                                        <tr>
                                            <td><%=Session["declare_Column02"] %></td>
                                            <td colspan="3"><input type="text" id="DC_FORM_E" placeholder="<%=Session["text_placeholder"] %>" style="width:100%"/></td>
                                        </tr>
                                        <tr><td colspan="4" style="height:10px"></td></tr>
                                        <tr>
                                            <td><%=Session["declare_Column03"] %></td>
                                            <td colspan="3"><input type="text" id="DC_TO_E" placeholder="<%=Session["text_placeholder"] %>" style="width:100%"/></td>
                                        </tr> 
                                    </table>
								</div>								
							</div>
						</div>
					</div>
				</div>  
            </td>
        </tr>       
        <tr><td colspan="4" style="height:10px"></td></tr>
        <tr>
            <td>Latitude</td>
            <td colspan="3"><input type="text" id="DC_LAT" placeholder="<%=Session["text_placeholder"] %>" style="width:100%"/></td>
        </tr>
        <tr><td colspan="4" style="height:10px"></td></tr>
        <tr>
            <td>Longitude</td>
            <td colspan="3"><input type="text" id="DC_LON" placeholder="<%=Session["text_placeholder"] %>" style="width:100%"/></td>
        </tr> 
    </table>   
    </center>
    <script>
        function fnLoad() {
            var KeyID = getParamValue("KeyID");
            if (KeyID != null) {
                $.post("../server/DeclareService.aspx",
                    {
                        Command: 'Declare',
                        Function: 'Load',
                        KeyID: KeyID
                    },
                    function (data, status) {
                        var data = eval(data);
                        document.getElementById('DC_Type_ID').value = data[0].DC_Type_ID.trim();
                        document.getElementById('DC_Name_T').value = data[0].DC_Name_T.trim();
                        document.getElementById('DC_FORM_T').value = data[0].DC_FORM_T.trim();
                        document.getElementById('DC_TO_T').value = data[0].DC_TO_T.trim();
                        document.getElementById('DC_Name_E').value = data[0].DC_Name_E.trim();
                        document.getElementById('DC_FORM_E').value = data[0].DC_FORM_E.trim();
                        document.getElementById('DC_TO_E').value = data[0].DC_TO_E.trim();
                        document.getElementById('DC_YEAR').value = data[0].DC_YEAR.trim();
                        document.getElementById('DC_LAT').value = data[0].DC_LAT.trim();
                        document.getElementById('DC_LON').value = data[0].DC_LON.trim();
                        document.getElementById('fileValue').value = data[0].DC_FileName.trim();
                        document.getElementById('fileView').href = "../upload/declare/" + data[0].DC_FileName.trim();
                        if (data[0].DC_GROUP.trim() == "ม.5") {
                            document.getElementById('DC_GROUP_01').checked = true;
                        } else {
                            document.getElementById('DC_GROUP_02').checked = true;
                        }


                        $('body').pleaseWait('stop');
                        fnLoadCtrl();
                    }
                );
            } else {
                $('body').pleaseWait('stop');
                fnLoadCtrl();
            }          

        }
        function fnClear() {
            document.getElementById('fileValue').value = "";
        }
        function fnFilechange() {
            var x = document.getElementById("fileUpload");
           
            if ('files' in x) {
                if (x.files.length == 0) {
                    txt = "Select one or more files.";
                } else {
                    for (var i = 0; i < x.files.length; i++) {                        
                        var file = x.files[i];
                        if ('name' in file) {
                            //txt += "name: " + file.name + "<br>";
                            document.getElementById('fileValue').value = file.name;
                            //document.getElementById('fileView').href = "../upload/declare/" + file.name;
                            return;
                        }                       
                    }
                }
            }            
        }
        function fnSave() {
            var KeyID = getParamValue("KeyID");
            var DC_Type_ID = document.getElementById('DC_Type_ID').value.trim();
            var DC_Name_T = document.getElementById('DC_Name_T').value.trim();
            var DC_FORM_T = document.getElementById('DC_FORM_T').value.trim();
            var DC_TO_T = document.getElementById('DC_TO_T').value.trim();
            var DC_Name_E = document.getElementById('DC_Name_E').value.trim();
            var DC_FORM_E = document.getElementById('DC_FORM_E').value.trim();
            var DC_TO_E = document.getElementById('DC_TO_E').value.trim();
            var DC_YEAR = document.getElementById('DC_YEAR').value.trim();
            var DC_LAT = document.getElementById('DC_LAT').value.trim();
            var DC_LON = document.getElementById('DC_LON').value.trim();
            var fileValue = document.getElementById('fileValue').value.trim();
            var DC_GROUP = "";

           
           
            if (document.getElementById('DC_GROUP_01').checked) {
                DC_GROUP = "ม.5";
            } else {
                DC_GROUP = "ม.8";
            }

            var lang = getParamValue("lang");

            if (DC_Type_ID == "") {
                fnErrorMessage("ข้อผิดพลาด / Error", "<%=Session["declare_ERROR_01"]%>");
                return;
            }
            if (DC_Name_T == "") {
                fnErrorMessage("ข้อผิดพลาด / Error", "<%=Session["declare_ERROR_02"]%>");
                return;
            }
            if (DC_FORM_T == "") {
                fnErrorMessage("ข้อผิดพลาด / Error", "<%=Session["declare_ERROR_03"]%>");
                return;
            }
            if (DC_TO_T == "") {
                fnErrorMessage("ข้อผิดพลาด / Error", "<%=Session["declare_ERROR_04"]%>");
                return;
            }

           
            
            $.post("../server/DeclareService.aspx",
              {
                  Command: 'Declare',
                  Function: 'Save',                 
                  KeyID: KeyID,
                  KeyID: KeyID,
                  DC_Type_ID: DC_Type_ID,
                  DC_Name_T: DC_Name_T,
                  DC_FORM_T: DC_FORM_T,
                  DC_TO_T: DC_TO_T,
                  DC_Name_E: DC_Name_E,
                  DC_FORM_E: DC_FORM_E,
                  DC_TO_E: DC_TO_E,
                  DC_YEAR: DC_YEAR,
                  DC_LAT: DC_LAT,
                  DC_LON: DC_LON,
                  DC_GROUP: DC_GROUP,
                  DC_FileName:fileValue,
                  lang: lang
              },
              function (data, status) {
                  var data = eval(data);
                  if (data[0].output == "OK") {

                      var file_data = $('#fileUpload').prop('files')[0];
                      var form_data = new FormData();
                      form_data.append('file', file_data);

                      $.ajax({
                          url: '../server/FileUpload.aspx', // point to server-side PHP script 
                          dataType: 'text',  // what to expect back from the PHP script, if anything                        
                          cache: false,
                          contentType: false,
                          processData: false,
                          data: form_data,
                          type: 'post',
                          success: function (php_script_response) {
                              window.parent.fnRefresh();
                          }
                      });
                      
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
            fnGetDCType($scope, $http);
        }
        function fnGetDCType() {

            $scope = $tmp_scope;
            $http = $tmp_http;

            var data = $.param({
                Command: 'GetMasterData',
                Function: 'DC_Type',
                PageName: 'declare'
            });

            $http.post("../server/Server.aspx", data, config)
            .success(function (data, status, headers, config) {
                $scope.DC_Type = data.records;
                setTimeout(fnLoad, 100);
            })
            .error(function (data, status, header, config) {
                $('body').pleaseWait('stop');
            });
        }
        


    </script>
</asp:Content>
