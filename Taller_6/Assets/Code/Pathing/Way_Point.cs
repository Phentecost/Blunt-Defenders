using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemies;

public class Way_Point : MonoBehaviour
{
    public bool Door;
    public int life;
    public List<Enemy> atk_Enemies;
    private float scalar = 0.2f;
    private float timer = 0;

    void Update()
    {
        if(!Door || Game_Manager._Current_Game_State == Game_Manager.Game_State.Preparation) return;

        if(life <= 0)OnBreakDown();
    }

    private void OnBreakDown()
    {
        Door = false;
        foreach(Enemy enemy in atk_Enemies)
        {
            enemy.Door_Break_Down(timer);
            timer += scalar;
        }

        atk_Enemies.Clear();
    }

    public void Register(Enemy enemy)
    {
        atk_Enemies.Add(enemy);
    }
}
