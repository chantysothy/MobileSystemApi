﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Secure_AjaxHandlers_ProcessLogin : System.Web.UI.Page, System.Web.SessionState.IReadOnlySessionState
{
    protected void Page_Load(object sender, EventArgs e)
    {
        System.Xml.Linq.XElement xeErrors = commonVariables.ErrorsXML;

        #region initialiseVariables
        int intProcessSerialId = 0;
        string strProcessId = Guid.NewGuid().ToString().ToUpper();
        string strPageName = "ProcessLogin";

        string strResultCode = string.Empty;
        string strResultDetail = string.Empty;
        string strErrorCode = string.Empty;
        string strErrorDetail = string.Empty;
        string strProcessRemark = string.Empty;
        bool isProcessAbort = false;
        bool isSystemError = false;

        long lngOperatorId = long.MinValue;
        string strMemberCode = string.Empty;
        string strPassword = string.Empty;
        string strSiteURL = string.Empty;
        string strLoginIp = string.Empty;
        string strDeviceId = string.Empty;

        string strVCode = string.Empty;
        string strSessionVCode = string.Empty;
        string strProcessCode = string.Empty;
        string strProcessMessage = string.Empty;
        string strCountryCode = string.Empty;
        string strLastLoginIP = string.Empty;
        string strPermission = string.Empty;

        bool runIovation = false;

        System.Xml.XmlDocument xdResponse = new System.Xml.XmlDocument();

        #endregion

        #region populateVariables
        lngOperatorId = long.Parse(commonVariables.OperatorId);
        strMemberCode = Request.Form.Get("txtUsername");
        strPassword = Request.Form.Get("txtPassword");
        strSiteURL = commonVariables.SiteUrl;
        strLoginIp = string.IsNullOrEmpty(Request.Form.Get("txtIPAddress")) ? commonIp.UserIP : Request.Form.Get("txtIPAddress");
        strDeviceId = HttpContext.Current.Request.UserAgent;
        strVCode = Request.Form.Get("txtCaptcha");
        strSessionVCode = commonVariables.GetSessionVariable("vCode");
        strCountryCode = Request.Form.Get("txtCountry");
        strPermission = Request.Form.Get("txtPermission");
        #endregion

        #region parametersValidation
        if (string.IsNullOrEmpty(strMemberCode)) { strProcessCode = "-1"; strProcessMessage = commonCulture.ElementValues.getResourceXPathString("Login/MissingUsername", xeErrors); isProcessAbort = true; }
        else if (string.IsNullOrEmpty(strPassword)) { strProcessCode = "-1"; strProcessMessage = commonCulture.ElementValues.getResourceXPathString("Login/MissingPassword", xeErrors); isProcessAbort = true; }
        else if (string.IsNullOrEmpty(strVCode)) { strProcessCode = "-1"; strProcessMessage = commonCulture.ElementValues.getResourceString("MissingVCode", xeErrors); isProcessAbort = true; }
        else if (commonValidation.isInjection(strMemberCode)) { strProcessCode = "-1"; strProcessMessage = commonCulture.ElementValues.getResourceXPathString("Login/InvalidUsername", xeErrors); isProcessAbort = true; }
        else if (commonValidation.isInjection(strPassword)) { strProcessCode = "-1"; strProcessMessage = commonCulture.ElementValues.getResourceXPathString("Login/InvalidPassword", xeErrors); isProcessAbort = true; }
        else if (commonValidation.isInjection(strVCode)) { strProcessCode = "-1"; strProcessMessage = commonCulture.ElementValues.getResourceString("IncorrectVCode", xeErrors); isProcessAbort = true; }
        else if (string.Compare(commonEncryption.encrypting(strVCode), strSessionVCode, true) != 0) { strProcessCode = "-1"; strProcessMessage = commonCulture.ElementValues.getResourceString("IncorrectVCode", xeErrors); isProcessAbort = true; }
        else
        {
            strPassword = commonEncryption.Encrypt(strPassword);
        }

        strProcessRemark = string.Format("MemberCode: {0} | Password: {1} | VCode: {2} | SVCode: {3} | IP: {4} | Country: {5}", strMemberCode, strPassword, strVCode, strSessionVCode, strLoginIp, strCountryCode);

        intProcessSerialId += 1;
        commonAuditTrail.appendLog("system", strPageName, "ParameterValidation", "DataBaseManager.DLL", strResultCode, strResultDetail, strErrorCode, strErrorDetail, strProcessRemark, Convert.ToString(intProcessSerialId), strProcessId, isSystemError);

        #endregion

        #region initiateLogin
        if (!isProcessAbort)
        {
            try
            {
                using (wsMemberMS1.memberWSSoapClient svcInstance = new wsMemberMS1.memberWSSoapClient())
                {
                    System.Data.DataSet dsSignin = null;
                    dsSignin = svcInstance.MemberSignin(lngOperatorId, strMemberCode, strPassword, strSiteURL, strLoginIp, strDeviceId);

                    if (dsSignin.Tables[0].Rows.Count > 0)
                    {
                        strProcessCode = Convert.ToString(dsSignin.Tables[0].Rows[0]["RETURN_VALUE"]);
                        switch (strProcessCode)
                        {
                            case "0":
                                strProcessMessage = commonCulture.ElementValues.getResourceString("Exception", xeErrors);
                                break;
                            case "1":
                                string strMemberSessionId = Convert.ToString(dsSignin.Tables[0].Rows[0]["memberSessionId"]);
                                
                                HttpContext.Current.Session.Add("MemberSessionId", Convert.ToString(dsSignin.Tables[0].Rows[0]["memberSessionId"]));
                                HttpContext.Current.Session.Add("MemberId", Convert.ToString(dsSignin.Tables[0].Rows[0]["memberId"]));
                                HttpContext.Current.Session.Add("MemberCode", Convert.ToString(dsSignin.Tables[0].Rows[0]["memberCode"]));
                                HttpContext.Current.Session.Add("CountryCode", Convert.ToString(dsSignin.Tables[0].Rows[0]["countryCode"]));
                                HttpContext.Current.Session.Add("CurrencyCode", Convert.ToString(dsSignin.Tables[0].Rows[0]["currency"]));
                                HttpContext.Current.Session.Add("LanguageCode", Convert.ToString(dsSignin.Tables[0].Rows[0]["languageCode"]));
                                HttpContext.Current.Session.Add("RiskId", Convert.ToString(dsSignin.Tables[0].Rows[0]["riskId"]));
                                //HttpContext.Current.Session.Add("PaymentGroup", "A"); //Convert.ToString(dsSignin.Tables[0].Rows[0]["paymentGroup"]));
                                HttpContext.Current.Session.Add("PartialSignup", Convert.ToString(dsSignin.Tables[0].Rows[0]["partialSignup"]));
                                HttpContext.Current.Session.Add("ResetPassword", Convert.ToString(dsSignin.Tables[0].Rows[0]["resetPassword"]));
                                HttpContext.Current.Session.Add("priorityVIP", Convert.ToString(dsSignin.Tables[0].Rows[0]["priorityVIP"]));
                                
                                commonCookie.CookieS = strMemberSessionId;
                                commonCookie.CookieG = strMemberSessionId;
                                HttpContext.Current.Session.Add("LoginStatus", "success");

                                strLastLoginIP = Convert.ToString(dsSignin.Tables[0].Rows[0]["lastLoginIP"]);
                                if (HttpContext.Current.Request.Cookies[strMemberCode] == null) { runIovation = true; }
                                else if (HttpContext.Current.Request.Cookies[strMemberCode] != null && string.Compare(strLastLoginIP, strLoginIp, true) != 0) { runIovation = true; }
                                if (runIovation) { this.IovationSubmit(ref intProcessSerialId, strProcessId, strPageName, strMemberCode, strLoginIp, strPermission); }

                                // test
                                try 
                                {
                                    Session["testMemberId"] = commonVariables.GetSessionVariable("MemberId");
                                    Session["testMemberCode"] = commonVariables.GetSessionVariable("MemberCode");
                                    Session["testCurrencyCode"] = commonVariables.GetSessionVariable("CurrencyCode");
                                }
                                catch (Exception ex) { }
                                break;
                            case "21":
                                strProcessMessage = commonCulture.ElementValues.getResourceXPathString("Login/InvalidUsername", xeErrors);
                                break;
                            case "22":
                                strProcessMessage = commonCulture.ElementValues.getResourceXPathString("Login/InactiveAccount", xeErrors).Replace("{0}",GetLiveChatURL());
                                break;
                            case "23":
                                strProcessMessage = commonCulture.ElementValues.getResourceXPathString("Login/InvalidPassword", xeErrors);
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                strProcessCode = "0";
                strProcessMessage = commonCulture.ElementValues.getResourceString("Exception", xeErrors);
                strProcessRemark = string.Format("{0} | Message: {1}", strProcessRemark, ex.Message);
            }

            strProcessRemark = string.Format("{0} | strProcessCode: {1}", strProcessRemark, strProcessCode);

            intProcessSerialId += 1;
            commonAuditTrail.appendLog("system", strPageName, "MemberSignin", "DataBaseManager.DLL", strResultCode, strResultDetail, strErrorCode, strErrorDetail, strProcessRemark, Convert.ToString(intProcessSerialId), strProcessId, isSystemError);
        }
        #endregion

        #region Response
        System.Xml.XmlNode xnRootNode = xdResponse.CreateElement("Login");
        System.Xml.XmlNode xnCodeNode = xdResponse.CreateElement("ErrorCode");
        System.Xml.XmlNode xnMessageNode = xdResponse.CreateElement("Message");
        
        xnCodeNode.InnerText = strProcessCode;
        xnMessageNode.InnerText = strProcessMessage;
        xnRootNode.AppendChild(xnCodeNode);
        xnRootNode.AppendChild(xnMessageNode);
        xdResponse.AppendChild(xnRootNode);

        Response.ContentType = "text/xml";
        Response.Write(xdResponse.DocumentElement.OuterXml);
        Response.End();
        #endregion
    }


    protected void IovationSubmit(ref int intProcessSerialId, string strProcessId, string strPageName, string strUsername, string strIPAddress, string strPermission)
    {
        string strResultCode = string.Empty;
        string strResultDetail = string.Empty;
        string strErrorCode = string.Empty;
        string strErrorDetail = string.Empty;
        string strProcessRemark = string.Empty;
        bool isSystemError = false;

        customConfig.IovationSettings ioSettings = new customConfig.IovationSettings("W88");
        string strCheckTransactionUrl = ioSettings.Values.Get("CheckTransactionUrl");
        string strGetEvidenceUrl = ioSettings.Values.Get("GetEvidenceUrl");
        string strAccountPrefix = ioSettings.Values.Get("AccountPrefix");
        string strSubscriberID = ioSettings.Values.Get("SubscriberId");
        string strSubscriberAccount = ioSettings.Values.Get("SubscriberAccount");
        string strSubscriberPassCode = ioSettings.Values.Get("SubscriberPassCode");
        string strServiceEnabled = ioSettings.Values.Get("ServiceEnabled");
        string strUserAccountCode = string.Format("{0}{1}", strAccountPrefix, strUsername);
        string strExceptions = ioSettings.Values.Get("Exceptions");

        List<string> lstPermission = strExceptions.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        if (lstPermission.FindIndex(x => x.Trim().Equals(strPermission, StringComparison.OrdinalIgnoreCase)) >= 0)
        {
            return;
        }
        else if (string.Compare(strServiceEnabled, "true", true) != 0)
        {
            return;
        }

        strProcessRemark = string.Format("CheckTransactionURL: {0} | GetEvidenceURL: {1} | AccountPrefix: {2} | SubscriberID: {3} | SubscriberAccount: {4} | SubscriberPassCode: {5} | UserAccountCode : {6}",
            strCheckTransactionUrl, strGetEvidenceUrl, strAccountPrefix, strSubscriberID, strSubscriberAccount, strSubscriberPassCode, strUserAccountCode);
        
        try
        {
            using (CheckTransactionDetailsService ioInstance = new CheckTransactionDetailsService(strCheckTransactionUrl))
            {
                CheckTransactionDetails ioRequest = new CheckTransactionDetails();

                ioRequest.accountcode = strUserAccountCode;
                ioRequest.enduserip = strIPAddress;

                ioRequest.beginblackbox = HttpContext.Current.Request.Form.Get("ioBlackBox");
                ioRequest.subscriberid = strSubscriberID;
                ioRequest.subscriberpasscode = strSubscriberPassCode;
                ioRequest.subscriberaccount = strSubscriberAccount;
                ioRequest.type = "login";

                CheckTransactionDetailsResponse ioResponse = new CheckTransactionDetailsResponse();

                ioResponse = ioInstance.CheckTransactionDetails(ioRequest);

                #region setIovationCookie
                HttpCookie cookie = new HttpCookie(strUsername);
                cookie.Value = ioResponse.result;
                if (!string.IsNullOrEmpty(commonIp.DomainName)) { cookie.Domain = commonIp.DomainName; }
                cookie.Expires = System.DateTime.Now.AddDays(Convert.ToInt32(ioSettings.Values.Get("ServiceDays")));
                HttpContext.Current.Response.Cookies.Add(cookie);
                #endregion
            }
        }
        catch (Exception ex)
        {
            strResultCode = "31";
            strResultDetail = "Error:Iovation";
            strErrorCode = Convert.ToString(ex.HResult);
            strErrorDetail = ex.Message;

            isSystemError = true;
        }


        intProcessSerialId += 1;
        commonAuditTrail.appendLog("system", strPageName, "Iovation", "DataBaseManager.DLL", strResultCode, strResultDetail, strErrorCode, strErrorDetail, strProcessRemark, Convert.ToString(intProcessSerialId), strProcessId, isSystemError);
    }



    private string GetLiveChatURL()
    {
        string strMemberId = string.Empty;
        string strMemberCode = string.Empty;
        string riskId = string.Empty;
        string redirectLink = string.Empty;

        try
        {
            string strMemberName = commonVariables.GetSessionVariable("name");
            string shortlang = commonVariables.SelectedLanguageShort;
            string lang = commonVariables.SelectedLanguage.ToLower();
            bool isVIP = false;

            string value = commonVariables.GetSessionVariable("priorityVIP");
            string CurrentUrl = System.Web.HttpContext.Current.Request.Url.ToString();

            Uri myUri = new Uri(CurrentUrl);
            string[] host = myUri.Host.Split('.');
            string domain = string.Format(ConfigurationManager.AppSettings["WebHandler"], host[1]);

            string chatLang = string.Empty;
            string skill = string.Empty;

            string platform = "Mobile";


            if (!string.IsNullOrEmpty(commonVariables.CurrentMemberSessionId))
            {
                strMemberId = commonVariables.GetSessionVariable("MemberId");
                strMemberCode = commonVariables.GetSessionVariable("MemberCode");
                riskId = commonVariables.GetSessionVariable("RiskId");

                if (riskId.Length >= 3)
                {
                    if (riskId.Trim().ToLower() == "vipg" || riskId.ToLower() == "vipd" || riskId.ToLower() == "vipp")
                        isVIP = true;
                }
            }

            //BO settings integration
            try
            {
                if (lang == "zh-cn" || lang == "vi-vn")
                {
                    Uri Myuri_ = new Uri(CurrentUrl);
                    string[] host_ = myUri.Host.Split('.');
                    string domain_ = string.Format(ConfigurationManager.AppSettings["LivePersonMobile"], host_[1]);
                    redirectLink = domain_;
                }
                else
                {
                    redirectLink = domain + CurrentUrl;
                }
            }
            catch (Exception)
            {
                if (shortlang == "en" || shortlang == "kh" || shortlang == "kr" || shortlang == "th" || shortlang == "jp" || shortlang == "id" || shortlang == "vn")
                {
                    #region livezilla

                    string KM = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes("KM"));
                    var code1 = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(strMemberCode));
                    var code2 = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(platform));
                    var code3 = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(CurrentUrl));
                    var code4 = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(strMemberId));
                    var code5 = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(System.Web.HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"].ToString()));

                    if (string.IsNullOrEmpty(strMemberCode) || string.IsNullOrEmpty(strMemberId))
                    {
                        code1 = "";
                        code4 = "";

                        switch (shortlang)//null member
                        {
                            case "en":
                                redirectLink = "http://en.chat.liveperson88.com/chat.php?a=1bb6a&hg=P1ZJUA__&en={0}&cf0={1}&cf1={2}&cf2={3}&cf3={4}&dl=MQ__&rgs=MQ__";
                                break;
                            case "kh":
                                redirectLink = "http://kh.chat.liveperson88.com/chat.php?a=d1712&hg=P1ZJUA__&en={0}&cf0={1}&cf1={2}&cf2={3}&cf3={4}&dl=MQ__&rgs=MQ__&el=" + KM;
                                break;
                            case "kr":
                                redirectLink = "http://kr.chat.liveperson88.com/chat.php?a=7b233&hg=P1ZJUA__&en={0}&cf0={1}&cf1={2}&cf2={3}&cf3={4}&mp=MQ__&rgs=MQ__";
                                break;
                            case "th":
                                redirectLink = "http://th.chat.liveperson88.com/chat.php?a=d4a99&hg=P1ZJUA__&en={0}&cf0={1}&cf1={2}&cf2={3}&cf3={4}&mp=MQ__&rgs=MQ__";
                                break;
                            case "id":
                                redirectLink = "http://id.chat.liveperson88.com/chat.php?a=5c303&hg=P1ZJUA__&en={0}&cf0={1}&cf1={2}&cf2={3}&cf3={4}&mp=MQ__&rgs=MQ__";
                                break;
                            case "jp":
                                redirectLink = "http://jp.chat.liveperson88.com/chat.php?a=f3c22&hg=P1ZJUA__&en={0}&cf0={1}&cf1={2}&cf2={3}&cf3={4}&mp=MQ__&rgs=MQ__";
                                break;
                            case "vn":
                                redirectLink = "http://vn.chat.liveperson88.com/chat.php?a=1052a&hg=P1ZJUA__&en={0}&cf0={1}&cf1={2}&cf2={3}&cf3={4}&mp=MQ__&rgs=MQ__";
                                break;
                            case "cn":
                                redirectLink = "http://cn.chat.liveperson88.com/chat.php?a=1cbe6&hg=P1ZJUA__&en={0}&cf0={1}&cf1={2}&cf2={3}&cf3={4}&mp=MQ__&rgs=MQ__";
                                break;
                            default:
                                redirectLink = "http://en.chat.liveperson88.com/chat.php?a=1bb6a&hg=P1ZJUA__&hg=P01hbmFnZXI_&en={0}&cf0={1}&cf1={2}&cf2={3}&cf3={4}&dl=MQ__&rgs=MQ__";
                                break;

                        }
                    }
                    else
                    {
                        switch (shortlang)
                        {
                            case "en":
                                if (isVIP)
                                    redirectLink = "http://en.chat.liveperson88.com/chat.php?a=a08c2&intgroup=VklQ&hg=Pw__&en={0}&cf0={1}&cf1={2}&cf2={3}&cf3={4}&dl=MQ__";
                                else
                                    redirectLink = "http://en.chat.liveperson88.com/chat.php?a=1bb6a&hg=P1ZJUA__&en={0}&cf0={1}&cf1={2}&cf2={3}&cf3={4}&dl=MQ__&rgs=MQ__";
                                break;
                            case "kh":
                                if (isVIP)
                                    redirectLink = "http://kh.chat.liveperson88.com/chat.php?a=35df1&intgroup=VklQ&hg=Pw__&en={0}&cf0={1}&cf1={2}&cf2={3}&cf3={4}&dl=MQ__&el=" + KM;
                                else
                                    redirectLink = "http://kh.chat.liveperson88.com/chat.php?a=d1712&hg=P1ZJUA__&en={0}&cf0={1}&cf1={2}&cf2={3}&cf3={4}&dl=MQ__&rgs=MQ__&el=" + KM;
                                break;
                            case "kr":
                                if (isVIP)
                                    redirectLink = "http://kr.chat.liveperson88.com/chat.php?a=581f1&intgroup=VklQ&hg=Pw__&en={0}&cf0={1}&cf1={2}&cf2={3}&cf3={4}";
                                else
                                    redirectLink = "http://kr.chat.liveperson88.com/chat.php?a=7b233&hg=P1ZJUA__&en={0}&cf0={1}&cf1={2}&cf2={3}&cf3={4}&mp=MQ__&rgs=MQ__";
                                break;
                            case "th":
                                if (isVIP)
                                    redirectLink = "http://th.chat.liveperson88.com/chat.php?a=0db09&intgroup=VklQ&en={0}&cf0={1}&cf1={2}&cf2={3}&cf3={4}";
                                else
                                    redirectLink = "http://th.chat.liveperson88.com/chat.php?a=d4a99&hg=P1ZJUA__&en={0}&cf0={1}&cf1={2}&cf2={3}&cf3={4}&mp=MQ__&rgs=MQ__";
                                break;
                            case "id":
                                if (isVIP)
                                    redirectLink = "http://id.chat.liveperson88.com/chat.php?a=f7bd2&intgroup=VklQ&en={0}&cf0={1}&cf1={2}&cf2={3}&cf3={4}";
                                else
                                    redirectLink = "http://id.chat.liveperson88.com/chat.php?a=5c303&hg=P1ZJUA__&en={0}&cf0={1}&cf1={2}&cf2={3}&cf3={4}&mp=MQ__&rgs=MQ__";
                                break;
                            case "jp":
                                if (isVIP)
                                    redirectLink = "http://jp.chat.liveperson88.com/chat.php?a=e94d5&intgroup=VklQ&hg=Pw__&en={0}&cf0={1}&cf1={2}&cf2={3}&cf3={4}";
                                else
                                    redirectLink = "http://jp.chat.liveperson88.com/chat.php?a=f3c22&hg=P1ZJUA__&en={0}&cf0={1}&cf1={2}&cf2={3}&cf3={4}&mp=MQ__&rgs=MQ__";
                                break;
                            case "vn":
                                if (isVIP)
                                    redirectLink = "http://vn.chat.liveperson88.com/chat.php?a=f9d71&intgroup=VklQ&en={0}&cf0={1}&cf1={2}&cf2={3}&cf3={4}";
                                else
                                    redirectLink = "http://vn.chat.liveperson88.com/chat.php?a=1052a&hg=P1ZJUA__&en={0}&cf0={1}&cf1={2}&cf2={3}&cf3={4}&mp=MQ__&rgs=MQ__";
                                break;
                            case "cn":
                                if (isVIP)
                                    redirectLink = "http://cn.chat.liveperson88.com/chat.php?a=dd1c7&intgroup=VklQ&hg=Pw__&en={0}&cf0={1}&cf1={2}&cf2={3}&cf3={4}";
                                else
                                    redirectLink = "http://cn.chat.liveperson88.com/chat.php?a=1cbe6&hg=P1ZJUA__&en={0}&cf0={1}&cf1={2}&cf2={3}&cf3={4}&mp=MQ__&rgs=MQ__";
                                break;
                            default:
                                if (isVIP)
                                    redirectLink = "http://en.chat.liveperson88.com/chat.php?a=a08c2&intgroup=VklQ&hg=Pw__&en={0}&cf0={1}&cf1={2}&cf2={3}&cf3={4}&dl=MQ__";
                                else
                                    redirectLink = "http://en.chat.liveperson88.com/chat.php?a=1bb6a&hg=P1ZJUA__&en={0}&cf0={1}&cf1={2}&cf2={3}&cf3={4}&dl=MQ__&rgs=MQ__";
                                break;
                        }
                    }
                    redirectLink = string.Format(redirectLink, code1, code2, code3, code4, code5);

                    #endregion livezilla

                }
                else
                {
                    #region liveperson
                    if (isVIP)
                    {
                        switch (lang)
                        {
                            case "id":
                                chatLang = "VIP-Bahasa"; skill = "VIP-Bahasa";
                                break;
                            case "vn":
                                chatLang = "VIP-TiengViet "; skill = "VIP-TiengViet ";
                                break;
                            case "cn":
                                chatLang = "VIP-Chinese "; skill = "VIP-Chinese ";
                                break;
                            default:
                                chatLang = "VIP-English"; skill = "VIP-English";
                                break;
                        }
                    }
                    else
                    {
                        switch (shortlang)
                        {
                            case "id": chatLang = "Indonesia"; skill = "Indonesia"; break;
                            case "jp": chatLang = "Japanese"; skill = "Japanese"; break;
                            case "vn": chatLang = "Vietnamese"; skill = "Vietnamese"; break;
                            case "cn": chatLang = "Chinese"; skill = "Chinese"; break;
                            default: chatLang = "English"; break;
                        }
                    }
                    redirectLink = "https://server.iad.liveperson.net/hc/88942816/?cmd=file&file=visitorWantsToChat&site=88942816&SV!skill=" + skill + "&leInsId=88942816527642465&skId=1&leEngId=88942816_29aeab82-a5fc-4de7-b801-c6a87c638106&leEngTypeId=8&leEngName=LiveHelp_default&leRepAvState=3&referrer=" + CurrentUrl + "&SESSIONVAR!visitor_profile=" + chatLang;

                    #endregion liveperson
                }
            }
            return redirectLink;
        }
        catch(Exception)
        {
            return redirectLink;
        }
    }
}