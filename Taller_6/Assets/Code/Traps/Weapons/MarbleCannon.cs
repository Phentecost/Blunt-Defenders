using Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleCannon : TrapsFather
{
    public AudioClip canica; 
    GameObject target;
    [SerializeField] private ParticleSystem particulasdisparo;

    
    protected override void DoSomething()
    {
        Marble Projectile = Bullet_Manager.Instance.GetMarble();
        particulasdisparo.Play();
        Projectile.config(bullet_Damage, bullet_Power);
        Projectile.transform.position = transform.position;
        target = _Enemy_Inside[0].gameObject;
        Vector2 EnemyLocation = target.transform.position - transform.position;
        EnemyLocation.Normalize();
        Projectile.Launch(EnemyLocation);
       
       

        
        if (canica != null)
        {
            AudioSource.PlayClipAtPoint(canica, transform.position);
            
        }
    }
}
