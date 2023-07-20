using System;
using SkiddyFunRace.Scripts.RaceGameEvents;
using UnityEngine;
using Zenject;

namespace SkiddyFunRace.Scripts.Handlers.EffectsHandler
{
    public class DeathFxHandler : MonoBehaviour, IInitializable, IDisposable
    {
        [SerializeField] private ParticleSystem deathParticle;
        [Inject] private SignalBus _signalBus;
        
        public void Initialize()
        {
            _signalBus.Subscribe<KillPlayerEvt>(OnPlayerDeath);
        }
        
        private void OnPlayerDeath(KillPlayerEvt evt)
        {
            if(evt.DeathPos == Vector3.zero)
                return;

            var newPart = Instantiate(deathParticle, transform);
            newPart.transform.position = evt.DeathPos;
            Destroy(newPart, 10f);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<KillPlayerEvt>(OnPlayerDeath);
        }
    }
}