﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.XPath;

public class commonConversion
{
    internal static string toUnicode(string messageText)
    {
        System.Text.StringBuilder unicodeStringBuilder = new System.Text.StringBuilder();

        foreach (char letter in messageText)
        {
            // Get the integral value of the character.
            int value = Convert.ToInt32(letter);
            // Convert the decimal value to a hexadecimal value in string form.
            string hexOutput = String.Format("{0:X}", value);

            unicodeStringBuilder.Append(hexOutput.PadLeft(4, '0'));
        }

        return unicodeStringBuilder.ToString();
    }

    public static System.DateTime convertDateTime(string dateTime, string format)
    {
        return System.DateTime.ParseExact(dateTime, format, null);
    }

    internal static bool convertDateTime(string dateTime, string format, out System.DateTime convertedDateTime)
    {
        bool validDateTime = System.DateTime.TryParseExact(dateTime, format, null, System.Globalization.DateTimeStyles.None, out convertedDateTime);
        return validDateTime;
    }

    internal static bool convertDateTime(string dateTime, string format, string format2, out System.DateTime convertedDateTime)
    {
        bool validDateTime = System.DateTime.TryParseExact(dateTime, new string[] { format, format2 }, null, System.Globalization.DateTimeStyles.None, out convertedDateTime);
        return validDateTime;
    }

    internal static void stringToDataSet(string str, out System.Data.DataSet dataSet)
    {
        System.IO.StringReader strReader = null;
        dataSet = null;

        if (!string.IsNullOrEmpty(str))
        {
            using (strReader = new System.IO.StringReader(str))
            {
                dataSet = new System.Data.DataSet();
                dataSet.ReadXml(strReader);
            }
        }
    }

    internal static void stringToDataTable(string str, out System.Data.DataTable dataTable)
    {
        System.IO.StringReader strReader = null;
        dataTable = null;
        System.Data.DataSet data_set = null;

        if (!string.IsNullOrEmpty(str))
        {
            using (strReader = new System.IO.StringReader(str))
            {
                using (data_set = new System.Data.DataSet())
                {
                    data_set.ReadXml(strReader);
                    dataTable = data_set.Tables[0];
                }
            }
        }
    }

    internal static void dataRowToQueryString(System.Data.DataRow dataRow, out string querySting)
    {
        querySting = string.Join("&", (from System.Data.DataColumn dc in dataRow.Table.Columns select String.Concat(dc.ColumnName, "=", dataRow[dc.ColumnName])).ToArray());
    }

    internal static string dataRowToQueryString(System.Data.DataRow dataRow)
    {
        return string.Join("&", (from System.Data.DataColumn dc in dataRow.Table.Columns select String.Concat(dc.ColumnName, "=", dataRow[dc.ColumnName])).ToArray());
    }

    internal static void sqlParamsToQueryString(System.Data.SqlClient.SqlParameterCollection sqlParams, out string querySting)
    {
        querySting = string.Join("&", (from System.Data.SqlClient.SqlParameter param in sqlParams select String.Concat(param.ParameterName, "=", param.SqlValue)).ToArray());
    }

    internal static string sqlParamsToQueryString(System.Data.SqlClient.SqlParameterCollection sqlParams)
    {
        return string.Join("&", (from System.Data.SqlClient.SqlParameter param in sqlParams select String.Concat(param.ParameterName, "=", param.SqlValue)).ToArray());
    }


    internal static void nameValueToQueryString(System.Collections.Specialized.NameValueCollection collection, out string queryString)
    {
        queryString = String.Join("&", (from string name in collection select String.Concat(name, "=", HttpUtility.UrlEncode(collection[name]))).ToArray());
    }

    internal static string nameValueToQueryString(System.Collections.Specialized.NameValueCollection collection)
    {
        return String.Join("&", (from string name in collection select String.Concat(name, "=", HttpUtility.UrlEncode(collection[name]))).ToArray());
    }
}

internal class xmlData
{
    private static System.Xml.XmlDocument xmlDocument = null;
    internal xmlData(string xmlString)
    {
        xmlDocument = new System.Xml.XmlDocument();
        xmlDocument.LoadXml(xmlString);
    }
    internal System.Xml.XmlNode getNode(string nodeName)
    {
        return xmlDocument.DocumentElement.SelectSingleNode(nodeName);
    }
    internal string getNodeText(string nodeName)
    {
        if (this.getNode(nodeName) == null) { return ""; }
        else if (xmlDocument.DocumentElement.FirstChild.SelectSingleNode(nodeName) != null) { return xmlDocument.DocumentElement.FirstChild.SelectSingleNode(nodeName).InnerText.Trim(); }
        else { return xmlDocument.DocumentElement.SelectSingleNode(nodeName).InnerText.Trim(); }

    }
    internal String getNodeAttribute(string nodeName, string attributeName)
    {

        if (this.getNode(nodeName) == null) { return ""; }
        else if (xmlDocument.DocumentElement.FirstChild.SelectSingleNode(nodeName) != null) { return xmlDocument.DocumentElement.FirstChild.SelectSingleNode(nodeName).Attributes[attributeName].Value; }
        else { return xmlDocument.DocumentElement.SelectSingleNode(nodeName).Attributes[attributeName].Value; }
    }

