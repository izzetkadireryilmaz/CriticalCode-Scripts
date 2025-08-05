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

        // �lk "_" karakterinin indeksini bul
        int index = currentText.IndexOf('_');

        // E�er "_" bulunduysa, o konumu belirlenen say� ile de�i�tir
        if (index != -1)
        {
            // Yeni metni olu�tur
            char[] charArray = currentText.ToCharArray();
            charArray[index] = Value.ToString()[0]; // Say�y� karaktere d�n��t�r

            // Text'i g�ncelle
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
        for (int i = CurrentText.Length - 1; i >= 0; i--) // d�ng�y� tersten ba�latarak en sa�daki "_" olnayan ilk karakteri ar�yoruz.
        {
            if (CurrentText[i] != '_') // "_" olmayan ilk karakteri se�iyoruz.
            {
                char[] chars = CurrentText.ToCharArray(); // string olan veriyi tek tek incelemek i�in bir char dizisi haline getiriyoruz.
                chars[i] = '_'; // se�ilen karkateri "_" yap�yoruz.
                PasswordText.text = new string(chars); // yeni olu�turulan char dizisini tekrar stringe d�n��t�r�yoruz.
                break;
            }
        }
    }
}
