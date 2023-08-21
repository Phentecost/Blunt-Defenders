using Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleCannon : TrapsFather
{
    [SerializeField] Marble Projectile;
    GameObject target;
    void Start()
    {
        this.ammo = 10;
    }

    protected override void DoSomething()
    {
        Marble bullet = Bullet_Manager.marblePool_Check ? Bullet_Manager.marblePool.Get(): Instantiate(this.Projectile, this.transform.position, Quaternion.identity);
        Vector2 EnemyLocation = this.target.transform.position;
        EnemyLocation.Normalize();
        bullet.Launch(EnemyLocation);

    }
    
}
