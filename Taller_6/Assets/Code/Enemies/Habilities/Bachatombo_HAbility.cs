using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemies;

public class Bachatombo_HAbility : MonoBehaviour
{
    [SerializeField]private List<Enemy> enemies = new List<Enemy>();
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject == transform.parent)return;
        Enemy enemy = col.GetComponent<Enemy>();
        if(enemy is Bachitombo) return;
        if(enemy != null)
        {
            enemies.Add(enemy);
            if(!enemy.Protection_Buff)enemy.Buff(true);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject == transform.parent)return;
        Enemy enemy = col.GetComponent<Enemy>();
        if(enemy != null)
        {
            enemies.Remove(enemy);
            enemy.Buff(false);
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if(col.gameObject == transform.parent)return;
        Enemy enemy = col.GetComponent<Enemy>();
        if(enemy is Bachitombo) return;
        if(enemy != null)
        {
            if(!enemies.Contains(enemy))
            {
                enemies.Add(enemy);
                if(!enemy.Protection_Buff)enemy.Buff(true);
            }
        }
    }

     void OnDisable()
    {
        foreach(Enemy enemy in enemies)
        {
            enemy.Buff(false);
        }

        enemies.Clear();
    }
}
