using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemies;
using TMPro;

public class Way_Point : MonoBehaviour, interactible_OGJ
{
    public bool Door;
    public bool TP;
    public float life;
    public List<Enemy> atk_Enemies;
    private float scalar = 0.2f;
    private float timer = 0;
    [SerializeField] private TextMeshPro txt;
    public SpriteRenderer spriteRenderer;
    private bool out_Lines,sub_out_lines;
    void Update()
    {
        if(!Door || Game_Manager._Current_Game_State == Game_Manager.Game_State.Preparation) return;

        txt.text = "Life: " + life;

        if(life <= 0)
        {
            OnBreakDown();
        }

        if(life/100 < 0.5f)
        {
            if(life/100 < 0.25)
            {
               spriteRenderer.color = Color.red;
               return; 
            }
            spriteRenderer.color = Color.yellow;
            return;
        }
        else
        {
            spriteRenderer.color = Color.green;
        }

        
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
        spriteRenderer.gameObject.SetActive(false);
    }

    public void Make_Door()
    {
        Door = true;
        life = 100;
        spriteRenderer.color = Color.green;
        spriteRenderer.gameObject.SetActive(true);
        txt.gameObject.SetActive(true);
        
    }

    public void Register(Enemy enemy)
    {
        atk_Enemies.Add(enemy);
    }

    public void OnRepear()
    {
        life = 100;
        txt.text = "Life: " + life;
        if(!spriteRenderer.gameObject.activeInHierarchy)spriteRenderer.gameObject.SetActive(true);
        spriteRenderer.color = Color.green;
    }

    public void show_Outlines()
    {
        if(Door)
        {
            out_Lines = true;

        }
        else
        {
            spriteRenderer.color = new Color(0,1,0,0.5f);
            spriteRenderer.gameObject.SetActive(true);
            sub_out_lines = true;
        }
        
    }

    public bool IsActive_OBJ()
    {
        return Door;
    }

    public bool IsActive_Outlines()
    {
        return out_Lines || sub_out_lines? true : false;
    }

    public Vector2 Pos()
    {
        return transform.parent.position;
    }

    public void Off_Outlines()
    {
        if(Door)
        {
            out_Lines = false;
        }
        else
        {
            spriteRenderer.gameObject.SetActive(false);
            sub_out_lines = false;
        }
    }
}
