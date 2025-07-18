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
        _animator.SetFloat(PlayerAnimName.DirectionX, _playerMove._directionVector2.x);
        _animator.SetFloat(PlayerAnimName.DirectionY, _playerMove._directionVector2.y);
        _animator.SetFloat(PlayerAnimName.IsMainSpeed, _playerMove._mainSpeed);
        _animator.SetFloat(PlayerAnimName.AngularDirectionX,_playerMove._directionVector.x);
        _animator.SetFloat(PlayerAnimName.AngularDirectionY, _playerMove._directionVector.y);
        _animator.SetFloat(PlayerAnimName.NewFloat, _playerMove._transitionDirect);
        _animator.SetFloat(PlayerAnimName.NewFloat0, _playerMove._transitionAngular);
    }
}