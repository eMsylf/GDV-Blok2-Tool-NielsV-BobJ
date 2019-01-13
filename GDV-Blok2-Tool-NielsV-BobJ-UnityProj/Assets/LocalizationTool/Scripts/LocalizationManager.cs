using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace LocalizationTool
{

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

    public class LocalizationManager
    { 

        public static List<BaseDialog> dialogsList;
        public static List<Language> languagesList;

        private static Language selectedLanguage;
        private static BaseDialog selectedDialog;

        public static string languagesPath = "Assets/LocalizationTool/Languages";
        public static string dialogsPath = "Assets/LocalizationTool/Dialogs";
        
        //public static int totalDialog = 0;
        public static string translatedText = "";
        //public static List<string> languageOptions = new List<string>();
        //public static List<string> dialogOptions = new List<string>();

        public static void Init()
        {
            SelectedDialog.index = 0;
            SelectedDialog.text = "";
            SelectedLanguage.index = 0;
            SelectedLanguage.text = "";
            dialogsList = new List<BaseDialog>();
            languagesList = new List<Language>();
            string[] assetsGUIDs = AssetDatabase.FindAssets("t:BaseDialog", new[] { "Assets/LocalizationTool/Dialogs" });
            foreach (string st in assetsGUIDs)
            {
                dialogsList.Add((BaseDialog)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(st), typeof(BaseDialog)));
            }

            string[] langGUIDs = AssetDatabase.FindAssets("t:Language", new[] { "Assets/LocalizationTool/Languages" });
            foreach (string st in langGUIDs)
            {
                languagesList.Add((Language)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(st), typeof(Language)));
            }

            if (languagesList.Count > 0)
            {
                selectedLanguage = languagesList[0];
                SelectedLanguage.text = selectedLanguage.LanguageName;
            } else
            {
                Debug.LogWarning("Localization Manager: No languages available yet, please add one or multiple");
            }

            if (dialogsList.Count > 0)
            {
                selectedDialog = dialogsList[0];
                SelectedDialog.text = selectedDialog.name;
            }
            else
            {
                Debug.LogWarning("Localization Manager: No dialogs available yet");
            }

            Debug.Log("Localization Manager: Initialized");
            Debug.Log("Localization Manager: Text Object Found.");

            translatedText = GetDialog();
        }

        /// <summary>
        /// Loads the selected dialog and returns the text from it.
        /// </summary>
        /// <returns></returns>
        public static string GetDialog()
        {
            if (selectedDialog != null)
            {
                Debug.Log(dialogsList[0].name + " -- " + SelectedDialog.text);
                selectedDialog = dialogsList.Find(dialog => dialog.name == SelectedDialog.text);
                Debug.Log(selectedDialog);
                return selectedDialog.Content(selectedLanguage);
            }
            return "";
        }

        public static string GetDialog(string previous)
        {
            SetDialog(previous);
            selectedLanguage = languagesList.Find(lang => lang.LanguageName == SelectedLanguage.text);
            Debug.Log(SelectedDialog.text + " -- " + selectedDialog + " -- " + selectedLanguage);
            return GetDialog();
            
        }

        /// <summary>
        /// Loads the selected dialog and returns the text from it.
        /// </summary>
        /// <returns></returns>
        public static string GetDialog(int index)
        {
            if (selectedDialog != null)
            {
                return selectedDialog.Content(index);
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// Saves the given text input to the selected dialog. Returns true if succesful and false otherwise.
        /// </summary>
        /// <returns></returns>
        public static bool SetDialog()
        {
            if (selectedDialog != null) {
                selectedDialog.dialogWrapper[SelectedLanguage.index].content = translatedText;
                Debug.Log("Localization Manager: Saved dialog in language: " + selectedLanguage.LanguageName);
                return true;
            }
            return false; //else
            //{
            //    AddDialog("", translatedText);
            //    return true;
            //}
        }

        /// <summary>
        /// Saves the given text input to the selected dialog. Returns true if succesful and false otherwise.
        /// </summary>
        /// <returns></returns>
        public static bool SetDialog(string lang) { 
            if (selectedDialog != null)
            {
                selectedDialog.dialogWrapper[int.Parse(lang)].content = translatedText;
                return true;
            }
            AddDialog("", translatedText);
            return true;
        }

        public static bool isEmpty
        {
            get
            {
                if (languagesList == null || languagesList.Count == 0)
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// Returns a list with the available languages
        /// </summary>
        /// <returns></returns>
        public static List<string> GetAvailableLanguages()
        {
            List<string> availableLanguages = new List<string>();
            foreach (Language lang in languagesList)
            {
                availableLanguages.Add(lang.LanguageName);
            }
            return availableLanguages;
        }

        /// <summary>
        /// Load the next text dialog. If the current selected dialog is the last element, the first text dialog will be selected.
        /// </summary>
        public static void NextDialog()
        {
            SetDialog();
            SelectedDialog.index += 1;
            Debug.Log(SelectedDialog.index + " -- " + (dialogsList.Count -1));
            if (SelectedDialog.index > dialogsList.Count - 1)
            {
                AddDialog("Dialog-" + dialogsList.Count, "");
                //SelectedDialog.index -= 1;
            } else
            {
                SelectedDialog.text = dialogsList[SelectedDialog.index].name;
                selectedDialog = dialogsList[SelectedDialog.index];
            }
            translatedText = GetDialog();
            Debug.Log("Localization Manager: Next Dialog Selected: " + SelectedDialog.text);
        }

        /// <summary>
        /// Load the previous text dialog. If it is the first element, it will load the last text dialog.
        /// </summary>
        public static void PreviousDialog()
        {
            SetDialog();
            if (SelectedDialog.index > 0)
            {
                SelectedDialog.index -= 1;
                SelectedDialog.text = dialogsList[SelectedDialog.index].name;
            }
            else
            {
                SelectedDialog.index = 0;
                SelectedDialog.text = dialogsList[SelectedDialog.index].name;
            }
            translatedText = GetDialog();
            //selectedDialog = dialogsList[SelectedDialog.index];
            Debug.Log("Localization Manager: Previous Dialog Selected: " + SelectedDialog.text);
        }

        /// <summary>
        /// Adding a language with the given string parameter as name.
        /// </summary>
        /// <param name="languageName"></param>
        public static void AddLang(string languageName)
        {
            Language newLanguage = (Language)ScriptableObject.CreateInstance("Language");// new Language();
            newLanguage.LanguageName = languageName;
            Debug.Log(languagesList + " -- " + newLanguage.LanguageName);
            languagesList.Add(newLanguage);
            foreach (BaseDialog dia in dialogsList)
            {
                //dia.dialogWrapper.Add("");
                BaseDialogWrapper newDialogWrapper = new BaseDialogWrapper
                {
                    content = "",
                    language = newLanguage
                };
                dia.dialogWrapper.Add(newDialogWrapper);
            }
            AssetDatabase.CreateAsset(newLanguage, languagesPath + "/Language_" + languageName + ".asset");
            //languageOptions.Add(languageName);
            selectedLanguage = newLanguage;
            SelectedLanguage.text = languageName;
            SelectedLanguage.index = languagesList.Count - 1; //SelectedLanguage.index = languageOptions.Count - 1;
            translatedText = GetDialog();
            AddDialog("", "");
            Debug.Log("Localization Manager: Added new language: " + languageName);
        }

        /// <summary>
        /// Adding a language with the given string parameter as name.
        /// </summary>
        /// <param name="languageName"></param>
        public static void AddDialog(string dialogName, string dialogText)
        {
            if (translatedText != "" || dialogsList.Count == 0)
            {
                BaseDialog newDialog = (BaseDialog)ScriptableObject.CreateInstance("BaseDialog");
                if (newDialog.dialogWrapper == null)
                {
                    newDialog.dialogWrapper = new List<BaseDialogWrapper>();
                }
                foreach (Language lang in languagesList)
                {
                    if (lang.LanguageName == selectedLanguage.LanguageName)
                    {

                        BaseDialogWrapper newDialogWrapper = new BaseDialogWrapper
                        {
                            content = dialogText,
                            language = lang
                        };
                        Debug.Log(dialogText + " -- " + newDialogWrapper);
                        newDialog.dialogWrapper.Add(newDialogWrapper);
                    }
                    else
                    {
                        BaseDialogWrapper newDialogWrapper = new BaseDialogWrapper
                        {
                            content = "",
                            language = lang
                        };
                        Debug.Log(lang + " -- " + selectedLanguage);
                        newDialog.dialogWrapper.Add(newDialogWrapper);
                    }
                }
                
                dialogsList.Add(newDialog);
                AssetDatabase.CreateAsset(newDialog, dialogsPath + "/Dialog_" + (dialogsList.Count - 1) + ".asset");
                Debug.Log(dialogText + " -- " + newDialog);

                dialogName = "Dialog_" + (dialogsList.Count - 1);
                selectedDialog = newDialog;
                //dialogOptions.Add(dialogName);
                SelectedDialog.text = dialogName;
                SelectedDialog.index = dialogsList.Count - 1;
                //totalDialog += 1;
                Debug.Log("Localization Manager: Added new dialog: " + dialogName + " - " + SelectedDialog.index);
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
            if (languagesList.Count == 0) {
                Debug.Log("Please add a language first.");
            } else {
                foreach (BaseDialog dia in dialogsList) {
                    EditorUtility.SetDirty(dia);
                }

                foreach (Language lang in languagesList) {
                    EditorUtility.SetDirty(lang);
                }
                SetDialog();
                Debug.Log("Localization Manager: Trying to save changes");
                JSONSaver.SaveDictionary(dialogsList);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }
    }
}
