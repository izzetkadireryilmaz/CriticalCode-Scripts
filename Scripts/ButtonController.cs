using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public Image LoadImage, StoryMascot, SettingsFrame;
    Animator animator;
    public GameObject ADSPanel,MenuObject, EndObject;


    private void Awake()
    {

        if (LoadImage != null)
        {
            animator = LoadImage.GetComponent<Animator>();
        }
        if (ADSPanel == null)
        {
            return;
        }
    }

    public void Button_Clicked(string SceneName)
    {
        if (animator != null) // þimdilik sadece menüdeki play tuþu
        {
            StartCoroutine(AnimSceneLoad(SceneName));
            NewMascotTextManager.Instance.StartMethod();
        }
        else
        {
            SceneManager.LoadScene(SceneName);
        }
    }

    public void TryAgainButton(string SceneName)
    {
        StartCoroutine(AnimSceneLoad(SceneName));
        NewMascotTextManager.Instance.StartMethod();
    }

    public void Quit()
    {
        Application.Quit();
    }

    IEnumerator AnimSceneLoad(string SceneName)
    {
        if (MenuObject != null)
        {
            MenuObject.SetActive(false);
        }
        if (EndObject != null)
        {
            EndObject.SetActive(false);
        }

        if (PlayerPrefs.GetInt("Health") > 0)
        {
            StoryMascot.gameObject.SetActive(true);
            if (LoadImage != null)
            {
                LoadImage.gameObject.SetActive(true);
                animator.Play("PlayLoadAnim");
            }
            PlayerPrefs.SetInt("Health", PlayerPrefs.GetInt("Health") - 1);
            yield return new WaitForSeconds(3f);
            SceneManager.LoadScene(SceneName);
        }
        else
        {
            ADSPanel.SetActive(true);
        }
    }

    public void ADSClose()
    {
        ADSPanel.SetActive(false);
        MenuObject.SetActive(true);
    }

    public void SettingsFrameOnOffButton()
    {
        if (SettingsFrame.gameObject.activeSelf)
        {
            BouncyPanel.instance.ClosePanel(SettingsFrame.gameObject);
        }
        else
        {
            BouncyPanel.instance.OpenPanel(SettingsFrame.gameObject);
        }
    }
}
