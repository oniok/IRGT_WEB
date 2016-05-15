<%@ Page Title="" Language="C#" MasterPageFile="~/master_page/popup.master" AutoEventWireup="true" CodeFile="pop_FundType.aspx.cs" Inherits="admin_popup_pop_FundType" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <center>
    <table style="width:600px">
        <tr>
            <td style="width:135px"><%=Session["fund_type_Column01"] %></td>
            <td colspan="3"><input type="text" id="Fund_Type_ID" style="width:100px" /></td>
        </tr>
        <tr><td colspan="4" style="height:10px"></td></tr>
        <tr>
            <td><%=Session["fund_type_Column02"] %></td>
            <td colspan="3"><input type="text" id="Fund_Type_Name_T" placeholder="<%=Session["text_placeholder"] %>" style="width:100%"/></td>
        </tr>
        <tr><td colspan="4" style="height:10px"></td></tr>
        <tr>
            <td><%=Session["fund_type_Column03"] %></td>
            <td colspan="3"><input type="text" id="Fund_Type_Name_E" placeholder="<%=Session["text_placeholder"] %>" style="width:100%"/></td>
        </tr>
        <tr><td colspan="4" style="height:10px"></td></tr>
        <tr>
            <td><%=Session["fund_type_Column04"] %></td>
            <td>
                <div class="input-group">
				    <input class="form-control date-picker" id="StartDate" type="text" data-date-format="dd/mm/yyyy" placeholder="<%=Session["text_placeholder"] %>"/>
				    <span class="input-group-addon">
					    <i class="fa fa-calendar bigger-110"></i>
				    </span>
			    </div>
            </td>
            <td style="width:80px;text-align:right"><%=Session["fund_type_Column05"] %>&nbsp;</td>
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
                $('body').pleaseWait();

                $.post("../server/Server.aspx",
                    {
                        Command: 'FundType',
                        Function: 'Load',
                        KeyID: KeyID
                    },
                    function (data, status) {
                        var data = eval(data);
                        document.getElementById('Fund_Type_ID').value = data[0].Fund_Type_ID.trim();
                        document.getElementById('Fund_Type_Name_T').value = data[0].Fund_Type_Name_T.trim();
                        document.getElementById('Fund_Type_Name_E').value = data[0].Fund_Type_Name_E.trim();
                        document.getElementById('StartDate').value = data[0].Start_Date.trim();
                        document.getElementById('EndDate').value = data[0].End_Date.trim();

                        $('body').pleaseWait('stop');
                    }
                );
            }
        }
        function fnSave() {
            var KeyID = getParamValue("KeyID");
            var Fund_Type_ID = document.getElementById('Fund_Type_ID').value.trim();
            var Fund_Type_Name_T = document.getElementById('Fund_Type_Name_T').value.trim();
            var Fund_Type_Name_E = document.getElementById('Fund_Type_Name_E').value.trim();
            var StartDate = document.getElementById('StartDate').value;
            var EndDate = document.getElementById('EndDate').value;
            var lang = getParamValue("lang");
            if (lang == '') lang = "TH";

            if (Fund_Type_ID == "") {
                fnErrorMessage("ข้อผิดพลาด / Error", "<%=Session["fund_type_ERROR_01"]%>");
                return;
            }
            if (Fund_Type_Name_T == "") {
                fnErrorMessage("ข้อผิดพลาด / Error", "<%=Session["fund_type_ERROR_02"]%>");
                return;
            }
            if (Fund_Type_Name_E == "") {
                fnErrorMessage("ข้อผิดพลาด / Error", "<%=Session["fund_type_ERROR_03"]%>");
                return;
            }
            
            if (EndDate != "") {
                if (StartDate == "") {
                    fnErrorMessage("ข้อผิดพลาด / Error", "<%=Session["fund_type_ERROR_04"]%>");
                    return;
                }

                var SP = StartDate.split("/");
                var SP_Start = parseInt(SP[2]) * 365 + parseInt(SP[1]) * 30 + parseInt(SP[0]);
                var SP2 = EndDate.split("/");
                var SP_END = parseInt(SP2[2]) * 365 + parseInt(SP2[1]) * 30 + parseInt(SP2[0]);
                if (SP_Start > SP_END) {
                    fnErrorMessage("ข้อผิดพลาด / Error", "<%=Session["fund_type_ERROR_06"]%>");
                    return;
                }
            }

            $.post("../server/Server.aspx",
			    {
			        Command: 'FundType',
			        Function: 'Check',
			        KeyID: KeyID,
			        Fund_Type_ID: Fund_Type_ID,
			        Fund_Type_Name_T: Fund_Type_Name_T,
			        Fund_Type_Name_E: Fund_Type_Name_E,
			        StartDate: StartDate,
			        EndDate: EndDate,
			        lang: lang
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
            var Fund_Type_ID = document.getElementById('Fund_Type_ID').value.trim();
            var Fund_Type_Name_T = document.getElementById('Fund_Type_Name_T').value.trim();
            var Fund_Type_Name_E = document.getElementById('Fund_Type_Name_E').value.trim();
            var StartDate = document.getElementById('StartDate').value;
            var EndDate = document.getElementById('EndDate').value;
            var lang = getParamValue("lang");

            $.post("../server/Server.aspx",
               {
                   Command: 'FundType',
                   Function: 'Save',
                   KeyID: KeyID,
                   Fund_Type_ID: Fund_Type_ID,
                   Fund_Type_Name_T: Fund_Type_Name_T,
                   Fund_Type_Name_E: Fund_Type_Name_E,
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
        window.onload = fnLoad;
    </script>
</asp:Content>

