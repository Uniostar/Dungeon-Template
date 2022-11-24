using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    public float[] weaponSpeed = { 2.5f , -2.5f };
    public float distance = 2.5f;
    public Transform[] weapons;

    private void Update()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].position = transform.position + new Vector3(-Mathf.Cos(Time.time * weaponSpeed[i]) * distance, Mathf.Sin(Time.time * weaponSpeed[i]) * distance, 0);
        }
    }
}
