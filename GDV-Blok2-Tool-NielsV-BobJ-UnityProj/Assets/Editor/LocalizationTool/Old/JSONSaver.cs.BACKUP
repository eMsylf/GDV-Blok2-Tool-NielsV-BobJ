using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using System.IO;

[Serializable]
public class DoubleDictionaryContainer
{
    public string dataName = "";
    public List<DictionaryContainer> data = new List<DictionaryContainer>();
}

[Serializable]
public class DictionaryContainer
{
    public string dataName = "";
    public string data = "";
}

[Serializable]
public class DataContainer
{
    //public string dataName;
    public List<DoubleDictionaryContainer> availableLanguages = new List<DoubleDictionaryContainer>();
}

public class JSONSaver {

	public static void SaveDictionary(Dictionary<string, Dictionary<string, string>> dict)
    {
        //Serialize dict to list
        DataContainer languages = new DataContainer();

        foreach (string language in dict.Keys.ToList())
        {
            DoubleDictionaryContainer languageData = new DoubleDictionaryContainer
            {
                dataName = language
            };

            Dictionary<string, string> dialogs;
            dict.TryGetValue(language, out dialogs);
            foreach (string dialog in dialogs.Keys.ToList())
            {
                DictionaryContainer dialogData = new DictionaryContainer
                {
                    dataName = dialog,
                    data = dialogs[dialog]
                };
                languageData.data.Add(dialogData);
            }
            languages.availableLanguages.Add(languageData);
        }

        //Convert object to JSON
        string jsonData = JsonUtility.ToJson(languages);

        //Finally write the json string to a file
        string filePath = Application.dataPath + "/Resources/LocalizationData.json";
        File.WriteAllText(filePath, jsonData);

        Debug.Log("Localization Manager: Changes saved to JSON!");
    }
}

