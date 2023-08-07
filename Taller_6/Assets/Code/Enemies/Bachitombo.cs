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

                    if(way_Point_Index == WayPointManager.Path1.Length -1)
                    {
                      transform.position = Vector2.zero; 
                      way_Point_Index = 0;
                      target = WayPointManager.Path1[way_Point_Index];
                      currentBehaviour = BehaviourParams.Moving_Towars_Target;
                      break;
                    }

                    way_Point_Index ++;
                    target = WayPointManager.Path1[way_Point_Index];
                    currentBehaviour = BehaviourParams.Moving_Towars_Target;

                    break;
                
                case BehaviourParams.Moving_Towars_Target:

                    Vector2 dir = target.position - transform.position;
                    transform.Translate(dir.normalized * speed *Time.deltaTime,Space.World);

                    if(Vector2.Distance(transform.position,target.position) <= 0.4f)
                    {
                        currentBehaviour = BehaviourParams.Recognize_The_Area;
                    }

                    break;
                case BehaviourParams.Breaking_In:

                    break;
            }
        }

       protected override void Config()
       {
            base.Config();
            target = WayPointManager.Path1[way_Point_Index];
       }
    }

}

