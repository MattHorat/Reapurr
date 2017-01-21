using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttractorController : MonoBehaviour {

    public bool hasBeenSelected;
    public string objectName;

    public void InteractAttractor()
    {
        if (!hasBeenSelected)
        {
            GameObject.FindGameObjectWithTag("MainScreen").GetComponentInChildren<GameUI>().AssignAttractor(gameObject);
            FindObjectOfType<ActionQueue>().AddAction(GetComponent<Actionable>());
            hasBeenSelected = true;
        }
    }



}
