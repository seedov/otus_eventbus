using Zenject;


namespace Lessons.Lesson19_EventBus
{
    public sealed class SceneScope : MonoInstaller
    {
        public override void InstallBindings()
        {
            ConfigureLevel();
            ConfigurePlayer();
            ConfigureControllers();

            Container.Bind<EntityInstaller>().FromComponentInHierarchy().AsSingle();
        }

        private void ConfigureLevel()
        {
            Container.Bind<TileMap>().FromComponentInHierarchy().AsSingle();
            Container.Bind<EntityMap>().AsSingle();
            Container.Bind<LevelMap>().AsSingle();
        }

        private void ConfigurePlayer()
        {
            Container.Bind<KeyboardInput>().FromComponentInHierarchy().AsSingle();
            Container.Bind<PlayerService>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerController>().AsSingle();
        }

        private void ConfigureControllers()
        {
            Container.Bind<ApplyDirectionController>().AsSingle();
            Container.Bind<AttackController>().AsSingle();
            Container.Bind<DealDamageController>().AsSingle();
            Container.Bind<DestroyController>().AsSingle();
            Container.Bind<MoveController>().AsSingle();
        }
    }
}