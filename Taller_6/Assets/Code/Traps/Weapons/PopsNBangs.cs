using Enemies;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PopsNBangs : TrapsFather
{
    [SerializeField] int range;
    [SerializeField] float Chargetime; 

    void Start()
    {
        //this.ammo = 1;
        this.range = 6;
    }

    protected override void DoSomething()
    {
        Chargetime -= Time.deltaTime;
        if (Chargetime < 0)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
            foreach (GameObject enemy in enemies)
            {
                Tombo_Con_Perro enemyController = enemy.GetComponent<Tombo_Con_Perro>();
                if (this.range >= Vector2.Distance(this.transform.position, enemy.transform.position))
                {
                    if (enemyController != null)
                    {
                        enemyController.Scared();
                    }
                }
            }
            Trap_Manager.Instance.Realice_Trap_To_Preveiw(this);
                
        }
    }

    /*protected IEnumerator Exploit()
    {
        yield return new WaitForSeconds(Chargetime);
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        foreach (GameObject enemy in enemies)
        {
            Tombo_Con_Perro enemyController =  enemy.GetComponent<Tombo_Con_Perro>();
            if (this.range >= Vector2.Distance(this.transform.position, enemy.transform.position))
            {
                if (enemyController != null)
                {
                    enemyController.Scared();           
                }

            }

        }
    }*/

 
}

