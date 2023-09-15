using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
        [SerializeField]protected int way_Point_Index;
        protected Way_Point target;
        private int path_Index;
        private Action<Enemy,int> _OnDeath,_OnReach;
        private int points;
        private int damage_To_Player;
        private float wait_Timer;
        private bool door;
        private bool tp;
        private bool traitor,Active;
        private Bribery money;
        [SerializeField] private Animator animator;
        private SpriteRenderer _renderer;
        private ParticleSystem particle;
        private float stun_Timer,ColRed_timer;
        public enum BehaviourParams
        {
            Recognize_The_Area, Moving_Towars_Target, Breaking_In, Tired, Go_Out,Stuned
        }

        public BehaviourParams currentBehaviour;
        protected virtual void Behaviour()
        {

            if(ColRed_timer <= 0)
            {
                _renderer.color = Color.white;
            }
            else
            {
                ColRed_timer -= Time.deltaTime;
            }

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
                    if(target.TP) tp = true;
                    if(animator != null)
                    {
                        Vector2 moveDir = target.transform.position - transform.position;
                        ChangerAnim(moveDir);
                    }
                    
                    currentBehaviour = BehaviourParams.Moving_Towars_Target;

                    break;
                
                case BehaviourParams.Moving_Towars_Target:

                    Vector2 dir = target.transform.position - transform.position;
                    transform.Translate(dir.normalized * speed *Time.deltaTime,Space.World);

                    if(Vector2.Distance(transform.position,target.transform.position) <= 0.2f)
                    {
                        if(traitor)
                        {
                            if(tp)
                            {
                                way_Point_Index--;
                                target = WayPointManager.Paths[path_Index][way_Point_Index];
                                transform.position = target.transform.position;
                                tp=false;

                            }
                            
                            currentBehaviour = BehaviourParams.Go_Out;
                            
                            return;
                        }

                        if(door)
                        {
                            target.atk_Enemies.Add(this);
                            currentBehaviour = BehaviourParams.Breaking_In;
                            return;
                        }

                        if(tp)
                        {
                            way_Point_Index++;
                            target = WayPointManager.Paths[path_Index][way_Point_Index];
                            transform.position = target.transform.position;
                            tp=false;
                            
                        }
                        
                        currentBehaviour = BehaviourParams.Recognize_The_Area;
                        
                        
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

                case BehaviourParams.Go_Out:

                    if(way_Point_Index == 0)
                    {
                        Trap_Manager.Instance.Realice_Trap_To_Preveiw(money);
                        _OnDeath(this,points);
                        break;
                    }

                    way_Point_Index --;
                    target = WayPointManager.Paths[path_Index][way_Point_Index];
                    if(target.TP) tp = true;
                    if(animator != null)
                    {
                        Vector2 moveDir = target.transform.position - transform.position;
                        ChangerAnim(moveDir);
                    }
                    
                    currentBehaviour = BehaviourParams.Moving_Towars_Target;

                break;

                case BehaviourParams.Stuned:

                if(stun_Timer<= 0)
                {
                    speed = config.speed;
                    currentBehaviour = BehaviourParams.Moving_Towars_Target;
                }
                else
                {
                    stun_Timer -= Time.deltaTime;
                }

                break;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if(!Active)return;
            if(Life > 0)
            {
                Behaviour();
            }
            else if(Life <= 0)
            {
                StartCoroutine(Death());
            }
        }

        public void Activation()
        {
            Active = !Active;
        }

        private IEnumerator Death()
        {

            particle.Play();
            speed = 0;
            _renderer.color = Color.red;
            Active = false;
            yield return new WaitForSeconds(0.2f);
            _OnDeath(this,points);
        }
        private void Reach(){_OnReach(this,damage_To_Player);}
        public void Config(Action<Enemy, int> _OnDeath, Action<Enemy, int> _OnReach)
        {
            speed = config.speed;
            door_Damage = config.door_Damage;
            Life = config.Life;
            damage_To_Player = config.damage_To_Player;
            way_Point_Index = 0;
            path_Index = config.GetRandomPath();
            transform.position = WayPointManager.Paths[path_Index][way_Point_Index].transform.position;
            way_Point_Index++; 
            target = WayPointManager.Paths[path_Index][way_Point_Index];
            if(animator != null)
            {
                Vector2 moveDir = target.transform.position - transform.position;
                ChangerAnim(moveDir);
            }
            this._OnDeath = _OnDeath;
            this._OnReach = _OnReach;
            points = config.points;
            door = false;
            traitor=false;
            tp = false;
            gameObject.layer = 3;
            _renderer = GetComponentInChildren<SpriteRenderer>();
            particle = GetComponentInChildren<ParticleSystem>();
            Active = true;
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
            ColRed();
        }
        void ColRed()
        {
            _renderer.color = Color.red;
            ColRed_timer = 0.2f;
        }

        public void stun(float i)
        {
            speed= 0;
            stun_Timer = i;
            currentBehaviour = BehaviourParams.Stuned;
        }

        public void OnBribery(Bribery bribery)
        {
            traitor = true;
            speed *= 2;
            money = bribery;
            gameObject.layer = 13;
            currentBehaviour = BehaviourParams.Go_Out;
        }

        public void Hit(){Death();}

        void ChangerAnim(Vector2 dir)
        {
            dir = Vector2Int.RoundToInt(dir);

            if(dir.x > dir.y)
            {
                if(dir.x >= 0.5) //Rigth
                {
                    animator.SetTrigger("Side");
                    transform.localScale = new Vector2(1,1);
                }
                else if(dir.x <= -0.5) //Left
                {
                    animator.SetTrigger("Side");
                    transform.localScale = new Vector2(-1,1);
                }
            }
            else
            {
                if(dir.y >= 0.5) // Up
                {
                    animator.SetTrigger("Up");
                }
                else if (dir.y <= -0.5) //Down
                {
                    animator.SetTrigger("Down");
                }
            }
            
        }

        void OnCollisionEnter2D(Collision2D col)
        {
            if(!traitor) return;
            Debug.Log("XD");
            Enemy en = col.collider.GetComponent<Enemy>();
            if(en != null)
            {
                en.Hit();
            }
        }

    }

    
}
