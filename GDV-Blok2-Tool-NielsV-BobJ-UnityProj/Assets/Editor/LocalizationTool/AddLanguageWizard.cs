using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class AddLanguageWizard : EditorWindow {

	string languageName;

	public static void Create(){
		GetWindow<AddLanguageWizard>();
	}

	void OnGUI(){
		languageName = EditorGUILayout.TextField (languageName, GUILayout.Height(position.height - 30));

		if (GUILayout.Button("Add")){
			LocalizeWindow.localizedLanguages.Add (LocalizeWindow.localizedLanguages.Count, new Dictionary<int, string>());
            LocalizeWindow.options.Add(languageName);
        }

	}
}

