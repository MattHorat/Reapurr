using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashUI : MonoBehaviour 
{
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
        Level level = FindObjectOfType<Level>();
        FindObjectOfType<GameUI>().DisplayMessage(level.levels[level.currentLevel].GetComponent<TutorialMessage>().message);
    }
	
}
