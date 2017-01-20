using UnityEngine;

public class SoundMovementAttractor : Actionable {
    private void Start () {
        FindObjectOfType<ActionQueue>().AddAction(this);
    }

    public override void Action()
    {
        Human[] humans = GameObject.FindObjectsOfType<Human>();
        
        foreach (Human human in humans)
        {
            human.SetMoveTarget(gameObject);
        }
    }
}
