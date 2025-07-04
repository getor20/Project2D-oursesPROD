using Assets.Script.Player;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator _Animator;
    private PlayerScript _PlayerScript;

    private void Awake()
    {
        _Animator = GetComponent<Animator>();
        _PlayerScript = GetComponent<PlayerScript>();
    }

    private void Update()
    {
        _Animator.SetFloat(PlayerAnimName.MoveY, _PlayerScript.directionVector2.y);
        _Animator.SetFloat(PlayerAnimName.MoveX, _PlayerScript.directionVector2.x);
    }
}