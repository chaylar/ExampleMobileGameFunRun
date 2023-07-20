using System;
using SkiddyFunRace.Scripts.Handlers.PlayerHandlers;
using UnityEngine;
using Zenject;

namespace SkiddyFunRace.Scripts.Handlers
{
    public class TrackEndingPointHandler : MonoBehaviour
    {
        [Inject] private PlayerHandler _playerHandler;

        private void OnTriggerEnter(Collider other)
        {
            if(!other.CompareTag("Player"))
                return;

            EndTrack();
        }

        private void EndTrack()
        {
            _playerHandler.EndTrack();
        }
    }
}