﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml.Linq;


public partial class Deposit_KexunPay : PaymentBasePage
{
    protected string lblTransactionId;

    protected void Page_Init(object sender, EventArgs e)
    {
        base.PageName = Convert.ToString(commonVariables.DepositMethod.KexunPayWeChat);
        base.PaymentType = commonVariables.PaymentTransactionType.Deposit;
        base.PaymentMethodId = Convert.ToString((int)commonVariables.DepositMethod.KexunPayWeChat);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            CheckAgentAndRedirect(string.Concat(V2DepositPath, "Pay", PaymentMethodId, ".aspx"));
            this.InitializeLabels();
        }
    }

    private void InitializeLabels()
    {
        XElement xRes;
        commonCulture.appData.getRootResource("Deposit/JTPayWeChat", out xRes);
        lblNote.Text = commonCulture.ElementValues.getResourceString("lblNote", xRes);

        lblDepositAmount.Text = base.strlblAmount;

        lblTransactionId = base.strlblTransactionId;
    }
}