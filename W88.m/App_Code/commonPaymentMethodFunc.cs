﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

/// <summary>
/// common functionalities of payment/deposit method
/// </summary>
public static class commonPaymentMethodFunc
{
    public static void getDepositMethodList(string methodsUnAvailable, HtmlGenericControl depositTabs, string sourcePage = null)
    {
        var depositList = Enum.GetValues(typeof(commonVariables.DepositMethod));

        string[] methodUnavailable = methodsUnAvailable.Split('|');
        foreach (commonVariables.DepositMethod depositItem in depositList)
        {
            string paymentCode = Convert.ToString((int)depositItem);

            bool isUnavailable = methodUnavailable.Contains(paymentCode);
            if (!isUnavailable)
            {
                setDepositMethodListLink(paymentCode, depositTabs, sourcePage);
            }
        }
    }

    private static void setDepositMethodListLink(string paymentCode, HtmlGenericControl depositTabs, string sourcePage)
    {
        HtmlGenericControl anchor;
        HtmlGenericControl list;
        switch (paymentCode)
        {
            case "110101": //commonVariables.DepositMethod.FastDeposit:

                list = new HtmlGenericControl("li");
                list.ID = string.Format("d{0}", paymentCode);

                anchor = new HtmlGenericControl("a");

                anchor.Attributes.Add("href", "/Deposit/FastDeposit");
                anchor.Attributes.Add("class", "ui-link ui-btn");
                anchor.InnerText = commonCulture.ElementValues.getResourceString("fastdeposit", commonVariables.LeftMenuXML);

                if (string.Compare(sourcePage, "default", true) == 0)
                {
                    anchor.Attributes.Add("class", "btn-primary");
                }

                list.Controls.Add(anchor);
                depositTabs.Controls.Add(list);
                break;

            case "120204": //commonVariables.DepositMethod.NextPay:
                list = new HtmlGenericControl("li");
                list.ID = string.Format("d{0}", paymentCode);

                anchor = new HtmlGenericControl("a");

                anchor.Attributes.Add("href", "/Deposit/NextPay");
                anchor.Attributes.Add("class", "ui-link ui-btn");
                anchor.InnerText = commonCulture.ElementValues.getResourceString("nextpay", commonVariables.LeftMenuXML);

                if (string.Compare(sourcePage, "nextpay", true) == 0)
                {
                    anchor.Attributes.Add("class", "btn-primary");
                }

                list.Controls.Add(anchor);
                depositTabs.Controls.Add(list);
                break;

            case "110308": //commonVariables.DepositMethod.WingMoney:
                list = new HtmlGenericControl("li");
                list.ID = string.Format("d{0}", paymentCode);

                anchor = new HtmlGenericControl("a");

                anchor.Attributes.Add("href", "/Deposit/WingMoney");
                anchor.Attributes.Add("class", "ui-link ui-btn");
                anchor.InnerText = commonCulture.ElementValues.getResourceString("wingmoney", commonVariables.LeftMenuXML);

                if (string.Compare(sourcePage, "wingmoney", true) == 0)
                {
                    anchor.Attributes.Add("class", "btn-primary");
                }

                list.Controls.Add(anchor);
                depositTabs.Controls.Add(list);
                break;

            case "120203": //commonVariables.DepositMethod.SDPay:
                list = new HtmlGenericControl("li");
                list.ID = string.Format("d{0}", paymentCode);


                anchor = new HtmlGenericControl("a");

                anchor.Attributes.Add("href", "/Deposit/SDPay");
                anchor.Attributes.Add("class", "ui-link ui-btn");
                anchor.InnerText = commonCulture.ElementValues.getResourceString("sdpay", commonVariables.LeftMenuXML);

                if (string.Compare(sourcePage, "sdpay", true) == 0)
                {
                    anchor.Attributes.Add("class", "btn-primary");
                }

                list.Controls.Add(anchor);
                depositTabs.Controls.Add(list);
                break;

            case "120227": //commonVariables.DepositMethod.Help2Pay:
                list = new HtmlGenericControl("li");
                list.ID = string.Format("d{0}", paymentCode);

                anchor = new HtmlGenericControl("a");

                anchor.Attributes.Add("href", "/Deposit/Help2Pay");
                anchor.Attributes.Add("class", "ui-link ui-btn");
                anchor.InnerText = commonCulture.ElementValues.getResourceString("help2pay", commonVariables.LeftMenuXML);

                if (string.Compare(sourcePage, "help2pay", true) == 0)
                {
                    anchor.Attributes.Add("class", "btn-primary");
                }

                list.Controls.Add(anchor);
                depositTabs.Controls.Add(list);
                break;

            case "120243": //commonVariables.DepositMethod.DaddyPay:
                list = new HtmlGenericControl("li");
                list.ID = string.Format("d{0}", paymentCode);

                anchor = new HtmlGenericControl("a");

                anchor.Attributes.Add("href", "/Deposit/DaddyPay.aspx?value=1");
                anchor.Attributes.Add("class", "ui-link ui-btn");
                anchor.InnerText = commonCulture.ElementValues.getResourceString("daddypay", commonVariables.LeftMenuXML);

                if (string.Compare(sourcePage, "daddypay", true) == 0)
                {
                    anchor.Attributes.Add("class", "btn-primary");
                }

                list.Controls.Add(anchor);
                depositTabs.Controls.Add(list);
                break;

            case "120244": //commonVariables.DepositMethod.DaddyPayQR:
                list = new HtmlGenericControl("li");
                list.ID = string.Format("d{0}", paymentCode);

                anchor = new HtmlGenericControl("a");

                anchor.Attributes.Add("href", "/Deposit/DaddyPay.aspx?value=2");
                anchor.Attributes.Add("class", "ui-link ui-btn");
                anchor.InnerText = commonCulture.ElementValues.getResourceString("daddypayqr", commonVariables.LeftMenuXML);

                if (string.Compare(sourcePage, "daddypayqr", true) == 0)
                {
                    anchor.Attributes.Add("class", "btn-primary");
                }

                list.Controls.Add(anchor);
                depositTabs.Controls.Add(list);
                break;

            case "120214": //commonVariables.DepositMethod.Neteller:
                list = new HtmlGenericControl("li");
                list.ID = string.Format("d{0}", paymentCode);

                anchor = new HtmlGenericControl("a");

                anchor.Attributes.Add("href", "/Deposit/Neteller.aspx");
                anchor.Attributes.Add("class", "ui-link ui-btn");
                anchor.InnerText = commonCulture.ElementValues.getResourceString("neteller", commonVariables.LeftMenuXML);

                if (string.Compare(sourcePage, "neteller", true) == 0)
                {
                    anchor.Attributes.Add("class", "btn-primary");
                }

                list.Controls.Add(anchor);
                depositTabs.Controls.Add(list);
                break;

            default:
                break;
        }
    }

