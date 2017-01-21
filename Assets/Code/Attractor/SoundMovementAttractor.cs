using System.Collections;
using UnityEngine;

public class SoundMovementAttractor : Actionable 
{
    public GameObject radioOnPrefab;
    public GameObject radioOffPrefab;

    public override void Action()
    {
        GetComponent<AudioSource>().Play();

        if(GetComponent<AttractorController>().objectName == "Radio")
        {
            RadioOn();
        }
        else
        {
            GetComponentInChildren<Animator>().enabled = true;
            StartCoroutine(StopAnimation());
        }

        Human[] humans = GameObject.FindObjectsOfType<Human>();
        foreach (Human human in humans)
        {
            human.SetMoveTarget(gameObject);
        }
    }

    private IEnumerator StopAnimation()
    {
        yield return new WaitForSeconds(0.8F);
        GetComponentInChildren<Animator>().enabled = false;
    }

    private void RadioOn()
    {

        radioOffPrefab.gameObject.SetActive(false);
        radioOnPrefab.gameObject.SetActive(true);

        radioOnPrefab.GetComponent<SpriteRenderer>().enabled = true;
        radioOnPrefab.GetComponent<Animator>().enabled = true;

        StartCoroutine(StopRadio());

    }

    private IEnumerator StopRadio()
    {
        yield return new WaitForSeconds(0.8F);
        RadioOff();
    }

    private void RadioOff()
    {
        radioOffPrefab.gameObject.SetActive(true);
        radioOnPrefab.gameObject.SetActive(false);
        radioOnPrefab.GetComponent<SpriteRenderer>().enabled = false;
        radioOnPrefab.GetComponent<SpriteRenderer>().enabled = false;
    }
}
