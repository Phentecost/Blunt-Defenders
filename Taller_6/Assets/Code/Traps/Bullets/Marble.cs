using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marble : BulletFather
{
    // Start is called before the first frame update
    void Start()
    {
        this.Damage = 1;
        this.Power = 400;
    }
}
