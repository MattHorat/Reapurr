using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUI : MonoBehaviour {

    public GameObject GameUIPrefab;


	// Use this for initialization
	private void Start()
    {
        Instantiate(GameUIPrefab);
	}
	


}
