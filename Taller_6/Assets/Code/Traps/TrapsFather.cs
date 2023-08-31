using Enemies;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class TrapsFather : MonoBehaviour
{

    //[SerializeField] protected int ammo;
    protected List<Enemy> _Enemy_Inside;
    [SerializeField] private float wait;
    private float timer;

    public void Config()
    {
        _Enemy_Inside = new List<Enemy>();
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

    void Update()
    {
        if(Game_Manager._Current_Game_State == Game_Manager.Game_State.Preparation) return;

        

        if(timer <= 0)
        {
            if(_Enemy_Inside.Count > 0)
            {
                Debug.Log("Si_Entro");
                timer = wait;
                DoSomething();
            }
        }
        else
        {
            timer-= Time.deltaTime;
        }

        
    }

    protected abstract void DoSomething();

}