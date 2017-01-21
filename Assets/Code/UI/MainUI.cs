using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour {

    public Image[] lockImages;
    private int count;
    private GameObject[] attractors;
    public GameObject panelWin;


    public void Start()
    {
        attractors = GameObject.FindGameObjectsWithTag("Attractor");
    }

    public void AssignAttractor(GameObject attractor)
    {
        lockImages[count].color = Color.green;
        lockImages[count].GetComponentInChildren<Text>().text = attractor.GetComponent<AttractorController>().objectName;
        count++;
    }

    public void ClickClear()
    {
        ResetUI();
    }

    public void ResetUI()
    {
        foreach (Image lockImage in lockImages)
        {
            lockImage.color = Color.white;
            lockImage.GetComponentInChildren<Text>().text = "";
        }
        foreach (GameObject interactableObject in attractors)
        {
            interactableObject.GetComponent<AttractorController>().hasBeenSelected = false;
        }
        count = 0;
    }

    public void ClickTry()
    {
        //do this as when you go and select a character to start the yawn
        FindObjectOfType<ActionQueue>().NextAction();
    }

    public void ShowWinScreen()
    {
        panelWin.SetActive(true);
    }

    public void ClickTryAgain()
    {
        FindObjectOfType<Level>().ResetLevel();
        panelWin.SetActive(false);
    }
}
