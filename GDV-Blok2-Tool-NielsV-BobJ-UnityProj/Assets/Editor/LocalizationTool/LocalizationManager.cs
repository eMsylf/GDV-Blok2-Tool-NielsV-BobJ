using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LocalizationManager : MonoBehaviour {


    //JSOn laden vana t begin en alleen als je afsluit eruit laden?????????
    //Scriptable object kun je in editor aanpassen, tijdens runtime kun je het niet aanpassen en dient het alleen als data container.
    //Scriptable object houd dus niet de geselecteerde taal bij.
    
    public static DataManager data;
    static string selectedLanguage;
    static string selectedDialog;
    static int selectedPopLanguage = 0;
    static int selectedPopDialog = 0;
    public static int totalDialog = 3;
    string translatedText = "";
    public static List<string> options = new List<string>();
    public static List<string> popDialog = new List<string>();

    /// <summary>
    /// Loads the selected dialog and returns the text from it.
    /// </summary>
    /// <returns></returns>
    public static string GetDialog()
    {
        return "";
    }

    /// <summary>
    /// Saves the given text input to the selected dialog. Returns true if succesful and false otherwise.
    /// </summary>
    /// <returns></returns>
    public static bool SetDialog()
    {
        return true;
    }

    //public static void SetLanguage()
    //{

    //}

    /// <summary>
    /// Copies all dialogs from the main language to given language.
    /// </summary>
    public static void SetAll()
    {
        //Get language and dialog if there are any, otherwise make new one
        //selectedLanguage = data.languages.Keys.FirstOrDefault();
        //Debug.Log("not empty");

        //Dictionary<string, string> outLang;
        //if (selectedLanguage != null && data.languages.TryGetValue(selectedLanguage, out outLang))
        //{
        //    selectedDialog = outLang.Keys.FirstOrDefault();
        //}
        //else
        //{
        //    selectedDialog = "";
        //}
    }

    public static List<string> GetLanguage(string inLang)
    {
        Dictionary<string, string> outLang;
        //List<string> popDialog = null;
        if (inLang != null && data.languages.TryGetValue(inLang, out outLang))
        {
            //selectedDialog = outLang.Keys.FirstOrDefault();
            //popDialog = outLang.Keys.ToList();
            return outLang.Keys.ToList();
        }
        return null;
    }

    /// <summary>
    /// Load the next text dialog. If the current selected dialog is the last element, the first text dialog will be selected.
    /// </summary>
    public static void NextDialog()
    {
        popDialog = GetLanguage(selectedLanguage);
        selectedDialog = popDialog[(selectedPopDialog + 1) % (totalDialog - 2)];
        Debug.Log("LanguageManager - Next Dialog Selected: ");
    }

    /// <summary>
    /// Load the previous text dialog. If it is the first element, it will load the last text dialog.
    /// </summary>
    public static void PreviousDialog()
    {
        popDialog = GetLanguage(selectedLanguage);
        selectedDialog = popDialog[(selectedPopDialog - 1 + totalDialog - 2) % (totalDialog - 2)];
        Debug.Log("LanguageManager - Previous Dialog Selected: ");
    }

    /// <summary>
    /// Adding a language with the given string parameter as name.
    /// </summary>
    /// <param name="languageName"></param>
    public static void AddLang(string languageName)
    {
        LocalizeWindow.data.languages.Add(languageName, new Dictionary<string, string>());
        LocalizeWindow.options.Add(languageName);
        Debug.Log("LanguageManager - Added new language: " + languageName);
    }

    /// <summary>
    /// Loading the text from a JSON file.
    /// </summary>
    public static void LoadText()
    {

    }
}
