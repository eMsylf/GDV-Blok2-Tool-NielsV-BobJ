using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SelectedLanguage
{
    public static int index;
    public static string text;
}

public class SelectedDialog
{
    public static int index;
    public static string text;
}

public class LocalizationManager : ScriptableObject
{
    public static LocalizationManager manager = new LocalizationManager();

    //JSOn laden vana t begin en alleen als je afsluit eruit laden?????????
    //Scriptable object kun je in editor aanpassen, tijdens runtime kun je het niet aanpassen en dient het alleen als data container.
    //Scriptable object houd dus niet de geselecteerde taal bij.

    public static DataManager data;
    //static string selectedLanguage;
    //static string selectedDialog;
    //static int selectedPopLanguage = 0;
    //static int selectedPopDialog = 0;
    public static int totalDialog = 0;
    public static string translatedText = "";
    public static List<string> languageOptions = new List<string>();
    public static List<string> dialogOptions = new List<string>();

    //public LocalizationManager()
    public static void Init()
    {
        //get scriptable object

        //DataManager data = (DataManager)EditorGUIUtility.Load("AllText");
        data = (DataManager)EditorGUIUtility.Load("Assets/Resources/AllText.asset");
        //data = Resources.Load("AllText") as DataManager;
        if (data.languages != null)
        {
            Debug.Log("Localization Manager: Text Object Found.");
            SelectedLanguage.text = data.languages.Keys.FirstOrDefault();
            //SelectedDialog.text = data.languages[SelectedLanguage.text].Keys.FirstOrDefault();

            SelectedLanguage.index = 0;
            //SelectedDialog.index = 0;

            Dictionary<string, string> outLang;
            if (SelectedLanguage.text != null && data.languages.TryGetValue(SelectedLanguage.text, out outLang))
            {
                SelectedDialog.text = outLang.Keys.FirstOrDefault();
            }
            else
            {
                SelectedDialog.text = "";
            }
        } else
        {
            Debug.LogError("Localization Manager: No text object was found. Please place a DataManager object in the Resources folder!");
        }
    }

    ~LocalizationManager()
    {
        //unload JSON
    }

    /// <summary>
    /// Loads the selected dialog and returns the text from it.
    /// </summary>
    /// <returns></returns>
    public static string GetDialog()
    {
        Dictionary<string, string> availableDialogs;
        string dialogText;

        if (data.languages.TryGetValue(SelectedLanguage.text, out availableDialogs))
        {
            if (availableDialogs.TryGetValue(SelectedDialog.text, out dialogText))
            {
                return dialogText;
            }
        }
        return "";
    }

    /// <summary>
    /// Loads the selected dialog and returns the text from it.
    /// </summary>
    /// <returns></returns>
    public static string GetDialog(int index)
    {
        Dictionary<string, string> availableDialogs;
        string dialogText;
        string selectLanguage = null;
        string selectDialog = null;

        if (languageOptions.Count > 0 && dialogOptions.Count > 0)
        {
            selectLanguage = languageOptions[index];
            selectDialog = dialogOptions[SelectedDialog.index];

            if (data.languages.TryGetValue(selectLanguage, out availableDialogs) && SelectedDialog.text != null)
            {
                if (availableDialogs.TryGetValue(selectDialog, out dialogText))
                {
                    return dialogText;
                }
            }
        }
        return "";
    }

