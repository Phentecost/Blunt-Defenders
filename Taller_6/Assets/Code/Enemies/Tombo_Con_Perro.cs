using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class Tombo_Con_Perro : Enemy
    {
        public void Scared()
        {
            speed *= 2;
            gameObject.layer = 13;
            currentBehaviour = BehaviourParams.Go_Out;
        }
    }
    
}