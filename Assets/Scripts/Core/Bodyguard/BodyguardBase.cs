using UnityEngine;

namespace Main
{
    public abstract class BodyguardBase
    {

        public abstract void EntryState(Bodyguard bodyguard);
        public abstract void UpdateState(Bodyguard bodyguard);
        public abstract void OnTriggerState(Bodyguard bodyguard, Enemy enemy);
    }
}


