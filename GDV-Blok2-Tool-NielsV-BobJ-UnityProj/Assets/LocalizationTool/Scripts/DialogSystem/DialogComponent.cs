using UnityEngine;
//using TMPro;
using UnityEngine.UI;

namespace LocalizationTool
{

    public class DialogComponent : MonoBehaviour
    {
        //TextMeshProUGUI textComponent;
        private Text textComponent;
        public BaseDialog dialog;

        void OnEnable()
        {
            if (dialog == null)
            {
                Debug.LogError("DialogComponent: There is no BaseDialog selected!");
                this.enabled = false;
            }
            else
            {
                dialog.Subscribe(SetText);
            }
        }

        void OnDisable()
        {
            if (dialog != null)
                dialog.Unsubscribe(SetText);
        }

        void Start()
        {
            //textComponent = GetComponent<TextMeshProUGUI>();
            textComponent = GetComponent<Text>();
            SetText();
        }

        void SetText()
        {
            UnityEditor.EditorUtility.SetDirty(dialog); //Saving the scriptable wizard just to be sure
            
            textComponent.text = dialog.Content();
        }
    }
}
