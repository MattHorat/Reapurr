using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {

    public Image[] lockImages;
    public Image[] yawnImages;
    public Image timeMarker;
    private int count;
    private GameObject[] attractors;
    public GameObject panelWin;
    private Vector2 intialMarkerPosition;


    public void Start()
    {
        attractors = GameObject.FindGameObjectsWithTag("Attractor");
        intialMarkerPosition = timeMarker.transform.position;
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
        timeMarker.transform.position = intialMarkerPosition;
        count = 0;
    }

    public void ClickTry()
    {
        if (FindObjectOfType<YawnController>().currentYawn != null)
        {
            //do this as when you go and select a character to start the yawn
            FindObjectOfType<ActionQueue>().NextAction();
        }
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

    public void StartTimer()
    {
        StartCoroutine(MoveTimer());
    }

    private IEnumerator MoveTimer()
    {
        float duration = .8F;
        float elapsedTime = 0.0F;
        Vector2 currentPosition = timeMarker.transform.position;
        Vector2 newPosition = new Vector2(currentPosition.x + 100, currentPosition.y);
        while(duration > elapsedTime)
        {
            timeMarker.transform.position = Vector2.Lerp(currentPosition, newPosition, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        FindObjectOfType<ActionQueue>().NextAction();
    }
}
