﻿using System;
using System.Collections.Generic;
using System.Data;
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

        string strProcessCode = string.Empty;
        string strProcessMessage = string.Empty;
        string strLastLoginIP = string.Empty;

        bool runIovation = false;

        System.Xml.XmlDocument xdResponse = new System.Xml.XmlDocument();

        #endregion

        #region populateVariables
        var lngOperatorId = long.Parse(commonVariables.OperatorId);
        var strMemberCode = Request.Form.Get("txtUsername");
        var strPassword = Request.Form.Get("txtPassword");
        var strSiteURL = commonVariables.SiteUrl;
        var strDeviceId = HttpContext.Current.Request.UserAgent;
        var strVCode = Request.Form.Get("txtCaptcha");
        var strSessionVCode = commonEncryption.decrypting(commonVariables.GetSessionVariable("vCode"));
        #endregion

        #region parametersValidation
        if (string.IsNullOrEmpty(strMemberCode)) { 
            strProcessCode = "-1"; 
            strProcessMessage = commonCulture.ElementValues.getResourceXPathString("Login/MissingUsername", xeErrors); 
            isProcessAbort = true; }
        else if (string.IsNullOrEmpty(strPassword)) { 
            strProcessCode = "-1"; 
            strProcessMessage = commonCulture.ElementValues.getResourceXPathString("Login/MissingPassword", xeErrors); 
            isProcessAbort = true; 
        }
        else if (commonValidation.isInjection(strMemberCode)) { 
            strProcessCode = "-1"; 
            strProcessMessage = commonCulture.ElementValues.getResourceXPathString("Login/InvalidUsername", xeErrors); 
            isProcessAbort = true; 
        }
        else if (commonValidation.isInjection(strPassword))
        {
            strProcessCode = "-1";
            strProcessMessage = commonCulture.ElementValues.getResourceXPathString("Login/InvalidPassword", xeErrors);
            isProcessAbort = true;
        }
        else if (!string.IsNullOrEmpty(strVCode) && !string.IsNullOrEmpty(strSessionVCode))
        {
            if (string.IsNullOrEmpty(strVCode))
            {
                strProcessCode = "-1";
                strProcessMessage = commonCulture.ElementValues.getResourceString("MissingVCode", xeErrors);
                isProcessAbort = true;
            }
            else if (commonValidation.isInjection(strVCode))
            {
                strProcessCode = "-1";
                strProcessMessage = commonCulture.ElementValues.getResourceXPathString("Register/InvalidVCode", xeErrors);
                isProcessAbort = true;
            }
            if (strVCode != strSessionVCode)
            {
                strProcessCode = "-1";
                strProcessMessage = commonCulture.ElementValues.getResourceXPathString("Register/IncorrectVCode", xeErrors);
                isProcessAbort = true;
            }
        }
        else
        {
            strPassword = commonEncryption.Encrypt(strPassword);
        }

        strProcessRemark = string.Format("MemberCode: {0} | Password: {1} | VCode: {2} | SVCode: {3} | IP: {4} ", strMemberCode, strPassword, strVCode, strSessionVCode, commonIp.UserIP);

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
                    dsSignin = svcInstance.MemberSignin(lngOperatorId, strMemberCode, strPassword, strSiteURL, commonIp.UserIP, strDeviceId);

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
                                HttpContext.Current.Session.Add("PaymentGroup", Convert.ToString(dsSignin.Tables[0].Rows[0]["paymentGroup"]));
                                HttpContext.Current.Session.Add("PartialSignup", Convert.ToString(dsSignin.Tables[0].Rows[0]["partialSignup"]));
                                HttpContext.Current.Session.Add("ResetPassword", Convert.ToString(dsSignin.Tables[0].Rows[0]["resetPassword"]));

                                commonCookie.CookieS = strMemberSessionId;
                                commonCookie.CookieG = strMemberSessionId;
                                commonCookie.CookiePalazzo = strPassword;

                                bool isResetPassword = Convert.ToBoolean(string.IsNullOrWhiteSpace(Session["ResetPassword"] as string) ? 0 : Session["ResetPassword"]);

                                if (isResetPassword)
                                {
                                    strProcessCode = "resetPassword";
                                }

                                strLastLoginIP = Convert.ToString(dsSignin.Tables[0].Rows[0]["lastLoginIP"]);
                                if (HttpContext.Current.Request.Cookies[strMemberCode] == null)
                                {
                                    runIovation = true;
                                }
                                else if (HttpContext.Current.Request.Cookies[strMemberCode] != null && string.Compare(strLastLoginIP, commonIp.UserIP, true) != 0)
                                {
                                    runIovation = true;
                                }
                                if (runIovation)
                                {
                                    this.IovationSubmit(ref intProcessSerialId, strProcessId, strPageName, strMemberCode, commonIp.UserIP);
                                }

                                DataSet dsMember = svcInstance.MemberSessionCheck(commonVariables.CurrentMemberSessionId, commonIp.UserIP);

                                if (dsMember.Tables[0].Rows.Count > 0)
                                {
                                    strProcessCode = Convert.ToString(dsSignin.Tables[0].Rows[0]["RETURN_VALUE"]);
                                    switch (strProcessCode)
                                    {
                                        case "0":
                                            strProcessMessage = commonCulture.ElementValues.getResourceString("Exception", xeErrors); ;
                                            break;
                                        case "1":
                                            HttpContext.Current.Session.Add("MemberName", Convert.ToString(dsMember.Tables[0].Rows[0]["lastName"]) + Convert.ToString(dsMember.Tables[0].Rows[0]["firstName"]));
                                            break;
                                    }
                                }

                                break;
                            case "21":
                                strProcessMessage = commonCulture.ElementValues.getResourceXPathString("Login/InvalidUsername", xeErrors);
                                break;
                            case "22":
                                strProcessMessage = commonCulture.ElementValues.getResourceXPathString("Login/InactiveAccount", xeErrors);
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


    protected void IovationSubmit(ref int intProcessSerialId, string strProcessId, string strPageName, string strUsername, string strIPAddress)
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

        if (string.Compare(strServiceEnabled, "true", true) != 0)
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
}