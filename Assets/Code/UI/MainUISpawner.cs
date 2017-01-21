using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUISpawner : MonoBehaviour {

    public GameObject GameUIPrefab;


	// Use this for initialization
	private void Start()
    {
        Instantiate(GameUIPrefab);
	}
	


}
