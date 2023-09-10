using Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bribery : TrapsFather
{
    [SerializeField] float range;
    private int power;
    
    // Start is called before the first frame update
    void Start()
    {
        this.range = 6;
        this.power = 2;
    }

    protected override void DoSomething()
    {
        int i = 0;
        GameObject[] affectedEnemies = new GameObject[power];
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        foreach (GameObject enemy in enemies)
        {
            if (i>power)
                return;
            if(this.range >= Vector2.Distance(this.transform.position, enemy.transform.position))
            {
                affectedEnemies[i] = enemy;
                i++;
            }
        }
        foreach(GameObject enemy in affectedEnemies)
        {
            //espantarlos;
        }
    }
}
