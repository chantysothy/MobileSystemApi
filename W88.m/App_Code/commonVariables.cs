﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Caching;
using System.Xml.Linq;


public class commonVariables
{
    private static Regex rxDomains_CN = new Regex(commonVariables.ChinaDomain);
    public static XElement LeftMenuXML { get { if (HttpContext.Current.Cache.Get("leftMenuXML_" + commonVariables.SelectedLanguage) != null) { return HttpContext.Current.Cache.Get("leftMenuXML_" + commonVariables.SelectedLanguage) as XElement; } else { XElement xcMenu = commonCulture.appData.getRootResource("/leftMenu"); HttpContext.Current.Cache.Add("leftMenuXML_" + commonVariables.SelectedLanguage, xcMenu, null, Cache.NoAbsoluteExpiration, new TimeSpan(0, 15, 0), CacheItemPriority.AboveNormal, null); return xcMenu; } } }
    public static XElement ProductsXML { get { if (HttpContext.Current.Cache.Get("ProductsXML_" + commonVariables.SelectedLanguage) != null) { return HttpContext.Current.Cache.Get("ProductsXML_" + commonVariables.SelectedLanguage) as XElement; } else { XElement xcMenu = commonCulture.appData.getRootResource("/Products"); HttpContext.Current.Cache.Add("ProductsXML_" + commonVariables.SelectedLanguage, xcMenu, null, Cache.NoAbsoluteExpiration, new TimeSpan(0, 15, 0), CacheItemPriority.AboveNormal, null); return xcMenu; } } }
    public static XElement ContactUsXML { get { if (HttpContext.Current.Cache.Get("ContactUsXML_" + commonVariables.SelectedLanguage) != null) { return HttpContext.Current.Cache.Get("ContactUsXML_" + commonVariables.SelectedLanguage) as XElement; } else { XElement xcMenu = commonCulture.appData.getRootResource("/ContactUs.aspx"); HttpContext.Current.Cache.Add("ContactUsXML_" + commonVariables.SelectedLanguage, xcMenu, null, Cache.NoAbsoluteExpiration, new TimeSpan(0, 15, 0), CacheItemPriority.AboveNormal, null); return xcMenu; } } }
    public static XElement ErrorsXML { get { if (HttpContext.Current.Cache.Get("errorsXML_" + commonVariables.SelectedLanguage) != null) { return HttpContext.Current.Cache.Get("errorsXML_" + commonVariables.SelectedLanguage) as XElement; } else { XElement xcErrors = commonCulture.appData.getRootResource("/Errors"); HttpContext.Current.Cache.Add("errorsXML_" + commonVariables.SelectedLanguage, xcErrors, null, Cache.NoAbsoluteExpiration, new TimeSpan(0, 15, 0), CacheItemPriority.AboveNormal, null); return xcErrors; } } }
    public static XElement PaymentMethodsXML
    {
        get
        {
            if (HttpContext.Current.Cache.Get("PaymentMethodsXML_" + commonVariables.SelectedLanguage) != null)
            {
                return HttpContext.Current.Cache.Get("PaymentMethodsXML_" + commonVariables.SelectedLanguage) as XElement;
            }
            else
            {
                XElement xcMenu = commonCulture.appData.getRootResource("/PaymentMethods");
                HttpContext.Current.Cache.Add("PaymentMethodsXML_" + commonVariables.SelectedLanguage, xcMenu, null,
                    Cache.NoAbsoluteExpiration, new TimeSpan(0, 15, 0), CacheItemPriority.AboveNormal, null);
                return xcMenu;
            }
        }
    }
    public static XElement HistoryXML
    {
        get
        {
            if (HttpContext.Current.Cache.Get("HistoryXML_" + commonVariables.SelectedLanguage) != null)
            {
                return HttpContext.Current.Cache.Get("HistoryXML_" + commonVariables.SelectedLanguage) as XElement;
            }
            else
            {
                XElement xcMenu = commonCulture.appData.getRootResource("/History");
                HttpContext.Current.Cache.Add("HistoryXML_" + commonVariables.SelectedLanguage, xcMenu, null,
                    Cache.NoAbsoluteExpiration, new TimeSpan(0, 15, 0), CacheItemPriority.AboveNormal, null);
                return xcMenu;
            }
        }
    }
    public static XElement PromotionsXML
    {
        get
        {
            if (HttpContext.Current.Cache.Get("PromotionsXML_" + commonVariables.SelectedLanguage) != null)
            {
                return HttpContext.Current.Cache.Get("PromotionsXML_" + commonVariables.SelectedLanguage) as XElement;
            }
            else
            {
                XElement xcMenu = commonCulture.appData.getRootResource("/Promotions");
                HttpContext.Current.Cache.Add("PromotionsXML_" + commonVariables.SelectedLanguage, xcMenu, null, Cache.NoAbsoluteExpiration, new TimeSpan(0, 15, 0), CacheItemPriority.AboveNormal, null);
                return xcMenu;
            }
        }
    }

    public static string SiteUrl { get { return HttpContext.Current.Request.ServerVariables["SERVER_NAME"]; } }

