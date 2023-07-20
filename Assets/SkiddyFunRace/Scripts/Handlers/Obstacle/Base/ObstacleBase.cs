using System;
using Cysharp.Threading.Tasks;
using SkiddyFunRace.Scripts.Handlers.PlayerHandlers;
using SkiddyFunRace.Scripts.RaceGameEvents;
using UnityEngine;
using Zenject;

namespace SkiddyFunRace.Scripts.Handlers.Obstacle.Base
{
    public abstract class ObstacleBase : MonoBehaviour, IInitializable
    {
        [SerializeField] protected ObstacleKillHandler _killHandler;
        [Inject] private SignalBus _signalBus;
        [Inject] private PlayerHandler _playerHandler;
        
        [SerializeField] protected float speed = .5f;
        [SerializeField] protected float backupSpeed = 2.8f;

        private bool _isOnKillProcess = false;

        public void Initialize()
        {
            _killHandler.Init(OnKillAction);
            MoveAction();
        }

        protected abstract void MoveAction();

        protected abstract void GetBackUp();

        private void OnKillAction(Vector3 deathPos)
        {
            if (_isOnKillProcess)
                return;

            _isOnKillProcess = true;
            _playerHandler.Break();
            _playerHandler.PlayDeath();
            async UniTask InitKill()
            {
                await UniTask.Delay(TimeSpan.FromSeconds(.4f), ignoreTimeScale: false);
                _signalBus.Fire(new KillPlayerEvt() { DeathPos = deathPos});
                _isOnKillProcess = false;
            }

            InitKill().Forget();
        }
    }
}