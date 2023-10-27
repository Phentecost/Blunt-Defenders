using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using Enemies;
using UnityEngine;

public class Tombo_Tactico_Hability : MonoBehaviour
{
    private List<TrapsFather> traps = new List<TrapsFather>();
    public float Stun_Timer;
    public float cool_Down;
    private float timer;
    bool Active;
    private Enemy en;

    void Start()
    {
        timer = cool_Down;
        Active = true;
        en = GetComponentInParent<Enemy>();
    }
    void Update()
    {
        if(!Active) return;
        
        if(timer <= 0 && traps.Count > 0)
        {
            StartCoroutine(Desable_Trap());
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    IEnumerator Desable_Trap()
    {
        Active = false;
        en.stop();
        yield return new WaitForSeconds(2);
        foreach(TrapsFather trapsFather in traps)
        {
            trapsFather.Disable_Trap(Stun_Timer);
        }

        timer = cool_Down;
        en.stop();
        Active = true;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        TrapsFather trap = col.GetComponent<TrapsFather>();
        if(trap != null)
        {
            traps.Add(trap);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        TrapsFather trap = col.GetComponent<TrapsFather>();
        if(trap != null)
        {
            traps.Remove(trap);
        }
    }
}
