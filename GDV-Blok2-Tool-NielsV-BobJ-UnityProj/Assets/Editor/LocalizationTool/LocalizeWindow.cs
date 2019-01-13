using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace LocalizationTool
{
    public class LocalizeWindow : EditorWindow
    {
        

        //string translatedText = "";
        private static int prevNextButtonWidth = 65;
        private static int minWindowWidth = 32;
        private static int minWindowHeight = 18;
        private static int standardButtonHeight = 40;
        private static float standardButtonWidth = 100;
        private static float minTextFieldWidth = 5 * EditorGUIUtility.singleLineHeight;
        private static float maxTextFieldWidth = 100 * EditorGUIUtility.singleLineHeight;
        private static float minTextFieldHeight = 5 * EditorGUIUtility.singleLineHeight;
        private static float maxTextFieldHeight = 100 * EditorGUIUtility.singleLineHeight;

        private static AddLanguageWizard wizard;

        [MenuItem("Tools/Localization")]
        public static void Create()
        {
            LocalizationManager.Init();
            
            GetWindow<LocalizeWindow>("Localization");
            GetWindow<LocalizeWindow>().minSize = new Vector2(minWindowWidth * EditorGUIUtility.singleLineHeight, minWindowHeight * EditorGUIUtility.singleLineHeight);
        }

        public void OnGUI()
        {
            Input.imeCompositionMode = IMECompositionMode.Auto;

            EditorGUILayout.BeginVertical();
            {

                #region Top Bar (Add Language)
                EditorGUILayout.BeginHorizontal("box");
                {


                    GUILayout.FlexibleSpace();

                    if (GUILayout.Button("Add Language", GUILayout.Width(120), GUILayout.Height(standardButtonHeight)))
                    {
                        AddLanguageWizard.Create(this);
                        Debug.Log("Opening new language window");
                    }
                }
                EditorGUILayout.EndHorizontal();

                #endregion

                #region Popup/Dropdown Languages
                EditorGUILayout.BeginHorizontal();
                {
                    Rect popup = new Rect(new Vector2(this.position.size.x - (2 * standardButtonWidth), 3.2f * EditorGUIUtility.singleLineHeight), new Vector2(2 * standardButtonWidth, EditorGUIUtility.singleLineHeight));

                    if (!LocalizationManager.isEmpty)
                    {
                        EditorGUI.BeginChangeCheck();
                        string previousSelected = SelectedLanguage.index.ToString();
                        SelectedLanguage.index = EditorGUI.Popup(popup, SelectedLanguage.index, LocalizationManager.GetAvailableLanguages().ToArray());
                        SelectedLanguage.text = LocalizationManager.GetAvailableLanguages()[SelectedLanguage.index];
                        if (EditorGUI.EndChangeCheck())
                        {
                            LocalizationManager.translatedText = LocalizationManager.GetDialog(previousSelected);
                        }
                    }
                    else
                    {
                        List<string> empty = new List<string>() {
                        "<list is empty>"
                    };
                        EditorGUI.Popup(popup, SelectedLanguage.index, empty.ToArray());
                        SelectedDialog.text = "";
                    }
                }
                EditorGUILayout.EndHorizontal();
                #endregion

                EditorGUILayout.Space(); EditorGUILayout.Space();

                #region TextBoxes (Original Text, Translation)
                EditorGUILayout.BeginHorizontal();
                {
                    EditorGUILayout.BeginVertical();
                    {
                        EditorGUILayout.LabelField("Original text", EditorStyles.boldLabel);
                        EditorGUILayout.BeginHorizontal("box");
                        {
                            GUI.skin.label.wordWrap = true;
                            GUILayout.Label(LocalizationManager.GetDialog(0),
                            //GUIStyle.none,
                            GUILayout.MinHeight(minTextFieldHeight),
                            GUILayout.MaxHeight(maxTextFieldHeight),
                            GUILayout.MinWidth(minTextFieldWidth),
                            GUILayout.MaxWidth(maxTextFieldWidth)
                            );
                        }
                        EditorGUILayout.EndHorizontal();
                    }
                    EditorGUILayout.EndVertical();

                    EditorGUILayout.BeginVertical();
                    {
                        EditorGUILayout.LabelField("Translation", EditorStyles.boldLabel);
                        LocalizationManager.translatedText = EditorGUILayout.TextField(LocalizationManager.translatedText,
                            GUILayout.MinHeight(minTextFieldHeight),
                            GUILayout.MaxHeight(maxTextFieldHeight),
                            GUILayout.MinWidth(minTextFieldWidth),
                            GUILayout.MaxWidth(maxTextFieldWidth)
                            );
                    }
                    EditorGUILayout.EndVertical();
                }
                EditorGUILayout.EndHorizontal();
                #endregion

                GUILayout.FlexibleSpace();

                #region Bottom Bar (Previous, Next, Save, Load)
                EditorGUILayout.BeginHorizontal();
                {
                    if (GUILayout.Button("Previous\ntext", GUILayout.Width(prevNextButtonWidth), GUILayout.Height(standardButtonHeight)))
                    {
                        LocalizationManager.PreviousDialog();
                    }

                    if (GUILayout.Button("Next\ntext", GUILayout.Width(prevNextButtonWidth), GUILayout.Height(standardButtonHeight)))
                    {
                        LocalizationManager.NextDialog();
                    }

                    GUILayout.FlexibleSpace();
                    /*
                    if (GUILayout.Button("Load Text", GUILayout.Height(standardButtonHeight), GUILayout.Width(standardButtonWidth))) {
                        Debug.Log("Loading All Text...");
                        LocalizationManager.LoadText();
                    }
                    */
                    if (GUILayout.Button("Save Changes", GUILayout.Height(standardButtonHeight), GUILayout.Width(standardButtonWidth)))
                    {
                        LocalizationManager.SaveText();
                    }
                }
                EditorGUILayout.EndHorizontal();
                #endregion

            }
            EditorGUILayout.EndVertical();
        }
    }
}
