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

    [Header("Aiming parameters")]
    [SerializeField] private int playerStrenght;
    [SerializeField] private float ballGravity; // based on altitude of stadium?

    private GameObject[] dots;

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
        Vector2 playerPosition = dotsParentTransform.position;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        return mousePosition - playerPosition; // a vector connecting the mouse to the player
    }

    private Vector2 CalculateDotsPosition(float t)
    {
        Vector2 aimDirection = AimDirection();
        Vector2 normalizedAimDirection = aimDirection.normalized;
        float facingDirectionMultiplier = qb.isFacingLeft ? -1 : 1;
        float verticalMultiplier = normalizedAimDirection.y > 0 ? 1 : -1;

        Vector2 position = (Vector2)dotsParentTransform.position + new Vector2(
            ((normalizedAimDirection.x * aimDirection.x * facingDirectionMultiplier)),
            ((normalizedAimDirection.y * aimDirection.y * verticalMultiplier))
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
