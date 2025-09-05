using UnityEngine;

namespace Assets.Script.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Transform _hitboxTransform;

        private PlayerMove _mover;
        private StatPlayer _stats;
        private PlayerInputData _inputData;
        private MeleeAttacker _meleeAttacker;

        public bool CanMove { get; set; } = true;

        private void Awake()
        {
            _mover = GetComponent<PlayerMove>();
            _stats = GetComponent<StatPlayer>();
            _meleeAttacker = GetComponent<MeleeAttacker>();
        }

        private void Update()
        {
            if (!CanMove)
            {
                _mover.Stop();
                return;
            }

            HandleMovement();
            HandleCombat();
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

        private void HandleCombat()
        {
            if (_inputData.IsAttacking)
            {
                _meleeAttacker.Attack();
            }
            TransformHelper.UpdateRotation(_hitboxTransform, _mover.MainDirection);
        }
            
        internal void SetInput(PlayerInputData inputData)
        {
            _inputData = inputData;
        }
    }
}
