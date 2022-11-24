using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collidable : MonoBehaviour
{
    //Declare variables
    public ContactFilter2D filter;
    private BoxCollider2D boxCollider;
    private Collider2D[] hits = new Collider2D[10];

    //When the game starts
    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    //When the game is running
    protected virtual void Update()
    {
        //Collision Work
        boxCollider.OverlapCollider(filter, hits);

        //We set the hits
        for (int i = 0; i < hits.Length; i++)
        {
            //If it is not a hit then skip
            if (hits[i] == null)
            {
                continue;
            }

            //Enter a hit
            OnCollide(hits[i]);

            //Clear memory
            hits[i] = null;
        }
    }

    //When we collide/hit
    protected virtual void OnCollide(Collider2D coll)
    {
        Debug.Log("OnCollide was never implemented on "+ this.name);
    }
}
