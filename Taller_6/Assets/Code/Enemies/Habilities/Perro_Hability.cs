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
        if(enemy is Tombo_Con_Perro)return;
        if(enemy != null)
        {
            enemies.Add(enemy);
            if(!enemy.Speed_Buff)enemy.Buff(buff);
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if(col.gameObject == transform.parent)return;
        Enemy enemy = col.GetComponent<Enemy>();
        if(enemy is Tombo_Con_Perro)return;
        if(enemy != null)
        {
            if(!enemies.Contains(enemy))
            {
                enemies.Add(enemy);
                if(!enemy.Speed_Buff)
                {
                    enemy.Buff(buff);
                }
                
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject == transform.parent)return;
        Enemy enemy = col.GetComponent<Enemy>();
        if(enemy is Tombo_Con_Perro)return;
        if(enemy != null)
        {
            enemies.Remove(enemy);
            if(enemy.Speed_Buff)enemy.DeBuff();
        }
    }

     void OnDisable()
    {
        foreach(Enemy enemy in enemies)
        {
            if(enemy.Speed_Buff)enemy.DeBuff();
        }

        enemies.Clear();
    }
}
