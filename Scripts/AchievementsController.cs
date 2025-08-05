using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementsController : MonoBehaviour
{
    public TMP_Text Achievement1, Achievement2, Achievement3;
    public Image AchievementsFrame;
    private void Awake()
    {
        #region playerprefs
        if (!PlayerPrefs.HasKey("WinCount"))
        {
            PlayerPrefs.SetInt("WinCount", 0);
        }
        if (!PlayerPrefs.HasKey("WinStreak"))
        {
            PlayerPrefs.SetInt("WinStreak", 0);
        }
        if (!PlayerPrefs.HasKey("TopWinStreak"))
        {
            PlayerPrefs.SetInt("TopWinStreak", 0);
        }
        #endregion
    }
    void Start()
    {
        Achievement1.text = $"Girilen Toplam\nSistem Sayýsý: {PlayerPrefs.GetInt("WinCount")}";
        Achievement2.text = $"En Uzun\nBaþarý Serisi: {PlayerPrefs.GetInt("TopWinStreak")}";
        Achievement3.text = $"Mevcut\nBaþarý Serisi: {PlayerPrefs.GetInt("WinStreak")}";
    }

    public void AchievementsFrameOnOffButton()
    {
        if (AchievementsFrame.gameObject.activeSelf)
        {
            BouncyPanel.instance.ClosePanel(AchievementsFrame.gameObject);
        }
        else
        {
            BouncyPanel.instance.OpenPanel(AchievementsFrame.gameObject);
        }
    }
}
