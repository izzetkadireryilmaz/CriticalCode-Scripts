using System.Reflection;
using TMPro;
using UnityEngine;

public class TextLanguageChanger : MonoBehaviour
{
    public static TextLanguageChanger Instance;
    public TMP_Text playButtonText, AchievementsButtonText, SettingsButtonText, AdsNote, Achievements1, Achievements2, Achievements3, SettingsInf, Settings1, Settings2;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        string lang = PlayerPrefs.GetString("Language", "tr");

        if (lang == "tr")
        {
            playButtonText.text = "Oyna";
            AchievementsButtonText.text = "Ba�ar�m";
            SettingsButtonText.text = "Ayarlar";
            AdsNote.text = "Yorgunsun, bir reklam izleyip enerjini toparlamak ister misin?";
            SettingsInf.text = "Oyununuzun zorlu�unu kendinize g�re ki�iselle�tirebilirsiniz.";
            Settings1.text = "Son girilen �ifreyi g�ster";
            Settings2.text = "Cevap sonras� numpadi kar��t�r";
            Achievements1.text = $"Girilen Toplam\nSistem Say�s�: {PlayerPrefs.GetInt("WinCount")}";
            Achievements2.text = $"En Uzun\nBa�ar� Serisi: {PlayerPrefs.GetInt("TopWinStreak")}";
            Achievements3.text = $"Mevcut\nBa�ar� Serisi: {PlayerPrefs.GetInt("WinStreak")}";
        }
        else
        {
            playButtonText.text = "Play";
            AchievementsButtonText.text = "Achievements";
            SettingsButtonText.text = "Settings";
            AdsNote.text = "You're tired, do you want to watch a commercial and recharge your batteries?";
            SettingsInf.text = "You can customize the difficulty of your game.";
            Settings1.text = "Show last entered password";
            Settings2.text = "Shuffle the numpad after the answer";
            Achievements1.text = $"Total Number of\nSuccessful Missions: {PlayerPrefs.GetInt("WinCount")}";
            Achievements2.text = $"Longest Streak\nOf Success: {PlayerPrefs.GetInt("TopWinStreak")}";
            Achievements3.text = $"Current Success\nStreak: {PlayerPrefs.GetInt("WinStreak")}";
        }
    }

    public void UpdateLanguage()
    {
        string lang = PlayerPrefs.GetString("Language", "tr");

        if (lang == "tr")
        {
            playButtonText.text = "Oyna";
            AchievementsButtonText.text = "Ba�ar�m";
            SettingsButtonText.text = "Ayarlar";
            AdsNote.text = "Yorgunsun, bir reklam izleyip enerjini toparlamak ister misin?";
            SettingsInf.text = "Oyununuzun zorlu�unu kendinize g�re ki�iselle�tirebilirsiniz.";
            Settings1.text = "Son girilen �ifreyi g�ster";
            Settings2.text = "Cevap sonras� numpadi kar��t�r";
            Achievements1.text = $"Girilen Toplam\nSistem Say�s�: {PlayerPrefs.GetInt("WinCount")}";
            Achievements2.text = $"En Uzun\nBa�ar� Serisi: {PlayerPrefs.GetInt("TopWinStreak")}";
            Achievements3.text = $"Mevcut\nBa�ar� Serisi: {PlayerPrefs.GetInt("WinStreak")}";
        }
        else
        {
            playButtonText.text = "Play";
            AchievementsButtonText.text = "Achievements";
            SettingsButtonText.text = "Settings";
            AdsNote.text = "You're tired, do you want to watch a commercial and recharge your batteries?";
            SettingsInf.text = "You can customize the difficulty of your game.";
            Settings1.text = "Show last entered password";
            Settings2.text = "Shuffle the numpad after the answer";
            Achievements1.text = $"Total Number of\nSuccessful Missions: {PlayerPrefs.GetInt("WinCount")}";
            Achievements2.text = $"Longest Streak\nOf Success: {PlayerPrefs.GetInt("TopWinStreak")}";
            Achievements3.text = $"Current Success\nStreak: {PlayerPrefs.GetInt("WinStreak")}";
        }
    }

    public void SetLanguage(string lang)
    {
        PlayerPrefs.SetString("Language", lang);
        PlayerPrefs.Save();

        UpdateLanguage(); // De�i�iklik hemen yans�t�l�r
    }

    public void ChangeLanguageButton()
    {
        if (PlayerPrefs.GetString("Language") == "tr")
        {
            NewMascotTextManager.Instance.currentLanguage = "en";
            SetLanguage("en");
        }
        else
        {
            NewMascotTextManager.Instance.currentLanguage = "tr";
            SetLanguage("tr");
        }
    }
}