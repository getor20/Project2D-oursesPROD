using UnityEngine;

public class Enemy1Animator : MonoBehaviour
{
    private Animator _animator;
    private Enemy1Move _enemy1Move;

    private int _isSpeedHash = Animator.StringToHash("IsSpeed");
    private int _directionXHash = Animator.StringToHash("DirectionX");
    private int _directionYHash = Animator.StringToHash("DirectionY");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _enemy1Move = GetComponent<Enemy1Move>();
    }

    private void Update()
    {
        _animator.SetFloat(_isSpeedHash, _enemy1Move.CurrentSpeed);
    }

    public void SetDirection(Vector2 direction)
    {
        _animator.SetFloat(_directionXHash, direction.x);
        _animator.SetFloat(_directionYHash, direction.y);
    }
}
