using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : Fighter
{
    //Delcare Variables
    protected BoxCollider2D boxCollider;
    protected Vector3 moveDelta;
    protected RaycastHit2D hit;
    public float ySpeed = 0.65f;
    public float xSpeed = 0.75f;

    //When the game starts
    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    protected virtual void UpdateMotor(Vector3 input)
    {
        //Rests Move Delta
        moveDelta = new Vector3(input.x * xSpeed, input.y * ySpeed, 0);

        //Swap Sprite, wether you are going left or right
        if (moveDelta.x > 0)
            transform.localScale = Vector3.one;
        else if (moveDelta.x < 0)
           transform.localScale = new Vector3(-1, 1, 1);

        //Get Pushed
        moveDelta += pushDirection;

        //Reduce push force every frams
        pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRecoverySpeed);

        //Make sure we can move in the direction and check this via a raycast
        //In the up and down direction
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        //If we can move
        if (hit.collider == null)
        {
            //Move The Player
            transform.Translate(new Vector3(0, moveDelta.y * Time.deltaTime, 0));
        }
        //In the side by side direction
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        //If we can move
        if (hit.collider == null)
        {
            //Move The Player
            transform.Translate(new Vector3(moveDelta.x * Time.deltaTime, 0, 0));
        }
    }
}
