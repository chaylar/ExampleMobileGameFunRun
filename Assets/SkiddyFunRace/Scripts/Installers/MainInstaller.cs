using SkiddyFunRace.Scripts.RaceGameEvents;
using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);
        
        InstallEvents();
    }

    private void InstallEvents()
    {
        Container.DeclareSignal<KillPlayerEvt>().OptionalSubscriber();
        Container.DeclareSignal<StartGameEvt>().OptionalSubscriber();
        Container.DeclareSignal<PauseGameEvt>().OptionalSubscriber();
        Container.DeclareSignal<ContinueGameEvt>().OptionalSubscriber();
        Container.DeclareSignal<EndLevelEvt>().OptionalSubscriber();
    }
}