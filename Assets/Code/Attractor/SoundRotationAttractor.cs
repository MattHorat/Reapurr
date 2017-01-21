using UnityEngine;

public class SoundRotationAttractor : Actionable {
    public override void Action()
    {
        Human[] humans = GameObject.FindObjectsOfType<Human>();
        foreach(Human human in humans)
        {
            human.FaceTarget(gameObject);
        }
    }
}
