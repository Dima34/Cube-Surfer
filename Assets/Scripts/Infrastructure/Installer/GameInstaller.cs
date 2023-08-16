using Infrastructure.Factories;
using Infrastructure.Factories.UI;
using Infrastructure.Logic;
using Infrastructure.Services.Death;
using Infrastructure.Services.Input;
using Infrastructure.Services.Random;
using Infrastructure.Services.WallsProvider;
using Infrastructure.StateMachines.GameStateMachine;
using Infrastructure.StateMachines.GameStateMachine.States;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installer
{
    public class GameInstaller : MonoInstaller, IInitializable
    {
        public override void InstallBindings()
        {
            BindSelfAsInitializable();
            BindInputService();
            BindRandomService();
            BindWallsProvider();
            BindDeathService();
            BindGameFactory();
            BindUIFactory();
            BindLevelBuilder();
            BindStateMachineStatesFactory();
            BindGameStateMachine();
        }

        private void BindDeathService()
        {
            Container
                .Bind<IDeathService>()
                .To<DeathService>()
                .AsSingle();
        }

        private void BindWallsProvider()
        {
            Container
                .Bind<IWallsProviderService>()
                .To<WallsProviderService>()
                .AsSingle();
        }

        private void BindSelfAsInitializable() =>
            Container
                .BindInterfacesAndSelfTo<GameInstaller>()
                .FromInstance(this)
                .AsSingle();

        private void BindInputService()
        {
            IInputService inputService;

            if (Application.isEditor)
                inputService = new StandaloneInputService();
            else
                inputService = new MobileInputService();

            Container
                .Bind<IInputService>()
                .FromInstance(inputService)
                .AsSingle();
        }

        private void BindRandomService() =>
            Container
                .Bind<IRandomService>()
                .To<RandomService>()
                .AsSingle();

        private void BindUIFactory() =>
            Container
                .Bind<IUIFactory>()
                .To<UIFactory>()
                .AsSingle();

        private void BindGameFactory() =>
            Container
                .Bind<IGameFactory>()
                .To<GameFactory>()
                .AsSingle();

        private void BindStateMachineStatesFactory()
        {
            BindInitializeStateFactory();
            BindGameIdleStateFactory();
            BindGameLoopStateFactory();
            BindEndgameStateFactory();
            BindCleanupLevelStateFactory();
        }

        private void BindInitializeStateFactory() =>
            Container
                .BindFactory<IGameStateMachine,InitializeState, InitializeState.Factory>();

        private void BindGameIdleStateFactory() =>
            Container
                .BindFactory<IGameStateMachine,GameIdleStateState, GameIdleStateState.Factory>();

        private void BindCleanupLevelStateFactory() =>
            Container
                .BindFactory<IGameStateMachine, CleanUpLevelState, CleanUpLevelState.Factory>();

        private void BindEndgameStateFactory() =>
            Container
                .BindFactory<IGameStateMachine, EndGameState, EndGameState.Factory>();

        private void BindGameLoopStateFactory() =>
            Container
                .BindFactory<IGameStateMachine,GameLoopState, GameLoopState.Factory>();

        private void BindGameStateMachine() =>
            Container
                .BindInterfacesAndSelfTo<GameStateMachine>()
                .AsSingle();

        private void BindLevelBuilder() =>
            Container
                .Bind<LevelBuilder>()
                .AsSingle()
                .NonLazy();

        public void Initialize() =>
            GetStateMachineAndEnterInitialState();

        private void GetStateMachineAndEnterInitialState()
        {
            GameStateMachine gameStateMachine = Container.Resolve<GameStateMachine>();
            gameStateMachine.EnterState<InitializeState>();
        }
    }
}