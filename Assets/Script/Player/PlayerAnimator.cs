using UnityEngine;


public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    private PlayerMove _playerMove;

    private int _isSpeedHash = Animator.StringToHash("IsSpeed");
    private int _directionXHash = Animator.StringToHash("DirectionX");
    private int _directionYHash = Animator.StringToHash("DirectionY");
    private int _angularDirectionXHash = Animator.StringToHash("AngularDirectionX");
    private int _angularDirectionYHash = Animator.StringToHash("AngularDirectionY");
    private int _newFloatHash = Animator.StringToHash("New Float");
    private int _newFloat0Hash = Animator.StringToHash("New Float 0");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerMove = GetComponent<PlayerMove>();
    }

    private void Update()
    {
        _animator.SetFloat(_isSpeedHash, _playerMove._mainSpeed);
        _animator.SetFloat(_directionXHash, _playerMove._directionVector2.x);
        _animator.SetFloat(_directionYHash, _playerMove._directionVector2.y);
        _animator.SetFloat(_angularDirectionXHash, _playerMove._directionVector.x);
        _animator.SetFloat(_angularDirectionYHash, _playerMove._directionVector.y);
        _animator.SetFloat(_newFloatHash, _playerMove._transitionDirect);
        _animator.SetFloat(_newFloat0Hash, _playerMove._transitionAngular);
    }
}