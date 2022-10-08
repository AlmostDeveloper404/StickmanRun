using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    public abstract class EnemyBase<T> where T : Enemy
    {
        public abstract void EntryState(T straightForwardEnemy, Animator animator);

        public abstract void UpdateState(T straightForwardEnemy);
    }
}


