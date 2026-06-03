using UnityEngine;
using UnityEngine.InputSystem;

public class MoveCharacterController : MonoBehaviour
{
    [SerializeField] private InputActionAsset input;
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float turnSpeed = 150f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private string mapName = "Player";
    private Animator animator;
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction sprintAction;

    private Rigidbody rb;
    //private bool isGrounded = false;

   // private CharacterController characterController;

    void Awake()
    {
       
        animator = GetComponent<Animator>();
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


        animator.SetFloat("Speed", speed);
        //animator.SetBool("Grounded", Grounded);





        // Springen
        if (jumpAction.WasPressedThisFrame())
        {
            animator.SetTrigger("Jump");
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            
        }

    }

    //void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Ground"));
           
    //}

    //void OnCollisionExit(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Ground"));
            
    //}
}
















//using UnityEngine;
//using UnityEngine.InputSystem;

//public class MoveCharacterController : MonoBehaviour
//{
//    [SerializeField] private InputActionAsset inputAsset;
//    [SerializeField] private string mapName = "Player";
//    [SerializeField] private float moveSpeed = 5f;
//    [SerializeField] private float sprintMultiplier = 2f;
//    [SerializeField] private float rotationSpeed = 150f;
//    [SerializeField] private float jumpHeight = 2f;
//    [SerializeField] private float gravity = -20f;

//    private InputActionMap map;

//    private InputAction moveAction;
//    private InputAction sprintAction;
//    private InputAction jumpAction;

//    private CharacterController characterController;
//    private Animator animator;
//    private float verticalVelocity;
//    private bool isGrounded = false;


//    void Awake()
//    {
//        characterController = GetComponent<CharacterController>();
//        animator = GetComponent<Animator>();

//        InputActionMap map = inputAsset.FindActionMap(mapName);

//        map = inputAsset.FindActionMap(mapName);

//        moveAction = map.FindAction("Move");
//        sprintAction = map.FindAction("Sprint");
//        jumpAction = map.FindAction("Jump");
//    }

//    void OnEnable()
//    {
//        map.Enable();
//    }
//    void OnDisable() 
//    { 
//        map.Disable(); 
//    }



//    void Update()
//    {
//        Vector2 movementInput = moveAction.ReadValue<Vector2>();

//        float speed = movementInput.y * moveSpeed;
//        if (sprintAction.IsPressed())
//            speed *= sprintMultiplier;

//        Vector3 move = transform.forward * speed * Time.deltaTime;
//        transform.Rotate(Vector3.up * movementInput.x * rotationSpeed * Time.deltaTime);

//        if (characterController.isGrounded)
//        {
//            verticalVelocity = -1f;
//            if (jumpAction.WasPressedThisFrame())
//            {
//                verticalVelocity = Mathf.Sqrt(2f * Mathf.Abs(gravity) * jumpHeight);
//                animator.SetTrigger("JumpTrigger");
//            }
//        }
//        else
//        {
//            verticalVelocity += gravity * Time.deltaTime;
//        }

//        move.y = verticalVelocity * Time.deltaTime;

//        characterController.Move(move);

//        animator.SetFloat("Speed", movementInput.y);
//        animator.SetBool("Grounded", characterController.isGrounded);
//    }
//    void OnCollisionEnter(Collision collision)
//    {
//        if (collision.gameObject.CompareTag("Ground"))
//            isGrounded = true;
//    }

//    void OnCollisionExit(Collision collision)
//    {
//        if (collision.gameObject.CompareTag("Ground"))
//            isGrounded = false;
//    }
//}
