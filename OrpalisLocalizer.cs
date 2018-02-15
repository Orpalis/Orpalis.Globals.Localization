/**********************************************************************
 * Projet:                  Orpalis.Globals.Localization
 * Author:					Evan Carrère.
 *
 * (C) Copyright 2018, ORPALIS.
 ** Licensed under the Apache License, Version 2.0 (the "License");
 ** you may not use this file except in compliance with the License.
 ** You may obtain a copy of the License at
 ** http://www.apache.org/licenses/LICENSE-2.0
 ** Unless required by applicable law or agreed to in writing, software
 ** distributed under the License is distributed on an "AS IS" BASIS,
 ** WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 ** See the License for the specific language governing permissions and
 ** limitations under the License.
 *
 **********************************************************************/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;


namespace Orpalis.Globals.Localization
{
    /// <summary>
    /// Implements a mechanism which allows the storage and the access of localized text content based on the OrpalisLocalizedString model. 
    /// </summary>
    public sealed class OrpalisLocalizer
    {
        /// <summary>
        /// Maps the different existing languages with their representing ISO 639-1 code.
        /// </summary>
        /// <remarks>
        /// For more information about ISO 639-1, visit https://www.iso.org/iso-639-language-codes.html
        /// </remarks>
        private static readonly IReadOnlyDictionary<string, string> ISOLanguages = new Dictionary<string, string>
        {
            //{"aa", "Qafar"},
            //{"af", "Afrikaans"},
            //{"ak", "Akan"},
            //{"am", "አማርኛ"},
            {"ar", "العربية"},
            //{"as", "অসমীয়া"},
            //{"az", "azərbaycan"},
            //{"ba", "Башҡорт"},
            //{"be", "Беларуская"},
            {"bg", "български"},
            //{"bm", "bamanakan"},
            //{"bn", "বাংলা"},
            //{"bo", "བོད་ཡིག"},
            //{"br", "brezhoneg"},
            //{"bs", "bosanski"},
            //{"ca", "català"},
            //{"ce", "нохчийн"},
            //{"co", "Corsu"},
            {"cs", "čeština"},
            //{"cu", "церковнослове́нскїй"},
            //{"cy", "Cymraeg"},
            {"da", "dansk"},
            {"de", "Deutsch"},
            //{"dv", "ދިވެހިބަސް"},
            //{"dz", "རྫོང་ཁ"},
            //{"ee", "Eʋegbe"},
            {"el", "Ελληνικά"},
            {"en", "English"},
            //{"eo", "esperanto"},
            {"es", "español"},
            //{"et", "eesti"},
            //{"eu", "euskara"},
            //{"fa", "فارسى"},
            //{"ff", "Fulah"},
            {"fi", "suomi"},
            //{"fo", "føroyskt"},
            {"fr", "Français"},
            //{"fy", "Frysk"},
            //{"ga", "Gaeilge"},
            //{"gd", "Gàidhlig"},
            //{"gl", "galego"},
            //{"gn", "Avañe’ẽ"},
            //{"gu", "ગુજરાતી"},
            //{"gv", "Gaelg"},
            //{"ha", "Hausa"},
            {"he", "עברית"},
            {"hi", "हिंदी"},
            //{"hr", "hrvatski"},
            {"hu", "magyar"},
            //{"hy", "Հայերեն"},
            //{"ia", "interlingua"},
            {"id", "Indonesia"},
            //{"ig", "Igbo"},
            //{"ii", "ꆈꌠꁱꂷ"},
            //{"is", "íslenska"},
            {"it", "italiano"},
            //{"iu", "Inuktitut"},
            {"ja", "日本語"},
            //{"jv", "Basa Jawa"},
            //{"ka", "ქართული"},
            //{"ki", "Gikuyu"},
            //{"kk", "қазақ тілі"},
            //{"kl", "kalaallisut"},
            //{"km", "ភាសាខ្មែរ"},
            //{"kn", "ಕನ್ನಡ"},
            {"ko", "한국어"},
            //{"kr", "Kanuri"},
            //{"ks", "کٲشُر"},
            //{"ku", "کوردیی ناوەڕاست"},
            //{"kw", "kernewek"},
            //{"ky", "Кыргыз"},
            //{"la", "lingua latīna"},
            //{"lb", "Lëtzebuergesch"},
            //{"lg", "Luganda"},
            //{"ln", "lingála"},
            //{"lo", "ລາວ"},
            //{"lt", "lietuvių"},
            //{"lu", "Tshiluba"},
            //{"lv", "latviešu"},
            //{"mg", "Malagasy"},
            //{"mi", "Reo Māori"},
            //{"mk", "македонски"},
            //{"ml", "മലയാളം"},
            //{"mn", "монгол"},
            //{"mr", "मराठी"},
            //{"ms", "Bahasa Melayu"},
            //{"mt", "Malti"},
            //{"my", "ဗမာ"},
            //{"nb", "norsk bokmål"},
            //{"nd", "isiNdebele"},
            //{"ne", "नेपाली"},
            {"nl", "Nederlands"},
            //{"nn", "nynorsk"},
            {"no", "norsk"},
            //{"nr", "isiNdebele"},
            //{"oc", "Occitan"},
            //{"om", "Oromoo"},
            //{"or", "ଓଡ଼ିଆ"},
            //{"os", "ирон"},
            //{"pa", "ਪੰਜਾਬੀ"},
            {"pl", "polski"},
            //{"ps", "پښتو"},
            {"pt", "português"},
            //{"rm", "rumantsch"},
            //{"rn", "Ikirundi"},
            {"ro", "română"},
            {"ru", "русский"},
            //{"rw", "Kinyarwanda"},
            //{"sa", "संस्कृत"},
            //{"sd", "سنڌي"},
            //{"se", "davvisámegiella"},
            //{"sg", "Sängö"},
            //{"si", "සිංහල"},
            {"sk", "slovenčina"},
            {"sl", "slovenščina"},
            //{"sn", "chiShona"},
            //{"so", "Soomaali"},
            //{"sq", "shqip"},
            //{"sr", "srpski"},
            //{"ss", "Siswati"},
            //{"st", "Sesotho"},
            {"sv", "svenska"},
            //{"sw", "Kiswahili"},
            //{"ta", "தமிழ்"},
            //{"te", "తెలుగు"},
            //{"tg", "Тоҷикӣ"},
            {"th", "ไทย"},
            //{"ti", "ትግርኛ"},
            //{"tk", "Türkmen dili"},
            //{"tn", "Setswana"},
            //{"to", "lea fakatonga"},
            {"tr", "Türkçe"},
            //{"ts", "Xitsonga"},
            //{"tt", "Татар"},
            //{"ug", "ئۇيغۇرچە"},
            //{"uk", "українська"},
            //{"ur", "اُردو"},
            //{"uz", "o‘zbek"},
            //{"ve", "Tshivenḓa"},
            {"vi", "Tiếng Việt"},
            //{"vo", "Volapük"},
            //{"wo", "Wolof"},
            //{"xh", "isiXhosa"},
            //{"yi", "ייִדיש"},
            //{"yo", "Èdè Yorùbá"},
            {"zh", "中文"},
            //{"zu", "isiZulu"}
        };

