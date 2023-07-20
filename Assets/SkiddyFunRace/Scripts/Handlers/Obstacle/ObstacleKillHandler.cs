using System;
using UnityEngine;

namespace SkiddyFunRace.Scripts.Handlers.Obstacle
{
    public class ObstacleKillHandler : MonoBehaviour
    {
        private Action<Vector3> _onKillAction;

        public void Init(Action<Vector3> onKill)
        {
            _onKillAction = onKill;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player"))
            {
                return;
            }

            _onKillAction?.Invoke(other.gameObject.transform.position);
        }
    }
}