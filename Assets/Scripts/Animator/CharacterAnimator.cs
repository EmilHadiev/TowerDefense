using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private const string Attacking = "IsAttacking";
    private const string Running = "IsRunning";

    private void OnValidate()
    {
        if (_animator == null)
            _animator = GetComponent<Animator>();
    }

    public void StartRunning() => _animator.SetBool(Running, true);
    public void StopRunning() => _animator.SetBool(Running, false);

    public void StartAttacking() => _animator.SetBool(Attacking, true);
    public void StopAttacking() => _animator.SetBool(Attacking, false);
}