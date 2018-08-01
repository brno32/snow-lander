using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QualityTracker : MonoBehaviour {

    [HideInInspector] static public int currentQual = 2;

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
