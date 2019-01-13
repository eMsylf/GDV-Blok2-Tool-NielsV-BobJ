using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogDecorator : DialogComponent {

    DialogComponent m_DialogComponent = null;

	protected DialogDecorator(DialogComponent baseComponent)
    {
        m_DialogComponent = baseComponent;
    }

    public new void OnEnable()
    {
        
    }
}
