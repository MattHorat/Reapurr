using UnityEngine;

public class YawnController : Actionable
{
    public Human currentYawn;
    public AudioSource yawnSound;

    public override void Action()
    {
        if (!currentYawn.asleep)
        {
            yawnSound.Play();
            Yawn yawn = Instantiate(currentYawn.yawnPrefab, currentYawn.transform.position, currentYawn.transform.rotation).GetComponent<Yawn>();
            FindObjectOfType<ActionQueue>().AddActionable(yawn);
            yawn.creator = currentYawn.gameObject;
            currentYawn.asleep = true;
        }
    }
}
