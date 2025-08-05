using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSoundScript : MonoBehaviour
{
    private static GameSoundScript instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
}
