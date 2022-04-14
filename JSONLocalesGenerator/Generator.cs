using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Data;
using System.Dynamic;
using Newtonsoft.Json;
using ExcelDataReader;

namespace JSONLocalesGenerator
{
    internal static class Generator
    {
        public static void Generate(string inputFileName, string destinationFolder)
        {
            if (!destinationFolder.EndsWith("\\"))
            {
                destinationFolder += "\\";
            }
            string outputFileName = destinationFolder + Path.GetFileNameWithoutExtension(inputFileName) + ".json";

            using (FileStream stream = File.Open(inputFileName, FileMode.Open, FileAccess.Read))
            {
                using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var worksheet = reader.AsDataSet();
                    List<string> langCodes = GetLanguages(reader, worksheet);
                    List<string> stringKeys = GetStringKeys(reader, worksheet);
                    List<string> resourceIDs = GetStringResourceIDs(reader, worksheet);
                    Dictionary<string, List<string>> translations = GetTranslations(worksheet, langCodes);
                    File.WriteAllText(outputFileName, GetLocalizationJson(stringKeys, resourceIDs, langCodes, translations));
                }
            }
        }


        private static string GetLocalizationJson(List<string> stringKeys, List<string> resourceIDs, List<string> languageCodes, Dictionary<string, List<string>> translations)
        {
            List<object> localization = new List<object>();
            foreach (string key in stringKeys)
            {
                dynamic item = new ExpandoObject();
                ((IDictionary<String, object>)item)["Name"] = key;
                ((IDictionary<String, object>)item)["ResourceIDs"] = resourceIDs.ElementAt(stringKeys.FindIndex(a => a == key));
                dynamic translatedValues = new ExpandoObject();
                foreach (string lang in languageCodes)
                {
                    ((IDictionary<String, object>)translatedValues)[lang] = translations[lang].ElementAt(stringKeys.FindIndex(a => a == key));
                }
                ((IDictionary<String, object>)item)["LocalizedValue"] = translatedValues;
                localization.Add(item);
            }
            return JsonConvert.SerializeObject(localization, Formatting.Indented);
        }

        private static List<string> GetLanguages(IExcelDataReader reader, DataSet worksheet)
        {
            List<string> languageCodes = new List<string>();
            var languageCount = reader.FieldCount - 2;
            for (int i = 1; i <= languageCount; i++)
            {
                var lang = worksheet.Tables[0].Rows[0].ItemArray[i + 1];
                languageCodes.Add(lang.ToString());
            }
            return languageCodes;
        }

        private static List<string> GetStringKeys(IExcelDataReader reader, DataSet worksheet)
        {
            var keyCount = worksheet.Tables[0].Rows.Count - 1;
            List<string> keys = new List<string>();
            for (int i = 1; i <= keyCount; i++)
            {
                var key = worksheet.Tables[0].Rows[i].ItemArray[0];
                keys.Add(key.ToString());
            }
            return keys;
        }

        private static List<string> GetStringResourceIDs(IExcelDataReader reader, DataSet worksheet)
        {
            var ResourceIDsCount = worksheet.Tables[0].Rows.Count - 1;
            List<string> resourceIDs = new List<string>();
            for (int i = 1; i <= ResourceIDsCount; i++)
            {
                var resourceID = worksheet.Tables[0].Rows[i].ItemArray[1];
                resourceIDs.Add(resourceID.ToString());
            }
            return resourceIDs;
        }

        private static Dictionary<string, List<string>> GetTranslations(DataSet worksheet, List<string> languageCodes)
        {
            Dictionary<string, List<string>> translations = new Dictionary<string, List<string>>();
            foreach (var language in languageCodes)
            {
                List<string> translation = new List<string>();
                int index = languageCodes.FindIndex(a => a == language) + 2;
                for (int i = 1; i < worksheet.Tables[0].Rows.Count; i++)
                {
                    translation.Add(worksheet.Tables[0].Rows[i].ItemArray[index].ToString().Replace("\\n", "\n"));
                }
                translations.Add(language, translation);
            }
            return translations;
        }
    }
}
