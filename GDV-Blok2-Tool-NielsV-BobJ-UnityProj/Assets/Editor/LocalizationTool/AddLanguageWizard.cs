using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class AddLanguageWizard : EditorWindow {

	string languageName;

	public static void Create(){
		GetWindow<AddLanguageWizard>();
	}

	void OnGUI(){
        Event e = Event.current;

		languageName = EditorGUILayout.TextField (languageName, GUILayout.Height(position.height - 30));

		if (GUILayout.Button("Add") || (e.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Return)){
			LocalizeWindow.data.languages.Add (languageName, new Dictionary<string, string>());
            LocalizeWindow.options.Add(languageName);
            this.Close();
            Debug.Log("hoi");
        }

	}
}

