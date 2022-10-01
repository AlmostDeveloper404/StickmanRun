using UnityEngine;
using Zenject;

namespace Main 
{
    public class JoystickInstaller : MonoInstaller
    {
        [SerializeField] private DynamicJoystick _fixedJoystick;

        public override void InstallBindings()
        {
            Container.Bind<DynamicJoystick>().FromInstance(_fixedJoystick);
        }
    }
}

