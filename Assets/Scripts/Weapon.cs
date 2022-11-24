using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    //Damage struct
    public int[] damagePoint;
    public float[] pushForce;

    //Swing
    private Animator animator;

    //Update
    public int weaponLevel = 0;
    private SpriteRenderer spriteRenderer;
    private float cooldown = 0.5f;
    private float lastSwing;

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.tag == "Fighter")
        {
            if (coll.name != "Player")
            {
                //Create a damage OBJECT and send it to the player
                Damage dmg = new Damage();
                dmg.damageAmount = damagePoint[weaponLevel];
                dmg.pushForce = pushForce[weaponLevel];
                dmg.origin = transform.position;

                coll.SendMessage("RecieveDamage", dmg);
            }
        }
    } 

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    protected override void Update()
    {
        base.Update();

        if (Input.GetKey(KeyCode.Space))
        {
            if (Time.time - lastSwing > cooldown)
            {
                lastSwing = Time.time;
                Swing();
            }
        }
    }
    private void Swing()
    {
        animator.SetTrigger("Swing");
    }

    public void upgradeWeapon()
    {
        if (GameManager.instance.TryUpgradeWeapon())
        {
            weaponLevel++;
            spriteRenderer.sprite = GameManager.instance.weapons[weaponLevel];
            GameManager.instance.SaveState();
        }
    }
}
