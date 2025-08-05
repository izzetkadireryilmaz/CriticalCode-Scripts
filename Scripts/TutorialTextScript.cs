using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialTextScript : MonoBehaviour
{
    public TMP_Text[] TutorialText;
    int CurrentlyOpenPage;
    public Button LeftButton, RightButton, FinishButton;

    void Start()
    {
        CurrentlyOpenPage = 0;
    }

    private void Update()
    {
        if (CurrentlyOpenPage == TutorialText.Length - 1)
        {
            RightButton.gameObject.SetActive(false);
            FinishButton.gameObject.SetActive(true);
        }
        else
        {
            FinishButton.gameObject.SetActive(false);
            RightButton.gameObject.SetActive(true);
        }

        if (CurrentlyOpenPage == 0)
        {
            LeftButton.gameObject.SetActive(false);
        }
        else
        {
            LeftButton.gameObject.SetActive(true);
        }
    }

    public void NewPage()
    {
        if (CurrentlyOpenPage == TutorialText.Length - 1)
        {
            return;
        }
        else
        {
            TutorialText[CurrentlyOpenPage].gameObject.SetActive(false);
            TutorialText[CurrentlyOpenPage + 1].gameObject.SetActive(true);
            CurrentlyOpenPage++;
        }
    }

    public void OldPage()
    {
        if (CurrentlyOpenPage == 0)
        {
            return;
        }
        else
        {
            TutorialText[CurrentlyOpenPage].gameObject.SetActive(false);
            TutorialText[CurrentlyOpenPage - 1].gameObject.SetActive(true);
            CurrentlyOpenPage--;
        }
    }
    public void FinishButtonClicked(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
        PlayerPrefs.SetInt("Tutorial", 1);
    }
}
