using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBox : Collidable
{
    //Damage
    public int damage = 1;
    public float pushForce = 5;

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.tag == "Fighter" && coll.name == "Player")
        {
            //Create a damage function and send it to the player
            Damage dmg = new Damage();
            dmg.damageAmount = damage;
            dmg.pushForce = pushForce;
            dmg.origin = transform.position;

            coll.SendMessage("RecieveDamage", dmg);
        }
    }
}