    public static string DisplayDateFormat { get { return ConfigurationManager.AppSettings.Get("DisplayDateFormat"); } }
    public static string DisplayDateTimeFormat { get { return ConfigurationManager.AppSettings.Get("DisplayDateTimeFormat"); } }
    public static string DateTimeFormat { get { return ConfigurationManager.AppSettings.Get("DateTimeFormat"); } }
    public static string DecimalFormat { get { return ConfigurationManager.AppSettings.Get("DecimalFormat"); } }
    public static string SelectedLanguage
    {
        get
        {
            if (!string.IsNullOrEmpty(commonCookie.CookieLanguage))
            {
                return commonCookie.CookieLanguage;
            }
            else if (!string.IsNullOrWhiteSpace(System.Web.HttpContext.Current.Session["SelectedLanguage"] as string))
            {
                return Convert.ToString(System.Web.HttpContext.Current.Session["SelectedLanguage"]);
            }
            else
            {
                return rxDomains_CN.IsMatch(HttpContext.Current.Request.ServerVariables["SERVER_NAME"]) ? "zh-cn" : "en-us";
            }
        }
        set
        {
            commonCookie.CookieLanguage = value;
            commonVariables.SetSessionVariable("SelectedLanguage", value);
        }
    }

    public static string SelectedLanguageShort
    {
        get
        {
            switch (commonVariables.SelectedLanguage.ToLower())
            {
                case "en-us":
                    return "en";
                case "id-id":
                    return "id";
                case "km-kh":
                    return "kh";
                case "ko-kr":
                    return "kr";
                case "th-th":
                    return "th";
                case "vi-vn":
                    return "vn";
                case "zh-cn":
                    return "cn";
                case "ja-jp":
                    return "jp";
                default:
                    return "en";
            }
        }
    }

    public static string CurrentMemberSessionId
    {
        get
        {
            return (!string.IsNullOrEmpty(commonCookie.CookieS)) ? commonCookie.CookieS : "";
        }
    }

    public static string EncryptedCurrentMemberSessionId
    {
        get
        {
            string ecryptedSessionId = string.Empty;
            if (!string.IsNullOrEmpty(commonCookie.CookieS))
            {
                var cipherKey = commonEncryption.Decrypt(ConfigurationManager.AppSettings.Get("PrivateKeyToken"));
                ecryptedSessionId = HttpUtility.UrlEncode(commonEncryption.EncryptToken(commonCookie.CookieS, cipherKey));
            }

            return ecryptedSessionId;
        }
    }

    public static string OperatorId
    {
        get
        {
            customConfig.OperatorSettings opSettings = new customConfig.OperatorSettings(ConfigurationManager.AppSettings.Get("Operator"));
            return opSettings.Values.Get("OperatorId");
        }
    }
    public static string OperatorCode
    {
        get
        {
            return ConfigurationManager.AppSettings.Get("Operator");
        }
    }

    public static string GetSessionVariable(string key)
    { return string.IsNullOrEmpty(HttpContext.Current.Session[key] as string) ? "" : Convert.ToString(HttpContext.Current.Session[key]); }
    public static void SetSessionVariable(string key, string value) { HttpContext.Current.Session.Add(key, value); }

    public static void ClearSessionVariables()
    {
        string strLanguage = string.Empty;
        string strVCode = string.Empty;

        strLanguage = commonVariables.SelectedLanguage;
        strVCode = commonVariables.GetSessionVariable("vCode");

        HttpContext.Current.Session.Clear();
        HttpContext.Current.Session.Abandon();

        commonVariables.SelectedLanguage = strLanguage;
        commonVariables.SetSessionVariable("vCode", strVCode);
    }

    internal enum TransferWallet
    {
        undefined = -1,
        main = 0,
        lottery = 1,
        oneworks = 2,
        casino = 3,
        playtech = 4,
        marketpulse = 5,
        rhymepoker = 6,
        sbtech = 7,
        pmahjong = 8,
        wft = 9
    }

    internal enum operatorCode
    {
        W88 = 1,
        BET8 = 3
    }

    public enum DepositMethod
    {
        FastDeposit = 110101,
        Cubits = 1202120,
        SDAPay = 120203,
        NextPay = 120204,
        Bill99 = 120206,
        JutaPay = 120280,
        JuyPayAlipay = 1202113,
        IPS = 120207,
        IWallet = 1202103,
        WingMoney = 110308,
        TrueMoney = 1103132,
        PayGo = 110394,
        SDPay = 120223,
        Help2Pay = 120227,
        KDPayWeChat = 1202114,
        DaddyPay = 120243,
        DaddyPayQR = 120244,
        Neteller = 120214,
        KexunPay = 1202127,
        PaySec = 120290,
        SDAPayAlipay = 120254,
        AlipayTransfer = 1204131,
        ShengPayAliPay = 1202111,
        NineVPayAlipay = 1202105,
        ECPSS = 120218,
        BofoPay = 120231,
        JTPayWeChat = 1202123,
        JTPayAliPay = 1202122,
        TongHuiPay = 120275,
        TongHuiWeChat = 120277,
        TongHuiAlipay = 120293,
        AllDebit = 120236,
        EGHL = 120265,
        NganLuong = 120212,
        VenusPoint = 120296,
        BaokimScratchCard = 120286,
        Baokim = 120272,
        AifuWeChat = 1202133,
        AifuAlipay = 1202134,

    }

    public enum WithdrawalMethod
    {
        PayGo = 210797,
        VenusPoint = 220895,
        BankTransfer = 210602,
        Cubits = 2208121,
        WingMoney = 210709,
        TrueMoney = 2107138,
        Neteller = 220815,
        IWallet = 2208102,
        Baokim = 220874
    }

    public enum AutoRouteMethod
    {
        QuickOnline = 999999,
        UnionPay = 999998,
        TopUpCard = 999997,
        AliPay = 999996,
        WeChat = 999995,
    }

    public enum TransactionSource
    {
        Mobile
    }

    public enum PaymentTransactionType
    {
        Deposit = 1,
        Withdrawal = 2
    }
    public static string ChinaDomain
    {
        get
        {
            return ConfigurationManager.AppSettings.Get("CN_domain");
        }
    }


}
