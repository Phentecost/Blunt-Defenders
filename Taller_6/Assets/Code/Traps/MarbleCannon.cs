using Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleCannon : TrapsFather
{
    [SerializeField] Marble Projectile;
    GameObject target;
    float CDTime = 4f;
    // Start is called before the first frame update
    void Start()
    {
        this.ammo = 10;
    }

    protected override void DoSomething()
    {
        Marble bullet = Instantiate(this.Projectile, this.transform.position, Quaternion.identity);
        Vector2 EnemyLocation = this.target.GetComponent<Rigidbody2D>().position;
        EnemyLocation.Normalize();
        bullet.Launch(EnemyLocation);

    }
    
}
