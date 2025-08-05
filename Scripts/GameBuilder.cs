using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameBuilder : MonoBehaviour
{
    public static GameBuilder Instance;
    public NumpadManager numpadManager;
    [Header("Feedback")]
    public TMP_Text PasswordText, Son, EndCanvasInfoText, ConfirmButtonText, TryAgainButton, MenuButton; // info texti �u an kullanm�yoruz ama oyuna zaman eklenince kaybetme de eklenece�i i�in orada kaybetme bilgliendirmesi i�in kullanaca��z.
    public Image Num1Feed, Num2Feed, Num3Feed, Num4Feed;
    public Image UpImage, DownImage, TrueImage;

    [Header("Animation")]
    public Canvas GameCanvas, EndCanvas;
    public Image LoadImage, EndCanvasImage;
    Animator animator, endAnimator;

    [Header("RandomPass And Timer")]
    public int RandomPassword, RandomSecond;
    public TMP_Text TimerText;
    float CurrentTime;
    bool isRunning = true;
    bool isWin = true;

    int WinStreakCount;

    [Header("SFX")]
    public AudioSource ButtonAudioSource;
    public AudioSource TimerAudioSource;
    public AudioSource WinAudioSource;
    public AudioSource LoseAudioSource;
    public AudioSource WrongPassAudioSource;
    private float NextSoundTime = 0f;

    private void Awake()
    {
        EndCanvasInfoText.text = (PlayerPrefs.GetString("Language") == "tr") ? "Tebrikler, G�rev Ba�ar�l�." : "Congratulations, Mission Completed.";
        ConfirmButtonText.text = (PlayerPrefs.GetString("Language") == "tr") ? "Giri�" : "Confirm";
        TryAgainButton.text = (PlayerPrefs.GetString("Language") == "tr") ? "Oyna" : "Play";
        MenuButton.text = (PlayerPrefs.GetString("Language") == "tr") ? "Men�" : "Menu";
        Instance = this;
        RandomPassword = Random.Range(1000, 9999); // oyuncudan bulmas�n� isteyece�imiz �ifreyi random olarak olu�turuyoruz.
        RandomSecond = NewMascotTextManager.Instance.Second;
        CurrentTime = RandomSecond;

        if (LoadImage != null)
        {
            animator = LoadImage.GetComponent<Animator>();
        }
        if (EndCanvasImage != null)
        {
            endAnimator = EndCanvasImage.GetComponent<Animator>();
        }
    }


    void Start()
    {
        WinStreakCount = PlayerPrefs.GetInt("WinStreak");
        PlayerPrefs.SetInt("WinStreak", 0);
        PasswordText.text = "____";
        Debug.Log(RandomPassword);

        RealTimeTimer.instance?.StartTimerIfNeeded();
        // start Animation
        StartCoroutine(StartAnim());
    }
    private void Update()
    {
        if (isRunning == true)
        {
            CurrentTime -= Time.deltaTime;
            TimerText.text = Mathf.CeilToInt(CurrentTime).ToString();
            if (Time.time >= NextSoundTime)
            {
                TimerAudioSource.Play();
                NextSoundTime = Time.time + 1f;
            }
            if (CurrentTime <= 0)
            {
                LoseAudioSource.Play();
                EndCanvasInfoText.text = (PlayerPrefs.GetString("Language") == "tr") ? "Zaman A��ld�, G�rev Ba�ar�s�z." : "Time Exceeded, Mission Failed.";
                isWin = false;
                StartCoroutine(EndAnim());
            }
        }
    }
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

    public void ConfirmButton()
    {
        string CurrentText = PasswordText.text;
        string strRandomPassword = RandomPassword.ToString();
        int _Control = CurrentText.IndexOf('_');

        if (_Control == -1)
        {
            if (PasswordText.text == RandomPassword.ToString())
            {
                WinAudioSource.Play();
                Num1Feed.sprite = TrueImage.sprite;
                Num2Feed.sprite = TrueImage.sprite;
                Num3Feed.sprite = TrueImage.sprite;
                Num4Feed.sprite = TrueImage.sprite;
                StartCoroutine(EndAnim());
            }
            else
            {
                #region Geri bildirimler
                Control(CurrentText, strRandomPassword, 0, Num1Feed);
                Control(CurrentText, strRandomPassword, 1, Num2Feed);
                Control(CurrentText, strRandomPassword, 2, Num3Feed);
                Control(CurrentText, strRandomPassword, 3, Num4Feed);
                #endregion
                WrongPassAudioSource.Play();

                if (PlayerPrefs.GetInt("Settings1") == 1)
                {
                    Son.text = CurrentText;
                }
                else
                {
                    Son.text = "****";
                }

                if (PlayerPrefs.GetInt("Settings2") == 1)
                {
                    numpadManager.ShuffleNumpad();
                }


                PasswordText.text = "____";
            }
        }
        else
        {
            // �ifrenin her karakterini girmemi�, geri bildirim g�nder.
        }
    }

    IEnumerator StartAnim()
    {
        LoadImage.gameObject.SetActive(true);
        animator.Play("PlayStartAnim");
        yield return new WaitForSeconds(0.9f);
        LoadImage.gameObject.SetActive(false);
    }

    IEnumerator EndAnim()
    {
        // AdManager.Instance.OnGameFinished();
        isRunning = false;
        LoadImage.gameObject.SetActive(true); // oyun sahnesindeki ge�i� resmini aktif ediyoruz.
        animator.Play("PlayLoadAnim"); // ge�i� animasyonunu ba�lat�yoruz.
        yield return new WaitForSeconds(0.9f); // animasyonun bitmesini bekliyoruz.
        GameCanvas.gameObject.SetActive(false); // oyun sahnesini kapat�p
        EndCanvas.gameObject.SetActive(true); // biti� sahnesini a��yoruz.
        endAnimator.Play("PlayStartAnim"); // biti� sahnesinde giri� animasyonunu ba�lat�yoruz.
        yield return new WaitForSeconds(0.9f); // animasyonun bitmesini bekliyoruz.
        EndCanvasImage.gameObject.SetActive(false); // biti� sahnesindeki ge�i� resmini deaktif ediyoruz.

        if (isWin == true)
        {
            PlayerPrefs.SetInt("WinCount", PlayerPrefs.GetInt("WinCount") + 1);
            PlayerPrefs.SetInt("WinStreak", WinStreakCount + 1);
            PlayerPrefs.SetInt("Health", PlayerPrefs.GetInt("Health") + 1);
            PlayerPrefs.Save();

            if (PlayerPrefs.GetInt("WinStreak") > PlayerPrefs.GetInt("TopWinStreak"))
            {
                PlayerPrefs.SetInt("TopWinStreak", PlayerPrefs.GetInt("WinStreak"));
            }
        }
    }

    void Control(string Current, string RandomPass, int Index, Image ControlImage)
    {
        if (Current[Index] > RandomPass[Index])
        {
            ControlImage.sprite = DownImage.sprite;
        }
        else if (Current[Index] < RandomPass[Index])
        {
            ControlImage.sprite = UpImage.sprite;
        }
        else
        {
            ControlImage.sprite = TrueImage.sprite;
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

    public void AdsReward()
    {
        CurrentTime += 15;
    }

}