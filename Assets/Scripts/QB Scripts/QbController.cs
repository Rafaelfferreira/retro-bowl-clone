using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class QbController : PlayerController
{
    // MARK: - Stored game assets
    private QBStateMachine stateMachine;
    private AimingSkillController aimingController;
    private PlayerInput playerInput;

    // MARK: - Movement properties
    [SerializeField] private float moveSpeed;
    private Vector2 movementVector = Vector2.zero;

    private bool _isAiming = false;
    private bool isAiming {
        get => _isAiming;
        set
        {
            aimingController.SetDotsActive(value);
            _isAiming = value;
        }
    }

    // MARK: - Subscribers
    private void GameManagerOnGameStateChanged(GameState newState) {
        // FIXME: - Implement proper state handling here in the future
    }

    // MARK: - Object Lifecycle
    private void Awake()
    {
        playerInput = new PlayerInput();
        stateMachine = new QBStateMachine(this, anim);
        aimingController = GetComponentInChildren<AimingSkillController>();
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged; // Subscribing to the GameManager event
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

    void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    }

    // MARK: - Input mapping
    #region
    private void OnMovementPerformed(InputAction.CallbackContext value)
    {
        movementVector = value.ReadValue<Vector2>();

        if (_isAiming) {
            stateMachine.ChangeState(stateMachine.dropBackAimingState);    
        } else {
            stateMachine.ChangeState(stateMachine.dropBackState);
        }   
    }

    private void OnMovementCancelled(InputAction.CallbackContext value)
    {
        movementVector = Vector2.zero;
        stateMachine.ChangeState(isAiming ? stateMachine.aimingState : stateMachine.idleState);
    }

    private void OnAimPerformed(InputAction.CallbackContext value)
    {
        isAiming = true;
        Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (movementVector == Vector2.zero) {
            aimingController.mouseInitialPosition = clickPosition;
            stateMachine.ChangeState(stateMachine.aimingState);
        } else {
            stateMachine.ChangeState(stateMachine.dropBackAimingState);
        }
            
    }
    private void OnAimCancelled(InputAction.CallbackContext value)
    {
        isAiming = false;
        stateMachine.ChangeState(stateMachine.idleState);
        GameManager.Instance.UpdateGameState(GameState.LiveBall);
        aimingController.CreateBall();
    }
    #endregion


    // MARK: - Input Action Life Cycle
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
