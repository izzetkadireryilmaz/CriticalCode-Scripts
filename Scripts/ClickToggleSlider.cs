using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickToggleSlider : MonoBehaviour, IPointerClickHandler
{
    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        slider.interactable = false; // elle sürüklemeyi kapatýyoruz



        if (!PlayerPrefs.HasKey("Settings1"))
        {
            PlayerPrefs.SetInt("Settings1", 1);
        }
        if (!PlayerPrefs.HasKey("Settings2"))
        {
            PlayerPrefs.SetInt("Settings2", 0);
        }
        slider.value = PlayerPrefs.GetInt(SettingsNumber) == 1 ? 0.5f : 0f;
    }

    [SerializeField] private string SettingsNumber = "";
    public void OnPointerClick(PointerEventData eventData)
    {
        if (Mathf.Approximately(slider.value, 0f))
        {
            slider.value = 0.5f;
            PlayerPrefs.SetInt(SettingsNumber, 1);
            PlayerPrefs.Save();
        }
        else
        {
            slider.value = 0f;
            PlayerPrefs.SetInt(SettingsNumber, 0);
            PlayerPrefs.Save();
        }

        // Burada istersen bir event çaðýrabilirsin
        Debug.Log("Slider state: " + slider.value);
    }
}
