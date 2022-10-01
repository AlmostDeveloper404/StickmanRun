using UnityEngine;
using Zenject;


namespace Main
{
    public class LevelControllerInstaller : MonoInstaller
    {
        [SerializeField] private LevelController _levelController;

        public override void InstallBindings()
        {
            var levelController = Container.InstantiatePrefabForComponent<LevelController>(_levelController);
            Container.Bind<LevelController>().FromInstance(levelController).AsSingle().NonLazy();
        }
    }
}

