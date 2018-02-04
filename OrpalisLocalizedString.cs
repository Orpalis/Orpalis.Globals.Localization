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

using System.Collections.Generic;


namespace Orpalis.Globals.Localization
{
    /// <summary>
    /// Represents a localized string, which can be manipulated by a OrpalisLocalizer instance.
    /// </summary>
    public sealed class OrpalisLocalizedString
    {
        /// <summary>
        /// Specifies the name of the localized string.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Specifies the different identifiers, separated by a comma, of the localized string.
        /// </summary>
        public string ResourceIDs { get; set; }

        /// <summary>
        /// Maps the different values of the localized string with a language.
        /// </summary>
        public readonly Dictionary<string, string> LocalizedValue = new Dictionary<string, string>();
    }
}
