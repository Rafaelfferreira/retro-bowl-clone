using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingSkillController : MonoBehaviour
{
    // MARK: - Aiming properties
    [Header("Aiming properties")]
    [SerializeField] private int numberOfDots;
    [SerializeField] private float spaceBetweenDots;
    [SerializeField] private GameObject dotsPrefab;
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Transform ThrowParentTransform;

    [Header("Debugging")]
    [SerializeField] private float DEBUG_ANGLE;
    private QbController qb;
    
    // MARK: - Aiming parameters
    [Header("Aiming parameters")]
    [SerializeField] private int playerStrenght;
    [SerializeField] private float ballGravity; // based on altitude of stadium?
    public Vector2 mouseInitialPosition;
    private float strenghtModifier;

    // MARK: - Stored properties
    private GameObject[] dots;

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

    public Vector2 AimDirection()
    {
        Vector2 mouseCurrentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        DEBUG_ANGLE = 1 * Vector2.SignedAngle(mouseInitialPosition, mouseCurrentPosition) + 20;
        return mouseInitialPosition - mouseCurrentPosition; // a vector connecting the mouse to the player
    }

    private Vector2 CalculateDotsPosition(float t)
    {
        Vector2 aimDirection = AimDirection();
        float facingDirectionMultiplier = qb.isFacingLeft ? -1 : 1;
        
        // TODO: - We might still want to tweak this around a little bit, to make sure it makes sense with attributes between 1-10 which is the initial goal
        // The 3 components of the posistion are:
        // 1. The aim direction vector
        // 2. A constant modifier
        // 3. A strenght modifier based on the player's strenght
        // Vector2 position = (Vector2)ThrowParentTransform.position + new Vector2(
        //     ((aimDirection.x + (15 * facingDirectionMultiplier) + (aimDirection.x * strenghtModifier))),
        //     ((aimDirection.y + 10 + (aimDirection.y * strenghtModifier)))
        // ) * t + .5f * (Physics2D.gravity * ballGravity) * (t * t);
        float radianAngle = Mathf.Deg2Rad * DEBUG_ANGLE;

        Vector2 position = (Vector2)ThrowParentTransform.position + new Vector2(
            playerStrenght * t * Mathf.Cos(radianAngle),
            playerStrenght * t * Mathf.Sin(radianAngle) - (ballGravity * t * t)/2
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
        
        Vector2 aimDirection = AimDirection();
        float facingDirectionMultiplier = qb.isFacingLeft ? -1 : 1;

        float radianAngle = Mathf.Deg2Rad * DEBUG_ANGLE;

        Vector2 position = new Vector2(
            playerStrenght * Mathf.Cos(radianAngle),
            playerStrenght * Mathf.Sin(radianAngle)
        );

        ballController.SetupBall(position);
    }
}
