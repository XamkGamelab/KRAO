using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private CharacterController controller => GetComponent<CharacterController>();

    public bool MovementEnabled = false;

    [SerializeField] private float movementSpeed = 7.5f;
    [SerializeField] private float movementDamping = 0.06f;
    [SerializeField] private float movementDampingWeb = 0.375f;
    [SerializeField] private float breakForce = 1.5f;
    private InputAction move;
    private Vector3 movement = Vector3.zero;

    [SerializeField] LayerMask groundLayer;

    Vector2 _input;

    public void StopMovement()
    {
        movement = Vector3.zero;
    }

    private void Start()
    {
        move = InputSystem.actions.FindAction("Move");

        #if !UNITY_EDITOR && UNITY_WEBGL
            movementDamping = movementDampingWeb;
        #endif
    }

    private void Update()
    {
        if (!MovementEnabled)
        {
            return;
        }
        Vector2 _input = move.ReadValue<Vector2>();
        Vector3 _newMovement = ((transform.right * _input.x) + (transform.forward * _input.y)).normalized * Time.deltaTime * movementSpeed;

        MotionInputOpposition(_newMovement);

        movement = Vector3.MoveTowards(movement, _newMovement, movementDamping * Time.deltaTime);

        if(movement.magnitude > 0)
        {
            controller.Move(movement + GroundPlayer());
        }
    }

    //If the player is going into a direction, then inputs the opposite direction, this is used to make the transition more immediate for a better game feel
    private void MotionInputOpposition(Vector3 _motion)
    {
        if (movement.x > 0)
        {
            if (_motion.x < 0)
            {
                movement.x /= breakForce;
            }
        }
        if (movement.x < 0)
        {
            if (_motion.x > 0)
            {
                movement.x /= breakForce;
            }
        }

        if (movement.z > 0)
        {
            if (_motion.z < 0)
            {
                movement.z /= breakForce;
            }
        }
        if (movement.z < 0)
        {
            if (_motion.z > 0)
            {
                movement.z /= breakForce;
            }
        }
    }

    private Vector3 GroundPlayer()
    {
        RaycastHit _hit;
        if (Physics.Raycast(controller.transform.position, Vector3.down, out _hit, 10f, groundLayer))
        {
            return _hit.point - controller.transform.position;
        }
        else return Vector3.zero;
    }
}
