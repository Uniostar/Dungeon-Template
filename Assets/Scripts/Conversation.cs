using System.Collections;
using UnityEngine;

public class Conversation : Collidable
{
    public string message;
    public float cooldown = 0f;
    private float lastShout;

    protected override void Start()
    {
        base.Start();
        lastShout = Time.time;
    }
    protected override void OnCollide(Collider2D coll)
    {
        if ((Time.unscaledTime - lastShout) > cooldown)
        {
            lastShout = Time.time;
            GameManager.instance.ShowText(message, 25, Color.white, transform.position + new Vector3(0, 0.16f, 0), Vector3.zero, cooldown);
        }
    }
}
