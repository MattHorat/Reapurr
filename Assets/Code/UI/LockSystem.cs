using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockSystem : MonoBehaviour {

    public Image[] lockImages;
    private int count;
    private GameObject[] attractors;


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
        foreach(Image lockImage in lockImages)
        {
            lockImage.color = Color.white;
            lockImage.GetComponentInChildren<Text>().text = "";        
        }
        foreach(GameObject interactableObject in attractors)
        {
            interactableObject.GetComponent<AttractorController>().hasBeenSelected = false;
        }
        count = 0;
    }

    public void ClickTry()
    {
        FindObjectOfType<ActionQueue>().NextAction();
    }
}
