using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class Bachitombo : Enemy
    {
        protected override void Behaviour()
        {
            switch(currentBehaviour)
            {
                case BehaviourParams.Recognize_The_Area:

                    break;
                
                case BehaviourParams.Moving_Towars_Target:

                    break;
                case BehaviourParams.Breaking_In:

                    break;
            }
        }
    }

}

