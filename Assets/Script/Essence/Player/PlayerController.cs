using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Script.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private LayerMask _targetLayerMask;

        [SerializeField] private InventoryUI _inventoryUI;
        [SerializeField] private ControllerStatBar _controllerStatBar;
        [SerializeField] private WeaponRotation _weaponRotation;
        [SerializeField] private MeleeAttacker _meleeAttacker;
        [SerializeField] private Bonfire _bonfire;

        [SerializeField] private float _timerLimitStaminaWalking = 3.5f;
        [SerializeField] private float _timerLimitStaminaRunning = 2f;
        [SerializeField] private float _timerLimitStaminaMain;
        [SerializeField] private float _timerStamina;

        [SerializeField] private float _timerLimitHealth;
        [SerializeField] private float _timerHealth;

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

            bool isBlocked = IsInteraction || DisplayInventory;

            if (isBlocked)
            {
                // Если есть ЛЮБАЯ блокировка высокого приоритета, отключаем триггер.
                IsTrigger = false;
            }
            else
            {
                IsTrigger = _bonfire.IsTrigger || _liftingObjects.IsTrigger;
               
            }

            SetHp();
            SetStamina();

            //Debug.Log($"Current Health: {_stats.CurrentHealth}");

            HandleMovement();
            HandleCombat();

            Debug.Log($"IsTrigger1: {IsTriggeri}");
        }

        internal void SetInput(PlayerInputData inputData)
        {
            _inputData = inputData;
        }

        private void HandleMovement()
        {
            float targetSpeed;
            if (_inputData.MoveDirection != Vector2.zero)
            {
                if (_stats.CurrentStamina <= 0f)
                {
                    // Если выносливость равна нулю, игрок не может бежать
                    targetSpeed = _stats.SlowSpeed;
                }
                else
                {
                    targetSpeed = _inputData.IsRunning ? _stats.RunSpeed : _stats.WalkingSpeed;

                }
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
                _meleeAttacker.Attack(_targetLayerMask);
            }
        }

        private void SetHp()
        {
            _controllerStatBar.UpdateHealthBar(_stats.CurrentHealth);

            if (_stats.CurrentHealth < _stats.HP)
            {
                // Debug.LogError($"Current Stamina for Health Regen: {_stats.CurrentStamina}");
                if (_stats.CurrentStamina == 1f)
                {
                    //Debug.LogError($"Max Stamina for Health Regen: {_stats.CurrentStamina}");
                    _timerLimitHealth = 2;
                }
                else if (_stats.CurrentStamina == 0.9f)
                {
                    //Debug.LogError($"Max Stamina for Health Regen: {_stats.CurrentStamina}");
                    _timerLimitHealth = 3;
                }
                else if (_stats.CurrentStamina == 0.8f)
                {
                    //Debug.LogError($"Max Stamina for Health Regen: {_stats.CurrentStamina}");
                    _timerLimitHealth = 4;
                }
                else if (_stats.CurrentStamina == 0.7f)
                {
                    // Debug.LogError($"Max Stamina for Health Regen: {_stats.CurrentStamina}");
                    _timerLimitHealth = 5;
                }
                else if (_stats.CurrentStamina == 0.6f)
                {
                    //Debug.LogError($"Max Stamina for Health Regen: {_stats.CurrentStamina}");
                    _timerLimitHealth = 6;
                }
                else if (_stats.CurrentStamina == 0.5f)
                {
                    //Debug.LogError($"Max Stamina for Health Regen: {_stats.CurrentStamina}");
                    _timerLimitHealth = 7;
                }
                else if (_stats.CurrentStamina == 0.4f)
                {
                    //Debug.LogError($"Max Stamina for Health Regen: {_stats.CurrentStamina}");
                    _timerLimitHealth = 8;
                }
                else if (_stats.CurrentStamina == 0.3f)
                {
                    //Debug.LogError($"Max Stamina for Health Regen: {_stats.CurrentStamina}");
                    _timerLimitHealth = 9;
                }
                else if (_stats.CurrentStamina == 0.2f)
                {
                    //Debug.LogError($"Max Stamina for Health Regen: {_stats.CurrentStamina}");
                    _timerLimitHealth = 10;
                }
                else if (_stats.CurrentStamina == 0.1f)
                {
                    //Debug.LogError($"Max Stamina for Health Regen: {_stats.CurrentStamina}");
                    _timerLimitHealth = 11;
                }
                else if (_stats.CurrentStamina == 0f)
                {
                    //Debug.LogError($"Max Stamina for Health Regen: {_stats.CurrentStamina}");
                    _timerLimitHealth = 0;

                }

                if (_timerHealth >= _timerLimitHealth)
                {
                    _stats.SetHealth(10f);
                    _timerHealth = 0f;
                    _timerHealth -= Time.deltaTime;
                }
                else
                {
                    // Отсчет
                    _timerHealth += Time.deltaTime;
                }
            }
            else { _timerHealth = 0f; }

        }

        private void SetStamina()
        {
            _controllerStatBar.UpdateStaminaBar(_stats.CurrentStamina);

            if (_mover.CurrentSpeed > 0)
            {
                _timerLimitStaminaMain = _inputData.IsRunning ? _timerLimitStaminaRunning : _timerLimitStaminaWalking;

                if (_timerStamina >= _timerLimitStaminaMain)
                {
                    _stats.TakeMinStamina(10f);
                    _timerStamina = 0f;
                    _controllerStatBar.UpdateStaminaBar(_stats.CurrentStamina);
                    //Debug.Log("Stamina decreased by 1");
                }
                else
                {
                    //Debug.Log("Stamina decreased by 0");
                    _timerStamina += Time.deltaTime;
                }
            }
            else
            {
                if (_timerStamina >= 0f)
                {
                    _timerStamina -= Time.deltaTime;


                    if (_timerStamina <= 0f)
                    {
                        _timerStamina = 0f;
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
            if (_bonfire.IsTrigger)
            {
                Debug.LogFormat ("Bonfire used");
                SceneManager.LoadScene(2);
            }
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
           // Debug.Log("Use item");
            _inventoryUI.OnUse();
            _stats.RestoreStamina(_inventoryUI.OnUses);
            
            // Debug.LogError($"Stamina restored by {_inventoryUI.OnUses}");
        }

        public void OnDeath()
        {
            gameObject.SetActive(false);
        }

        private bool IsTriggeri;

        public void IsTriggers()
        {
            Debug.LogFormat("Trigger true");
            IsTriggeri = true;
        }
    }
}
