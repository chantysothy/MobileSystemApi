﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class _Lang : BasePage
{
    protected string strAlertMessage = string.Empty;
    protected System.Xml.Linq.XElement xeErrors = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        xeErrors = commonVariables.ErrorsXML;
        bool login = false;

        #region Logout

        if (string.Compare(Convert.ToString(this.RouteData.DataTokens["logout"]), "true", true) == 0)
        {
            login = true;
            commonVariables.ClearSessionVariables();
            commonCookie.ClearCookies();
        }
        if (string.Compare(Convert.ToString(this.RouteData.DataTokens["expire"]), "true", true) == 0)
        {
            login = true;
            commonVariables.ClearSessionVariables();
            commonCookie.ClearCookies();
            strAlertMessage = commonCulture.ElementValues.getResourceString("SessionExpired", xeErrors);
        }
        if (string.Compare(Convert.ToString(this.RouteData.DataTokens["invalid"]), "true", true) == 0)
        {
            login = true;
            commonVariables.ClearSessionVariables();
            commonCookie.ClearCookies();
            strAlertMessage = commonCulture.ElementValues.getResourceString("SessionExpired", xeErrors);
        }

        #endregion

        customConfig.OperatorSettings opSettings = new customConfig.OperatorSettings("W88");
        string arrStrLanguageSelection = opSettings.Values.Get("LanguageSelection");
        List<string> lstLanguageSelection =
            arrStrLanguageSelection.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(p => p.Trim())
                .ToList();

        System.Text.StringBuilder sbLanguageHTML = new System.Text.StringBuilder();
        foreach (string language in lstLanguageSelection)
        {
            string strLanguage = language.Trim();

            if (commonVariables.CDNCountryCode.Equals("MY", StringComparison.OrdinalIgnoreCase))
            {
                if (strLanguage.Equals("zh-cn", StringComparison.OrdinalIgnoreCase)){
                    sbLanguageHTML.AppendFormat("<li><a data-ajax='false' href='/Index.aspx?lang={0}' data-inline='true' id='divzh-my' class='divLangImg divzh-my'></a></li>", strLanguage);
                    continue;
                }
                else if (strLanguage.Equals("en-us", StringComparison.OrdinalIgnoreCase))
                {
                    sbLanguageHTML.AppendFormat("<li><a data-ajax='false' href='/Index.aspx?lang={0}' data-inline='true' id='diven-my' class='divLangImg diven-my'></a></li>", strLanguage);
                    continue;
                }
            }

            sbLanguageHTML.AppendFormat("<li><a data-ajax='false' href='/Index.aspx?lang={0}' data-inline='true' id='div{0}' class='divLangImg div{0}'></a></li>", strLanguage);
        }
        divLanguageContainer.InnerHtml = Convert.ToString(sbLanguageHTML);

        string affiliateId = HttpContext.Current.Request.QueryString.Get("AffiliateId");

        if (!string.IsNullOrEmpty(affiliateId))
        {
            commonVariables.SetSessionVariable("AffiliateId", affiliateId);

            commonCookie.CookieAffiliateId = affiliateId;
        }

    }


}
