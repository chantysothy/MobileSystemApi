﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ECPSS.aspx.cs" Inherits="Deposit_ECPSS" %>

<%@ Register TagPrefix="uc" TagName="Wallet" Src="~/UserControls/MainWalletBalance.ascx" %>

<!DOCTYPE html>
<html>
<head>
    <title></title>
    <!--#include virtual="~/_static/head.inc" -->
    <script type="text/javascript" src="/_Static/JS/modules/gateways/defaultpayments.js"></script>
    <script type="text/javascript" src="/_Static/JS/modules/gateways/ecpss.js"></script>
    <script type="text/javascript" src="/_Static/JS/modules/gateways/gateway.js"></script>
    <script type="text/javascript" src="/_Static/JS/modules/gateways/ecpsspay.js"></script>
</head>
<body>
    <div data-role="page" data-theme="b">
        <header data-role="header" data-theme="b" data-position="fixed" id="header">
            <% if (commonCookie.CookieIsApp != "1")
               { %>
            <a class="btn-clear ui-btn-left ui-btn" href="#divPanel" data-role="none" id="aMenu" data-load-ignore-splash="true">
                <i class="icon-navicon"></i>
            </a>
            <% } %>

            <h1 class="title" id="headerTitle"><%=commonCulture.ElementValues.getResourceString("deposit", commonVariables.LeftMenuXML)%></h1>
        </header>

        <div class="ui-content" role="main">
            <div class="wallet main-wallet">
                <uc:Wallet ID="uMainWallet" runat="server" />
            </div>

            <div class="toggle-list-box">
                <button class="toggle-list-btn btn-active" id="activeDepositTabs"></button>
                <ul class="toggle-list hidden" id="depositTabs">
                </ul>
            </div>

            <form class="form" id="form1" runat="server" data-ajax="false">
                <br>
                <ul class="list fixed-tablet-size">
                    <li class="row">
                        <div class="col">
                            <asp:Literal ID="lblMode" runat="server" />
                        </div>
                        <div class="col">
                            <asp:Literal ID="txtMode" runat="server" />
                        </div>
                    </li>
                    <li class="row">
                        <div class="col">
                            <asp:Literal ID="lblMinMaxLimit" runat="server" />
                        </div>
                        <div class="col">
                            <asp:Literal ID="txtMinMaxLimit" runat="server" />
                        </div>
                    </li>
                    <li class="row">
                        <div class="col">
                            <asp:Literal ID="lblDailyLimit" runat="server" />
                        </div>
                        <div class="col">
                            <asp:Literal ID="txtDailyLimit" runat="server" />
                        </div>
                    </li>
                    <li class="row">
                        <div class="col">
                            <asp:Literal ID="lblTotalAllowed" runat="server" />
                        </div>
                        <div class="col">
                            <asp:Literal ID="txtTotalAllowed" runat="server" />
                        </div>
                    </li>
                    <li class="item item-input">
                        <asp:Label ID="lblDepositAmount" runat="server" AssociatedControlID="txtDepositAmount" />
                        <asp:TextBox ID="txtDepositAmount" runat="server" type="number" step="any" min="1" data-clear-btn="true" />
                    </li>
                    <li class="item item-select">
                        <asp:Label ID="lblBank" runat="server" AssociatedControlID="drpBank" />
                        <asp:DropDownList ID="drpBank" runat="server" data-corners="false" />
                    </li>
                    <li class="item item-select">
                        <asp:Label ID="lblMessage" runat="server" />
                    </li>
                    <li class="item row">
                        <div class="col">
                            <asp:Button data-theme="b" ID="btnSubmit" runat="server" CssClass="button-blue" data-corners="false" />
                        </div>
                    </li>
                </ul>
            </form>
        </div>

        <% if (commonCookie.CookieIsApp != "1")
           { %>
        <!--#include virtual="~/_static/navMenu.shtml" -->
        <% } %>

        <script type="text/javascript">
            $(document).ready(function () {
                window.w88Mobile.Gateways.DefaultPayments.Deposit("<%=base.strCountryCode %>", "<%=base.strMemberID %>", '<%= commonCulture.ElementValues.getResourceString("paymentNotice", commonVariables.PaymentMethodsXML)%>', "<%=base.PaymentMethodId %>");

                $('#form1').submit(function (e) {
                    e.preventDefault();
                    var data = {
                        Amount: $('#txtDepositAmount').val(),
                        BankText: $('#drpBank option:selected').text(),
                        BankValue: $('#drpBank option:selected').val(),
                        MethodId: "<%=base.PaymentMethodId%>"
                    };
                    var action = "/Deposit/Pay.aspx";
                    var params = decodeURIComponent($.param(data));
                    window.open(action + "?" + params, "<%=base.PageName%>");
                    return;
                });
            });
        </script>
    </div>
</body>
</html>
