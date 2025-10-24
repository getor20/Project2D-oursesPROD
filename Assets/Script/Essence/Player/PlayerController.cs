using UnityEngine;

namespace Assets.Script.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private InventoryUI _inventoryUI;
        [SerializeField] private ControllerStatBar _controllerStatBar;
        [SerializeField] private WeaponRotation _weaponRotation;
        [SerializeField] private MeleeAttacker _meleeAttacker;


        [SerializeField] private float _timerLimit = 1f;
        [SerializeField] private float _timer;

        private PlayerInputData _inputData;

        private PlayerMove _mover;
        private StatPlayer _stats;
        private LiftingObjects _liftingObjects;
        private Inventory _inventory;
        private ItemDropper _itemDropper;

        public bool IsInteraction { get; private set; }
        public bool DisplayInventory { get; private set; }
        public bool IsTrigger { get; private set; }
        public bool CanMove { get; set; } = true;

        private void Awake()
        {
            _mover = GetComponent<PlayerMove>();
            _stats = GetComponent<StatPlayer>();
            _liftingObjects = GetComponent<LiftingObjects>();
            _itemDropper = GetComponent<ItemDropper>();
            _inventory = GetComponent<Inventory>();
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

            SetStamina();

            HandleMovement();
            HandleCombat();
        }

        internal void SetInput(PlayerInputData inputData)
        {
            _inputData = inputData;
        }

        private void HandleMovement()
        {
            if (_inputData.MoveDirection != Vector2.zero)
            {
                float targetSpeed = _inputData.IsRunning ? _stats.RunSpeed : _stats.WalkingSpeed;
                _mover.Move(_inputData.MoveDirection, targetSpeed);
                _weaponRotation.SetDirection(_mover.DirectionVector);
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
        }

        private void SetStamina() 
        {
            if (_mover.CurrentSpeed > 0)
            {
                if (_timer >= _timerLimit)
                {
                    _stats.TakeMinStamina(10f);
                    _timer = 0f;
                    _controllerStatBar.UpdateStaminaBar(_stats.CurrentStamina);
                    Debug.Log("Stamina decreased by 1");
                }
                else
                {
                    Debug.Log("Stamina decreased by 0");
                    _timer += Time.deltaTime;
                }
            }
            else // Блок выполняется, когда _mover.CurrentSpeed <= 0 (персонаж остановился)
            {
                if (_timer >= 0f) // Проверяем, нужно ли вообще сбрасывать таймер
                {
                    _timer -= Time.deltaTime; // Уменьшаем таймер

                    // Гарантируем, что таймер не уйдет в минус
                    if (_timer <= 0f)
                    {
                        _timer = 0f;
                    }
                }
            }
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

        public void SetUseItem()
        {
            Debug.Log("Use item");
            _inventoryUI.OnUse();
            _stats.RestoreStamina(_inventoryUI.OnUses);
           // Debug.LogError($"Stamina restored by {_inventoryUI.OnUses}");
        }    
    }
}
