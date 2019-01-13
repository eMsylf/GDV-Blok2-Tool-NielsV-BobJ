using UnityEngine;

public class ChangeCurrentLanguage : MonoBehaviour
{
    public SystemOptions gameOptions;
    public Language langToChange;

    public void Change()
    {
        gameOptions.ChangeLanguage(langToChange);
    }
}
