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

            Debug.Log("JSONSaver: Changes saved to JSON!");
        }

        public static List<BaseDialog> LoadDictionary(SystemOptions options)
        {
            //DataContainer languages = new DataContainer();

            //foreach (BaseDialogWrapper language in dialogsList[0].dialogWrapper)
            //{
            //    DoubleDictionaryContainer languageData = new DoubleDictionaryContainer
            //    {
            //        Language = language.language.LanguageName
            //    };


            //    foreach (BaseDialog dialog in dialogsList)
            //    {
            //        DictionaryContainer dialogData = new DictionaryContainer
            //        {
            //            DialogName = dialog.name,
            //            DialogContent = dialog.dialogWrapper.Find(l => l.language.LanguageName == language.language.LanguageName).content
            //        };
            //        languageData.Dialogs.Add(dialogData);
            //    }
            //    languages.AvailableLanguages.Add(languageData);
            //}





            //Finally write the json string to a file
            string filePath = Application.dataPath + "/Resources/Localization/LocalizationData.json";
            string jsonData = File.ReadAllText(filePath);
            //Convert object to JSON
            DataContainer languages = JsonUtility.FromJson<DataContainer>(jsonData);

            List<BaseDialog> dialogsList = new List<BaseDialog>();

            //foreach(DictionaryContainer dia in languages.AvailableLanguages[0].Dialogs)
            //{
            //    BaseDialog dialog = new BaseDialog();
            //    foreach (DoubleDictionaryContainer lang in languages.AvailableLanguages)
            //    {
            //        BaseDialogWrapper wrapper = new BaseDialogWrapper();
            //        wrapper.language.LanguageName = lang.Language;
            //        wrapper.content = lang.Dialogs[0].DialogContent;
            //    }
            //    dialogsList.Add(dialog);
            //}

            for (int i = 0; i < languages.AvailableLanguages[0].Dialogs.Count; i++)
            {
                BaseDialog dialog = (BaseDialog)ScriptableObject.CreateInstance("BaseDialog");
                dialog.Init(options);
                //Debug.LogWarning("Options is: " + dialog.gameOptions);
                dialog.name = languages.AvailableLanguages[0].Dialogs[i].DialogName;
                dialog.dialogWrapper = new List<BaseDialogWrapper>();
                foreach (DoubleDictionaryContainer lang in languages.AvailableLanguages)
                {
                    BaseDialogWrapper wrapper = new BaseDialogWrapper();
                    wrapper.language = (Language)ScriptableObject.CreateInstance("Language");
                    wrapper.language.name = "Language_" + lang.Language;
                    wrapper.language.LanguageName = lang.Language;
                    wrapper.content = lang.Dialogs[i].DialogContent;
                    dialog.dialogWrapper.Add(wrapper);
                    Debug.LogWarning("Languages is: " + wrapper.language);
                    Debug.LogWarning("Dialog is: " + wrapper.content);
                }
                dialogsList.Add(dialog);
            }
            Debug.LogError("Languages is: " + dialogsList[0].dialogWrapper[0].language);
            Debug.LogError("Dialog is: " + dialogsList[0].dialogWrapper[0].content);
            Debug.LogError(dialogsList[0].Content());
            Debug.Log("JSONSaver: Text loaded from JSON!");

            return dialogsList;
        }
    }
}

