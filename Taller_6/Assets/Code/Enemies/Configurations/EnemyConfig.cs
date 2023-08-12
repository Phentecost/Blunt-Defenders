using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    [CreateAssetMenu]
    public class EnemyConfig : ScriptableObject
    {
        public float speed;
        public float door_Damage;
        public int Life;
        public int path_Index;
    }
}
