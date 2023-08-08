using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int health;

    private void Start()
    {
        health = maxHealth;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PJ")) 
        {
            TakeDamage(10); 
        }
    }

    private void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            health = 0;
           
            Destroy(gameObject);
        }

        Debug.Log("Vida actual: " + health);
    }
}
