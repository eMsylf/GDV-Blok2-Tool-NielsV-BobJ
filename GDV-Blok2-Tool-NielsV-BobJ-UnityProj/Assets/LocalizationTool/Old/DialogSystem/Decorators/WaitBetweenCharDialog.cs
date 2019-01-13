using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitBetweenCharDialog : DialogDecorator {

    public WaitBetweenCharDialog(DialogComponent baseComponent)
        : base(baseComponent)
    {
        
    }

    public new void OnEnable()
    {
        this.textComponent.text = this.text;
    }
}
