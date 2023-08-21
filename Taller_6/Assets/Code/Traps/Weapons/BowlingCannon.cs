using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingCannon : TrapsFather
{
    [SerializeField] BowlingBall Projectile;
    GameObject target;
    void Start()
    {
        this.ammo = 10;
    }

    protected override void DoSomething()
    {
        BowlingBall bullet = Bullet_Manager.bowlPool_Check ? Bullet_Manager.bowlPool.Get() : Instantiate(this.Projectile, this.transform.position, Quaternion.identity);
        Vector2 EnemyLocation = this.target.transform.position;
        EnemyLocation.Normalize();
        bullet.Launch(EnemyLocation);

    }
}
