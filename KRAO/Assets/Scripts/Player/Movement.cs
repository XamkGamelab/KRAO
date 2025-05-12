using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private CharacterController controller => GetComponent<CharacterController>();

    public bool MovementEnabled = false;

    [SerializeField] private float movementSpeed = 7.5f;
    [SerializeField] private float movementDamping = 0.06f;
    private InputAction move;
    private Vector3 movement = Vector3.zero;

    private void Start()
    {
        move = InputSystem.actions.FindAction("Move");
    }

    private void Update()
    {
        if (!MovementEnabled)
        {
            return;
        }
        Vector2 _input = move.ReadValue<Vector2>();
        Vector3 _newMovement = ((transform.right * _input.x) + (transform.forward * _input.y)).normalized * Time.deltaTime * movementSpeed;

        movement = Vector3.MoveTowards(movement, _newMovement, movementDamping * Time.deltaTime);

        controller.Move(movement);
    }
}
