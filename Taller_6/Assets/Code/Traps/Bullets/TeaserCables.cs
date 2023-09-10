using Enemies;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TeaserCables : BulletFather
{
    [SerializeField] float ShockPower;
    private void Start()
    {
        this.Damage = 2;
        ShockPower = 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy e = collision.GetComponent<Enemy>();
        if (e != null)
        {
            e.onTeaserHit(ShockPower);
        }

    }
}
