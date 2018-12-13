using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationManager : MonoBehaviour {

    public static DataManager data;

    static string selectedLanguage;
    static string selectedDialog;
    int selectPopLanguage = 0;
    int selectPopDialog = 0;
    public static int totalDialog = 3;
    string translatedText = "";
    public static List<string> options = new List<string>();

 public static string GetDialog()
    {
        return "";
    }

    public static bool SetDialog()
    {
    return true;
    }

    public static void SetLanguage()
    {

    }

    public static void SetAll()
    {
        //Get language and dialog if there are any, otherwise make new one
        //selectedLanguage = data.languages.Keys.FirstOrDefault();
        //Debug.Log("not empty");

        //Dictionary<string, string> outLang;
        //if (selectedLanguage != null && data.languages.TryGetValue(selectedLanguage, out outLang))
        //{
        //    selectedDialog = outLang.Keys.FirstOrDefault();
        //}
        //else
        //{
        //    selectedDialog = "";
        //}
    }

    public static void NextDialog()
    {
        //Debug.Log("Next Text");
        //selectedDialog = popDialog[(selectPopDialog + 1) % (totalDialog - 2)];
    }

    public static void PreviousDialog()
    {
        //Debug.Log("Previous Text");
        //selectedDialog = popDialog[(selectPopDialog - 1 + totalDialog - 2) % (totalDialog - 2)];
        //Debug.Log(selectedDialog);
    }

    public static void AddLang()
    {

    }
}
