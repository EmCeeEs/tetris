using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if(instance == null) {
                Debug.LogError("Game Manager is NULL.");
            }
            return instance;
        }
    }

    public GameObject BoardPrefab;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(instance);

        Instantiate(BoardPrefab);
    }
}
