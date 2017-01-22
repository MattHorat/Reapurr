using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanAnimationController : MonoBehaviour
{
    public GameObject humanscript;


	public void TransitionToSleeping()
    {
        humanscript.GetComponent<Human>().MakeSleep();
    }
}
