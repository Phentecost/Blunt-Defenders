using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BowlingBall : BulletFather
{
    //public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        this.Damage = 3;
      //  animator = GetComponent<Animator>();
    }
}
