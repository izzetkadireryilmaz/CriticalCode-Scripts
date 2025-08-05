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
    public TMP_Text PasswordText, Son, EndCanvasInfoText, ConfirmButtonText, TryAgainButton, MenuButton; // info texti þu an kullanmýyoruz ama oyuna zaman eklenince kaybetme de ekleneceði için orada kaybetme bilgliendirmesi için kullanacaðýz.
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
        EndCanvasInfoText.text = (PlayerPrefs.GetString("Language") == "tr") ? "Tebrikler, Görev Baþarýlý." : "Congratulations, Mission Completed.";
        ConfirmButtonText.text = (PlayerPrefs.GetString("Language") == "tr") ? "Giriþ" : "Confirm";
        TryAgainButton.text = (PlayerPrefs.GetString("Language") == "tr") ? "Oyna" : "Play";
        MenuButton.text = (PlayerPrefs.GetString("Language") == "tr") ? "Menü" : "Menu";
        Instance = this;
        RandomPassword = Random.Range(1000, 9999); // oyuncudan bulmasýný isteyeceðimiz þifreyi random olarak oluþturuyoruz.
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
                EndCanvasInfoText.text = (PlayerPrefs.GetString("Language") == "tr") ? "Zaman Aþýldý, Görev Baþarýsýz." : "Time Exceeded, Mission Failed.";
                isWin = false;
                StartCoroutine(EndAnim());
            }
        }
    }
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
            // þifrenin her karakterini girmemiþ, geri bildirim gönder.
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
        LoadImage.gameObject.SetActive(true); // oyun sahnesindeki geçiþ resmini aktif ediyoruz.
        animator.Play("PlayLoadAnim"); // geçiþ animasyonunu baþlatýyoruz.
        yield return new WaitForSeconds(0.9f); // animasyonun bitmesini bekliyoruz.
        GameCanvas.gameObject.SetActive(false); // oyun sahnesini kapatýp
        EndCanvas.gameObject.SetActive(true); // bitiþ sahnesini açýyoruz.
        endAnimator.Play("PlayStartAnim"); // bitiþ sahnesinde giriþ animasyonunu baþlatýyoruz.
        yield return new WaitForSeconds(0.9f); // animasyonun bitmesini bekliyoruz.
        EndCanvasImage.gameObject.SetActive(false); // bitiþ sahnesindeki geçiþ resmini deaktif ediyoruz.

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

    public void AdsReward()
    {
        CurrentTime += 15;
    }

}