        /// <summary>
        /// Contains the localized strings which have been deserialized.
        /// </summary>
        private readonly List<OrpalisLocalizedString> _localizedStrings;
        /// <summary>
        /// <see cref="DefaultLanguage"/>
        /// </summary>
        private readonly string _defaultLanguage;

        /// <summary>
        /// Specifies the default language which is used by the instance.
        /// </summary>
        public string DefaultLanguage => _defaultLanguage;


        /// <summary>
        /// Initializes a new OrpalisLocalizer instance with the provided localized data.
        /// </summary>
        /// <param name="localizedData">A stream to the localized data to be deserialized.</param>
        /// <param name="defaultLanguage">The default language, use an empty string to not specify any in case of non-translated resources.</param>
        public OrpalisLocalizer(Stream localizedData, string defaultLanguage = Constants.DEFAULT_LANGUAGE)
        {
            using (StreamReader streamReader = new StreamReader(localizedData))
            {
                _localizedStrings = JsonConvert.DeserializeObject<List<OrpalisLocalizedString>>(streamReader.ReadToEnd());

                IReadOnlyList<string> localizedLanguages = GetLocalizedLanguages();

                foreach (string lang in localizedLanguages)
                {
                    if (!ISOLanguages.ContainsKey(lang))
                    {
                        throw new Exception("Unsupported language: " + lang);
                    }
                }

                if (!string.IsNullOrWhiteSpace(defaultLanguage))
                {
                    if (!localizedLanguages.Contains(defaultLanguage))
                    {
                        throw new ArgumentException("The specified default language is not available into the localized data", nameof(defaultLanguage));
                    }

                    _defaultLanguage = defaultLanguage;
                }
            }
        }


