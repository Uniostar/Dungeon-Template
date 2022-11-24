using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mover
{
    //Experience
    public int xpValue = 1;

    //Logic
    public float triggerLength = 0.5f;
    public float chaseLength = 5f;
    private bool chasing;
    private bool collidingWithPlayer;
    private Transform playerTransform;
    private Vector3 startingPosition;

    //Hitbox
    public ContactFilter2D filter;
    private BoxCollider2D hitBox;
    private Collider2D[] hits = new Collider2D[10];

    protected override void Start()
    {
        base.Start();
        playerTransform = GameManager.instance.player.transform;
        startingPosition = transform.position;
        hitBox = transform.GetChild(0).gameObject.GetComponent<BoxCollider2D>();
    }
    private void FixedUpdate()
    {
        if (Vector3.Distance(playerTransform.position, startingPosition) < chaseLength)
        {
            //Is player in the range?
            if (Vector3.Distance(playerTransform.position, startingPosition) < triggerLength)
                chasing = true;

            //If we are chasing the player
            if (chasing)
            {
                if (!collidingWithPlayer)
                {
                    UpdateMotor((playerTransform.position - transform.position).normalized);
                }
            }
        }
        else
        {
            UpdateMotor(startingPosition - transform.position);
            chasing = false;
        }

        //Check for overlaps
        collidingWithPlayer = false;
        boxCollider.OverlapCollider(filter, hits);

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
    
    protected override void UpdateMotor(Vector3 input)
    {
        //Rests Move Delta
        moveDelta = new Vector3(input.x * xSpeed, input.y * ySpeed, 0);

        //Swap Sprite, wether you are going left or right
        if (chasing)
        {
            if ((playerTransform.position - transform.position).x > 0)
                transform.localScale = new Vector3(-1, 1, 1);
            else if ((playerTransform.position - transform.position).x < 0)
                transform.localScale = Vector3.one;
        }
        else
        {
            if ((startingPosition - transform.position).x > 0)
                transform.localScale = new Vector3(-1, 1, 1);
            else if ((startingPosition - transform.position).x < 0)
                transform.localScale = Vector3.one;
        }

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
    protected override void Death()
    {
        GameManager.instance.GrantXP(xpValue);
        GameManager.instance.ShowText("+" + xpValue + "xp", 25, Color.magenta, transform.position, Vector3.up*40, 1f);
        GameManager.instance.SaveState();
        Destroy(gameObject);
    }
    //When we collide/hit
    protected virtual void OnCollide(Collider2D coll)
    {
        if (coll.tag == "Fighter" && coll.name == "Player")
        {
            collidingWithPlayer = true;
        }
    }
}
