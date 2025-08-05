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
            Text.text = "Critical Code'a hoþ geldin. Amacýmýz tek ve çok basit; sisteme gir. Bunu yaparken sana verilecek ipuçlarý son derece önemli, merak etme þimdi sana nasýl yapýlacaðýný anlatacaðým.";
            Text0.text = "Tahminin Gayet iyi! Devam edelim ve sana buradaki ifadelerin ne anlama geldiðini anlatayým.";
            Text1.text = "Ýlk sýrada yer alan yukarý ok ifadesi, sisteme girmek için ihtiyacýn olan þifrede o basamaðýn daha yüksek olduðu anlamýna gelir.";
            Text2.text = "Ýkinci ve üçüncü sýrada yer alan tik iþareti, þifrede yaptýðýn tahminin doðru olduðu anlamýna gelir.";
            Text3.text = "Dördüncü sýrada yer alan aþaðý ok iþareti ise, tahmin etmeye çalýþtýðýn þifrede o basamaðýn daha düþük bir deðer olduðunu ifade eder.";
            Text4.text = "Her baþlangýç senden bir pil götürür, ama sisteme sýzmayý baþarýrsan pili geri kazanýrsýn, mantýk basit; daha fazla kazanmak için kazan!";
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
            Text.text = "Critical Code'a hoþ geldin. Amacýmýz tek ve çok basit; sisteme gir. Bunu yaparken sana verilecek ipuçlarý son derece önemli, merak etme þimdi sana nasýl yapýlacaðýný anlatacaðým.";
            Text0.text = "Tahminin Gayet iyi! Devam edelim ve sana buradaki ifadelerin ne anlama geldiðini anlatayým.";
            Text1.text = "Ýlk sýrada yer alan yukarý ok ifadesi, sisteme girmek için ihtiyacýn olan þifrede o basamaðýn daha yüksek olduðu anlamýna gelir.";
            Text2.text = "Ýkinci ve üçüncü sýrada yer alan tik iþareti, þifrede yaptýðýn tahminin doðru olduðu anlamýna gelir.";
            Text3.text = "Dördüncü sýrada yer alan aþaðý ok iþareti ise, tahmin etmeye çalýþtýðýn þifrede o basamaðýn daha düþük bir deðer olduðunu ifade eder.";
            Text4.text = "Her baþlangýç senden bir pil götürür, ama sisteme sýzmayý baþarýrsan pili geri kazanýrsýn, mantýk basit; daha fazla kazanmak için kazan!";
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

        UpdateLanguage(); // Deðiþiklik hemen yansýtýlýr
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
            Debug.Log("dil türkçe");
            SetLanguage("tr");
        }
    }
}
