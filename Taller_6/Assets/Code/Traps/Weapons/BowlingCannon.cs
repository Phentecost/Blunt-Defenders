using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingCannon : TrapsFather
{
    public AudioClip Bolos;

    [SerializeField] private ParticleSystem ParticulasBolos;

    //[SerializeField] BowlingBall Projectile;
    GameObject target;
    //private Animator animator;

    protected override void DoSomething()
    {
        BowlingBall Projectile =  Bullet_Manager.Instance.GetBowlingBall();
        Projectile.config(bullet_Damage,bullet_Power);
        Projectile.transform.position = transform.position;
        ParticulasBolos.Play();
        target = _Enemy_Inside[0].gameObject;
        Vector2 EnemyLocation = target.transform.position - transform.position;
        EnemyLocation.Normalize();
        Projectile.Launch(EnemyLocation);


         if (Bolos != null)
        {
            AudioSource.PlayClipAtPoint(Bolos, transform.position);
        }
        //animator = GetComponent<Animator>();
        // animator.SetTrigger("LanzarProyectil");
    }

}