    internal static bool nodeExist(System.Xml.Linq.XElement xmlNode, string nodeName)
    {
        return xmlNode.Element(nodeName) != null;
    }
    internal static string getNodeText(System.Xml.Linq.XDocument xDocument, string nodeName)
    {
        return xDocument.Descendants(nodeName).FirstOrDefault() == null ? "" : xDocument.Descendants(nodeName).FirstOrDefault().Value;
    }
    internal static string getNodeText(System.Xml.Linq.XElement xmlNode, string nodeName)
    {
        return xmlNode.Descendants(nodeName).FirstOrDefault() == null ? "" : xmlNode.Descendants(nodeName).FirstOrDefault().Value;
    }
    internal static string getNodeAttribute(System.Xml.Linq.XElement xmlNode, string nodeName, string attributeName)
    {
        return xmlNode.Descendants(nodeName).FirstOrDefault() == null ? "" : xmlNode.Descendants(nodeName).FirstOrDefault().Attribute(attributeName).Value;
    }
    internal static string getNodeValueByAttribute(System.Xml.Linq.XElement xmlNode, string nodeName, string attributeName, string attributeNameValue)
    {
        return (from x in xmlNode.Descendants(nodeName) where x.Attribute(attributeName).Value == attributeNameValue select x.Value).FirstOrDefault();
    }

    internal static string getNodeText(System.Xml.XmlNode xmlNode, string nodeName)
    {
        if (xmlNode.SelectSingleNode(nodeName) == null) { return ""; }
        else if (xmlNode.FirstChild.SelectSingleNode(nodeName) != null) { return xmlNode.FirstChild.SelectSingleNode(nodeName).InnerText.Trim(); }
        else { return xmlNode.SelectSingleNode(nodeName).InnerText.Trim(); }

    }
    internal static string getNodeText(System.Xml.XmlDocument xmlNode, string nodeName)
    {        
        if (xmlNode.DocumentElement.SelectSingleNode(nodeName) == null) { return ""; }
        else if (xmlNode.DocumentElement.FirstChild.SelectSingleNode(nodeName) != null) { return xmlNode.DocumentElement.FirstChild.SelectSingleNode(nodeName).InnerText.Trim(); }
        else { return xmlNode.DocumentElement.SelectSingleNode(nodeName).InnerText.Trim(); }
    }
    

    //Implemented based on interface, not part of algorithm
    internal static string RemoveAllNamespaces(string xmlDocument)
    {
        System.Xml.Linq.XElement xmlDocumentWithoutNs = null;

        try { xmlDocumentWithoutNs = RemoveAllNamespaces(System.Xml.Linq.XElement.Parse(xmlDocument)); }
        catch (Exception ex1)
        {
            System.Text.StringBuilder sbXMLDocument = new System.Text.StringBuilder();
            sbXMLDocument.AppendFormat("<root>{0}</root>", xmlDocument);

            try { xmlDocument = Convert.ToString(sbXMLDocument); xmlDocumentWithoutNs = RemoveAllNamespaces(System.Xml.Linq.XElement.Parse(xmlDocument)); }
            catch (Exception ex2) { }
        }

        return xmlDocumentWithoutNs.ToString();
    }
    //Core recursion function
    private static System.Xml.Linq.XElement RemoveAllNamespaces(System.Xml.Linq.XElement xmlDocument)
    {
        if (xmlDocument.HasAttributes) { xmlDocument.RemoveAttributes(); }
        if (!xmlDocument.HasElements)
        {
            System.Xml.Linq.XElement xElement = new System.Xml.Linq.XElement(xmlDocument.Name.LocalName);
            xElement.Value = xmlDocument.Value;

            foreach (System.Xml.Linq.XAttribute attribute in xmlDocument.Attributes())
                xElement.Add(attribute);

            return xElement;
        }
        return new System.Xml.Linq.XElement(xmlDocument.Name.LocalName, xmlDocument.Elements().Select(el => RemoveAllNamespaces(el)));
    }
}