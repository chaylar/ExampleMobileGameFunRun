using System;
using System.Collections.Generic;
using SkiddyFunRace.Scripts.Handlers.PlayerHandlers;
using SkiddyFunRace.Scripts.Handlers.UI;
using SkiddyFunRace.Scripts.RaceGameEvents;
using UnityEngine;
using Zenject;

namespace SkiddyFunRace.Scripts.Handlers.MainGameHandler
{
    public class GameHandler : MonoBehaviour, IInitializable, IDisposable
    {
        [Inject] private SignalBus _signalBus;
        [Inject] private BaseMenuContainer _baseMenuContainer;

        public void Initialize()
        {
            _signalBus.Subscribe<EndLevelEvt>(OnEndLevel);
        }

        private void OnEndLevel(EndLevelEvt evt)
        {
            _baseMenuContainer.EndGameExternal();
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<EndLevelEvt>(OnEndLevel);
        }
    }
}