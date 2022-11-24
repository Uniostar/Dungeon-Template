using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    //Declare Variables
    public int hitPoint = 10;
    public int maxHitPoint = 10;
    public float pushRecoverySpeed = 0.2f;

    //Immunity
    public float immuneTime = 1.0f;
    public float lastImmune;

    //Push
    public Vector3 pushDirection;

    //All fighters can RecieveDamage and Die
    protected virtual void RecieveDamage(Damage dmg)
    {
        if (Time.unscaledTime - lastImmune > immuneTime)
        {
            lastImmune = Time.unscaledTime;
            hitPoint -= dmg.damageAmount;
            pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;

            GameManager.instance.ShowText("-" + dmg.damageAmount, 25, Color.red, transform.position, Vector2.zero, 1);

            if (hitPoint <= 0)
            {
                hitPoint = 0;
                Death();
            }
        }
    }
    protected virtual void Death()
    {

    }
}
