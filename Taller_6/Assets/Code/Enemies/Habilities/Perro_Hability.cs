using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;

public class Perro_Hability : MonoBehaviour
{
    private List<Enemy> enemies = new List<Enemy>();
    public float buff = 2; 
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject == transform.parent)return;
        Enemy enemy = col.GetComponent<Enemy>();
        if(enemy != null)
        {
            enemies.Add(enemy);
            enemy.Buff(buff);
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if(col.gameObject == transform.parent)return;
        Enemy enemy = col.GetComponent<Enemy>();
        if(enemy != null)
        {
            if(!enemies.Contains(enemy))
            {
                enemies.Add(enemy);
                enemy.Buff(buff);
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject == transform.parent)return;
        Enemy enemy = col.GetComponent<Enemy>();
        if(enemy != null)
        {
            enemies.Remove(enemy);
            enemy.DeBuff();
        }
    }

     void OnDisable()
    {
        foreach(Enemy enemy in enemies)
        {
            enemy.DeBuff();
        }

        enemies.Clear();
    }
}
