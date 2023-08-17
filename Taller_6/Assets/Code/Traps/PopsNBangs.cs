using Enemies;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PopsNBangs : TrapsFather
{
    [SerializeField] int range;
    
    // Start is called before the first frame update
    void Start()
    {
        this.ammo = 1;
        range = 6;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void DoSomething()
    {
        StartCoroutine(Exploit());
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        foreach (GameObject enemy in enemies)
        {
            if(range >= Vector2.Distance(this.transform.position, enemy.GetComponent<Rigidbody2D>().position))
            {
                if(enemy.GetComponent<Tombo_Con_Perro>() != null){
                    /*hacer 1 de daño
                    espantar al perro*/
                    }
                else
                {
                    //hacer 0.5f de daño
                }

            }
             
        }

    }
    
    IEnumerator Exploit()
    {
        yield return new WaitForSeconds(3f);
    }

 
}

