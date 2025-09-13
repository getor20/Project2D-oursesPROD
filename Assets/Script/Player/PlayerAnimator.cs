using UnityEngine;


public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    private PlayerMove _playerMove;

    private int _isSpeedHash = Animator.StringToHash("IsSpeed");
    private int _directionXHash = Animator.StringToHash("DirectionX");
    private int _directionYHash = Animator.StringToHash("DirectionY");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerMove = GetComponent<PlayerMove>();
    }

    private void Update()
    {
        _animator.SetFloat(_isSpeedHash, _playerMove.CurrentSpeed);
        _animator.SetFloat(_directionXHash, _playerMove.DirectionVector.x);
        _animator.SetFloat(_directionYHash, _playerMove.DirectionVector.y);
    }
}