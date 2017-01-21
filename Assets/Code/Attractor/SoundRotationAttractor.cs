using UnityEngine;

public class SoundRotationAttractor : MonoBehaviour {
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
            human.FaceTarget(gameObject);
        }
        FindObjectOfType<ActionQueue>().NextAction();
    }
}
