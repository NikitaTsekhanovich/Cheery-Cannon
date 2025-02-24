using Zenject;

namespace SceneControllers.Installers
{
    public class GlobalInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<LevelLaunchData>().AsSingle().NonLazy();
        }

        private void Awake()
        {
            Container.Bind<SceneDataLoader>().FromComponentInHierarchy().AsSingle().NonLazy();
        }
    }
}
