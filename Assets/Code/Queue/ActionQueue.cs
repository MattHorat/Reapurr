using System.Collections.Generic;
using UnityEngine;

public class ActionQueue : MonoBehaviour
{
    private Queue<Actionable> actionQueue = new Queue<Actionable>();
    private Human nextYawn;
    private bool yawn = true;

    public void AddActionable(Actionable action)
    {
        actionQueue.Enqueue(action);
    }

    public void NextAction()
    {
        if (yawn)
        {
            if (nextYawn.asleep)
            {
                // Level is lost, we either propagated to a sleeping yawn target, or we didn't hit anyone
                FindObjectOfType<Level>().EndLevel();
                return;
            }
            nextYawn.Yawn();
        }
        else
        {
            actionQueue.Dequeue().Action();
        }
        yawn = !yawn;
    }

    public void AddYawnTarget(Human human)
    {
        nextYawn = human;
    }
}
