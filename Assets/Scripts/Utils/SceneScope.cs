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

            ConfigureZenjectSignalBus();

        }
        private void ConfigureSignalBus()
        {
            Container.BindInterfacesTo<EventBus>().AsSingle();
        }
        private void ConfigureZenjectSignalBus()
        {
            Container.BindInterfacesTo<ZenjectEventBus>().AsSingle();
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<ApplyDirectionHandler>();
            Container.DeclareSignal<MoveHandler>();
            Container.DeclareSignal<AttackHandler>();
            Container.DeclareSignal<DealDamageHandler>();
            Container.DeclareSignal<DestroyHandler>();
            Container.DeclareSignal<StrengthHandler>();
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
            Container.BindInterfacesAndSelfTo<StatsController>().AsSingle();
        }
    }
}