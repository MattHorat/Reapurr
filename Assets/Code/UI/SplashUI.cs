using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashUI : MonoBehaviour {

    public GameObject player;

	private void Start()
    {
        GetComponent<Canvas>().worldCamera = FindObjectOfType<Camera>();
	}

    public void ClickStartGame()
    {
        FindObjectOfType<UISpawner>().SpawnGameUI();
        player.SetActive(true);
        Destroy(gameObject);
    }
	
}
