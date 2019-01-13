using System.Collections.Generic;
using UnityEngine;

namespace LocalizationTool
{

    [CreateAssetMenu(menuName = "Localization/BaseDialog")]
    public class BaseDialog : ScriptableObject
    {
        [SerializeField]
        SystemOptions gameOptions;

        [SerializeField]
        public List<BaseDialogWrapper> dialogWrapper;

        bool initialized = false;

        public string Content()
        {
            if (!initialized)
            {
                initialized = true;
            }
            for (int i = 0; i < dialogWrapper.Count; i++)
            {
                if (gameOptions.currentLanguage == dialogWrapper[i].language)
                    return dialogWrapper[i].content;
            }
            return "";
        }

        public string Content(Language currentLanguage)
        {
            if (!initialized)
            {
                initialized = true;
            }
            for (int i = 0; i < dialogWrapper.Count; i++)
            {
                Debug.Log(currentLanguage, dialogWrapper[i].language);
                if (currentLanguage == dialogWrapper[i].language)
                    return dialogWrapper[i].content;
            }
            return "";
        }

        public string Content(int index)
        {
            if (!initialized)
            {
                initialized = true;
            }
            for (int i = 0; i < dialogWrapper.Count; i++)
            {
                return dialogWrapper[index].content;
            }
            return "";
        }

        public void Subscribe(SystemOptions.LanguageChange method)
        {
            gameOptions.Subscribe(method);
        }

        public void Unsubscribe(SystemOptions.LanguageChange method)
        {
            gameOptions.Unsubscribe(method);
        }
    }
    [System.Serializable]
    public class BaseDialogWrapper
    {
        public Language language;
        public string content;
    }
}
