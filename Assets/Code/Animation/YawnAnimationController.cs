using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YawnAnimationController : MonoBehaviour
{
    public Animator animator;
    public GameObject hair;
    public Sprite bedSprite;

    public void YawnDone()
    {
        GetComponent<Animator>().Stop();
        transform.parent.GetComponent<YawnAnimationController>().StartAnimator();
        hair.SetActive(false);
        gameObject.SetActive(false);
    }

    public void StartAnimator()
    {
        animator.enabled = true;
    }

    public void PoofDone()
    {
        GetComponent<Animator>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = bedSprite;
    }
}
