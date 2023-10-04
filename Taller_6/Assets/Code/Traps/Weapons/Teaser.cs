using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teaser : TrapsFather
{
    public AudioClip taser;
    GameObject target;
    [SerializeField] private ParticleSystem particulastaser;

    protected override void DoSomething()
    {
        TeaserCables Projectile = Bullet_Manager.Instance.GetCable();
        particulastaser.Play();
        Projectile.config(bullet_Damage, bullet_Power);
        Projectile.transform.position = transform.position;
        target = _Enemy_Inside[0].gameObject;
        Vector2 EnemyLocation = target.transform.position - transform.position;
        EnemyLocation.Normalize();
        Projectile.Launch(EnemyLocation);

       if (taser != null)
        {
            AudioSource.PlayClipAtPoint(taser, transform.position);
        }

    }
}
