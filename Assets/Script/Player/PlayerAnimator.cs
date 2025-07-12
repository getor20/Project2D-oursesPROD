using Assets.Script.Player;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    private PlayerMove _playerMove;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerMove = GetComponent<PlayerMove>();
    }

    private void Update()
    {
        _animator.SetFloat(PlayerAnimName.MoveY, _playerMove.MoveDirection.y);
        _animator.SetFloat(PlayerAnimName.MoveX, _playerMove.MoveDirection.x);
        _animator.SetFloat(PlayerAnimName.IsMainSpeed, _playerMove._mainSpeed);
    }
}