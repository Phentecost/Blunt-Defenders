using System;
using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;
using UnityEngine.UI;

public class Camera_UI : MonoBehaviour
{
    [SerializeField] private Image image;
    private List<Enemy> enemies_Inside;

    void Start()
    {
        enemies_Inside = new List<Enemy>();
    }

    void Update()
    {
        if(Game_Manager._Current_Game_State != Game_Manager.Game_State.Defending) return;
        
        if(enemies_Inside.Count != 0)
        {
            image.color = Color.red;
        }
        else
        {
            image.color = Color.white;
        }
    }

     void OnTriggerEnter2D(Collider2D other)
    {   

        Enemy enemy = other.GetComponent<Enemy>();
        if(enemy != null)
        {
            enemies_Inside.Add(enemy);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if(enemy != null)
        {
            enemies_Inside.Remove(enemy);
        }
    }
}
