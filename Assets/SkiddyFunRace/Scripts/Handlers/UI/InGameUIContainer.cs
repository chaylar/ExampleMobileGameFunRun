using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SkiddyFunRace.Scripts.Handlers.UI
{
    public class InGameUIContainer : MonoBehaviour
    {
        [SerializeField] private Button breakButton;
        [SerializeField] private Button turnButton;

        private bool _isOnPause = true;
        
        private Action _onPauseGame;
        private Action _onTurnAction;
        private Action _onBreaksAction;
        private Action _onStopBreaks;

        public void Init(Action turnAction, Action putOnBreaks, Action stopBreaks, Action onPauseGame)
        {
            _onPauseGame = onPauseGame;
            _onTurnAction = turnAction;
            _onBreaksAction = putOnBreaks;
            _onStopBreaks = stopBreaks;
        }

        public void PutOnBreaks()
        {
            if(_isOnPause) return;
            
            _onBreaksAction?.Invoke();
        }
        
        public void StopBreaks()
        {
            if(_isOnPause) return;
            
            _onStopBreaks?.Invoke();
        }

        public void Turn()
        {
            if(_isOnPause) return;
            
            _onTurnAction?.Invoke();
        }

        public void Pause()
        {
            _isOnPause = true;
            _onPauseGame?.Invoke();
        }

        public void UnPauseGame()
        {
            _isOnPause = false;
        }
    }
}