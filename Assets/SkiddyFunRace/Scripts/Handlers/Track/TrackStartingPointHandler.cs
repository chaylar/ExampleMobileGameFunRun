using SkiddyFunRace.Scripts.Handlers.PlayerHandlers;
using UnityEngine;
using Zenject;

namespace SkiddyFunRace.Scripts.Handlers
{
    public class TrackStartingPointHandler : MonoBehaviour
    {
        public Vector3 GetStartingPositin()
        {
            return transform.position;
        }
    }
}