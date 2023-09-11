using Enemies;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class TrapsFather : MonoBehaviour
{
    protected List<Enemy> _Enemy_Inside;
    private float wait;
    private float timer;
    [SerializeField] private Traps_Config traps_Config;
    public int Current_Level {get; private set;}
    private CircleCollider2D fire_Range;
    protected int bullet_Damage;
    protected float bullet_Power;
    public int _level_Up_Money_Cost {get; private set;}
    public int _level_Up_Weed_Cost {get; private set;}
    [SerializeField]private GameObject out_Line;
    [SerializeField] private SpriteRenderer spriteRenderer;
    protected abstract void DoSomething();

    void Update()
    {

        if(Game_Manager._Current_Game_State == Game_Manager.Game_State.Preparation) return;

        if(timer <= 0)
        {
            if(_Enemy_Inside.Count > 0)
            {
                timer = wait;
                DoSomething();
            }
        }
        else
        {
            timer-= Time.deltaTime;
        }
    }

    public void Config(bool deploy)
    {
        if(deploy)
        {
            _Enemy_Inside = new List<Enemy>();
            Current_Level = 1;
            fire_Range = GetComponent<CircleCollider2D>();
            wait = traps_Config.levels[Current_Level-1].CoolDown;
            float range = traps_Config.levels[Current_Level-1].Range;
            fire_Range.radius = range;
            Vector2 scale_Outlines = new Vector2(range / 2.5f, range/2.5f);
            out_Line.transform.localScale = scale_Outlines;
            bullet_Damage = traps_Config.levels[Current_Level-1].Damage;
            bullet_Power = traps_Config.levels[Current_Level-1].Power;
            _level_Up_Money_Cost = traps_Config.levels[Current_Level].Coins;
            _level_Up_Weed_Cost = traps_Config.levels[Current_Level].Weed;
            spriteRenderer.sprite = traps_Config.levels[Current_Level-1].levelSprite;
            transform.GetChild(0).gameObject.SetActive(true);

        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
            float range = traps_Config.levels[0].Range;
            Vector2 scale_Outlines = new Vector2(range / 2.5f, range/2.5f);
            out_Line.transform.localScale = scale_Outlines;
            spriteRenderer.sprite = traps_Config.levels[0].levelSprite;
        }
        
    }

    public void Level_Up()
    {
        Current_Level ++;
        wait = traps_Config.levels[Current_Level-1].CoolDown;
        float range = traps_Config.levels[Current_Level-1].Range;
        fire_Range.radius = range;
        Vector2 scale_Outlines = new Vector2(range / 2.5f, range/2.5f);
        out_Line.transform.localScale = scale_Outlines;
        bullet_Damage = traps_Config.levels[Current_Level-1].Damage;
        bullet_Power = traps_Config.levels[Current_Level-1].Power;
        spriteRenderer.sprite = traps_Config.levels[Current_Level-1].levelSprite;
        if(Current_Level < 5)
        {
            _level_Up_Money_Cost = traps_Config.levels[Current_Level].Coins;
            _level_Up_Weed_Cost = traps_Config.levels[Current_Level].Weed;
        }
    }

    public void Show_Outlines()
    {
        if(out_Line.activeInHierarchy)
        {
            out_Line.SetActive(false);
        }
        else
        {
            out_Line.SetActive(true);
        }
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemycontroller = collision.gameObject.GetComponent<Enemy>();
        if (enemycontroller != null)
        {
            _Enemy_Inside.Add(enemycontroller);
        }
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        Enemy enemycontroller = collision.gameObject.GetComponent<Enemy>();
        if (enemycontroller != null)
        {
            _Enemy_Inside.Remove(enemycontroller);
        }
    }

    



}