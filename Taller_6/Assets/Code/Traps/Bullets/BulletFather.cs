using Enemies;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFather : MonoBehaviour
{
    protected Rigidbody2D RB2D;
    protected int Damage;
    protected float Power;
    
    protected Action<BulletFather> OnDestroy;
    public void config(int Damage, float Power)
    {
        this.Damage = Damage;
        this.Power = Power;
        RB2D = GetComponent<Rigidbody2D>();
    }

    public void config(Action<BulletFather> OnDestroy)
    {
        this.OnDestroy = OnDestroy;
    }

    protected void Update()
    {
        if (transform.position.magnitude > 1000f)
            OnDestroy(this);
    }

    public void Launch(Vector2 Direction)
    {
        RB2D.AddForce(Direction * Power, ForceMode2D.Impulse);
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy e = collision.collider.GetComponent<Enemy>();
        if ( e!=null )
        {
            e.OnTouched(-Damage);
        }
        
        OnDestroy(this);
    }



}
