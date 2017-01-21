using UnityEngine;

public class SoundRotationAttractor : MonoBehaviour {
    public void Attract()
    {
        Human[] humans = GameObject.FindObjectsOfType<Human>();
        foreach(Human human in humans)
        {
            human.FaceTarget(gameObject);
        }
        FindObjectOfType<ActionQueue>().NextAction();
    }
}
