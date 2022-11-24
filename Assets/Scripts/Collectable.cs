using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : Collidable
{
    //Declare variables
    protected bool collected;

    //We see what we have collided with
    protected override void OnCollide(Collider2D coll)
    {
        //If we collided with player
        if (coll.name == "Player")
        {
            OnCollect();
        }
    }
    //When we collect the loot
    protected virtual void OnCollect()
    {
        collected = true;
    }
}
