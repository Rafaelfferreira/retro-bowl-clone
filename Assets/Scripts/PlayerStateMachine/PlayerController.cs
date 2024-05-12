using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // MARK: - Stored game assets
    public Rigidbody2D rb;
    public Animator anim;
    public bool isFacingLeft = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    // MARK: - Life Cycle Methods
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // MARK: - Generic functions
    public void Flip() {
        rb.transform.Rotate(0, 180, 0);
        isFacingLeft = !isFacingLeft;
    }
}
