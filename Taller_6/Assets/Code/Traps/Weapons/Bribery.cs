using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bribery : TrapsFather
{
    [SerializeField] int range;
    
    // Start is called before the first frame update
    void Start()
    {
        //this.ammo = 1;
        range = 6;
    }

    protected override void DoSomething()
    {
        

    }
}
