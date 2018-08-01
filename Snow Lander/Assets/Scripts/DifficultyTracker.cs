using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyTracker : MonoBehaviour {

    [HideInInspector] static public bool isEasy = false;

    private void Awake()
    {
        int numOfTrackers = FindObjectsOfType<GameObject>().Length;

        if (numOfTrackers > 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }
}
