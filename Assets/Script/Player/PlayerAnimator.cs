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
    private int _transitionAngular = Animator.StringToHash("TransitionAngular");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerMove = GetComponent<PlayerMove>();
    }

    private void Update()
    {
        _animator.SetFloat(_isSpeedHash, _playerMove.MainSpeed);
        _animator.SetFloat(_directionXHash, _playerMove.DirectionVector2.x);
        _animator.SetFloat(_directionYHash, _playerMove.DirectionVector2.y);
       // _animator.SetFloat(_angularDirectionXHash, _playerMove.DirectionVector.x);
        //_animator.SetFloat(_angularDirectionYHash, _playerMove.DirectionVector.y);
        _animator.SetFloat(_newFloatHash, _playerMove.TransitionDirect);
        _animator.SetFloat(_transitionAngular, _playerMove.TransitionAngular);
    }
}