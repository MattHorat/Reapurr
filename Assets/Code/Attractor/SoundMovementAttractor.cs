﻿using UnityEngine;

public class SoundMovementAttractor : Actionable 
{
    public void Update()
    {
        GetComponent<SpriteRenderer>().sortingOrder = -(int)(transform.position.y * 100);
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
