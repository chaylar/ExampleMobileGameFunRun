using System;
using SkiddyFunRace.Scripts.Handlers.Obstacle;
using SkiddyFunRace.Scripts.Handlers.PlayerHandlers;
using SkiddyFunRace.Scripts.RaceGameEvents;
using UnityEngine;
using Zenject;

namespace SkiddyFunRace.Scripts.Handlers
{
    public class TrackHandler : MonoBehaviour, IInitializable, IDisposable
    {
        [SerializeField] private TrackStartingPointHandler startingPointHandler;
        [SerializeField] private TrackEndingPointHandler endingPointHandler;
        [SerializeField] private DeathBox deathBox;

        [Inject] private PlayerHandler _playerHandler;
        [Inject] private SignalBus _signalBus;

        public void Initialize()
        {
            ResetPlayerPosition();
            
            _signalBus.Subscribe<KillPlayerEvt>(OnKillPlayer);
            _signalBus.Subscribe<StartGameEvt>(OnStartGame);
            _signalBus.Subscribe<ContinueGameEvt>(OnContinue);
            
            deathBox.Init(DeathBoxKillAction);
        }
        
        private void DeathBoxKillAction()
        {
            _signalBus.Fire(new KillPlayerEvt());
        }

        private void OnKillPlayer(KillPlayerEvt evt)
        {
            Debug.Log("OnKillPlayer");
            ResetPlayerPosition();
        }
        
        private void OnStartGame(StartGameEvt evt)
        {
            ResetPlayerPosition();
            _playerHandler.StopBreaks();
            _playerHandler.StartRun();
        }
        
        private void OnContinue(ContinueGameEvt evt)
        {
            _playerHandler.StopBreaks();
            _playerHandler.StartRun();
        }

        private void ResetPlayerPosition()
        {
            _playerHandler.Death(startingPointHandler.GetStartingPositin());
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<KillPlayerEvt>(OnKillPlayer);
            _signalBus.Unsubscribe<StartGameEvt>(OnStartGame);
            _signalBus.Unsubscribe<ContinueGameEvt>(OnContinue);
        }
    }
}