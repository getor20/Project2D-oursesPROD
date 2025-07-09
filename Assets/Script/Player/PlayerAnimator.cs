using Assets.Script.Player;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    private PlayerMove playerMove;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerMove = GetComponent<PlayerMove>();
    }

    private void Update()
    {
        animator.SetFloat(PlayerAnimName.MoveY, playerMove.moveDirection.y);
        animator.SetFloat(PlayerAnimName.MoveX, playerMove.moveDirection.x);
        animator.SetBool(PlayerAnimName.IsMove, playerMove.IsMove);
    }
}