    /// <summary>
    /// Saves the given text input to the selected dialog. Returns true if succesful and false otherwise.
    /// </summary>
    /// <returns></returns>
    public static bool SetDialog()
    {
        Dictionary<string, string> selectedLanguageData;
        if (data.languages.TryGetValue(SelectedLanguage.text, out selectedLanguageData))
        {
            string dialogText;
            if (selectedLanguageData.TryGetValue(SelectedDialog.text, out dialogText))
            {
                selectedLanguageData[SelectedDialog.text] = translatedText;
                //dialogText = translatedText;
                return true;
            } else
            {
                string dialogName = "Dialog-" + totalDialog;
                AddDialog(dialogName, translatedText);
                return true;
            }
        }
        //Debug.Log("Localization Manager: Added new language: " + languageName);
        return false;
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

    public static bool isEmpty
    {
        get {
            if (data.languages == null || data.languages.Count == 0) {
                //Debug.LogWarning("Language Manager: No languages added yet. Please click the \"Add Language\" button to add a language to your game.");
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="inLang"></param>
    /// <returns></returns>
    public static List<string> GetLanguagesDialogs(string inLang)
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
    /// Returns a list with the available languages
    /// </summary>
    /// <returns></returns>
    public static List<string> GetAvailableLanguages()
    {
        //Dictionary<string, string> outLang;
        //List<string> popDialog = null;
        if (data.languages != null)
        {
            //selectedDialog = outLang.Keys.FirstOrDefault();
            //popDialog = outLang.Keys.ToList();
            return data.languages.Keys.ToList();
        }
        return null;
    }

    /// <summary>
    /// Load the next text dialog. If the current selected dialog is the last element, the first text dialog will be selected.
    /// </summary>
    public static void NextDialog()
    {
        //popDialog = GetLanguagesDialogs(selectedLanguage);
        //selectedDialog = popDialog[(selectedPopDialog + 1) % (totalDialog - 2)];
        //Debug.Log("Localization Manager: Next Dialog Selected: ");
        SetDialog();
        dialogOptions = GetLanguagesDialogs(SelectedLanguage.text);
        if (dialogOptions.Count > 0)
        {
            SelectedDialog.index += 1;
            if ((SelectedDialog.index) > totalDialog - 1)
            {
                AddDialog("Dialog-" + totalDialog, "");
            } else
            {
                SelectedDialog.text = dialogOptions[SelectedDialog.index];
            }

            translatedText = GetDialog();
            //SelectedDialog.text = dialogOptions[(SelectedDialog.index + 1) % (totalDialog - 2)];
            //SelectedDialog.text = dialogOptions[SelectedDialog.index];
        }
        else
        {
            //AddDialog("Dialog-" + totalDialog, "");
            SelectedDialog.text = null;
        }
        Debug.Log("Localization Manager: Next Dialog Selected: " + SelectedDialog.text);
    }

    /// <summary>
    /// Load the previous text dialog. If it is the first element, it will load the last text dialog.
    /// </summary>
    public static void PreviousDialog()
    {
        SetDialog();
        dialogOptions = GetLanguagesDialogs(SelectedLanguage.text);
        if (dialogOptions.Count > 0)
        {
            if (SelectedDialog.index > 0)
            {
                SelectedDialog.index -= 1;
                SelectedDialog.text = dialogOptions[SelectedDialog.index];// - 1) + totalDialog - 2) % (totalDialog - 2)];
            }
            else
            {
                SelectedDialog.index = 0;
                SelectedDialog.text = dialogOptions[SelectedDialog.index];// - 1) + totalDialog - 2) % (totalDialog - 2)];
            }
            translatedText = GetDialog();
        }
        Debug.Log("Localization Manager: Previous Dialog Selected: " + SelectedDialog.text);
    }

    /// <summary>
    /// Adding a language with the given string parameter as name.
    /// </summary>
    /// <param name="languageName"></param>
    public static void AddLang(string languageName)
    {
        if (languageOptions.Count > 0)
        {
            data.languages.Add(languageName, new Dictionary<string, string>(data.languages[languageOptions[0]])); //TODO: maybe empty dialogText strings afterwards?
        } else
        {
            data.languages.Add(languageName, new Dictionary<string, string>()); //TODO: maybe empty dialogText strings afterwards?
        }
        languageOptions.Add(languageName);
        SelectedLanguage.text = languageName;
        SelectedLanguage.index = languageOptions.Count - 1;
        Debug.Log("Localization Manager: Added new language: " + languageName);
    }

    /// <summary>
    /// Adding a language with the given string parameter as name.
    /// </summary>
    /// <param name="languageName"></param>
    public static void AddDialog(string dialogName, string dialogText)
    {
        if (translatedText != "") { 
            Dictionary<string, string> selectedLanguageData;
            if (data.languages.TryGetValue(SelectedLanguage.text, out selectedLanguageData))
            {
                for (int i = 0; i < data.languages.Count; i++)
                {
                    if (languageOptions[i] == SelectedLanguage.text)
                    {
                        selectedLanguageData.Add(dialogName, dialogText);
                    }
                    else
                    {
                        data.languages[languageOptions[i]].Add(dialogName, "");
                    }
                }

                dialogOptions.Add(dialogName);
                SelectedDialog.text = dialogName;
                SelectedDialog.index = dialogOptions.Count - 1;
                totalDialog += 1;

                Debug.Log("Localization Manager: Added new dialog: " + dialogName + "-" + SelectedDialog.index);
            }
        } else
        {
            SelectedDialog.index -= 1;
        }
    }

    /// <summary>
    /// Loading the text from a JSON file.
    /// </summary>
    public static void LoadText()
    {

    }

    /// <summary>
    /// Loading the text from a JSON file.
    /// </summary>
    public static void SaveText()
    {

        //First save in Scriptable Object
        EditorUtility.SetDirty(data);
        EditorUtility.SetDirty(manager);
        AssetDatabase.SaveAssets();
        Debug.Log("Localization Manager: Trying to save changes");
        //Then save in JSON
        JSONSaver.SaveDictionary(data.languages);
    }
}
