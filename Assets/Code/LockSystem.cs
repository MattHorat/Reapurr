using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockSystem : MonoBehaviour {

    public Image[] lockImages;
    private int count;
    public GameObject[] interactableObjects;


    public void ClickObject(string objectName)
    {
        lockImages[count].color = Color.green;
        lockImages[count].GetComponentInChildren<Text>().text = objectName;
        count++;
        //objectNumbers.Add(objectID);
    }

    public void ClickClear()
    {
        foreach(Image lockImage in lockImages)
        {
            lockImage.color = Color.white;
            lockImage.GetComponentInChildren<Text>().text = "";        
        }
        foreach(GameObject interactableObject in interactableObjects)
        {
            interactableObject.GetComponent<ObjectController>().hasBeenSelected = false;
        }
        count = 0;
    }
}
