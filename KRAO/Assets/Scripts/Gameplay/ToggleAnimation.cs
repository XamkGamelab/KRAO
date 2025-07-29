using UnityEngine;

public class ToggleAnimation : MonoBehaviour
{
    private Animator animator => GetComponent<Animator>();

    public void ToggleAnimationState()
    {
        animator.SetTrigger("Toggle");
    }
}
