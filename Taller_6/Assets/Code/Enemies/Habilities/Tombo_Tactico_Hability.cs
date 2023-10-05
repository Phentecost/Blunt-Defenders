using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tombo_Tactico_Hability : MonoBehaviour
{
    private List<TrapsFather> traps = new List<TrapsFather>();
    public float Stun_Timer;
    public float cool_Down;
    private float timer;

    void Start()
    {
        timer = cool_Down;
    }
    void Update()
    {
        if(timer <= 0)
        {
            foreach(TrapsFather trapsFather in traps)
            {
                trapsFather.Disable_Trap(Stun_Timer);
            }

            timer = cool_Down;
        }
        else
        {
            timer -= Time.deltaTime;
        }
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
