using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialControl : MonoBehaviour
{
    private void Awake()
    {
        //PlayerPrefs.SetInt("Tutorial", 0);
        if (!PlayerPrefs.HasKey("Language"))
        {
            PlayerPrefs.SetString("Language", "tr");
        }
    }
    void Start()
    {
        if (!PlayerPrefs.HasKey("Tutorial"))
        {
            PlayerPrefs.SetInt("Tutorial", 0);
            SceneManager.LoadScene("TutorialScene");
        }
        else if (PlayerPrefs.GetInt("Tutorial") == 1)
        {
            SceneManager.LoadScene("MenuScene");
        }
        else
        {
            SceneManager.LoadScene("TutorialScene");
        }
    }
}
