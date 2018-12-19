using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class AddLanguageWizard : EditorWindow {

	string languageName;
    static LocalizeWindow tool;

    public static void Create(LocalizeWindow toolWin) {
        tool = toolWin;
        GetWindow<AddLanguageWizard>("Add Language");
    }

    private static AddLanguageWizard wizard;
   

	void OnGUI(){
        Event e = Event.current;

		languageName = EditorGUILayout.TextField (languageName, GUILayout.Height(position.height - 30));

		if (GUILayout.Button("Add") || (e.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Return)){
            //LocalizeWindow.data.languages.Add (languageName, new Dictionary<string, string>());
            //         LocalizeWindow.options.Add(languageName);
            LocalizationManager.AddLang(languageName);
            tool.Repaint();
            this.Close();
            Debug.Log("Added new language: " + languageName);
        }

	}
}

