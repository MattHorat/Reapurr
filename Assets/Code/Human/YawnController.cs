using UnityEngine;

public class YawnController : Actionable
{
    public Human currentYawn;

    public override void Action()
    {
        if (!currentYawn.asleep)
        {
            Yawn yawn = Instantiate(currentYawn.yawnPrefab, currentYawn.transform.position, currentYawn.transform.rotation).GetComponent<Yawn>();
            FindObjectOfType<ActionQueue>().AddActionable(yawn);
            yawn.creator = currentYawn.gameObject;
            currentYawn.asleep = true;
        }
    }
}
