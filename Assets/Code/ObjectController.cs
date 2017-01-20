using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectController : MonoBehaviour {

    private float detectionRange = 2.0F;
    private bool isCloseEnough;
    public bool hasBeenSelected;
    public Text testText;
    private GameObject mainScreen;
    public string objectName;

    private void Start()
    {
        mainScreen = GameObject.FindGameObjectWithTag("MainScreen");
    }


    public void LockInObject()
    {
        if (!hasBeenSelected)
        {
            mainScreen.GetComponentInChildren<LockSystem>().ClickObject(objectName);
            hasBeenSelected = true;
        }
    }



}
