﻿using System.Collections;
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
        FindObjectOfType<ActionQueue>().AddActionable(this);
    }

    private IEnumerator StopAnimation()
    {
        yield return new WaitForSeconds(0.7F);
        GetComponent<AudioSource>().Stop();
        GetComponentInChildren<Animator>().enabled = false;

        Human[] humans = GameObject.FindObjectsOfType<Human>();
        foreach (Human human in humans)
        {
            human.SetMoveTarget(gameObject);
        }
        FindObjectOfType<ActionQueue>().MarkComplete(this);
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
        GetComponent<AudioSource>().Stop();

        Human[] humans = GameObject.FindObjectsOfType<Human>();
        foreach (Human human in humans)
        {
            human.SetMoveTarget(gameObject);
        }
        FindObjectOfType<ActionQueue>().MarkComplete(this);
    }
}
