﻿using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using Helpers.GameProviders;
using Models;

namespace Factories.Slots.Handlers
{
    /// <summary>
    /// This is the handler for GPI-QTech (QT)
    /// Lobby = Club Landing Page
    /// </summary>
    public class QTHandler : GameLoaderBase
    {
        public QTHandler(string token, string lobby) : base(GameProvider.QT)
        {
            GameProvider = GameProvider.QT;
            GameLink = new GameLinkInfo
            {
                Fun = GameSettings.GetGameUrl(GameProvider, GameLinkSetting.Fun),
                Real = GameSettings.GetGameUrl(GameProvider, GameLinkSetting.Real),
                MemberSessionId = token,
                LobbyPage = lobby
            };
        }

        protected override string CreateFunUrl(XElement element)
        {
            var gpi = new Gpi(GameLink).CheckRSlot(GameLinkSetting.Fun, element);
            if (!string.IsNullOrWhiteSpace(gpi))
            {
                return gpi;
            }

            string lang = GetGameLanguage(element);
            string gameName = element.Attribute("Id") != null ? element.Attribute("Id").Value : "";
            return GameLink.Fun.Replace("{GAME}", gameName).Replace("{LANG}", lang).Replace("{CURRENCY}", GetCurrencyByLanguage()).Replace("{LOBBY}", GameLink.LobbyPage);
        }

        protected override string CreateRealUrl(XElement element)
        {
            var gpi = new Gpi(GameLink).CheckRSlot(GameLinkSetting.Real, element);
            if (!string.IsNullOrWhiteSpace(gpi))
            {
                return gpi;
            }

            string lang = GetGameLanguage(element);
            string gameName = element.Attribute("Id") != null ? element.Attribute("Id").Value : "";
            return GameLink.Real.Replace("{GAME}", gameName).Replace("{LANG}", lang).Replace("{TOKEN}", GameLink.MemberSessionId).Replace("{LOBBY}", GameLink.LobbyPage);
        }

        private string GetCurrencyByLanguage()
        {
            string currency;
            switch (commonVariables.SelectedLanguage)
            {
                case "zh-cn":
                    currency = "CNY";
                    break;
                case "ja-jp":
                    currency = "JPY";
                    break;
                default:
                    currency = "USD";
                    break;
            }

            if (HttpContext.Current.Session["LanguageCode"] != null && HttpContext.Current.Session["CurrencyCode"] != null)
            {
                if ((string)HttpContext.Current.Session["LanguageCode"] == "en-us" && ((string)HttpContext.Current.Session["CurrencyCode"] == "MY"))
                {
                    currency = "MYR";
                }
            }

            return currency;
        }

        private string GetGameLanguage(XElement element)
        {
            if (element.Attribute("LanguageCode") == null) return "en_US";

            var lang = langCode.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                
            string[] languagesCodes = element.Attribute("LanguageCode").Value.Split(',');

            bool isLangSupp = languagesCodes.Contains(langCode, StringComparer.OrdinalIgnoreCase);

            return isLangSupp ? string.Format("{0}_{1}", lang[0], lang[1].ToUpper()) : "en_US";
        }
    }
}