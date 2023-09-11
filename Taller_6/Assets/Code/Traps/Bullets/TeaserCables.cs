using Enemies;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TeaserCables : BulletFather
{
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy e = collision.collider.GetComponent<Enemy>();
        if ( e!=null )
        {
            e.OnTouched(-Damage);
            e.stun(1);
        }
        
        OnDestroy(this);
    }
}
