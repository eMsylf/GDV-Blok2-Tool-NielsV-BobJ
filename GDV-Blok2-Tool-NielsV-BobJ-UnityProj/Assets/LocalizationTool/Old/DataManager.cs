using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class DataManager : ScriptableObject
{
    [SerializeField]
    public Dictionary<string, Dictionary<string, string>> languages = new Dictionary<string, Dictionary<string, string>>();
}
