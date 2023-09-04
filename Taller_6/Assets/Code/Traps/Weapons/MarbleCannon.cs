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
        //this.ammo = 10;
    }

    protected override void DoSomething()
    {
        Projectile = Bullet_Manager.Instance.GetMarble();
        Vector2 EnemyLocation = this.target.transform.position;
        EnemyLocation.Normalize();
        Projectile.Launch(EnemyLocation);

    }
    
}
