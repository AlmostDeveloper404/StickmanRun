using UnityEngine;
using Zenject;

namespace Main
{
    public class SoundManagerInstaller : MonoInstaller
    {
        [SerializeField] private SoundManager _smPrefab;

        public override void InstallBindings()
        {
            var prefab = Container.InstantiatePrefabForComponent<SoundManager>(_smPrefab);
            Container.Bind<SoundManager>().FromInstance(prefab).AsSingle();
        }
    }
}

