using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

namespace LocalizationTool
{

    [Serializable]
    public class DoubleDictionaryContainer
    {
        public string Language = "";
        public List<DictionaryContainer> Dialogs = new List<DictionaryContainer>();
    }

    [Serializable]
    public class DictionaryContainer
    {
        public string DialogName = "";
        public string DialogContent = "";
    }

    [Serializable]
    public class DataContainer
    {
        public List<DoubleDictionaryContainer> AvailableLanguages = new List<DoubleDictionaryContainer>();
    }

    public class JSONSaver
    {

        public static void SaveDictionary(List<BaseDialog> dialogsList)
        {
            DataContainer languages = new DataContainer();
            
            foreach (BaseDialogWrapper language in dialogsList[0].dialogWrapper)
            {
                DoubleDictionaryContainer languageData = new DoubleDictionaryContainer
                {
                    Language = language.language.LanguageName
                };
                

                foreach (BaseDialog dialog in dialogsList)
                {
                    DictionaryContainer dialogData = new DictionaryContainer
                    {
                        DialogName = dialog.name,
                        DialogContent = dialog.dialogWrapper.Find(l => l.language.LanguageName == language.language.LanguageName).content
                    };
                    languageData.Dialogs.Add(dialogData);
                }
                languages.AvailableLanguages.Add(languageData);
            }



            //Convert object to JSON
            string jsonData = JsonUtility.ToJson(languages);

            //Finally write the json string to a file
            string filePath = Application.dataPath + "/Resources/Localization/LocalizationData.json";
            File.WriteAllText(filePath, jsonData);

            Debug.Log("Localization Manager: Changes saved to JSON!");
        }
    }
}

