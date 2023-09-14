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
    public bool POV;

    void Start()
    {
        enemies_Inside = new List<Enemy>();
    }

    void Update()
    {
        if(Game_Manager._Current_Game_State != Game_Manager.Game_State.Defending) return;
        
        if(!POV&&enemies_Inside.Count != 0)
        {
            image.color = new Color(1,0,0,0.5f);
        }
        else
        {
            image.color = POV ? new Color(0, 1, 0, 0.5f) : new Color(0, 0, 0, 0);
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
