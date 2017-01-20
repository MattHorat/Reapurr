using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockSystem : MonoBehaviour {

    public Image[] lockImages;
    private int count;
    List<int> objectNumbers = new List<int>();


    public void ClickObject(int objectID)
    {
        lockImages[count].color = Color.green;
        count++;
        objectNumbers.Add(objectID);
    }

    public void ClickCheck()
    {
        foreach (int objectID in objectNumbers)
        {

        }
    }
}
