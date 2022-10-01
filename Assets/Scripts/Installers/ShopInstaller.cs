using UnityEngine;
using Zenject;

namespace Main
{
    public class ShopInstaller : MonoInstaller
    {
        [SerializeField] private Shop _shop;
        public override void InstallBindings()
        {
            Container.Bind<Shop>().FromInstance(_shop).AsSingle();
        }
    }
}
