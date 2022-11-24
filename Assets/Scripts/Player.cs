using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Player : Mover
{
    private bool isAlive;
    protected override void Start()
    {
        base.Start();
        isAlive = true;
    }
    protected override void Death()
    {
        isAlive = false;
        GameManager.instance.deathMenu.SetTrigger("Show");
    }
    protected override void RecieveDamage(Damage dmg)
    {
        if (!isAlive)
            return;

        base.RecieveDamage(dmg);
        GameManager.instance.OnHPChanged();
        GameManager.instance.SaveState();
    }

    private void FixedUpdate()
    {
        //Get Input from the keyboard
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if (isAlive)
            UpdateMotor(new Vector3(x, y, 0));
    }

    public void OnLevelUp()
    {
        maxHitPoint++;
        hitPoint = maxHitPoint;
    }
    public void SetLevel(int level)
    {
        for (int i = 0; i < level; i++)
        {
            OnLevelUp();
        }
    }
    public void Heal(int healingAmount)
    {
        if (hitPoint == maxHitPoint)
            return;

        hitPoint += healingAmount;

        if (hitPoint > maxHitPoint)
            hitPoint = maxHitPoint;

        GameManager.instance.ShowText("+" + healingAmount, 25, Color.green, transform.position, Vector3.up * 50, 3);
        GameManager.instance.OnHPChanged();
        GameManager.instance.SaveState();
    }
    public void Respawn()
    {
        Heal(maxHitPoint);
        isAlive = true;
        immuneTime = Time.time;
        pushDirection = Vector3.zero;
    }
}