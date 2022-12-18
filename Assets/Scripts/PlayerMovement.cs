using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float speed = 8f;
    
    private Vector3 mov;
    private Vector3 input;
    private Vector2 mouseInput;

    private CharacterController controler;
    private float gravity;

    private float mouseX, mouseY, pitch;
    [SerializeField]
    private float sens;

    private bool isLocked = false;
    [Header("Audio")]
    [SerializeField] private float stepDelay = 0.3f;
    float stepTime = 0;
    [SerializeField] private AudioSource stepSource;
    [SerializeField] private AudioClip[] steps;
    private void Awake()
    {
        controler = gameObject.GetComponent<CharacterController>();
    }
    void Start()
    {
        gravity = Physics.gravity.y;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Update()
    {
        mouseX = (mouseInput.x * sens) * Time.deltaTime;
        mouseY = (mouseInput.y * sens) * Time.deltaTime;

        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -90f, 90f);
        playerCamera.transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        if (isLocked) return;
        Vector3 movementVector = transform.right * input.x + transform.forward * input.y;
        if (input.magnitude > 0.1 && controler.isGrounded)
        {
            stepTime += Time.deltaTime;
            if(stepTime > stepDelay)
            {
                stepSource.PlayOneShot(steps[Random.Range(0,steps.Length)]);
                stepTime = 0;
            }
        }
        else
        {
            stepTime = stepDelay*0.5f;
        }
        controler.Move(movementVector * speed * Time.deltaTime);
        if (mov.y > gravity)
        {
            mov.y += gravity * Time.deltaTime;
        }
        controler.Move(mov * Time.deltaTime);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;


        if (body == null || body.isKinematic)
        {
            return;
        }

        // We dont want to push objects below us
        if (hit.moveDirection.y < -0.3)
        {
            return;
        }

        // Calculate push direction from move direction,
        // we only push objects to the sides never up and down
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        // If you know how fast your character is trying to move,
        // then you can also multiply the push velocity by that.

        // Apply the push
        body.velocity = pushDir * 2;
    }
    public void Move(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
    }
    public void Look(InputAction.CallbackContext context)
    {
        mouseInput = context.ReadValue<Vector2>();
    }
    public void Fire(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            RaycastHit hit;
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.TransformDirection(Vector3.forward), out hit, 5))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                if (hit.transform.gameObject.TryGetComponent(out Button button))
                {
                    button.Interact();
                }
            }
        }
    }
    public void Teleport(Transform pos)
    {
        controler.enabled = false;
        transform.position = pos.position;
        controler.enabled = true;
    }
    public void Die()
    {
        isLocked = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void BreakLegs()
    {
        isLocked = true;
    }
}