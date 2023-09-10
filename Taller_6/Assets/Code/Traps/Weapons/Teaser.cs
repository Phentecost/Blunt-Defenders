using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teaser : TrapsFather
{
    GameObject target;

    protected override void DoSomething()
    {
        TeaserCables Projectile = Bullet_Manager.Instance.GetCable();
        Projectile.config(bullet_Damage, bullet_Power);
        Projectile.transform.position = transform.position;
        target = _Enemy_Inside[0].gameObject;
        Vector2 EnemyLocation = target.transform.position - transform.position;
        EnemyLocation.Normalize();
        Projectile.Launch(EnemyLocation);
    }
}
