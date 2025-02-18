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
            ConfigureZenjectEventBus();
        }
        private void ConfigureZenjectEventBus()
        {
            SignalBusInstaller.Install(Container);
            Container.BindInterfacesAndSelfTo<ZenjectEventBus>().AsSingle();
            Container.DeclareSignal<MoveEvent>();
            Container.DeclareSignal<DealDamageEvent>();
            Container.DeclareSignal<ApplyDirectionEvent>();
            Container.DeclareSignal<DestroyEvent>();
            Container.DeclareSignal<AttackEvent>();


        }
        private void ConfigureEventBus()
        {
            Container.BindInterfacesAndSelfTo<EventBus>().AsSingle();
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
            Container.BindInterfacesAndSelfTo<ApplyDirectionController>().AsSingle();
            Container.BindInterfacesAndSelfTo<AttackController>().AsSingle();
            Container.BindInterfacesAndSelfTo<DealDamageController>().AsSingle();
            Container.BindInterfacesAndSelfTo<DestroyController>().AsSingle();
            Container.BindInterfacesAndSelfTo<MoveController>().AsSingle();
            Container.BindInterfacesAndSelfTo<PushController>().AsSingle();
        }
    }
}