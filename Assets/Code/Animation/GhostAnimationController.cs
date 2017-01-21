using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAnimationController : MonoBehaviour
{
    public void GhostDisappear()
    {
        FindObjectOfType<InputController>().gameObject.GetComponent<SpriteRenderer>().enabled = false;
        FindObjectOfType<ActionQueue>().NextAction();
    }
}
