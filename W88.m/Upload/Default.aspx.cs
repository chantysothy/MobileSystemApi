﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Upload_Default : System.Web.UI.Page
{
    private System.Xml.Linq.XElement xeResources = null;
    protected string strAlertCode = string.Empty;
    protected string strAlertMessage = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        commonCulture.appData.getLocalResource(out xeResources);

        lblUsername.Text = commonCulture.ElementValues.getResourceString("lblUsername", xeResources);
        txtUsername.Text = commonVariables.GetSessionVariable("MemberCode");

        lblCurrency.Text = commonCulture.ElementValues.getResourceString("lblCurrency", xeResources);
        txtCurrency.Text = commonVariables.GetSessionVariable("MemberCode");

        lblRemarks.Text = commonCulture.ElementValues.getResourceString("lblRemarks", xeResources);
        txtRemarks.Text = commonVariables.GetSessionVariable("MemberCode");

        lblFileUpload.Text = commonCulture.ElementValues.getResourceString("lblFileUpload", xeResources);
        txtUsername.Text = commonVariables.GetSessionVariable("MemberCode");

        btnSubmit.Text = commonCulture.ElementValues.getResourceString("btnSubmit", xeResources);

        lblSuccess.Text = commonCulture.ElementValues.getResourceString("Success", xeResources);
    }

    protected void btnSubmit_Click(object sender, EventArgs e) 
    {
        string strSMTPHost = System.Configuration.ConfigurationManager.AppSettings.Get("SMTPHOST");
        string strEmailFrom = "fileupload-mobile@w88.com";
        string strUsername = "";
        string strRemarks = "";
        string strCurrency = "";
        string strSubmissionID = System.DateTime.Now.ToString("hhmmssDDMMYY");

        if (fuFileUpload.HasFile)
        {
            try
            {
                System.Net.Mail.SmtpClient sClient = new System.Net.Mail.SmtpClient();

                sClient.Host = strSMTPHost;
                sClient.Port = 25;

                if (string.Compare(sClient.Host, "retail.smtp.com", true) == 0)
                {
                    System.Net.NetworkCredential nCredentials = new System.Net.NetworkCredential();
                    nCredentials.UserName = "dev@w88.com";
                    nCredentials.Password = "2NDbr0isFAT!";
                    sClient.UseDefaultCredentials = false;
                    sClient.Credentials = nCredentials;
                }

                using (System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage())
                {
                    message.From = new System.Net.Mail.MailAddress(strEmailFrom);
                    message.To.Add("gb.martymcfly@gmail.com");
                    message.Body = string.Format("Username: {0}{1}Currency: {2}{3}Remarks: {4}", strUsername, System.Environment.NewLine, strCurrency, System.Environment.NewLine, strRemarks);
                    message.Subject = string.Format("Attachment Upload - {0} / {1} / {2}", strSubmissionID, strUsername, strCurrency);
                    message.Attachments.Add(new System.Net.Mail.Attachment(fuFileUpload.PostedFile.InputStream, fuFileUpload.PostedFile.FileName));
                    sClient.Send(message);
                }

                strAlertCode = "00";
                lblSuccess.Text = commonCulture.ElementValues.getResourceString("Success", xeResources).Replace("[submissionID]", strSubmissionID);
            }
            catch (Exception ex)
            {
                //textBox4.Text += Environment.NewLine + ex.Message;
            }
            GC.Collect();
        }
        else 
        { 
            strAlertCode = "01";
            strAlertMessage = commonCulture.ElementValues.getResourceString("MissingAttachment", xeResources);
        }
    }
}