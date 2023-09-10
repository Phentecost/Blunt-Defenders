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
        StartCoroutine(Exploit());
    }

    protected IEnumerator Exploit()
    {
        yield return new WaitForSeconds(Chargetime);
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        foreach (GameObject enemy in enemies)
        {
            if (this.range >= Vector2.Distance(this.transform.position, enemy.transform.position))
            {
                if (enemy.GetComponent<Tombo_Con_Perro>() != null)
                {
                    /*hacer 1 de dano
                    espantar al perro*/
                }
                else
                {
                    //hacer 0.5f de da�o
                }

            }

        }
    }

 
}

