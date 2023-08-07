using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public abstract class Enemy : MonoBehaviour
    {
        [SerializeField] protected float speed;
        protected float take_Down_Door_Timer;
        protected float door_Timer;
        protected int Life;
        [SerializeField] private EnemyConfig config;
        protected int way_Point_Index;
        protected Transform target;

        protected enum BehaviourParams
        {
            Recognize_The_Area, Moving_Towars_Target, Breaking_In
        }

        protected BehaviourParams currentBehaviour;
        protected abstract void Behaviour();

        void Start()
        {
            Config();
        }

        // Update is called once per frame
        void Update()
        {
            if(Life > 0)
            {
                Behaviour();
            }
        }

        private void Death(){}
        protected virtual void Config()
        {
            this.speed = config.speed;
            this.take_Down_Door_Timer = config.take_Down_Door_Timer;
            this.Life = config.Life;
            way_Point_Index = 0;
            currentBehaviour = BehaviourParams.Moving_Towars_Target;
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            Life -= 1;
            if(Life <= 0)
            {
                Death();
            }
        }
    }
}

