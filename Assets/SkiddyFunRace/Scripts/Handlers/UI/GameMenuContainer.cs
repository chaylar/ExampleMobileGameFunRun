using System;
using UnityEngine;
using UnityEngine.UI;

namespace SkiddyFunRace.Scripts.Handlers.UI
{
    public class GameMenuContainer : MonoBehaviour
    {
        private Action _onGo;
        private Action _onReset;
        
        public void Init(Action onGo, Action reset)
        {
            _onGo = onGo;
            _onReset = reset;
        }

        public void Go()
        {
            _onGo?.Invoke();
        }

        public void Reset()
        {
            _onReset?.Invoke();
        }
    }
}