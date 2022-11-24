using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingFountain : Collidable
{
    public int healingAmount = 1;

    public float healCooldown = 0.5f;
    public float lastHealed;
    protected override void OnCollide(Collider2D coll)
    {
        if (Time.unscaledTime - lastHealed >= healCooldown)
        {
            if (coll.name == "Player")
            {
                lastHealed = Time.unscaledTime;
                GameManager.instance.player.Heal(healingAmount);
            }
        }
    }
}
