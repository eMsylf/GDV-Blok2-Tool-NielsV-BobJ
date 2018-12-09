using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LocalizeWindow : EditorWindow {

    public static DataManager data;

    static string selectedLanguage;
    static string selectedDialog;
    int selectPopLanguage = 0;
    int selectPopDialog = 0;
    public static int totalDialog = 3;
	string translatedText = "";
	public static List<string> options = new List<string>();

	public static Dictionary<int, string> localizedDialogs = new Dictionary<int, string>();
	public static Dictionary<int, Dictionary<int, string>> localizedLanguages = new Dictionary<int, Dictionary<int, string>>();

	private static AddLanguageWizard wizard;

	[MenuItem("Tools/Localization")]
	public static void Create(){
        data = Resources.Load("AllText") as DataManager;
        if (data.languages != null)
        {
            selectedLanguage = data.languages.Keys.FirstOrDefault();
            Debug.Log("not empty");

            Dictionary<string, string> outLang;
            if (selectedLanguage != null && data.languages.TryGetValue(selectedLanguage, out outLang))
            {
                selectedDialog = outLang.Keys.FirstOrDefault();
            } else
            {
                selectedDialog = "";
            }
        }
        //Debug.Log(Resources.Load("AllText") as DataManager);
        //selectedLanguage = data.languages.Keys.FirstOrDefault();
        //Dictionary<string, string> outLang;
        //if (data.languages.TryGetValue(selectedLanguage, out outLang))
        //{
        //    selectedDialog = outLang.Keys.FirstOrDefault();
        //}
     
        GetWindow<LocalizeWindow> ();
		GetWindow<LocalizeWindow> ().minSize = new Vector2 ( 12 * EditorGUIUtility.singleLineHeight, 18 * EditorGUIUtility.singleLineHeight );
	}

	void OnGUI(){

        //#region ObjectField
        //Object dataObj = null;
        //dataObj = EditorGUILayout.ObjectField(dataObj, typeof(DataManager), false);
        //data = dataObj as DataManager;
        //#endregion

        #region Buttons
        EditorGUILayout.BeginHorizontal();
        Dictionary<string, string> outLang;
        List<string> popDialog = null;
        if (selectedLanguage != null && data.languages.TryGetValue(selectedLanguage, out outLang))
        {
            //selectedDialog = outLang.Keys.FirstOrDefault();
            popDialog = outLang.Keys.ToList();
        }
        //List<string> popLang = data.languages.Keys.ToList();
        if (GUILayout.Button("Previous"))
        {
            Debug.Log("Previous Text");
            selectedDialog = popDialog[(selectPopDialog - 1 + totalDialog - 2) % (totalDialog - 2)];
            Debug.Log(selectedDialog);
        }
        if (GUILayout.Button("Next"))
        {
            Debug.Log("Next Text");
            selectedDialog = popDialog[(selectPopDialog + 1) % (totalDialog - 2)];
        }
        if (GUILayout.Button("Add Lang"))
        {
            Debug.Log("Added Language");
            wizard = AddLanguageWizard.CreateInstance<AddLanguageWizard>();
            wizard.Show();
        }

        EditorGUILayout.EndHorizontal();

        GUILayout.FlexibleSpace();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Load Text"))
        {
            Debug.Log("Loading All Text...");
        }
        if (GUILayout.Button("Save Changes"))
        {
            Debug.Log("Saving Changes");
            //SaveTextDebug(translatedText);
        }
        EditorGUILayout.EndHorizontal();
        #endregion

        #region Popup
        Rect popup = new Rect(new Vector2(0, 2 * EditorGUIUtility.singleLineHeight), new Vector2(this.position.width, EditorGUIUtility.singleLineHeight));

        if (localizedLanguages.Count > 0)
        {
            List<string> popLang = data.languages.Keys.ToList();
            selectedLanguage = popLang[EditorGUI.Popup(popup, selectPopLanguage, options.ToArray())];
        }
        #endregion

        #region OriginalText
        GUILayout.BeginArea(new Rect(0, 3 * EditorGUIUtility.singleLineHeight, this.position.width, EditorGUIUtility.singleLineHeight * 6));
        EditorGUILayout.BeginHorizontal();
        GUILayout.TextArea(GetTextDebug(data.languages.Keys.FirstOrDefault(), selectedDialog), GUILayout.Height(position.height - 30));
        EditorGUILayout.EndHorizontal();
        GUILayout.EndArea();
        #endregion

        #region LocalizedText
        GUILayout.BeginArea(new Rect(0, 10 * EditorGUIUtility.singleLineHeight, this.position.width, EditorGUIUtility.singleLineHeight * 6));
        EditorGUILayout.BeginHorizontal();
        translatedText = GUILayout.TextArea(translatedText, GUILayout.Height(position.height - 30));
        EditorGUILayout.EndHorizontal();
        GUILayout.EndArea();
        #endregion
    }

    //public void SaveTextDebug(string text)
    //{
    //    Dictionary<int, string> localDialog;
    //    if (localizedLanguages.TryGetValue(selectedLanguage, out localDialog))
    //    {
    //        string dialog;
    //        if (localDialog.TryGetValue(selectedDialog, out dialog))
    //        {
    //            dialog = text;
    //        }
    //        else
    //        {
    //            Debug.LogError("Developer Speaking, This is not part of the game. Please contact the developer. Error: This dialog does not exist.");
    //        }
    //    }
    //    else
    //    {
    //        Debug.LogError("Developer Speaking, This is not part of the game. Please contact the developer. Error: This language does not exist.");
    //    }
    //}

    //public string GetTextDebug(int selectedLanguage, int selectedDialog)
    //{
    //    Dictionary<int, string> localDialog;
    //    if (localizedLanguages.TryGetValue(selectedLanguage, out localDialog))
    //    {
    //        string dialog;
    //        if (localDialog.TryGetValue(selectedDialog, out dialog))
    //        {
    //            return dialog;
    //        }
    //        else
    //        {
    //            Debug.LogError("Developer Speaking, This is not part of the game. Please contact the developer. Error: This dialog does not exist.");
    //        }
    //    }
    //    else
    //    {
    //        if (localizedLanguages.Count == 0)
    //        {
    //            Debug.LogWarning("There is no language present, please click \"Add Lang\" to add a languages to the game.");
    //        }
    //        else
    //        {
    //            Debug.LogError("Developer Speaking, This is not part of the game. Please contact the developer. Error: This language does not exist.");
    //        }
    //    }
    //    return null;
    //}

    public static string GetTextDebug(string selectedLanguage, string selectedDialog)
    {
        Dictionary<string, string> outLang = null;
        if (selectedLanguage != null && data.languages.TryGetValue(selectedLanguage, out outLang))
        {
            string outDialog;
            if (outLang.TryGetValue(selectedDialog, out outDialog))
            {
                return outDialog;
            }
        }
        return null;
    }

    //public string GetTextDebug(int selectedLanguage, string selectedDialog)
    //{
    //    Dictionary<string, string> outLang = null;
    //    data.languages.FirstOrDefault();
    //    if (data.languages.TryGetValue(selectedLanguage, out outLang))
    //    {
    //        string outDialog;
    //        if (data.dialogs.TryGetValue(selectedDialog, out outDialog))
    //        {
    //            return outDialog;
    //        }
    //    }
    //    return null;
    //}
}

