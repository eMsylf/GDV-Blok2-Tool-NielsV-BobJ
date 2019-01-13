using UnityEngine;

[CreateAssetMenu(menuName = "Localization/SystemOptions")]
public class SystemOptions : ScriptableObject
{
    public Language currentLanguage;

    public delegate void LanguageChange();
    public event LanguageChange OnLanguageChange;
    public void Subscribe(LanguageChange lang)
    {
        OnLanguageChange += lang;
    }
    public void Unsubscribe(LanguageChange lang)
    {
        OnLanguageChange -= lang;
    }
    public void ChangeLanguage(Language lang)
    {
        currentLanguage = lang;
        OnLanguageChange();
    }
}
