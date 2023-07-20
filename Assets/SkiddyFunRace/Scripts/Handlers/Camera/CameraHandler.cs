using System;
using Cysharp.Threading.Tasks;
using SkiddyFunRace.Scripts.Handlers.PlayerHandlers;
using UnityEngine;
using Zenject;

namespace SkiddyFunRace.Scripts.Handlers
{
    public class CameraHandler: MonoBehaviour, IInitializable, IFixedTickable, ILateTickable
    {
        [SerializeField] private float _followSpeed = 3.0f;
        [SerializeField] private float _smoothTime = 0.1f;

        [Inject] private PlayerHandler _player;
        
        private Vector3 _diff;
        private Vector3 _cameraRelativePosition;
        private Transform _target;
        private Transform _previousTarget;
        private bool _isActive;
        private float _tmpDistanceToTarget;
        private Vector3 _tmpDiff;
        private Vector3 _followVelocity;
        
        public void Initialize()
        {
            Execute(_player.transform);
        }

        private float FollowSpeed
        {
            get => _followSpeed;
            set => _followSpeed = Mathf.Max(0.0f, value);
        }

        private void Execute(Transform target)
        {
            _diff = transform.position - target.position;
            _target = target;

            if (_previousTarget == null)
                transform.position = GetRelativePosition();

            _previousTarget = _target;
            _isActive = true;
        }

        private Vector3 GetRelativePosition()
        {
            return _target.position - transform.forward + _diff;
        }

        public void FixedTick()
        {
            if (!_isActive)
                return;

            transform.position = Vector3.Lerp(transform.position, GetRelativePosition(), FollowSpeed * Time.deltaTime);
        }

        public void LateTick()
        {
            if (!_isActive)
                return;

            if (_target == null)
            {
                return;
            }

            var targetPosition = _target.position - transform.forward + _diff;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _followVelocity, _smoothTime);
        }
    }
}