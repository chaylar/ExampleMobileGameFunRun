using System;
using Cysharp.Threading.Tasks;
using SkiddyFunRace.Scripts.Models;
using SkiddyFunRace.Scripts.RaceGameEvents;
using UnityEngine;
using Zenject;

namespace SkiddyFunRace.Scripts.Handlers.PlayerHandlers
{
    public class PlayerHandler : MonoBehaviour, IInitializable, IDisposable
    {
        [SerializeField] private ParticleSystem victoryParticle; 
        [SerializeField] private ParticleSystem deathParticle; 
        [SerializeField] private PlayerMovementHandler movementHandler;
        [SerializeField] private PlayerModel playerModel;

        [Inject] private SignalBus _signalBus;
        
        public void Initialize()
        {
            victoryParticle.Stop();
            victoryParticle.gameObject.SetActive(false);
            
            deathParticle.Stop();
            deathParticle.gameObject.SetActive(false);
        }

        public void Dispose()
        {
        }

        public void StartRun()
        {
            deathParticle.gameObject.SetActive(false);
            victoryParticle.gameObject.SetActive(false);
            
            deathParticle.Stop();
            victoryParticle.Stop();
            movementHandler.StopBreak();
            movementHandler.StartRun();
        }

        public void PlayDeath()
        {
            deathParticle.gameObject.SetActive(true);
            deathParticle.Play();
        }

        public void EndTrack()
        {
            victoryParticle.gameObject.SetActive(true);
            victoryParticle.Play();
            Debug.Log("EndTrack");
            movementHandler.Break();
            playerModel.Level++; //TODO : SAVE!

            async UniTask EndLevelAsync()
            {
                await UniTask.Delay(TimeSpan.FromSeconds(3f), ignoreTimeScale: false);
                _signalBus.Fire(new EndLevelEvt());
            }

            EndLevelAsync().Forget();
        }

        public int GetCurrentLevel()
        {
            return playerModel.Level;
        }

        private void ResetTrack(Vector3 resetPos)
        {
            movementHandler.ResetMovement(resetPos);
        }

        public void Turn()
        {
            movementHandler.Turn();
        }

        public void Break()
        {
            movementHandler.Break();
        }
        
        public void StopBreaks()
        {
            movementHandler.StopBreak();
        }

        public void Death(Vector3 resetPos)
        {
            //TODO : DEATH fx
            ResetTrack(resetPos);
        }
        
#if UNITY_EDITOR
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                StartRun();
            }
        }
#endif
    }
}