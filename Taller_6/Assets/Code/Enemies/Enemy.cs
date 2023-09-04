using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public abstract class Enemy : MonoBehaviour
    {
        protected float speed;
        protected int door_Damage;
        protected float door_atk_Delay = 1;
        private float door_Timer;
        protected int Life;
        [SerializeField] private EnemyConfig config;
        protected int way_Point_Index;
        protected Way_Point target;
        private int path_Index;
        private Action<Enemy,int> _OnDeath,_OnReach;
        private int points;
        private int damage_To_Player;
        private float wait_Timer;
        private bool door;

        public enum BehaviourParams
        {
            Recognize_The_Area, Moving_Towars_Target, Breaking_In, Tired
        }

        public BehaviourParams currentBehaviour;
        protected virtual void Behaviour()
        {
            switch(currentBehaviour)
            {
                case BehaviourParams.Recognize_The_Area:

                    if(way_Point_Index == WayPointManager.Paths[path_Index].Count -1)
                    {
                      Reach();
                      break;
                    }

                    way_Point_Index ++;
                    target = WayPointManager.Paths[path_Index][way_Point_Index];
                    if(target.Door) door = true;

                    currentBehaviour = BehaviourParams.Moving_Towars_Target;

                    break;
                
                case BehaviourParams.Moving_Towars_Target:

                    Vector2 dir = target.transform.position - transform.position;
                    transform.Translate(dir.normalized * speed *Time.deltaTime,Space.World);

                    if(Vector2.Distance(transform.position,target.transform.position) <= 0.2f)
                    {
                        if(door)
                        {
                            target.atk_Enemies.Add(this);
                            currentBehaviour = BehaviourParams.Breaking_In;
                        }
                        else
                        {
                            currentBehaviour = BehaviourParams.Recognize_The_Area;
                        }
                        
                    }

                    break;
                case BehaviourParams.Breaking_In:

                    if(door_Timer <= 0)
                    {
                        target.life -= door_Damage;
                        door_Timer = door_atk_Delay;                      
                    }
                    else
                    {
                        door_Timer-= Time.deltaTime;
                    }

                    break;
                
                case BehaviourParams.Tired:

                    if(wait_Timer <= 0)
                    {
                      currentBehaviour = BehaviourParams.Recognize_The_Area;  
                    }else
                    {
                        wait_Timer -= Time.deltaTime;
                    }

                    break;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if(Life > 0)
            {
                Behaviour();
            }
            else if(Life <= 0)
            {
                Death();
            }
        }

        private void Death(){_OnDeath(this,points);}
        private void Reach(){_OnReach(this,damage_To_Player);}
        public void Config(Action<Enemy, int> _OnDeath, Action<Enemy, int> _OnReach)
        {
            this.speed = config.speed;
            this.door_Damage = config.door_Damage;
            this.Life = config.Life;
            this.damage_To_Player = config.damage_To_Player;
            way_Point_Index = 0;
            path_Index = config.GetRandomPath();
            transform.position = WayPointManager.Paths[path_Index][way_Point_Index].transform.position;
            way_Point_Index++; 
            target = WayPointManager.Paths[path_Index][way_Point_Index];
            this._OnDeath = _OnDeath;
            this._OnReach = _OnReach;
            this.points = config.points;
            door = false;
            currentBehaviour = BehaviourParams.Moving_Towars_Target;
        }

        public void Door_Break_Down(float timer)
        {
            wait_Timer = timer;
            door = false;
            currentBehaviour = BehaviourParams.Tired;
        }

        public void OnTouched(int i)
        {
            Life += i;
        }
    }
}

