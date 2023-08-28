using Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapsFather : MonoBehaviour
{

    [SerializeField] protected int ammo;
    private List<Enemy> _Enemy_Inside;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemycontroller = collision.gameObject.GetComponent<Enemy>();
        if (enemycontroller != null)
        {
            _Enemy_Inside.Add(enemycontroller);
        }
    }

    /*protected void OnTriggerExit(Collider2D collision)
    {
        Enemy enemycontroller = collision.gameObject.GetComponent<Enemy>();
        if (enemycontroller != null)
        {
            _Enemy_Inside.Remove(enemycontroller);
        }
    }*/

    void Update()
    {
        /*if(_Enemy_Inside.Count > 0)
        {
            DoSomething();
        }*/
    }

    protected virtual void DoSomething(){}

}