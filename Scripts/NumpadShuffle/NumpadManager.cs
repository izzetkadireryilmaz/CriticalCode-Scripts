using UnityEngine;
using System.Collections.Generic;

public class NumpadManager : MonoBehaviour
{
    public Transform numpadGrid;

    private List<Transform> numpadButtons = new List<Transform>();

    void Start()
    {
        // Grid altýndaki sadece numpad butonlarýný bul
        foreach (Transform child in numpadGrid)
        {
            if (child.GetComponent<NumpadButton>() != null)
            {
                numpadButtons.Add(child);
            }
        }
    }


    public void ShuffleNumpad()
    {
        List<Transform> numberButtons = new List<Transform>();
        Transform spacer = null;
        Transform backspace = null;

        foreach (Transform child in numpadGrid)
        {
            if (child.name == "GameObject")
                spacer = child;
            else if (child.name == "BackspaceButton")
                backspace = child;
            else
                numberButtons.Add(child);
        }

        for (int i = 0; i < numberButtons.Count; i++)
        {
            int rand = Random.Range(i, numberButtons.Count);
            (numberButtons[i], numberButtons[rand]) = (numberButtons[rand], numberButtons[i]);
        }
        int spacerIndex = spacer != null ? spacer.GetSiblingIndex() : -1;
        int backspaceIndex = backspace != null ? backspace.GetSiblingIndex() : -1;

        List<int> freeIndices = new List<int>();
        for (int i = 0; i < numpadGrid.childCount; i++)
        {
            if (i != spacerIndex && i != backspaceIndex)
                freeIndices.Add(i);
        }

        for (int i = 0; i < numberButtons.Count; i++)
        {
            numberButtons[i].SetSiblingIndex(freeIndices[i]);
        }
    }

}
