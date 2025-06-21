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
        _Animator.SetBool(PlayerAnimName.Move, _PlayerScript.move);
    }
}