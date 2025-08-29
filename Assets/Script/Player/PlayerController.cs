using UnityEngine;

namespace Assets.Script.Player
{
    public class PlayerController : MonoBehaviour
    {
        private PlayerMove _mover;
        private StatPlayer _stats;
        private PlayerInputData _inputData;

        public bool CanMove { get; set; } = true;

        private void Awake()
        {
            _mover = GetComponent<PlayerMove>();
            _stats = GetComponent<StatPlayer>();
        }

        private void Update()
        {
            if (!CanMove)
            {
                _mover.Stop();
                return;
            }

            HandleMovement();
        }

        private void HandleMovement()
        {
            if (_inputData.MoveDirection != Vector2.zero)
            {
                float targetSpeed = _inputData.IsRunning ? _stats.RunSpeed : _stats.WalkingSpeed;
                _mover.Move(_inputData.MoveDirection, targetSpeed);
            }
            else
            {
                _mover.Stop();
            }
        }

        internal void SetInput(PlayerInputData inputData)
        {
            _inputData = inputData;
        }
    }
}
