using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable
{
    //Declare Variables
    public Sprite emptyChest;
    public int euroGranted = 5;

    //We override the function OnCollect() to grant the player euros
    protected override void OnCollect()
    {
        //If the player hasn't collected the treasure yet
        if (!collected)
        {
            collected = true;
            gameObject.GetComponent<SpriteRenderer>().sprite = emptyChest;
            GameManager.instance.euros += euroGranted;
            GameManager.instance.SaveState();
            GameManager.instance.ShowText("+" + euroGranted + " euros", 25, Color.yellow, transform.position, transform.up * 50, 2);
        }
    }
}