        /// <summary>
        /// Gets the localized string for a specific language.
        /// </summary>
        /// <param name="name">The name of the localized string to be retrieved.</param>
        /// <param name="language">The language in which the localized string shall be gotten.</param>
        /// <returns>
        /// The localized string in the requested language.
        /// </returns>
        /// <remarks>
        /// If the requested string is found but not available in the requested language, it is returned in the default one.
        /// </remarks>
        /// <exception cref="ArgumentException">
        /// Thrown when the requested string is unknown. 
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the name of the requested string or the language is null.
        /// </exception>
        public string GetString(string name, string language)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            if (language == null)
            {
                throw new ArgumentNullException(nameof(language));
            }

            foreach (OrpalisLocalizedString localizedString in _localizedStrings)
            {//todo: implement a lookup sorted dictionnary to speed-up the Name retrieval.
                if (localizedString.Name == name)
                {
                    string value;

                    if (localizedString.LocalizedValue.TryGetValue(language, out value) && !string.IsNullOrEmpty(value))
                    {
                        return value;
                    }

                    if (!string.IsNullOrWhiteSpace(_defaultLanguage))
                    {
                        return localizedString.LocalizedValue[_defaultLanguage];
                    }
                    else
                    {
                        throw new ArgumentException(name + " localization key was found but it contains empty data", nameof(name));
                    }
                }
            }

            throw new ArgumentException("Unknown " + name + " localization key", nameof(name));
        }


        /// <summary>
        /// Gets the languages in which the localized strings are available.
        /// </summary>
        /// <returns>
        /// The list of available languages for the localized strings.
        /// </returns>
        public IReadOnlyList<string> GetLocalizedLanguages()
        {
            if (_localizedStrings.Count > 0)
            {
                return new List<string>(_localizedStrings[0].LocalizedValue.Keys);
            }

            return null;
        }


        /// <summary>
        /// Determines whether the localized strings are available in the requested language.
        /// </summary>
        /// <param name="language">The language of interest.</param>
        /// <returns>Whether the localized strings are available in the requested language.</returns>
        public bool IsSupportedLanguage(string language)
        {
            IReadOnlyList<string> supportedLanguages = GetLocalizedLanguages();

            if (supportedLanguages != null)
            {
                return supportedLanguages.Contains(language);
            }

            return false;
        }


        /// <summary>
        /// Gets all the different resource identifiers of the localized strings.
        /// </summary>
        /// <returns>
        /// The different resource identifiers of the localized strings.
        /// </returns>
        public IReadOnlyList<string> GetAllLocalizedResources()
        {
            List<string> resourceIDs = new List<string>();

            foreach (OrpalisLocalizedString entry in _localizedStrings)
            {
                if (!string.IsNullOrEmpty(entry.ResourceIDs))
                {
                    string[] localizedResourceIDs = entry.ResourceIDs.Split(',');

                    foreach (string localizedResourceID in localizedResourceIDs)
                    {
                        if (resourceIDs.IndexOf(localizedResourceID) == -1)
                        {
                            resourceIDs.Add(localizedResourceID);
                        }
                    }
                }
            }

            return resourceIDs;
        }


        /// <summary>
        /// Gets all the localized strings which have a resource ID matching the provided one.
        /// </summary>
        /// <param name="resourceID">The resource ID to be searched for.</param>
        /// <returns>The localized strings which have a resource ID matching the provided one.</returns>
        public IReadOnlyList<OrpalisLocalizedString> GetAllLocalizationsForResource(string resourceID)
        {
            List<OrpalisLocalizedString> localizations = new List<OrpalisLocalizedString>();

            foreach (OrpalisLocalizedString entry in _localizedStrings)
            {
                string[] localizedResourceIDs = entry.ResourceIDs.Split(',');

                foreach (string localizedResourceID in localizedResourceIDs)
                {
                    if (localizedResourceID == resourceID)
                    {
                        localizations.Add(entry);
                        break;
                    }
                }
            }

            return localizations;
        }


        /// <summary>
        /// Gets the ISO 639-1 code corresponding to the provided language.
        /// </summary>
        /// <param name="language">The language of interest.</param>
        /// <returns>
        /// The ISO 639-1 code corresponding to the provided language.</returns>
        /// <remarks>
        /// For more information about ISO 639-1, visit https://www.iso.org/iso-639-language-codes.html
        /// </remarks>
        /// <exception cref="ArgumentException">
        /// Thrown when the ISO 639-1 code of the provided language is unknown.
        /// </exception>
        public static string GetISOLanguageName(string language)
        {
            if (ISOLanguages.ContainsKey(language))
            {
                return ISOLanguages[language];
            }

            throw new ArgumentException(language + " ISO 639-1 code is unknown", nameof(language));
        }
    }
}
