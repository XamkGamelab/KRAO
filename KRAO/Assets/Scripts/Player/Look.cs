using UnityEngine;
using UnityEngine.InputSystem;

public class Look : MonoBehaviour
{
    private CharacterController controller => GetComponentInParent<CharacterController>();

    public bool LookEnabled = false;

    [SerializeField] float lookSensitivity = 20f;
    [SerializeField] float maxVerticalAngle = 75f;
    private InputAction look;
    private float verticalRotation = 0f;

    private void Start()
    {
        look = InputSystem.actions.FindAction("Look");
    }
    void Update()
    {
        if(!LookEnabled)
        {
            return;
        }

        //Get input
        Vector2 _input = look.ReadValue<Vector2>();

        //Horizontal rotation
        controller.transform.Rotate(Vector3.up, _input.x * Time.deltaTime * lookSensitivity);

        //Vertical rotation
        verticalRotation += -_input.y * Time.deltaTime * lookSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -maxVerticalAngle, maxVerticalAngle);

        Vector3 _rotationEulers = transform.rotation.eulerAngles;
        _rotationEulers.x = verticalRotation;
        Quaternion _newRotation = Quaternion.Euler(_rotationEulers);

        transform.rotation = _newRotation;
    }
}
