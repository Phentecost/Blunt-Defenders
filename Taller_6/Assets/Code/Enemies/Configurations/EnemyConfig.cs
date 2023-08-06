using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    [CreateAssetMenu]
    public class EnemyConfig : ScriptableObject
    {
        public float speed;
        public float take_Down_Door_Timer;
        public int Life;
    }
}
