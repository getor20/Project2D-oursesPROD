using UnityEngine;

public class ScrapperAnimator : MonoBehaviour
{
    private Animator _animator;
    private ScrapperMove _scrapperMove;

    private int _isSpeedHash = Animator.StringToHash("IsSpeed");
    private int _directionXHash = Animator.StringToHash("DirectionX");
    private int _directionYHash = Animator.StringToHash("DirectionY");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _scrapperMove = GetComponent<ScrapperMove>();
    }

    private void Update()
    {
        _animator.SetFloat(_isSpeedHash, _scrapperMove.CurrentSpeed);
    }

    public void SetDirection(Vector2 direction)
    {
        _animator.SetFloat(_directionXHash, direction.x);
        _animator.SetFloat(_directionYHash, direction.y);
    }
}
