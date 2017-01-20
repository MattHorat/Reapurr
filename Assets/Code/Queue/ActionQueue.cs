using System;
using System.Collections.Generic;
using UnityEngine;

public class ActionQueue : MonoBehaviour
{
    private List<MonoBehaviour> actionQueue = new List<MonoBehaviour>();
    private LinkedList<Actionable> actions = new LinkedList<Actionable>();

    public void AddActionable(MonoBehaviour action)
    {
        actionQueue.Add(action);
    }

    public void NextAction()
    {

        if (actions.Count == 0)
        {
            // Level is lost, ran out of actions
            FindObjectOfType<Level>().EndLevel();
            return;
        }
        actions.First.Value.Action();
        actions.RemoveFirst();
        if (actionQueue.Count == 0)
        {
            NextAction();
        }
    }

    public void MarkComplete(MonoBehaviour action)
    {
        actionQueue.Remove(action);
        if (actionQueue.Count == 0)
        {
            NextAction();
        }
    }

    public void AddYawnTarget(Human human)
    {
        actions.AddAfter(actions.First, human);
    }

    public void AddAction(Actionable action)
    {
        actions.AddLast(action);
    }
}
