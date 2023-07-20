using System;
using UnityEngine;

namespace SkiddyFunRace.Scripts.Handlers.Obstacle
{
    public class DeathBox : MonoBehaviour
    {
        private Action _onKillAction;

        public void Init(Action onKill)
        {
            _onKillAction = onKill;
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player"))
            {
                return;
            }
            
            _onKillAction?.Invoke();
        }
    }
}