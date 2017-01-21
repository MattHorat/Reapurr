using System.Collections;
using UnityEngine;

public class SoundRotationAttractor : Actionable
{
    public override void Action()
    {
        GetComponent<AudioSource>().Play();

        GetComponentInChildren<Animator>().enabled = true;

        Human[] humans = GameObject.FindObjectsOfType<Human>();
        foreach(Human human in humans)
        {
            human.FaceTarget(gameObject);
        }
        StartCoroutine(StopAnimation());
    }

    private IEnumerator StopAnimation()
    {
        yield return new WaitForSeconds(0.8F);
        GetComponentInChildren<Animator>().enabled = false;
    }
}
