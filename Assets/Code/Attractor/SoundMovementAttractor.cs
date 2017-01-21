using UnityEngine;

public class SoundMovementAttractor : Actionable 
{
    public override void Action()
    {
        //GetComponent<AudioSource>().Play();

        Human[] humans = GameObject.FindObjectsOfType<Human>();
        foreach (Human human in humans)
        {
            human.SetMoveTarget(gameObject);
        }
    }
}
