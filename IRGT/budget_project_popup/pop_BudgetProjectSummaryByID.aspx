﻿<%@ Page Title="" Language="C#" MasterPageFile="~/master_page/popup.master" AutoEventWireup="true" CodeFile="pop_BudgetProjectSummaryByID.aspx.cs" Inherits="budget_popup_pop_BudgetProjectSummaryByID" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script src="../Scripts/angular.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <div class="main-content" ng-app="myApp" ng-controller="fnMain">
            <div class="widget-body" style="margin-bottom:10px;">
		        <%--<div class="widget-main no-padding">
			        <textarea id="BJ_Summary" name="BJ_Summary" data-provide="markdown" data-iconlibrary="fa" rows="50">
			        </textarea>
		        </div>--%>
                <div class="renderbox widget-main no-padding"></div>
	        </div>
        </div>
    <script>
        var $tmp_scope;
        var $tmp_http;
        var $tmp_data;
        var app = angular.module('myApp', []);
        var config = { headers: { 'Content-Type': 'application/x-www-form-urlencoded;charset=utf-8;' } }
        app.controller('fnMain', fnMain);

        //ข้อมูลผู้ใต้บังคับบัญชาระดับอื่น ๆ    
        function fnMain($scope, $http) {
            $('body').pleaseWait();
            $tmp_scope = $scope;
            $tmp_http = $http;

            $.get("docProject.html", function (data) {
                $tmp_data = data;
                fnGetData($scope, $http);
            });

            
        }

        function fnGetData($scope, $http) {
            var BJ_ID = '<%=Session["BJ_ID"]%>';
            var lang = '<%=Session["language_budget_project"]%>';

            var data = $.param({
                Command: 'BudgetProjectByID',
                Function: 'Select',
                BJ_ID: BJ_ID,
                lang: lang
            });

            $http.post("../server/Server_Budget_Project.aspx", data, config)
            .success(function (data, status, headers, config) {
               $scope.Data = data.records;
               if (data.records.length > 0) {
                   var map = {
                       BJ_Issue: data.records[0].BJ_Issue.trim(),
                       BJ_Goal: data.records[0].BJ_Goal.trim(),
                       BJ_Strategy: data.records[0].BJ_Strategy.trim(),
                       BJ_ProjectName: data.records[0].BJ_ProjectName.trim(),
                       BJ_Reason: data.records[0].BJ_Reason.trim(),
                       BJ_Objective: data.records[0].BJ_Objective.trim(),
                       BJ_Place: data.records[0].BJ_Place.trim(),
                       BJ_Duration: data.records[0].BJ_Duration.trim(),
                       BJ_Amount: data.records[0].BJ_Amount.trim(),
                       BJ_Detail: data.records[0].BJ_Detail.trim(),
                       BJ_Measure: data.records[0].BJ_Measure.trim(),
                       BJ_Benefit: data.records[0].BJ_Benefit.trim(),
                       BJ_Responsible: data.records[0].BJ_Responsible.trim(),
                       BJ_Year: data.records[0].BJ_Year.trim()
                   }
                   var html = Mustache.render($tmp_data, map);

                   //$("textarea#BJ_Summary").val(toMarkdown(html));
                   //$("textarea#BJ_Summary").val(html);
                   $('.renderbox').html(html);
                   /*
                   $('textarea[data-provide="markdown"]').each(function () {
                       var $this = $(this);

                       if ($this.data('markdown')) {
                          // $this.data('markdown').showEditor();
                       }
                       else $this.markdown();

                       //$this.data('markdown').$isPreview = true;
                       $this.data('markdown').showPreview();
                       $this.parent().find('.btn').addClass('btn-white');
                       $this.parent().find('.btn-toolbar').remove();
                   });
                   */
                   
                   $('body').pleaseWait('stop');
               } else {
                   $('body').pleaseWait('stop');
               }
            })
            .error(function (data, status, header, config) {
               $('body').pleaseWait('stop');
            });
        }

        
    </script>
    
</asp:Content>

