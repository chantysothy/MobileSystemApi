﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SDPay.aspx.cs" Inherits="Deposit_SDPay" %>
<%@ Register TagPrefix="uc" TagName="Wallet" Src="~/UserControls/MainWalletBalance.ascx" %>

<!DOCTYPE html>
<html>
<head>
    <title><%=string.Format("{0} {1}", commonCulture.ElementValues.getResourceString("brand", commonVariables.LeftMenuXML), commonCulture.ElementValues.getResourceString("sdpay", commonVariables.LeftMenuXML))%></title>
    <!--#include virtual="~/_static/head.inc" -->
    <script type="text/javascript" src="/_Static/Js/Main.js"></script>
</head>
<body>
    <div data-role="page" data-theme="b">
        <header data-role="header" data-theme="b" data-position="fixed" id="header">
            <h1 class="title"><%=string.Format("{0} - {1}", commonCulture.ElementValues.getResourceString("deposit", commonVariables.LeftMenuXML), commonCulture.ElementValues.getResourceString("sdpay", commonVariables.LeftMenuXML))%></h1>
        </header>

        <div class="ui-content" role="main">
            <div class="wallet main-wallet">
                <uc:Wallet id="uMainWallet" runat="server" />
            </div>

            <div data-role="navbar" id="depositTabs" runat="server">
            </div>

            <form class="form" id="form1" runat="server" data-ajax="false">
                <br />
                <ul class="list fixed-tablet-size">
                    <li class="row">
                        <div class="col">
                            <asp:Literal ID="lblMode" runat="server" /></div>
                        <div class="col">
                            <asp:Literal ID="txtMode" runat="server" /></div>
                    </li>
                    <li class="row">
                        <div class="col">
                            <asp:Literal ID="lblMinMaxLimit" runat="server" /></div>
                        <div class="col">
                            <asp:Literal ID="txtMinMaxLimit" runat="server" /></div>
                    </li>
                    <li class="row">
                        <div class="col">
                            <asp:Literal ID="lblDailyLimit" runat="server" /></div>
                        <div class="col">
                            <asp:Literal ID="txtDailyLimit" runat="server" /></div>
                    </li>
                    <li class="row">
                        <div class="col">
                            <asp:Literal ID="lblTotalAllowed" runat="server" /></div>
                        <div class="col">
                            <asp:Literal ID="txtTotalAllowed" runat="server" /></div>
                    </li>
                    <li class="item item-input">
                        <asp:Label ID="lblDepositAmount" runat="server" AssociatedControlID="txtDepositAmount" />
                        <asp:TextBox ID="txtDepositAmount" runat="server" type="number" step="any" min="1" data-clear-btn="true" />
                    </li>
                    <li class="row">
                        <div class="col">
                            <asp:Button data-theme="b" ID="btnSubmit" runat="server" CssClass="button-blue" data-corners="false" OnClick="btnSubmit_Click" /></div>
                    </li>
                    <li class="item-text-wrap">
                        <asp:Literal ID="lblNoticeDownload" runat="server" />
                    </li>
                    <li class="item-text-wrap">
                        <a href="http://mobile.unionpay.com/getclient?platform=android&type=securepayplugin" target="_blank"><%=commonCulture.ElementValues.getResourceString("lblAndroidDownload", xeResources)%></a>
                    </li>
                    <li class="item-text-wrap">
                        <a href="http://mobile.unionpay.com/getclient?platform=ios&type=securepayplugin" target="_blank"><%=commonCulture.ElementValues.getResourceString("lblIOSDownload", xeResources)%></a>
                    </li>
                </ul>

                <div class="row">
                    <div class="col">
                        <input type="button" data-theme="b" onclick="location.href = '/Withdrawal/Default_app.aspx';" value="<%=commonCulture.ElementValues.getResourceString("withrawal", commonVariables.LeftMenuXML)%>" class="button-blue" data-corners="false" />
                    </div>
                    <div class="col">
                        <input type="button" data-theme="b" onclick="location.href = '/FundTransfer/FundTransfer.aspx';" value="<%=commonCulture.ElementValues.getResourceString("fundTransfer", commonVariables.LeftMenuXML)%>" class="button-blue" data-corners="false" />
                    </div>
                </div>

            </form>
        </div>

        <script type="text/javascript">
            $('#form1').submit(function (e) {
                window.w88Mobile.FormValidator.disableSubmitButton('#btnSubmit');
            });
            $(function () {
                window.history.forward();

                var responseCode = '<%=strAlertCode%>';
                var responseMsg = '<%=strAlertMessage%>';
                if (responseCode.length > 0) {
                    switch (responseCode) {
                        case '-1':
                            alert(responseMsg);
                            break;
                        case '0':
                            var sdpayurl = '/_secure/ajaxhandlers/sdpay.ashx?v=' + new Date().getTime() + '&requestAmount=' + $('#txtDepositAmount').val();
                            window.open(sdpayurl);
                            break;
                        default:
                            break;
                    }
                }

            });
            
        </script>
    </div>
</body>
</html>
