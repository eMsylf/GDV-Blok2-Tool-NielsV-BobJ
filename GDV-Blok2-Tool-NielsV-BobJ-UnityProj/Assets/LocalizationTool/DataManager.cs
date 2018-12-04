using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

[CreateAssetMenu]
public class DataManager : ScriptableObject {

    public Dictionary<string, Dictionary<string, string>> languages;
    //public Dictionary<string, string> dialogs;
}
