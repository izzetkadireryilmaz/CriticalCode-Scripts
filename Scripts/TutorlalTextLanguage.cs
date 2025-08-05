using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class TutorlalTextLanguage : MonoBehaviour
{
    public TMP_Text Text, Text0, Text1, Text2, Text3, Text4;

    void Start()
    {
        string lang = PlayerPrefs.GetString("Language", "tr");

        if (lang == "tr")
        {
            Text.text = "Critical Code'a ho� geldin. Amac�m�z tek ve �ok basit; sisteme gir. Bunu yaparken sana verilecek ipu�lar� son derece �nemli, merak etme �imdi sana nas�l yap�laca��n� anlataca��m.";
            Text0.text = "Tahminin Gayet iyi! Devam edelim ve sana buradaki ifadelerin ne anlama geldi�ini anlatay�m.";
            Text1.text = "�lk s�rada yer alan yukar� ok ifadesi, sisteme girmek i�in ihtiyac�n olan �ifrede o basama��n daha y�ksek oldu�u anlam�na gelir.";
            Text2.text = "�kinci ve ���nc� s�rada yer alan tik i�areti, �ifrede yapt���n tahminin do�ru oldu�u anlam�na gelir.";
            Text3.text = "D�rd�nc� s�rada yer alan a�a�� ok i�areti ise, tahmin etmeye �al��t���n �ifrede o basama��n daha d���k bir de�er oldu�unu ifade eder.";
            Text4.text = "Her ba�lang�� senden bir pil g�t�r�r, ama sisteme s�zmay� ba�ar�rsan pili geri kazan�rs�n, mant�k basit; daha fazla kazanmak i�in kazan!";
        }
        else
        {
            Text.text = "Welcome to Critical Code. Our goal is one and simple: get into the system. The clues you will be given to do this are extremely important, so don't worry, I will tell you how to do it now.";
            Text0.text = "Your guess is good! Let's go ahead and I'll tell you what these statements mean.";
            Text1.text = "The up arrow in the first row means that the password you need to enter the system is higher in that row.";
            Text2.text = "The second and third tick marks mean that your guess at the password was correct.";
            Text3.text = "The down arrow in the fourth row indicates that the digit in the password you are trying to guess has a lower value.";
            Text4.text = "Every startup takes a battery away from you, but if you manage to infiltrate the system you get it back, the logic is simple; win to win more!";
        }
    }

    public void UpdateLanguage()
    {
        string lang = PlayerPrefs.GetString("Language", "tr");

        if (lang == "tr")
        {
            Text.text = "Critical Code'a ho� geldin. Amac�m�z tek ve �ok basit; sisteme gir. Bunu yaparken sana verilecek ipu�lar� son derece �nemli, merak etme �imdi sana nas�l yap�laca��n� anlataca��m.";
            Text0.text = "Tahminin Gayet iyi! Devam edelim ve sana buradaki ifadelerin ne anlama geldi�ini anlatay�m.";
            Text1.text = "�lk s�rada yer alan yukar� ok ifadesi, sisteme girmek i�in ihtiyac�n olan �ifrede o basama��n daha y�ksek oldu�u anlam�na gelir.";
            Text2.text = "�kinci ve ���nc� s�rada yer alan tik i�areti, �ifrede yapt���n tahminin do�ru oldu�u anlam�na gelir.";
            Text3.text = "D�rd�nc� s�rada yer alan a�a�� ok i�areti ise, tahmin etmeye �al��t���n �ifrede o basama��n daha d���k bir de�er oldu�unu ifade eder.";
            Text4.text = "Her ba�lang�� senden bir pil g�t�r�r, ama sisteme s�zmay� ba�ar�rsan pili geri kazan�rs�n, mant�k basit; daha fazla kazanmak i�in kazan!";
        }
        else
        {
            Text.text = "Welcome to Critical Code. Our goal is one and simple: get into the system. The clues you will be given to do this are extremely important, so don't worry, I will tell you how to do it now.";
            Text0.text = "Your guess is good! Let's go ahead and I'll tell you what these statements mean.";
            Text1.text = "The up arrow in the first row means that the password you need to enter the system is higher in that row.";
            Text2.text = "The second and third tick marks mean that your guess at the password was correct.";
            Text3.text = "The down arrow in the fourth row indicates that the digit in the password you are trying to guess has a lower value.";
            Text4.text = "Every startup takes a battery away from you, but if you manage to infiltrate the system you get it back, the logic is simple; win to win more!";
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
            Debug.Log("dil ingilizce");
            SetLanguage("en");
        }
        else
        {
            Debug.Log("dil t�rk�e");
            SetLanguage("tr");
        }
    }
}
