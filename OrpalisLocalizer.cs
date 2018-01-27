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
        //todo urgent: try to add all existing languages into the dictionnary.
        /// <summary>
        /// Maps the different existing languages with their representing ISO 639 code.
        /// </summary>
        /// <remarks>
        /// For more information about ISO 639, visit https://www.iso.org/iso-639-language-codes.html
        /// </remarks>
        private static IReadOnlyDictionary<string, string> ISOLanguages = new Dictionary<string, string>() {
        {"ar", "العربية"},
        {"bg", "български"},
        {"cs", "čeština"},
        {"da", "dansk"},
        {"de", "Deutsch"},
        {"el", "Ελληνικά"},
        {"en", "English"},
        {"es", "español"},
        {"fi", "suomi"},
        {"fr", "Français"},
        {"he", "עברית"},
        {"hi", "हिंदी"},
        {"hu", "magyar"},
        {"id", "Indonesia"},
        {"it", "italiano"},
        {"ja", "日本語"},
        {"ko", "한국어"},
        {"nl", "Nederlands"},
        {"no", "norsk"},
        {"pl", "polski"},
        {"pt", "português"},
        {"ro", "română"},
        {"ru", "русский"},
        {"sk", "slovenčina"},
        {"sl", "slovenščina"},
        {"sv", "svenska"},
        {"th", "ไทย"},
        {"tr", "Türkçe"},
        {"vi", "Tiếng Việt"},
        {"zh", "中文"}
    };

        /// <summary>
        /// Contains the localized strings which have been deserialized.
        /// </summary>
        private List<OrpalisLocalizedString> _localizedStrings;
        /// <summary>
        /// <see cref="DefaultLanguage"/>
        /// </summary>
        private string _defaultLanguage;

        /// <summary>
        /// Specifies the default language which is used by the instance.
        /// </summary>
        public string DefaultLanguage
        {
            get
            {
                return _defaultLanguage;
            }
        }


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
                        throw new ArgumentException("The specified default language is not available into the localized data");
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
        public string GetString(string name, string language)
        {
            foreach (OrpalisLocalizedString localizedString in _localizedStrings)
            {//todo: implement a lookup sorted dictionnary to speed-up the Name retrieval.
                if (localizedString.Name == name)
                {
                    string value = localizedString.LocalizedValue[language];

                    if (!string.IsNullOrEmpty(value))
                    {
                        return value;
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(_defaultLanguage))
                        {
                            return localizedString.LocalizedValue[_defaultLanguage];
                        }
                    }
                }
            }

            throw new ArgumentException("Unknown " + name + " localization key");
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
        /// Gets the ISO 639 code corresponding to the provided language.
        /// </summary>
        /// <param name="language">The language of interest.</param>
        /// <returns>
        /// The ISO 639 code corresponding to the provided language.</returns>
        /// <remarks>
        /// For more information about ISO 639, visit https://www.iso.org/iso-639-language-codes.html
        /// </remarks>
        /// <exception cref="ArgumentException">
        /// Thrown when the ISO 639 code of the provided language is unknown.
        /// </exception>
        public static string GetISOLanguageName(string language)
        {
            if (ISOLanguages.ContainsKey(language))
            {
                return ISOLanguages[language];
            }

            throw new ArgumentException(language + " ISO 639 code is unknown");
        }
    }
}
