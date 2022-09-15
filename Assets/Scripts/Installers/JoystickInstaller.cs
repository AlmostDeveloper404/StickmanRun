using UnityEngine;
using Zenject;

namespace Main 
{
    public class JoystickInstaller : MonoInstaller
    {
        [SerializeField] private FixedJoystick _fixedJoystick;

        public override void InstallBindings()
        {
            Container.Bind<FixedJoystick>().FromInstance(_fixedJoystick);
        }
    }
}

