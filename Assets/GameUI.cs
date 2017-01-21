using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {

    public Image[] lockImages;
    public Image[] yawnImages;
    public Image timeMarker;
    private int count;
    public GameObject panelWin;
    private Vector2 intialMarkerPosition;
    public Button buttonTry;
    private Color32 originalColour;

    public Image notOnImage;
    private GameObject musicObject;
    bool isMusicPlaying = true;


    public void Start()
    {
        intialMarkerPosition = timeMarker.rectTransform.localPosition;
        originalColour = lockImages[0].color;
        musicObject = GameObject.FindGameObjectWithTag("Music");
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
        GameObject[] attractors = GameObject.FindGameObjectsWithTag("Attractor");
        foreach (GameObject interactableObject in attractors)
        {
            interactableObject.GetComponent<AttractorController>().hasBeenSelected = false;
        }
        timeMarker.rectTransform.localPosition = intialMarkerPosition;
        count = 0;
        Animator anim = FindObjectOfType<Animator>();
        if(anim.GetBool("isPossessing"))
        {
            anim.SetBool("isPossessing", false);
            anim.SetTrigger("Reset");
        }
        anim.GetComponent<SpriteRenderer>().enabled = true;
        FindObjectOfType<InputController>().isPossessing = false;
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

    public void ClickPlayNextLevel()
    {
        FindObjectOfType<Level>().PlayNextLevel();
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
            musicObject.GetComponent<AudioSource>().Pause();
            notOnImage.enabled = true;
            isMusicPlaying = false;
        }
        else
        {
            musicObject.GetComponent<AudioSource>().Play();
            notOnImage.enabled = false;
            isMusicPlaying = true;
        }
    }
}
