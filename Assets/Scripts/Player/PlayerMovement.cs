using Environment;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player {
    public class PlayerMovement : MonoBehaviour {
        // this is a test change for the git lecture
        [SerializeField]
        private Rigidbody rb;

        [SerializeField]
        private Animator animator = null;

        [Header("Input")]
        [SerializeField]
        private InputActionReference moveAction = null;
        [SerializeField]
        private InputActionReference jumpAction = null;
        [SerializeField]
        private InputActionReference interactAction = null;

        [Header("Movement")]
        [SerializeField]
        private float movementSpeed = 10f;
        [SerializeField]
        private float jumpSpeed = 50f;
        [SerializeField]
        float rotationSpeed = 5f;
        [Header("Interaction")]
        [SerializeField]
        private float interactionRange = 5f;

        private Vector2 movementInput = Vector2.zero;

        private void Awake() {
            if(!animator) {
                Debug.LogError("Animator not set");
                return;
            }

            if(!moveAction || !interactAction || !jumpAction) {
                Debug.LogError("Input actions not set");
                return;
            }

            moveAction.action.performed += OnMove;
            moveAction.action.canceled += OnMove;

            jumpAction.action.performed += OnJump;

            interactAction.action.performed += OnInteract;
        }

        private void OnMove(InputAction.CallbackContext context) {
            movementInput = context.ReadValue<Vector2>();
            animator.SetBool("bIsMoving", movementInput != Vector2.zero);
        }
        private void OnStop(InputAction.CallbackContext context) {
            
        }

        private void OnJump(InputAction.CallbackContext context) {
            rb.AddForce(Vector3.up*jumpSpeed, ForceMode.Impulse);
        }

        private void OnInteract(InputAction.CallbackContext context) {
            Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, interactionRange);
            IInteractable interactable = hit.collider?.gameObject.GetComponentInParent<IInteractable>();
            if(interactable != null) {
                interactable.OnInteract();
            }
        }

        private void Update() {
            Vector3 moveDir = new Vector3(movementInput.x, 0, movementInput.y).normalized;
            //transform.position += (new Vector3(movementInput.x, 0f, movementInput.y)) * (movementSpeed * Time.deltaTime);
            rb.AddForce(moveDir * movementSpeed * Time.deltaTime);
            //rb.MoveRotation(Quaternion.Euler(0, rb.rotation.x + moveDir.x * rotationSpeed, 0)); < test till att rotera kameran.
            //transform.Rotate(0, moveDir.x * rotationSpeed, 0);
        }


        private void OnEnable() {
            moveAction.action.Enable();
            interactAction.action.Enable();
        }

        private void OnDisable() {
            moveAction.action.performed -= OnMove;
            moveAction.action.canceled -= OnMove;

            interactAction.action.performed -= OnInteract;

            moveAction.action.Disable();
            interactAction.action.Disable();
        }
    }
}
