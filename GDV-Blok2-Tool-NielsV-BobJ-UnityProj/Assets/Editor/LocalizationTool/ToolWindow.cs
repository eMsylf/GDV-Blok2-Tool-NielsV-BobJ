using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LocalizeWindow : EditorWindow {

    public static DataManager data;

    private List<string> popDialog = null;
    private Dictionary<string, string> outLang;

    static string selectedLanguage;
    static string selectedDialog;
    int selectedPopupLanguage = 0;
    int selectedPopupDialog = 0;
    public static int totalDialog = 3;
    string translatedText = "";
    public static List<string> options = new List<string>();
    private static int prevNextButtonWidth = 65;
    private static int minWindowWidth = 32;
    private static int minWindowHeight = 18;
    private static int standardButtonHeight = 40;
    private static float standardButtonWidth = 100;
    private static float minTextFieldWidth = 5 * EditorGUIUtility.singleLineHeight;
    private static float maxTextFieldWidth = 100 * EditorGUIUtility.singleLineHeight;
    private static float minTextFieldHeight = 5 * EditorGUIUtility.singleLineHeight;
    private static float maxTextFieldHeight = 100 * EditorGUIUtility.singleLineHeight;


    //public static Dictionary<int, string> localizedDialogs = new Dictionary<int, string>();
    //public static Dictionary<int, Dictionary<int, string>> localizedLanguages = new Dictionary<int, Dictionary<int, string>>();

    private static AddLanguageWizard wizard;

    [MenuItem("Tools/Localization")]
    public static void Create() {
        data = Resources.Load("AllText") as DataManager;
        if (data.languages != null) {
            selectedLanguage = data.languages.Keys.FirstOrDefault();
            Debug.Log("not empty");

            Dictionary<string, string> outLang;
            if (selectedLanguage != null && data.languages.TryGetValue(selectedLanguage, out outLang)) {
                selectedDialog = outLang.Keys.FirstOrDefault();
            } else {
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

        GetWindow<LocalizeWindow>("Localization");
        GetWindow<LocalizeWindow>().minSize = new Vector2(minWindowWidth * EditorGUIUtility.singleLineHeight, minWindowHeight * EditorGUIUtility.singleLineHeight);
    }

    public void OnGUI() {
        EditorGUILayout.BeginVertical(); {

            //#region ObjectField
            //Object dataObj = null;
            //dataObj = EditorGUILayout.ObjectField(dataObj, typeof(DataManager), false);
            //data = dataObj as DataManager;
            //#endregion

            #region Top Bar (Add Language)
            EditorGUILayout.BeginHorizontal("box"); {
                

                GUILayout.FlexibleSpace();

                if (GUILayout.Button("Add Language", GUILayout.Width(120), GUILayout.Height(standardButtonHeight))) {
                    Debug.Log("Added Language");
                    //wizard = AddLanguageWizard.CreateInstance<AddLanguageWizard>();
                    AddLanguageWizard.Create(this);
                    //wizard.titleContent = GUIContent;
                    //wizard.Show();
                    Debug.Log("Opening new language window");
                    //wizard = AddLanguageWizard.CreateInstance<AddLanguageWizard>();
                    //AddLanguageWizard.Create("Add Language");
                }
            } EditorGUILayout.EndHorizontal();

            #endregion

            #region Popup/Dropdown Languages
            EditorGUILayout.BeginHorizontal(); {
                Rect popup = new Rect(new Vector2(this.position.size.x - (2 * standardButtonWidth), 3.2f * EditorGUIUtility.singleLineHeight), new Vector2(2 * standardButtonWidth, EditorGUIUtility.singleLineHeight));

                if (data.languages.Keys.Count > 0) {
                    List<string> options = new List<string>(data.languages.Keys);
                    List<string> popupLang = data.languages.Keys.ToList();
                    selectedPopupLanguage = EditorGUI.Popup(popup, selectedPopupLanguage, options.ToArray());
                    selectedLanguage = popupLang[selectedPopupLanguage];
                } else {
                    List<string> leeg = new List<string>() {
                        "<list is empty>"
                    };
                    EditorGUI.Popup(popup, selectedPopupLanguage, leeg.ToArray());
                    selectedDialog = "";
                }
            } EditorGUILayout.EndHorizontal();
            #endregion

            EditorGUILayout.Space(); EditorGUILayout.Space();

            #region TextBoxes (Original Text, Translation)
            EditorGUILayout.BeginHorizontal(); {
                EditorGUILayout.BeginVertical(); {
                    EditorGUILayout.LabelField("Original text", EditorStyles.boldLabel);
                    EditorGUILayout.BeginHorizontal("box"); { 
                        GUI.skin.label.wordWrap = true;
                        GUILayout.Label("Helo I am Beb and I am a Game Developer right now I'm trying to build a cool looking tool that is kinda easy to use but Unity is being a doodoo and I do not like it.",
                        //GUIStyle.none,
                        GUILayout.MinHeight(minTextFieldHeight),
                        GUILayout.MaxHeight(maxTextFieldHeight),
                        GUILayout.MinWidth(minTextFieldWidth),
                        GUILayout.MaxWidth(maxTextFieldWidth)
                        );
                    }EditorGUILayout.EndHorizontal();
                } EditorGUILayout.EndVertical();

                EditorGUILayout.BeginVertical(); {
                    EditorGUILayout.LabelField("Translation", EditorStyles.boldLabel);
                    translatedText = GUILayout.TextArea(translatedText,
                        GUILayout.MinHeight(minTextFieldHeight),
                        GUILayout.MaxHeight(maxTextFieldHeight),
                        GUILayout.MinWidth(minTextFieldWidth),
                        GUILayout.MaxWidth(maxTextFieldWidth)
                        );
                } EditorGUILayout.EndVertical();
            } EditorGUILayout.EndHorizontal();
            #endregion

            GUILayout.FlexibleSpace();

            #region Bottom Bar (Previous, Next, Save, Load)
            EditorGUILayout.BeginHorizontal(); {
                if (GUILayout.Button("Next\ntext", GUILayout.Width(prevNextButtonWidth), GUILayout.Height(standardButtonHeight))) {
                    //Debug.Log("Next Text");
                    LocalizationManager.NextDialog();
                    //selectedDialog = popDialog[(selectPopDialog + 1) % (totalDialog - 2)];
                }
                if (selectedLanguage != null && data.languages.TryGetValue(selectedLanguage, out outLang)) {
                    //selectedDialog = outLang.Keys.FirstOrDefault();
                    popDialog = outLang.Keys.ToList();
                }
                //List<string> popLang = data.languages.Keys.ToList();
                if (GUILayout.Button("Previous\ntext", GUILayout.Width(prevNextButtonWidth), GUILayout.Height(standardButtonHeight))) {
                    // Debug.Log("Previous Text");
                    LocalizationManager.PreviousDialog();
                    //selectedDialog = popDialog[(selectPopDialog - 1 + totalDialog - 2) % (totalDialog - 2)];
                    //Debug.Log(selectedDialog);
                }
                GUILayout.FlexibleSpace();
                /*
                if (GUILayout.Button("Load Text", GUILayout.Height(standardButtonHeight), GUILayout.Width(standardButtonWidth))) {
                    Debug.Log("Loading All Text...");
                }
                */
                if (GUILayout.Button("Save Changes", GUILayout.Height(standardButtonHeight), GUILayout.Width(standardButtonWidth))) {
                    Debug.Log("Saving Changes");
                    //SaveTextDebug(translatedText);
                }
            } EditorGUILayout.EndHorizontal();
            #endregion

        } EditorGUILayout.EndVertical();
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

    //public static string GetTextDebug(string selectedLanguage, string selectedDialog)
    //{
    //    Dictionary<string, string> outLang = null;
    //    if (selectedLanguage != null && data.languages.TryGetValue(selectedLanguage, out outLang))
    //    {
    //        string outDialog;
    //        if (outLang.TryGetValue(selectedDialog, out outDialog))
    //        {
    //            return outDialog;
    //        }
    //    }
    //    return null;
    //}

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
