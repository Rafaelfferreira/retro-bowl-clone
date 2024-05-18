using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingSkillController : MonoBehaviour
{
    // MARK: - Aiming properties
    [Header("Aiming properties")]
    [SerializeField] private int numberOfDots;
    [SerializeField] private float spaceBetweenDots;
    [SerializeField] private  GameObject dotsPrefab;
    [SerializeField] private Transform dotsParentTransform;
    [SerializeField] private QbController qb;
    
    // MARK: - Aiming parameters
    [Header("Aiming parameters")]
    [SerializeField] private int playerStrenght;
    [SerializeField] private float ballGravity; // based on altitude of stadium?
    public Vector2 mouseInitialPosition;

    // MARK: - Stored properties
    private GameObject[] dots;

    // MARK: - Life cycle methods
    void Awake()
    {
        qb = GetComponentInParent<QbController>();
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

    // Generate the dots based on the aiming properties parameters; does not activate dots
    private void GenerateDots()
    {
        dots = new GameObject[numberOfDots];
        for (int i = 0; i < numberOfDots; i++)
        {
            dots[i] = Instantiate(dotsPrefab, dotsParentTransform.position, Quaternion.identity, dotsParentTransform); // TODO: - Do we need this last parameter?
            dots[i].SetActive(false);
        }
    }

    public Vector2 AimDirection()
    {
        Vector2 mouseCurrentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return mouseInitialPosition - mouseCurrentPosition; // a vector connecting the mouse to the player
    }

    private Vector2 CalculateDotsPosition(float t)
    {
        Vector2 aimDirection = AimDirection();
        float facingDirectionMultiplier = qb.isFacingLeft ? -1 : 1;

        Vector2 position = (Vector2)dotsParentTransform.position + new Vector2(
            ((aimDirection.x + (3 * facingDirectionMultiplier)) * playerStrenght),
            ((aimDirection.y + 2) * playerStrenght)
        ) * t + .5f * (Physics2D.gravity * ballGravity) * (t * t);

        return position;
    }

    public void SetDotsActive(bool _isActive)
    {
        for (int i = 0; i < dots.Length; i++)
        {
            dots[i].SetActive(_isActive);
        }
    }
}
