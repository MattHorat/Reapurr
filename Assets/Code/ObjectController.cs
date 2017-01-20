using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectController : MonoBehaviour {

    public bool hasBeenSelected;
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
