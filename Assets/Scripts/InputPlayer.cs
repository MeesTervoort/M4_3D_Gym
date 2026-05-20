using UnityEngine;
using UnityEngine.InputSystem;

public class InputPlayer : MonoBehaviour
{
    [SerializeField] private InputActionAsset input;
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float turnSpeed = 150f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private string mapName = "Player";

    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction sprintAction;

    private Rigidbody rb;
    private bool isGrounded = false;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckRadius = 0.2f;

    void Awake()
    {
        InputActionMap map = input.FindActionMap(mapName);
        moveAction = map.FindAction("Move");
        jumpAction = map.FindAction("Jump");
        sprintAction = map.FindAction("Sprint");
        rb = GetComponent<Rigidbody>();
    }

    void OnEnable() { input.FindActionMap(mapName).Enable(); }
    void OnDisable() { input.FindActionMap(mapName).Disable(); }

    void Update()
    {
        // Opvragen van de input
        Vector2 moveInput = moveAction.ReadValue<Vector2>();

        //bepalen wat de snelheid is
        float speed = walkSpeed * moveInput.y;

        //sprinten
        if (sprintAction.IsPressed())
            speed *= 2f;

        //bewegen van de speler
        Vector3 movement = transform.forward * speed * Time.deltaTime;
        transform.Translate(movement, Space.World);

        //draaien van de speler
        float angle = moveInput.x * turnSpeed * Time.deltaTime;
        transform.Rotate(0f, angle, 0f, Space.World);


        // Springen
        if (jumpAction.WasPressedThisFrame() && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        if (jumpAction.WasPressedThisFrame() && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = true;
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = false;
    }

    private bool IsGrounded()
    {
        // Controleer vlak onder het object of er grond is
        return Physics.CheckSphere(
            transform.position + Vector3.down * 0.5f,
            groundCheckRadius,
            groundLayer
        );
    }
}


//using UnityEngine;
//using UnityEngine.InputSystem;

//public class InputPlayer : MonoBehaviour
//{
//    [SerializeField] private InputActionAsset input;
//    [SerializeField] private float WalkSpeed = 5f;
//    [SerializeField] private float TurnSpeed = 150f;
//    [SerializeField] private float JumpForce = 5f;
//    [SerializeField] private string MapName = "Player";

//    private InputAction MoveAction;
//    private InputAction JumpAction;
//    private InputAction SprintAction;

//    private Rigidbody rb;
//    private bool IsGrounded = false;

//    void Awake()
//    {
//        InputActionMap map = input.FindActionMap(MapName);
//        MoveAction = map.FindAction("Move");
//        JumpAction = map.FindAction("Jump");
//        SprintAction = map.FindAction("Sprint");
//        rb = GetComponent<Rigidbody>();
//    }
//    private void OnEnable()
//    {
//        input.FindActionMap(MapName).Enable();
//    }
//    private void OnDisable()
//    {
//        input.FindActionMap(MapName).Disable();
//    }

//    void Update()
//    {
//        Vector2 moveInput = MoveAction.ReadValue<Vector2>();

//        float Speed = WalkSpeed * moveInput.y;

//        if (SprintAction.IsPressed())
//        Speed *= 2f;

//        Vector3 movement = transform.forward * Speed * Time.deltaTime;
//        transform.Translate(movement, Space.World);

//        if (JumpAction.WasPressedThisFrame() && IsGrounded)
//        {
//            rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
//            IsGrounded = false;
//        } 

//        void OnCollisionEnter(Collision collision)
//        {
//            if (collision.gameObject.CompareTag("ground"))
//                IsGrounded = true;
//        }

//        void OnCollisionExit(Collision collision)
//        {
//            if (collision.gameObject.CompareTag("ground"))
//                IsGrounded = false;
//        }

//    }
//}
