using UnityEngine;
using System;
using TMPro;
using UnityEngine.SceneManagement;

public class RealTimeTimer : MonoBehaviour
{
    public static RealTimeTimer instance;
    public float targetSeconds = 20f;
    public TMP_Text TimerText;

    private bool isTimerRunning = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded; // Burada abone ol!
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Aboneli�i kald�r
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        TimerText = GameObject.FindWithTag("TimerTextTag")?.GetComponent<TMP_Text>();

        int health = PlayerPrefs.GetInt("Health", 5);
        if (TimerText != null)
        {
            TimerText.gameObject.SetActive(health < 5);
        }

        // Timer �al��m�yorsa ve sa�l�k 5'ten k���kse timer ba�lat
        if (health < 5 && !isTimerRunning)
        {
            StartTimerIfNeeded();
            InvokeRepeating(nameof(CheckTimer), 0f, 1f);
            isTimerRunning = true;
        }
        else if (health >= 5 && isTimerRunning)
        {
            CancelInvoke(nameof(CheckTimer));
            isTimerRunning = false;
        }
    }

    private void Start()
    {
        // Start() i�erisinde art�k InvokeRepeating'i �a��rmana gerek yok,
        // ��nk� OnSceneLoaded sahne y�klendi�inde hallediyor.
    }

    public void StartTimerIfNeeded()
    {
        if (!PlayerPrefs.HasKey("TimerStartTime"))
        {
            PlayerPrefs.SetString("TimerStartTime", DateTime.Now.ToString());
            Debug.Log("Timer ba�lat�ld�.");
        }
    }

    void CheckTimer()
    {
        int health = PlayerPrefs.GetInt("Health", 5);

        if (health >= 5)
        {
            PlayerPrefs.DeleteKey("TimerStartTime");
            if (TimerText != null) TimerText.gameObject.SetActive(false);
            CancelInvoke(nameof(CheckTimer));
            isTimerRunning = false;
            return;
        }

        if (PlayerPrefs.HasKey("TimerStartTime"))
        {
            DateTime startTime = DateTime.Parse(PlayerPrefs.GetString("TimerStartTime"));
            TimeSpan timePassed = DateTime.Now - startTime;

            int healthToAdd = (int)(timePassed.TotalSeconds / targetSeconds);
            if (healthToAdd > 0)
            {
                int newHealth = Mathf.Min(5, health + healthToAdd);
                PlayerPrefs.SetInt("Health", newHealth);
                PlayerPrefs.Save();
                HealthSystem.instance.HealthUpdate();

                if (newHealth >= 5)
                {
                    PlayerPrefs.DeleteKey("TimerStartTime");
                    if (TimerText != null) TimerText.gameObject.SetActive(false);
                    CancelInvoke(nameof(CheckTimer));
                    isTimerRunning = false;
                    return;
                }
                else
                {
                    double leftoverSeconds = timePassed.TotalSeconds % targetSeconds;
                    DateTime newStartTime = DateTime.Now - TimeSpan.FromSeconds(leftoverSeconds);
                    PlayerPrefs.SetString("TimerStartTime", newStartTime.ToString());
                }
            }

            TimeSpan updatedPassed = DateTime.Now - DateTime.Parse(PlayerPrefs.GetString("TimerStartTime"));
            double remaining = targetSeconds - updatedPassed.TotalSeconds;
            remaining = Math.Max(0, remaining);
            TimeSpan time = TimeSpan.FromSeconds(remaining);

            if (TimerText != null)
            {
                TimerText.text = string.Format("{0:D2}:{1:D2}", time.Minutes, time.Seconds);
            }
        }
    }
}
