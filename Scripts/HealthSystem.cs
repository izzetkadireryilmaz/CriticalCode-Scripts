using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public static HealthSystem instance;
    public List<Image> Health = new List<Image>();
    public TMP_Text TimerText;
    bool started = false;

    private void Awake()
    {
        instance = this;
        if (!PlayerPrefs.HasKey("Health"))
        {
            PlayerPrefs.SetInt("Health", 5);
        }
        if (Health.Count > 0)
        {
            for (int i = 0; i < Health.Count; i++)
            {
                Health[i].gameObject.SetActive(false);
            }
        }

        Debug.Log(PlayerPrefs.GetInt("Health"));

    }
    void Start()
    {
        HealthUpdate();
    }

    private void Update()
    {
        if (PlayerPrefs.GetInt("Health") < 5)
        {
            if (TimerText != null && started == false)
            {
                TimerText.gameObject.SetActive(true);
                started = true;
            }
        }
        else
        {
            /*
            TimerText.gameObject.SetActive(false);
            started = false;
            */
        }
    }

    public void HealthUpdate()
    {
        int currentHealth = PlayerPrefs.GetInt("Health");

        if (Health != null)
        {
            for (int i = 0; i < Health.Count; i++)
            {
                if (i < currentHealth)
                    Health[i].gameObject.SetActive(true);
                else
                    Health[i].gameObject.SetActive(false);
            }
        }
        else
        {
            return;
        }
    }   
}
