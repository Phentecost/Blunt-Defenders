using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class Esmad : Enemy
    {
        public int shield_Life = 5;

        public override void OnTouched(int i)
        {
            if(shield_Life <= 0)
            {
                base.OnTouched(i);
            }
            else
            {
                shield_Life -= i;
            }
            
        }

        protected override void Power()
        {
        }
    }
}
