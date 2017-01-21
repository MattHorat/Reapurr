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
    public Button buttonTry;
    private Color32 originalColour;

    public Image notOnImage;
    bool isMusicPlaying = true;


    public void Start()
    {
        attractors = GameObject.FindGameObjectsWithTag("Attractor");
        intialMarkerPosition = timeMarker.rectTransform.localPosition;
        originalColour = lockImages[0].color;
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
        FindObjectOfType<ActionQueue>().ResetActionQueues();
    }

    public void AssignYawn()
    {
        lockImages[count].color = Color.green;
        lockImages[count].GetComponentInChildren<Text>().text = "Yawn";
        count++;
    }

    public void ResetUI()
    {
        foreach (Image lockImage in lockImages)
        {
            lockImage.color = originalColour;
            lockImage.GetComponentInChildren<Text>().text = "";
        }
        foreach (GameObject interactableObject in attractors)
        {
            interactableObject.GetComponent<AttractorController>().hasBeenSelected = false;
        }
        timeMarker.rectTransform.localPosition = intialMarkerPosition;
        buttonTry.interactable = true;
        count = 0;
    }

    public void ClickTry()
    {
        if (FindObjectOfType<YawnController>().currentYawn != null)
        {
            buttonTry.interactable = false;
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
        Vector2 currentPosition = timeMarker.rectTransform.localPosition;
        Vector2 newPosition = new Vector2(currentPosition.x + 130, currentPosition.y);
        while(duration > elapsedTime)
        {
            timeMarker.rectTransform.localPosition = Vector2.Lerp(currentPosition, newPosition, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForSeconds(0.2F);
        FindObjectOfType<ActionQueue>().NextAction();
    }

    public void ClickSoundButton()
    {
        if (isMusicPlaying)
        {
            FindObjectOfType<AudioSource>().Pause();
            notOnImage.enabled = true;
            isMusicPlaying = false;
        }
        else
        {
            FindObjectOfType<AudioSource>().Play();
            notOnImage.enabled = false;
            isMusicPlaying = true;
        }
    }
}
