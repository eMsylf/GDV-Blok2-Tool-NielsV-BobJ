using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class JSONSaver : MonoBehaviour {

	void SaveDictionary(Dictionary<string, Dictionary<string, string>> dict)
    {
        //Serialize dict to list
        List<string> languageKeysList;
        List<string> dialogKeysList;

        languageKeysList = dict.Keys.ToList();
        //dialogKeysList = dict.
        //do stuff
    }
}
