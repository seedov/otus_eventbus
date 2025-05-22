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
            Container.Bind<IEventBus>().To<ZenjectEventBus>().AsSingle();

            SignalBusInstaller.Install(Container);

            Container.DeclareSignal<AttackEvent>();
            Container.DeclareSignal<DealDamageEvent>();
            Container.DeclareSignal<DestroyEvent>();
            Container.DeclareSignal<MoveEvent>();
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
            Container.BindInterfacesAndSelfTo<ApplyDirectionController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<AttackController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<DealDamageController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<DestroyController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<MoveController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PushController>().AsSingle().NonLazy();
        }
    }
}