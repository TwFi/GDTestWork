using UnityEngine;

public class PlayerControlAnimator : MonoBehaviour
{
    const string speedParamName = "Speed";

    [SerializeField] PlayerController controller;
    [SerializeField] Animator animator;

    private void Start()
    {
        controller = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();

        controller.OnStartMoving += StartMoving;
        controller.OnStopMoving += StopMoving;
        controller.OnChangeDirection += ChangeDirection;
    }

    private void OnDestroy()
    {
        controller.OnStartMoving -= StartMoving;
        controller.OnStopMoving -= StopMoving;
        controller.OnChangeDirection -= ChangeDirection;
    }

    private void StartMoving()
    {
        animator.SetFloat(speedParamName, controller.Speed);
    }

    private void ChangeDirection(Vector3 direction)
    {
        controller.transform.rotation = Quaternion.LookRotation(direction);
    }

    private void StopMoving()
    {
        animator.SetFloat(speedParamName, 0);
    }
}