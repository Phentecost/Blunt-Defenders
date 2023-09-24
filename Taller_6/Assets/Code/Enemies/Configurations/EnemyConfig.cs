using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    [CreateAssetMenu]
    public class EnemyConfig : ScriptableObject
    {
        public float speed;
        public int door_Damage;
        public int Life;
        public int damage_To_Player;
        public int points;
    }
}
