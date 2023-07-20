using DG.Tweening;
using UnityEngine;
using Zenject;

namespace SkiddyFunRace.Scripts.Handlers.PlayerHandlers
{
    public class PlayerMovementHandler : MonoBehaviour, IFixedTickable
    {
        [SerializeField] private float speed = 36f;
        [SerializeField] private float acceleration = 36f;
        [SerializeField] private float turningSpeed = 1f;
        [SerializeField] private float breakForce = 1f;
        [SerializeField] private float jumpPower = 800f;

        private Rigidbody _rigidBody;
        private bool _isStarted = false;
        private bool _isApplyingBreak = false;

        private bool _isLeft = true;
        private Vector3 _left;

        public void OnEnable()
        {
            _left = transform.rotation.eulerAngles;
            _rigidBody = GetComponent<Rigidbody>();
        }

        public void StartRun()
        {
            _isStarted = true;
        }

        public void FixedTick()
        {
            if(!_isStarted)
                return;

            if (_isApplyingBreak)
            {
                if(_rigidBody.velocity.magnitude > 0)
                    _rigidBody.velocity -= (_rigidBody.velocity * Time.deltaTime * breakForce);
                
                return;
            }

            _rigidBody.AddForce(transform.forward * acceleration * Time.deltaTime);
            if (_rigidBody.velocity.magnitude > speed)
                _rigidBody.velocity = _rigidBody.velocity.normalized * speed;
        }

        private void TurnRight()
        {
            _isLeft = false;
            transform.DORotate(new Vector3(_left.x, _left.y + 90, _left.z), turningSpeed);
        }

        private void TurnLeft()
        {
            _isLeft = true;
            transform.DORotate(new Vector3(_left.x, _left.y, _left.z), turningSpeed);
        }

        private void Jump()
        {
            //TODO : Animate
            //TODO : Check grounded
            _rigidBody.AddForce(transform.up * jumpPower, ForceMode.Impulse);
        }
        
        public void Break()
        {
            _isApplyingBreak = true;
        }

        public void StopBreak()
        {
            _isApplyingBreak = false;            
        }

        public void Turn()
        {
            if(!_isStarted)
                return;
        
            if (_isLeft)
            {
                TurnRight();
            }
            else
            {
                TurnLeft();
            }
        }

        public void ResetMovement(Vector3 resetPosition)
        {
            _isLeft = true;
            _isStarted = false;
            transform.eulerAngles = _left;
            _rigidBody.velocity = Vector3.zero;
            transform.position = resetPosition;
        }

#if UNITY_EDITOR
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Turn();    
            }
            
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                TurnRight();
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                TurnLeft();
            }
            
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Break();
            }
            else if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                StopBreak();   
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }
#endif
    }
}