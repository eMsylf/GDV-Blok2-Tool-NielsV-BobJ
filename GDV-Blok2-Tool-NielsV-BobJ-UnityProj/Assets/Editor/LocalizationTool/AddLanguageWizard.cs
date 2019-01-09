using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class AddLanguageWizard : EditorWindow {

	string languageName;
    static LocalizeWindow tool;

    private static float textFieldSize = 1 * EditorGUIUtility.singleLineHeight;
    private static float windowHeight = 2.2f * EditorGUIUtility.singleLineHeight;
    private static float windowWidth = 18 * EditorGUIUtility.singleLineHeight;

    public static void Create(LocalizeWindow toolWin) {
        tool = toolWin;
        GetWindow<AddLanguageWizard>("Add Language");
        GetWindow<AddLanguageWizard>().minSize = new Vector2(windowWidth, windowHeight);
        GetWindow<AddLanguageWizard>().maxSize = new Vector2(windowWidth, windowHeight);
    }

    private static AddLanguageWizard wizard;
   

	private void OnGUI(){
        Event e = Event.current;

        

		languageName = EditorGUILayout.TextField (languageName, GUILayout.Height(position.height - 30), GUILayout.Height(textFieldSize));

		if (GUILayout.Button("Add") || (Event.current.keyCode == KeyCode.Return || Event.current.keyCode == KeyCode.KeypadEnter)){
            //LocalizeWindow.data.languages.Add (languageName, new Dictionary<string, string>());
            //         LocalizeWindow.options.Add(languageName);
            LocalizationManager.AddLang(languageName);
            tool.Repaint();
            this.Close();
            Debug.Log("Added new language: " + languageName);
        }

	}
}