    public static void getWithdrawalMethodList(string methodsUnAvailable, HtmlGenericControl withdrawalTabs, string sourcePage = null)
    {
        var depositList = Enum.GetValues(typeof(commonVariables.WithdrawalMethod));

        string[] methodUnavailable = methodsUnAvailable.Split('|');
        foreach (commonVariables.WithdrawalMethod depositItem in depositList)
        {
            string paymentCode = Convert.ToString((int)depositItem);

            bool isUnavailable = methodUnavailable.Contains(paymentCode);
            if (!isUnavailable)
            {
                setWithdrawalMethodListLink(paymentCode, withdrawalTabs, sourcePage);
            }
        }
    }

    private static void setWithdrawalMethodListLink(string paymentCode, HtmlGenericControl withdrawalTabs, string sourcePage)
    {
        HtmlGenericControl anchor;
        HtmlGenericControl list;
        switch (paymentCode)
        {
            case "210602": //commonVariables.WithdrawalMethod.BankTransfer:

                list = new HtmlGenericControl("li");
                list.ID = string.Format("w{0}", paymentCode);

                anchor = new HtmlGenericControl("a");

                anchor.Attributes.Add("href", "/Withdrawal/BankTransfer");
                anchor.Attributes.Add("class", "ui-link ui-btn");
                anchor.InnerText = commonCulture.ElementValues.getResourceString("banktransfer", commonVariables.LeftMenuXML);

                if (string.Compare(sourcePage, "default", true) == 0)
                {
                    anchor.Attributes.Add("class", "btn-primary");
                }

                list.Controls.Add(anchor);
                withdrawalTabs.Controls.Add(list);
                break;

            case "210709": //commonVariables.WithdrawalMethod.WingMoney:
                list = new HtmlGenericControl("li");
                list.ID = string.Format("w{0}", paymentCode);

                anchor = new HtmlGenericControl("a");

                anchor.Attributes.Add("href", "/Withdrawal/WingMoney");
                anchor.Attributes.Add("class", "ui-link ui-btn");
                anchor.InnerText = commonCulture.ElementValues.getResourceString("wingmoney", commonVariables.LeftMenuXML);

                if (string.Compare(sourcePage, "wingmoney", true) == 0)
                {
                    anchor.Attributes.Add("class", "btn-primary");
                }

                list.Controls.Add(anchor);
                withdrawalTabs.Controls.Add(list);
                break;

            case "220815": //commonVariables.WithdrawalMethod.Neteller:
                list = new HtmlGenericControl("li");
                list.ID = string.Format("w{0}", paymentCode);

                anchor = new HtmlGenericControl("a");

                anchor.Attributes.Add("href", "/Withdrawal/Neteller.aspx");
                anchor.Attributes.Add("class", "ui-link ui-btn");
                anchor.InnerText = commonCulture.ElementValues.getResourceString("neteller", commonVariables.LeftMenuXML);

                if (string.Compare(sourcePage, "neteller", true) == 0)
                {
                    anchor.Attributes.Add("class", "btn-primary");
                }

                list.Controls.Add(anchor);
                withdrawalTabs.Controls.Add(list);
                break;

            default:
                break;
        }
    }
}