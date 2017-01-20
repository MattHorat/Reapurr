using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttractorController : MonoBehaviour {

    public bool hasBeenSelected;
    private GameObject mainScreen;
    public string objectName;

    private void Start()
    {
        mainScreen = GameObject.FindGameObjectWithTag("MainScreen");
    }


    public void InteractAttractor()
    {
        if (!hasBeenSelected)
        {
            mainScreen.GetComponentInChildren<LockSystem>().AssignAttractor(gameObject);
            FindObjectOfType<ActionQueue>().AddAction(GetComponent<Actionable>());
            hasBeenSelected = true;
        }
    }



}
