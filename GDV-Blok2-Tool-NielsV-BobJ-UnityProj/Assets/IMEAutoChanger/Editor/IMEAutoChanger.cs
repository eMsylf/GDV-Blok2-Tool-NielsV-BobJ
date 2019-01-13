using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Reflection;

[InitializeOnLoad]
public class IMEAutoChanger
{
	private static FieldInfo s_ActiveEditorField;
	private static FieldInfo s_TextEditorHasFocusField;

	static IMEAutoChanger()
	{
		System.Type textEditorType = typeof(TextEditor);
		s_TextEditorHasFocusField = textEditorType.GetField( "m_HasFocus",BindingFlags.NonPublic | BindingFlags.Instance );
		
		System.Type editorGUIType = typeof(EditorGUI);
		s_ActiveEditorField = editorGUIType.GetField ( "activeEditor",BindingFlags.NonPublic | BindingFlags.Static );

		EditorApplication.update += Update;
	}

	private static TextEditor activeEditor
	{
		get
		{
			return (TextEditor)s_ActiveEditorField.GetValue(null);
		}
	}
	
	private static bool HasFocus( TextEditor textEditor )
	{
		return (bool)s_TextEditorHasFocusField.GetValue( textEditor );
	}

	static void Update()
	{
		if( EditorGUIUtility.editingTextField && activeEditor != null && GUIUtility.keyboardControl != 0 && GUIUtility.keyboardControl == activeEditor.controlID && HasFocus( activeEditor ) )
		{
			Input.imeCompositionMode = IMECompositionMode.On;
		}
		else
		{
			Input.imeCompositionMode = IMECompositionMode.Auto;
		}
	}
}
