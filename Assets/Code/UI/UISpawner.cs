using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISpawner : MonoBehaviour {

    public GameObject gameUIPrefab;

    // Use this for initialization

    public void SpawnGameUI()
    {
        Instantiate(gameUIPrefab);
    }
}
