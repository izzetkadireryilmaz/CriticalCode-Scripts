using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class MascotTextManager : MonoBehaviour
{
    public static MascotTextManager Instance;
    private List<string> textTemplates = new List<string>();
    public TMP_Text Mascot_Text;
    public int Second;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        Second = Random.Range(17, 23);
        LoadCSV();
        ShowRandomMascotText(Second);
    }

    void LoadCSV()
    {
        TextAsset csvFile = Resources.Load<TextAsset>("MascotTexts"); // Assets/Resources/MascotTexts.csv
        string[] lines = csvFile.text.Split('\n');

        for (int i = 1; i < lines.Length; i++) // 0. satýr baþlýk
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;
            string[] parts = lines[i].Split(',');
            textTemplates.Add(parts[1].Trim());
        }
    }
    void ShowRandomMascotText(int seconds)
    {
        int index = Random.Range(0, textTemplates.Count);
        string text = textTemplates[index];
        string finalText = text.Replace("{seconds}", seconds.ToString());

        Mascot_Text.text = finalText;
    }
}
