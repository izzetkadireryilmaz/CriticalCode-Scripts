using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    public TMP_Text PasswordText, Son;
    public AudioSource ButtonAudioSource;
    public GameObject TextFrame, NumpadPanel;
    public Button ConfirmButton;
    public Image UpImage, DownImage, TrueImage;
    public Image ChangeImage1, ChangeImage2, ChangeImage3, ChangeImage4, Mascot;
    public Canvas TutorialCanvas, GameCanvas;

    public void ChangeFirstUnderscore(int Value)
    {
        string currentText = PasswordText.text;

        // Ýlk "_" karakterinin indeksini bul
        int index = currentText.IndexOf('_');

        // Eðer "_" bulunduysa, o konumu belirlenen sayý ile deðiþtir
        if (index != -1)
        {
            // Yeni metni oluþtur
            char[] charArray = currentText.ToCharArray();
            charArray[index] = Value.ToString()[0]; // Sayýyý karaktere dönüþtür

            // Text'i güncelle
            PasswordText.text = new string(charArray);
            ButtonAudioSource.Play();
            Debug.Log(PasswordText.text);
        }
    }

    public void ContinueButton()
    {
        TutorialCanvas.gameObject.SetActive(false);
        GameCanvas.gameObject.SetActive(true);
    }

    public void TutorialConfirm()
    {
        string CurrentText = PasswordText.text;
        int _Control = CurrentText.IndexOf('_');
        if (_Control == -1)
        {
            ChangeImage1.sprite = UpImage.sprite;
            ChangeImage2.sprite = TrueImage.sprite;
            ChangeImage3.sprite = TrueImage.sprite;
            ChangeImage4.sprite = DownImage.sprite;
            Son.text = CurrentText;
            PasswordText.text = "____";
            NumpadPanel.SetActive(false);
            ConfirmButton.gameObject.SetActive(false);
            Mascot.gameObject.SetActive(true);
            TextFrame.SetActive(true);
        }
    }
    public void Backspace()
    {
        string CurrentText = PasswordText.text;
        for (int i = CurrentText.Length - 1; i >= 0; i--) // döngüyü tersten baþlatarak en saðdaki "_" olnayan ilk karakteri arýyoruz.
        {
            if (CurrentText[i] != '_') // "_" olmayan ilk karakteri seçiyoruz.
            {
                char[] chars = CurrentText.ToCharArray(); // string olan veriyi tek tek incelemek için bir char dizisi haline getiriyoruz.
                chars[i] = '_'; // seçilen karkateri "_" yapýyoruz.
                PasswordText.text = new string(chars); // yeni oluþturulan char dizisini tekrar stringe dönüþtürüyoruz.
                break;
            }
        }
    }
}
