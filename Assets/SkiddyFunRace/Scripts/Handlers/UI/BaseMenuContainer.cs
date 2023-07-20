using System;
using System.Collections.Generic;
using SkiddyFunRace.Scripts.Handlers.PlayerHandlers;
using SkiddyFunRace.Scripts.RaceGameEvents;
using UnityEngine;
using Zenject;

namespace SkiddyFunRace.Scripts.Handlers.UI
{
    public class BaseMenuContainer : MonoBehaviour, IInitializable, IDisposable
    {
        [SerializeField] private GameMenuContainer gameMenuContainer;
        [SerializeField] private InGameUIContainer inGameUi;
        
        [SerializeField] private List<GameObject> showOnPauseList = new List<GameObject>();

        [SerializeField] private GameObject hideOnVictory;
        [SerializeField] private GameObject showOnVictory;

        [Inject] private PlayerHandler _playerHandler;
        [Inject] private SignalBus _signalBus;

        private float _standardTimeScale = 1f;
        private float _pausedTimeScale = 0f;

        public void Initialize()
        {
            showOnVictory.SetActive(false);
            hideOnVictory.SetActive(true);
            gameMenuContainer.Init(Go, ResetLevel);
            inGameUi.Init(Turn, PutOnBreaks, StopBreaks, PauseGame);
            inGameUi.Pause();
            
            _signalBus.Subscribe<KillPlayerEvt>(OnKillPlayer);
        }

        public void PauseExternal()
        {
            inGameUi.Pause();
        }

        public void EndGameExternal()
        {
            //TODO : Show end game menu!
            showOnVictory.SetActive(true);
            hideOnVictory.SetActive(false);
            inGameUi.Pause();
        }

        private void Turn()
        {
            _playerHandler.Turn();
        }

        private void PutOnBreaks()
        {
            _playerHandler.Break();
        }
        
        private void StopBreaks()
        {
            _playerHandler.StopBreaks();
        }

        private void Go()
        { 
            Time.timeScale = _standardTimeScale;
            if(gameMenuContainer.gameObject.activeSelf) 
                gameMenuContainer.gameObject.SetActive(false);

            ShowHidePauseObjects(false);
            inGameUi.UnPauseGame();
            _playerHandler.StartRun();
            _signalBus.Fire(new ContinueGameEvt());
        }
        
        private void ResetLevel()
        { 
            if(showOnVictory.activeSelf)
                showOnVictory.SetActive(false);
            
            if(!hideOnVictory.activeSelf)
                hideOnVictory.SetActive(true);
            
            Time.timeScale = _standardTimeScale;
            if(gameMenuContainer.gameObject.activeSelf) 
                gameMenuContainer.gameObject.SetActive(false);

            ShowHidePauseObjects(false);
            inGameUi.UnPauseGame();
            _signalBus.Fire(new StartGameEvt());
        }

        private void PauseGame()
        {
            Time.timeScale = _pausedTimeScale;
            if(!gameMenuContainer.gameObject.activeSelf) 
                gameMenuContainer.gameObject.SetActive(true);

            ShowHidePauseObjects(true);
            _signalBus.Fire(new PauseGameEvt());
        }

        private void ShowHidePauseObjects(bool setActive)
        {
            foreach(var go in showOnPauseList)
            {
                go.SetActive(setActive);
            }
        }

        private void OnKillPlayer(KillPlayerEvt evt)
        {
            inGameUi.Pause();
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<KillPlayerEvt>(OnKillPlayer);
        }
    }
}