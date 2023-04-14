using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private static readonly int IsRun = Animator.StringToHash("isRun");
    private Animator _animator;
    
    public void StartMoveAnimation()
    {
        _animator.SetBool(IsRun, true);
    }

    public void EndMoveAnimation()
    {
        _animator.SetBool(IsRun, false);
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
}