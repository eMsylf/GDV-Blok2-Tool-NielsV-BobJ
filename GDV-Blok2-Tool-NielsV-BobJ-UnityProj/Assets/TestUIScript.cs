using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.IO;

[CustomEditor(typeof(MyPlayer))]
//[CanEditMultipleObjects]

public class TestUIScript : Editor {

    SerializedProperty damageProp;
    SerializedProperty armorProp;
    SerializedProperty gunProp;

    public Rect rect;

    //GUIStyle style;
    GUIStyle style;
    
    

    private void OnEnable() {
        damageProp = serializedObject.FindProperty("damage");
        armorProp = serializedObject.FindProperty("armor");
        gunProp = serializedObject.FindProperty("gun");

        GUISkin skin = AssetDatabase.LoadAssetAtPath<GUISkin>("Assets/CustomStyle.guiskin");
        skin.GetStyle("AAAA");
    }

    void ProgressBar(float value, string label) {
        rect = GUILayoutUtility.GetRect(18, 18, "TextField");
        EditorGUI.ProgressBar(rect, value, label);
        EditorGUILayout.Space();
    }

    public override void OnInspectorGUI() {
        serializedObject.Update();

        
        //style.alignment = TextAnchor.MiddleRight;
        //style.border.right = 10;



        EditorGUILayout.TextArea("Testing testing", style); //Krijg je nullreference? Haal dan deze lijn weg. Groetjes Niels

        EditorGUILayout.TagField("aaaaa");

        EditorGUILayout.TextField(
            "<Enter translated text>"
            );

        //EditorGUIUtility.DrawColorSwatch(rect, Color.black);



        EditorGUILayout.Space();

        EditorGUILayout.Separator();



        EditorGUI.DrawRect(rect, Color.black);

        EditorGUILayout.SelectableLabel("bbbbb");

        EditorGUILayout.IntSlider(damageProp, 0, 100, new GUIContent("Damage"));

        if (!damageProp.hasMultipleDifferentValues) {
            ProgressBar(damageProp.intValue / 100.0f, "Damage");
        }

        EditorGUILayout.IntSlider(armorProp, 0, 100, new GUIContent("Armor"));

        if (!armorProp.hasMultipleDifferentValues) {
            ProgressBar(armorProp.intValue / 100.0f, "Armor");
        }

        EditorGUILayout.PropertyField(gunProp, new GUIContent("Gun"));

        serializedObject.ApplyModifiedProperties();
        //base.OnInspectorGUI();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
