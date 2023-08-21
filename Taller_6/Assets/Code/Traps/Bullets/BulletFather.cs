using Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFather : MonoBehaviour
{
    protected Rigidbody2D RB2D;
    protected float Damage;
    protected float Power;
    private void Awake()
    {
        RB2D = GetComponent<Rigidbody2D>();
    }

    protected void Update()
    {
        if (transform.position.magnitude > 1000f)
            Destroy(gameObject);
    }

    public void Launch(Vector2 Direction)
    {
        RB2D.AddForce(Direction * Power);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy e = collision.collider.GetComponent<Enemy>();
        if ( e!=null )
        {
            e.OnTouched();
        }
        Destroy(gameObject);
    }



}
