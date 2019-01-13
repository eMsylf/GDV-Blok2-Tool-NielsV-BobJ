using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Dialog : DialogComponent {

    //public Text textComponent;
    //private string text;

    private void GetTextComponent()
    {
        textComponent = GetComponent<Text>();
    }

    public new void OnEnable()
    {
        GetTextComponent();
        //SelectedLanguage.text = dialogData.languages.Keys.First();
        //SelectedDialog.text = dialogData.languages[dialogData.languages.Keys.First()].Keys.First();
        text = dialogData.languages[SelectedLanguage.text][SelectedDialog.text];
        textComponent.text = text;
    }
}
