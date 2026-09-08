using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using TeamCherry.Localization;
using TeamCherry.SharedUtils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BasicItemSync.Modules;

internal static class ObjectHelper
{
    static SystemLanguage LastLanguage = SystemLanguage.Unknown;
    static Dictionary<string, Dictionary<string, string>> CurrentLanguage;
    public static PersistentItem<T>? FindPersistent<T>(string sceneName, string id) where T : IEquatable<T>
    {
        var scene = SceneManager.GetActiveScene();
        return Resources.FindObjectsOfTypeAll<PersistentItem<T>>()
            .FirstOrDefault(p => p.ItemData.ID == id && p.ItemData.SceneName == sceneName && p.gameObject.scene == scene);
    }

    static string GetLanguageFileContents(string sheetTitle, LanguageCode language)
    {
        var textAsset = Resources.Load<TextAsset>($"Languages/{language}_{sheetTitle}");
        if (textAsset == null) return string.Empty;

        return Encryption.Decrypt(textAsset.text);
    }

    static void LoadLanguage()
    {
        var lang = ModSettings.PreferredLanguage;
        var langCode = Language.LanguageNameToCode(lang);

        foreach (var sheet in Language.Settings.sheetTitles)
        {
            CurrentLanguage[sheet] = [];
            var languageFileContents = GetLanguageFileContents(sheet, langCode);

            if (!string.IsNullOrEmpty(languageFileContents))
            {
                using XmlReader xmlReader = XmlReader.Create(new StringReader(languageFileContents));

                while (xmlReader.ReadToFollowing("entry"))
                {
                    xmlReader.MoveToFirstAttribute();
                    var key = xmlReader.Value;

                    xmlReader.MoveToElement();
                    var text = xmlReader.ReadElementContentAsString().Trim().UnescapeXml();
                    
                    CurrentLanguage[sheet][key] = text;
                }
            }
        }
    }

    extension(Language)
    {
        public static string GetLocal(LocalisedString str)
        {
            var sheet = str.Sheet;
            var key = str.Key;

            var lang = ModSettings.PreferredLanguage;
            if (lang == SystemLanguage.Unknown)
            {
                return Language.Get(key, sheet);
            }

            if (LastLanguage != lang)
            {
                LoadLanguage();
            }

            if (!CurrentLanguage.TryGetValue(sheet, out Dictionary<string, string>? value))
            {
                Debug.LogError($"The sheet '{sheet}' does not exist!");
                return "";
            }

            if (value.TryGetValue(key, out var text))
            {
                return text;
            }

            return "#!#" + key + "#!#";
        }
    }
}