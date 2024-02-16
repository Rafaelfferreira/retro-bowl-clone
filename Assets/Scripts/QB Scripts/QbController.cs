using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class QbController : MonoBehaviour
{
    // MARK: - Stored game assets
    private Rigidbody2D rb;
    private Animator anim;
    private PlayerInput playerInput;
    private QBStateMachine stateMachine;

    // MARK: - Movement properties
    [SerializeField] private float moveSpeed;
    private Vector2 movementVector = Vector2.zero;
    private bool isAiming = false;

    // MARK: - OBJECT LIFECYCLE
    private void Awake()
    {
        playerInput = new PlayerInput();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        stateMachine = new QBStateMachine(anim);
    }

    // Start is called before the first frame update
    void Start()
    {
        stateMachine.Initialize(stateMachine.preSnapState);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = movementVector * moveSpeed;
        stateMachine.currentState.Update();
    }

    private void OnMovementPerformed(InputAction.CallbackContext value)
    {
        movementVector = value.ReadValue<Vector2>();
        stateMachine.ChangeState(stateMachine.dropBackState);
    }

    private void OnMovementCancelled(InputAction.CallbackContext value)
    {
        movementVector = Vector2.zero;
        stateMachine.ChangeState(isAiming ? stateMachine.aimingState : stateMachine.idleState);
    }

    private void OnAimPerformed(InputAction.CallbackContext value)
    {
        isAiming = true;
        if (movementVector == Vector2.zero)
            stateMachine.ChangeState(stateMachine.aimingState);
    }
    private void OnAimCancelled(InputAction.CallbackContext value)
    {
        isAiming = false;
        stateMachine.ChangeState(stateMachine.idleState);
    }

    // MARK: - INPUT ACTION LIFE CYCLE
    #region
    private void OnEnable()
    {
        playerInput.Enable();
        playerInput.QB.Movement.performed += OnMovementPerformed;
        playerInput.QB.Movement.canceled += OnMovementCancelled;
        playerInput.QB.Aim.performed += OnAimPerformed;
        playerInput.QB.Aim.canceled += OnAimCancelled;

    }

    private void OnDisable()
    {
        playerInput.Disable();
        playerInput.QB.Movement.performed -= OnMovementPerformed;
        playerInput.QB.Movement.canceled -= OnMovementCancelled;
        playerInput.QB.Aim.performed -= OnAimPerformed;
        playerInput.QB.Aim.canceled -= OnAimCancelled;
    }
    #endregion
}
