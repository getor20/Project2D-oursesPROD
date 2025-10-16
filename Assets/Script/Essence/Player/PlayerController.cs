using UnityEngine;

namespace Assets.Script.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Transform _hitboxTransform;
        [SerializeField] private InventoryUI _inventoryUI;

        private PlayerInputData _inputData;

        private PlayerMove _mover;
        private StatPlayer _stats;
        private MeleeAttacker _meleeAttacker;
        private LiftingObjects _liftingObjects;
        private ItemDropper _itemDropper;

        public bool IsInteraction { get; private set; }
        public bool DisplayInventory { get; private set; }
        public bool IsTrigger { get; private set; }
        public bool CanMove { get; set; } = true;

        private void Awake()
        {
            _mover = GetComponent<PlayerMove>();
            _stats = GetComponent<StatPlayer>();
            _meleeAttacker = GetComponent<MeleeAttacker>();
            _liftingObjects = GetComponent<LiftingObjects>();
            _itemDropper = GetComponent<ItemDropper>();
        }

        private void Update()
        {
            if (!CanMove)
            {
                _mover.Stop();
                return;
            }

            if (IsInteraction == true)
            {
                // Заняты другим делом, отключаем индикатор
                IsTrigger = false;
            }
            // 2. ИЛИ проверяем, открыт ли инвентарь
            else if (DisplayInventory == true)
            {
                // Инвентарь открыт, отключаем индикатор
                IsTrigger = false;
            }
            // 3. Иначе (если ничто не блокирует) — используем реальное значение от скрипта подбора
            else
            {
                IsTrigger = _liftingObjects.IsTrigger;
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
            TransformHelper.UpdateRotation(_hitboxTransform, _mover.DirectionVector);
        }

        public void SetInventory(bool isInInventory)
        {
            DisplayInventory = isInInventory;
            //Debug.Log($"Inventory state: {DisplayInventory}");
        } 

        public void SetInteraction(bool isInteracting)
        {
            IsInteraction = isInteracting;
            _liftingObjects.Interaction(IsInteraction);
            //Debug.Log($"Interaction state: {IsInteraction}");
        }

        public void SetDropItem()
        {
            Debug.Log($"Drop item");
            _inventoryUI.OnDrop();
            //_itemDropper.DropItem();
        }

        internal void SetInput(PlayerInputData inputData)
        {
            _inputData = inputData;
        }
    }
}
