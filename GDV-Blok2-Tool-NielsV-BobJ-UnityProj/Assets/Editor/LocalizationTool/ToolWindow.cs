using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LocalizeWindow : EditorWindow {

	int selectedLanguage = 0;
	int selectedDialog = 0;
	public static int totalDialog = 3;
	string translatedText = "";
	public static List<string> options;

	public static Dictionary<int, string> localizedDialogs = new Dictionary<int, string>();
	public static Dictionary<int, Dictionary<int, string>> localizedLanguages = new Dictionary<int, Dictionary<int, string>>();

	[MenuItem("Tools/Localization")]
	public static void Create(){
		EditorWindow.GetWindow<LocalizeWindow> ();
		EditorWindow.GetWindow<LocalizeWindow> ().minSize = new Vector2 ( 12 * EditorGUIUtility.singleLineHeight, 18 * EditorGUIUtility.singleLineHeight );
	}

	void OnGUI(){

		#region Buttons
		EditorGUILayout.BeginHorizontal ();
		if (GUILayout.Button("Previous")){
			Debug.Log ("Previous Text");
			selectedDialog = ((selectedDialog - 1) + totalDialog-2) % (totalDialog-2);
			Debug.Log(selectedDialog);
		}
		if (GUILayout.Button("Next")){
			Debug.Log ("Next Text");
			selectedDialog = (selectedDialog + 1) % (totalDialog-2);
		}
		if (GUILayout.Button("Add Lang")){
			Debug.Log ("Added Language");
			AddLanguageWizard.Create();
		}
		EditorGUILayout.EndHorizontal ();

		GUILayout.FlexibleSpace();

		EditorGUILayout.BeginHorizontal ();
		if (GUILayout.Button("Load Text")){
			Debug.Log ("Loading All Text...");
		}
		if (GUILayout.Button("Save Changes")){
			Debug.Log ("Saving Changes");
			SaveTextDebug(translatedText);
		}
		EditorGUILayout.EndHorizontal ();
		#endregion

		#region Popup
		Rect popup = new Rect(new Vector2(0, 2*EditorGUIUtility.singleLineHeight), new Vector2(EditorWindow.GetWindow<LocalizeWindow>().position.width, EditorGUIUtility.singleLineHeight));

		selectedLanguage = EditorGUI.Popup(popup, selectedLanguage, options.ToArray());

		#endregion

		#region OriginalText
		GUILayout.BeginArea(new Rect(0, 3*EditorGUIUtility.singleLineHeight, EditorWindow.GetWindow<LocalizeWindow>().position.width, EditorGUIUtility.singleLineHeight * 6));
		EditorGUILayout.BeginHorizontal ();
		GUILayout.TextArea (GetTextDebug(0, selectedDialog), GUILayout.Height(position.height - 30));
		EditorGUILayout.EndHorizontal ();
		GUILayout.EndArea();
		#endregion

		#region LocalizedText
		GUILayout.BeginArea(new Rect(0, 10*EditorGUIUtility.singleLineHeight, EditorWindow.GetWindow<LocalizeWindow>().position.width, EditorGUIUtility.singleLineHeight * 6));
		EditorGUILayout.BeginHorizontal ();
		translatedText = GUILayout.TextArea (translatedText, GUILayout.Height(position.height - 30));
		EditorGUILayout.EndHorizontal ();
		GUILayout.EndArea();
		#endregion
	}

	public void SaveTextDebug(string text){
		Dictionary<int, string> localDialog;
		if (localizedLanguages.TryGetValue(selectedLanguage, out localDialog)){
			string dialog;
			if (localDialog.TryGetValue(selectedDialog, out dialog)){
				dialog = text;
			} else {
				Debug.LogError ("Developer Speaking, This is not part of the game. Please contact the developer. Error: This dialog does not exist.");
			}
		} else {
			Debug.LogError ("Developer Speaking, This is not part of the game. Please contact the developer. Error: This language does not exist.");
		}
	}

	public string GetTextDebug(int selectedLanguage, int selectedDialog){
		Dictionary<int, string> localDialog;
		if (localizedLanguages.TryGetValue(selectedLanguage, out localDialog)){
			string dialog;
			if (localDialog.TryGetValue(selectedDialog, out dialog)){
				return dialog;
			} else {
				Debug.LogError ("Developer Speaking, This is not part of the game. Please contact the developer. Error: This dialog does not exist.");
			}
		} else {
			Debug.LogError ("Developer Speaking, This is not part of the game. Please contact the developer. Error: This language does not exist.");
		}
		return null;
	}
}

public class AddLanguageWizard : ScriptableWizard {

	string languageName;

	public static void Create(){
		ScriptableWizard.DisplayWizard<AddLanguageWizard>("Screen");
	}

	void OnGUI(){
		languageName = GUILayout.TextArea (languageName, GUILayout.Height(position.height - 30));

		if (GUILayout.Button("Add")){
			LocalizeWindow.localizedLanguages.Add (LocalizeWindow.localizedLanguages.Count ,new Dictionary<int, string>());
			LocalizeWindow.options.Add (languageName);
		}
	}
}
