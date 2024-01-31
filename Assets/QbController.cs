using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class QbController : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerControls playerInput;

    [SerializeField] private float moveSpeed;
    private Vector2 movementVector = Vector2.zero;

    private void Awake()
    {
        playerInput = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = movementVector * moveSpeed;
    }

    private void OnMovementPerformed(InputAction.CallbackContext value)
    {
        movementVector = value.ReadValue<Vector2>();
    }

    private void OnMovementCancelled(InputAction.CallbackContext value)
    {
        movementVector = Vector2.zero;
    }

    // MARK: - Subscribing to Input Actions life cycle
    private void OnEnable()
    {
        playerInput.Enable();
        playerInput.QB.Movement.performed += OnMovementPerformed;
        playerInput.QB.Movement.canceled += OnMovementCancelled;
    }

    private void OnDisable()
    {
        playerInput.Disable();
        playerInput.QB.Movement.performed -= OnMovementPerformed;
        playerInput.QB.Movement.canceled -= OnMovementCancelled;
    }
}
