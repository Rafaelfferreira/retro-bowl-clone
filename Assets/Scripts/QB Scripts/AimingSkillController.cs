using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AimingSkillController : MonoBehaviour
{
    // MARK: - Aiming properties
    #region Aiming properties
    [Header("Aiming properties")]
    [SerializeField] private int numberOfDots;
    [SerializeField] private float spaceBetweenDots;
    [SerializeField] private GameObject dotsPrefab;
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Transform ThrowParentTransform;
    #endregion

    [Header("Debugging")]
    [SerializeField] private float throwAngle;
    [SerializeField] private float passStrenghtModifier;
    
    // MARK: - Aiming parameters
    [Header("Aiming parameters")]
    [SerializeField] private int playerStrenght;
    [SerializeField] private float ballGravity; // based on altitude of stadium?
    public Vector2 mouseInitialPosition;
    private float strenghtModifier;
    private int initialThrowingForceAdjustmentConstat = 5; 

    // MARK: - Stored properties
    private QbController qb;
    private GameObject[] dots;
    private int facingDirectionMultiplier {
        get 
        {
            return qb.isFacingLeft ? -1 : 1;
        }
    }

    // MARK: - Life cycle methods
    void Awake()
    {
        qb = GetComponentInParent<QbController>();
        strenghtModifier = playerStrenght / 3;
    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateDots();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < dots.Length; i++)
        {
            dots[i].transform.position = CalculateDotsPosition(i * spaceBetweenDots);
        }
    }

    // MARK: - Dots
    // Generate the dots based on the aiming properties parameters; does not activate dots
    private void GenerateDots()
    {
        dots = new GameObject[numberOfDots];
        for (int i = 0; i < numberOfDots; i++)
        {
            dots[i] = Instantiate(dotsPrefab, ThrowParentTransform.position, Quaternion.identity, ThrowParentTransform); // TODO: - Do we need this last parameter?
            dots[i].SetActive(false);
        }
    }

    public void UpdateThrowAngle()
    {
        Vector2 mouseCurrentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 baseThrowVector = new Vector2(1 * facingDirectionMultiplier,0).normalized; // A normalized horizontal vector that faces the direction the player is facing; used to calculate throw angle
        Vector2 mousePositionDiffVector = mouseInitialPosition - mouseCurrentPosition; // The vector indicating the difference between the initial mouse position and the current mouse position
        Vector2 normalizedThrowDirection = (baseThrowVector + mousePositionDiffVector).normalized; // The normalized vector indicating the direction the player is aiming at; Used to calculate throw angle
        
        int initialThrowAngleAdjustmentConstant = 15;        

        passStrenghtModifier = Mathf.Abs(mousePositionDiffVector.x * playerStrenght / 3);

        // FIXME: Remove debug code
        Debug.DrawLine(Vector2.zero, baseThrowVector, Color.yellow);
        Debug.DrawLine(Vector2.zero, normalizedThrowDirection, Color.green);
        throwAngle = Vector2.Angle(baseThrowVector, normalizedThrowDirection) * (mousePositionDiffVector.y > 0 ? 1 : -1) + initialThrowAngleAdjustmentConstant;

        if (throwAngle > 90 + initialThrowAngleAdjustmentConstant) {
            qb.Flip();
        }
    }

    private Vector2 CalculateDotsPosition(float t)
    {
        UpdateThrowAngle();

        float radianAngle = Mathf.Deg2Rad * throwAngle;
        Vector2 position = (Vector2)ThrowParentTransform.position + new Vector2(
            (initialThrowingForceAdjustmentConstat + passStrenghtModifier) * facingDirectionMultiplier * t * Mathf.Cos(radianAngle),
            (initialThrowingForceAdjustmentConstat + passStrenghtModifier) * t * Mathf.Sin(radianAngle) - (ballGravity * t * t/2)
        );

        return position;
    }

    public void SetDotsActive(bool _isActive)
    {
        for (int i = 0; i < dots.Length; i++)
        {
            dots[i].SetActive(_isActive);
        }
    }

    // MARK: - Ball
    public void CreateBall() {
        GameObject ball = Instantiate(ballPrefab, ThrowParentTransform.position, Quaternion.identity);
        BallController ballController = ball.GetComponent<BallController>();

        float radianAngle = Mathf.Deg2Rad * throwAngle;
        Vector2 ballVelocity = new Vector2(
            (initialThrowingForceAdjustmentConstat + 2 + passStrenghtModifier) * Mathf.Cos(radianAngle) * facingDirectionMultiplier,
            (initialThrowingForceAdjustmentConstat + 2 + passStrenghtModifier) * Mathf.Sin(radianAngle)
        );

        ballController.SetupBall(ballVelocity);
    }
}
