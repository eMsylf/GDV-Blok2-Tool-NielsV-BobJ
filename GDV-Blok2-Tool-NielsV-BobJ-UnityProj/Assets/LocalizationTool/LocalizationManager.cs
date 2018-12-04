using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationManager : ScriptableObject {

    public static DataManager data;

    public static string GetDialog(string selectedLang, string selectedDialog)
    {
        Dictionary<string, string> outLang = null;
        if (data.languages.TryGetValue(selectedLang, out outLang)){
            string outDialog;
           if (outLang.TryGetValue(selectedDialog, out outDialog)){
                return outDialog;
            }
        }
        return null;
    }

    public static void SaveLocalization()
    {
        //write to file?
    }
}
