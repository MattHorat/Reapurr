using UnityEngine;

public class SoundMovementAttractor : MonoBehaviour {
    private void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attract();
        }
    }

    public void Attract()
    {
        Human[] humans = GameObject.FindObjectsOfType<Human>();
        foreach(Human human in humans)
        {
            human.SetMoveTarget(gameObject);
        }
    }
}
