using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class NewMascotTextManager : MonoBehaviour
{
    public static NewMascotTextManager Instance;

    private List<string> textTemplatesTR = new List<string>();
    private List<string> textTemplatesEN = new List<string>();

    public TMP_Text Mascot_Text;
    public int Second;

    // Örnek: bu deðeri ayarlayarak dil kontrolü yapabilirsin
    public string currentLanguage; // veya "en"

    private void Awake()
    {
        currentLanguage = PlayerPrefs.GetString("Language");
        Instance = this;
    }

    void Start()
    {
        Second = Random.Range(17, 23);
        
    }

    public void StartMethod()
    {
        LoadCSV();
        ShowRandomMascotText(Second);
    }
    void LoadCSV()
    {
        TextAsset csvFile = Resources.Load<TextAsset>("DualMascotTexts");
        string[] lines = csvFile.text.Split('\n');

        for (int i = 1; i < lines.Length; i++) // 0. satýr baþlýk
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;
            string[] parts = lines[i].Split(',');

            if (parts.Length >= 3)
            {
                textTemplatesTR.Add(parts[1].Trim());
                textTemplatesEN.Add(parts[2].Trim());
            }
        }
    }

    void ShowRandomMascotText(int seconds)
    {
        int index = Random.Range(0, textTemplatesTR.Count);
        string selectedText;

        if (currentLanguage == "en")
            selectedText = textTemplatesEN[index];
        else
            selectedText = textTemplatesTR[index];

        string finalText = selectedText.Replace("{seconds}", seconds.ToString());
        Mascot_Text.text = finalText;
    }
}
