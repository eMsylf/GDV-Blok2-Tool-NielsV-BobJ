using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class DialogComponent : MonoBehaviour {

    public Text textComponent;
    public string text;
    public DataManager dialogData;

    private void GetTextComponent()
    {
    }

    public void OnEnable()
    {
    }
}
